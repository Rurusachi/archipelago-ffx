﻿using Fahrenheit.Core;
using Fahrenheit.Core.FFX;
using Fahrenheit.Core.Atel;
using Fahrenheit.Core.FFX.Ids;
using Fahrenheit.Modules.ArchipelagoFFX.Client;
using Fahrenheit.Modules.ArchipelagoFFX.GUI;
//using Fahrenheit.Core.ImGuiNET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;


//using Fahrenheit.Modules.ArchipelagoFFX.GUI;
using static Fahrenheit.Modules.ArchipelagoFFX.ArchipelagoData;
using static Fahrenheit.Modules.ArchipelagoFFX.Client.FFXArchipelagoClient;
using static Fahrenheit.Modules.ArchipelagoFFX.delegates;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;
using Fahrenheit.Core.FFX.Battle;
using static Fahrenheit.Core.FFX.Globals;
using System.Numerics;
using Archipelago.MultiClient.Net.Enums;
using Color = Archipelago.MultiClient.Net.Models.Color;

namespace Fahrenheit.Modules.ArchipelagoFFX;

[FhLoad(FhGameId.FFX)]
public unsafe partial class ArchipelagoFFXModule : FhModule {

    public static FhModContext mod_context;

    public  static bool   skip_state_updates = false;
    private static ushort last_story_progress = 0;
    private static ushort last_room_id = 0;
    private static ushort last_entrance_id = 0;

    public static RegionEnum current_region = RegionEnum.None;
    public static Dictionary<RegionEnum, bool> region_is_unlocked = [];
    public static Dictionary<RegionEnum, ArchipelagoRegion> region_states = [];

    public const int NUM_CHARACTERS = 0x12;
    public static Dictionary<int, bool> unlocked_characters = [];
    public static Dictionary<int, bool> locked_characters = []; // Overrides unlocked characters
    public static bool party_overridden = false;
    public static bool is_character_unlocked(int character) => unlocked_characters[character] && !locked_characters[character];

    public static int ap_multiplier = 1;

    public static nint voice_fev = 0;
    public static nint voice_bank = 0;

    public static nint prev_fev = 0;
    public static nint prev_length = 0;
    public static nint prev_bank = 0;

    public static FhLogger logger;

    public ArchipelagoFFXModule() {
        init_hooks();
    }

    private class ArchipelagoState {
        public string                                    SeedId                 { get; set; }
        public Dictionary<RegionEnum, ArchipelagoRegion> region_states           { get; set; }
        public Dictionary<RegionEnum, bool>              region_is_unlocked      { get; set; }
        public Dictionary<int,        bool>              unlocked_characters     { get; set; }
        public List<long>                                local_checked_locations { get; set; }
        public int                                       received_items          { get; set; }

        public bool skip_state_updates { get; set; }

        public ArchipelagoState() {
            this.SeedId                  = ArchipelagoFFXModule.seed.SeedId;
            this.region_states           = ArchipelagoFFXModule.region_states;
            this.region_is_unlocked      = ArchipelagoFFXModule.region_is_unlocked;
            this.unlocked_characters     = ArchipelagoFFXModule.unlocked_characters;
            this.skip_state_updates      = ArchipelagoFFXModule.skip_state_updates;
            this.local_checked_locations = FFXArchipelagoClient.local_checked_locations;
            this.received_items          = FFXArchipelagoClient.received_items;
        }
    }

    private const int TreasureOffset = 0x1000;
    private const int BossOffset = 0x2000;
    private const int PartyMemberOffset = 0x3000;
    private const int OverdriveOffset = 0x4000;
    private const int OverdriveModeOffset = 0x5000;
    private const int OtherOffset = 0x6000;
    private const int SphereGridOffset = 0x7000;
    public record Location(string location_name, int location_id, uint item_id, string item_name, string player_name);
    public struct ArchipelagoSeed {
        [JsonInclude]
        public string          SeedId;
        [JsonInclude]
        public GoalRequirement GoalRequirement;
        [JsonInclude]
        public int             APMultiplier;
        [JsonInclude]          
        public List<uint>      StartingItems;
        [JsonInclude]          
        public List<Location>  Treasure;
        [JsonInclude]          
        public List<Location>  Boss;
        [JsonInclude]          
        public List<Location>  PartyMember;
        [JsonInclude]          
        public List<Location>  Overdrive;
        [JsonInclude]          
        public List<Location>  OverdriveMode;
        [JsonInclude]          
        public List<Location>  Other;
        [JsonInclude]          
        public List<Location>  SphereGrid;
        //public Dictionary<int, Location> Treasure;
        //public Dictionary<int, Location> Boss;
        //public Dictionary<int, Location> PartyMember;
        //public Dictionary<int, Location> Overdrive;
        //public Dictionary<int, Location> OverdriveMode;
        //public Dictionary<int, Location> Other;
        //public Dictionary<int, Location> SphereGrid;

        public ArchipelagoSeed() {
            SeedId = "";
            GoalRequirement = GoalRequirement.None;
            APMultiplier = 1;
            StartingItems = [];
            Treasure = [];
            Boss = [];
            PartyMember = [];
            Overdrive = [];
            OverdriveMode = [];
            Other = [];
            SphereGrid = [];
        }
    }
    public record ArchipelagoItem(uint id, string name, string player) {
        //public GCHandle name_handle = GCHandle.Alloc(FhCharset.Us.to_bytes(name), GCHandleType.Pinned);
    }

    public static List<CustomString> cached_strings = [];
    public record ArchipelagoLocations(ArchipelagoSeed seed) {
        public Dictionary<int, ArchipelagoItem> treasure  =      seed.Treasure.ToDictionary(     x => x.location_id, x => new ArchipelagoItem(x.item_id, x.item_name, x.player_name));
        public Dictionary<int, ArchipelagoItem> boss =           seed.Boss.ToDictionary(         x => x.location_id, x => new ArchipelagoItem(x.item_id, x.item_name, x.player_name));
        public Dictionary<int, ArchipelagoItem> party_member =   seed.PartyMember.ToDictionary(  x => x.location_id, x => new ArchipelagoItem(x.item_id, x.item_name, x.player_name));
        public Dictionary<int, ArchipelagoItem> overdrive =      seed.Overdrive.ToDictionary(    x => x.location_id, x => new ArchipelagoItem(x.item_id, x.item_name, x.player_name));
        public Dictionary<int, ArchipelagoItem> overdrive_mode = seed.OverdriveMode.ToDictionary(x => x.location_id, x => new ArchipelagoItem(x.item_id, x.item_name, x.player_name));
        public Dictionary<int, ArchipelagoItem> other =          seed.Other.ToDictionary(        x => x.location_id, x => new ArchipelagoItem(x.item_id, x.item_name, x.player_name));
        public Dictionary<int, ArchipelagoItem> sphere_grid =    seed.SphereGrid.ToDictionary(   x => x.location_id, x => new ArchipelagoItem(x.item_id, x.item_name, x.player_name));
    }

    public static ArchipelagoSeed seed;
    //public static string SeedId = "";
    public static ArchipelagoLocations item_locations = new(new());
    public static List<ArchipelagoSeed> loaded_seeds = [];

    public static void loadSeedList() {
        var seeds = mod_context.Paths.ResourcesDir.GetDirectories("seeds").FirstOrDefault()?.GetFiles("*.json");
        if (seeds is null || seeds.Length == 0) {
            logger.Warning("No seeds found");
            return;
        }

        foreach (var file in seeds) {
            try {
                using (FileStream stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                    var loaded_seed = JsonSerializer.Deserialize<ArchipelagoSeed>(stream);
                    loaded_seeds.Add(loaded_seed);
                }
            }   
            catch {
                logger.Warning($"Failed to load {file.Name}");
            }
        }
    }

    public static bool loadSeed() {
        if (FFXArchipelagoClient.SeedId is not null) {
            var seed = loaded_seeds.FirstOrDefault(seed => seed.SeedId == FFXArchipelagoClient.SeedId);

            if (seed.SeedId is not null) return loadSeed(seed);

            string message = "Seed for connected slot not found";
            ArchipelagoGUI.add_log_message([(message, Color.Red)]);
            logger.Error($"Seed for connected slot not found");
            return false;
        }
        return loadSeed(loaded_seeds[ArchipelagoGUI.selected_seed]);
    }

    public static bool loadSeed(ArchipelagoSeed loaded_seed) {
        if (FFXArchipelagoClient.is_connected) {
            if (FFXArchipelagoClient.SeedId != loaded_seed.SeedId) {
                string message = "Seed doesn't match connected slot";
                ArchipelagoGUI.add_log_message([(message, Color.Red)]);
                logger.Error($"Seed doesn't match connected slot");
                return false;
            }
        }
        initalize_states();
        seed = loaded_seed;
        ap_multiplier = loaded_seed.APMultiplier;
        item_locations = new ArchipelagoLocations(loaded_seed);
        return true;
        //var files = mod_context.Paths.ResourcesDir.GetFiles();
        //foreach (var file in files) {
        //    if (file.Name != "locations.json") continue;
        //    try {
        //        initalize_states();
        //        using (FileStream stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
        //            var loaded_seed = JsonSerializer.Deserialize<ArchipelagoSeed>(stream);
        //            seed = loaded_seed;
        //            ap_multiplier = loaded_seed.APMultiplier;
        //            item_locations = new ArchipelagoLocations(loaded_seed);
        //        }
        //    }   
        //    catch {
        //        seed = new ArchipelagoSeed();
        //        logger.Warning("Failed to load seed");
        //    }
        //    break;
        //}
    }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        ArchipelagoFFXModule.mod_context = mod_context;
        // Initialize Archipelago Client
        logger = _logger;
        initalize_states();
        //loadSeed();
        loadSeedList();
        return hook();
    }

    public static void initalize_states() {
        region_is_unlocked.Clear();
        foreach (var region in region_to_ids) {
            region_is_unlocked.Add(region.Key, region.Key == RegionEnum.DreamZanarkand);
        }
        region_states.Clear();
        foreach (var region in region_starting_state) {
            region_states.Add(region.Key, region.Value);
        }
        unlocked_characters.Clear();
        locked_characters.Clear();
        for (int i = 0; i < NUM_CHARACTERS; i++) {
            unlocked_characters.Add(i, false);
            locked_characters.Add(i, false);
        }
        // Until Archipelago handles this
        //unlocked_characters[PlySaveId.PC_TIDUS] = true;
    }


    [GeneratedRegex("^(?<major>0|[1-9]\\d*)\\.(?<minor>0|[1-9]\\d*)\\.(?<patch>0|[1-9]\\d*)(?:-(?<prerelease>(?:0|[1-9]\\d*|\\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\\.(?:0|[1-9]\\d*|\\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\\+(?<buildmetadata>[0-9a-zA-Z-]+(?:\\.[0-9a-zA-Z-]+)*))?$")]
    private static partial Regex RegexSemVer();

    private static readonly string VersionString = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

    private static readonly SemVer FullVersion = new(VersionString);
    private static readonly SemVer Version = FullVersion.WithoutMetadata();
    private record SemVer(int major,
                          int minor,
                          int patch,
                          string prerelease,
                          string buildmetadata) : IComparable<SemVer>, IEquatable<SemVer> {
        public SemVer(string versionString) : this(0, 0, 0, "", "") {
            var version_match = RegexSemVer().Match(versionString);
            major = int.Parse(version_match.Groups["major"].Value);
            minor = int.Parse(version_match.Groups["minor"].Value);
            patch = int.Parse(version_match.Groups["patch"].Value);
            prerelease = version_match.Groups["prerelease"].Value;
            buildmetadata = version_match.Groups["buildmetadata"].Value;
        }

        public SemVer WithoutMetadata() {
            return new SemVer(major, minor, patch, "", "");
        }

        public override string ToString() {
            string s = $"{major}.{minor}.{patch}";
            if (prerelease != "") s += "-"+prerelease;
            if (buildmetadata != "") s += "+"+buildmetadata;
            return s;
        }

        public static int Compare(SemVer? lhs, SemVer? rhs) {
            if (lhs is null) return (rhs is null ? 0 : -1);

            return lhs.CompareTo(rhs);
        }
        public int CompareTo(SemVer? other) {
            if (other is null) return 1;

            int result = major.CompareTo(other.major);
            if (result != 0) return result;
            result = minor.CompareTo(other.minor);
            if (result != 0) return result;
            result = patch.CompareTo(other.patch);
            if (result != 0) return result;

            if (prerelease == "") {
                if (other.prerelease == "") return 0;
                return 1;
            }
            if (other.prerelease == "") return -1;

            var lhs = prerelease.Split('.');
            var rhs = other.prerelease.Split(".");
            int i = 0;
            int ln;
            int rn;
            for (; i < lhs.Length && i < rhs.Length; i++) {
                string l = lhs[i];
                string r = rhs[i];
                if (int.TryParse(l, out ln)) {
                    if (int.TryParse(r, out rn)) {
                        result = ln.CompareTo(rn);
                        if (result != 0) return result;
                        continue;
                    }
                    return -1;
                }
                if (int.TryParse(r, out rn)) {
                    return 1;
                }
                result = l.CompareTo(r);
                if (result != 0) return result;
            }
            if (i < lhs.Length) return 1; // left longer
            if (i < rhs.Length) return -1; // right longer
            return 0;
        }

        bool IEquatable<SemVer>.Equals(SemVer? other) => Compare(this, other) == 0;

        //public static bool operator ==(SemVer lhs, SemVer rhs) => Compare(lhs, rhs) == 0;
        //public static bool operator !=(SemVer lhs, SemVer rhs) => Compare(lhs, rhs) != 0;

        public static bool operator  <(SemVer? lhs, SemVer? rhs) => Compare(lhs, rhs)  < 0;
        public static bool operator  >(SemVer? lhs, SemVer? rhs) => Compare(lhs, rhs)  > 0;
        public static bool operator <=(SemVer? lhs, SemVer? rhs) => Compare(lhs, rhs) <= 0;
        public static bool operator >=(SemVer? lhs, SemVer? rhs) => Compare(lhs, rhs) >= 0;
    }

    public override void save_local_state(FileStream local_state_file) {
        ArchipelagoState state = new();
        JsonSerializer.Serialize(local_state_file, state);
        local_state_file.SetLength(local_state_file.Position);
    }
    public override void load_local_state(FileStream local_state_file, FhLocalStateInfo local_state_info) {
        SemVer save_version = new(local_state_info.Version);
        if (save_version != Version) {
            logger.Warning($"Saved with different AP version: current:{Version} save:{save_version}");
        }

        var loaded_state = JsonSerializer.Deserialize<ArchipelagoState>(local_state_file);
        if (loaded_state != null) {
            var seed = loaded_seeds.FirstOrDefault(s => s.SeedId == loaded_state.SeedId);
            if (seed.SeedId is not null) {
                if (!loadSeed(seed)) {
                    FFXArchipelagoClient.disconnect();
                    return;
                }
            }
            else { 
                logger.Error($"Seed for loaded state not found");
                return;
            }
            //foreach (var seed in loaded_seeds) {
            //    if (loaded_state.SeedId == seed.SeedId) {
            //        loadSeed(seed);
            //        break;
            //    }
            //}
            //if (loaded_state.SeedId != seed.SeedId) {
            //    logger.Error($"Loaded state has a different AP seed");
            //}
            foreach (var region in loaded_state.region_states) {
                    region_states[region.Key].story_progress = region.Value.story_progress;
                    region_states[region.Key].room_id = region.Value.room_id;
                    region_states[region.Key].entrance = region.Value.entrance;
                    region_states[region.Key].completed_visits = region.Value.completed_visits;
                }
            foreach (var region in loaded_state.region_is_unlocked) {
                region_is_unlocked[region.Key] = region.Value;
            }
            foreach (var character in loaded_state.unlocked_characters) {
                unlocked_characters[character.Key] = character.Value;
                locked_characters[character.Key] = false;
            }
            FFXArchipelagoClient.local_checked_locations.Clear();
            FFXArchipelagoClient.local_checked_locations.AddRange(loaded_state.local_checked_locations);
            FFXArchipelagoClient.local_locations_updated = true;
            FFXArchipelagoClient.received_items = loaded_state.received_items;
            skip_state_updates = loaded_state.skip_state_updates;
        }
    }

    public override void pre_update() {
        // Update Archipelago Client
        FFXArchipelagoClient.update();
        if (last_story_progress != Globals.save_data->story_progress) {
            ushort story_progress = Globals.save_data->story_progress;
            _logger.Info($"story_progress changed: {last_story_progress} -> {story_progress}");

            if (current_region != RegionEnum.None) {
                ArchipelagoRegion region = region_states[current_region];
                if (region.story_checks.TryGetValue(story_progress, out var storyCheck)) {
                    storyCheck.check_delegate?.Invoke(region);
                    if (region_is_unlocked.TryGetValue(storyCheck.return_if_locked, out var is_unlocked) && !is_unlocked) {
                        call_warp_to_map(382, 0);
                        //Globals.save_data->current_room_id = 382;
                        //Globals.save_data->current_spawnpoint = 0;
                    }
                    region.story_progress = storyCheck.next_story_progress ?? region.story_progress;
                    region.room_id = storyCheck.next_room_id ?? region.room_id;
                    region.entrance = storyCheck.next_entrance ?? region.entrance;
                    region.completed_visits += storyCheck.visit_complete ? 1 : 0;
                    skip_state_updates = storyCheck.next_story_progress.HasValue || storyCheck.next_room_id.HasValue || storyCheck.next_entrance.HasValue;
                }
            }
            last_story_progress = story_progress;
        }
        if (last_room_id != Globals.save_data->current_room_id) {
            _logger.Info($"Room changed: Entered {Globals.save_data->current_room_id} at entrance {Globals.save_data->current_spawnpoint}");
            
            on_map_change();
            last_room_id = Globals.save_data->current_room_id;
            last_entrance_id = Globals.save_data->current_spawnpoint;

            /*
            if (Globals.save_data->last_room_id == 368 && Globals.save_data->current_room_id == 376) {
                _logger.Debug("Skip Dream Zanarkand");
                call_warp_to_map(382, 0);
            }
             */
        }
    }

    public override void handle_input() {
        /*
        if (Globals.Input.select.held) {
            if (Globals.Input.l1.just_pressed) {
                transitionsEnabled = !transitionsEnabled;
                _logger.Debug($"transitionsEnabled = {transitionsEnabled}");
            }
        }
         */
        if (Globals.Input.l1.held && Globals.Input.r1.held && Globals.Input.start.just_pressed) {
            _logger.Debug("Soft Reset");
            //Globals.save_data->current_room_id = 23;
            if (Globals.Battle.btl->battle_state != 0) {
                Globals.Battle.btl->battle_end_type = 1;
            } else {
                call_warp_to_map(23, 0);
            }

        }
        if (Globals.Input.select.held && Globals.Input.l1.just_pressed) {
            AtelBasicWorker* worker0 = Atel.controllers[0].worker(0);

            float minDistance = -1;
            int closestEntranceIndex = -1;
            for (int i = 0; i < worker0->script_chunk->map_entrances.Length; i++) {
                MapEntrance entrance = worker0->script_chunk->map_entrances[i];
                Vector4 pos = new(entrance.x, entrance.y, entrance.z, 0);
                float distance = (Globals.actors->chr_pos_vec - pos).Length();
                logger.Debug($"Entrance: pos:({entrance.x}, {entrance.y}, {entrance.z}) distance:{distance}");
                if (closestEntranceIndex == -1 || distance < minDistance) {
                    minDistance = distance;
                    closestEntranceIndex = i;
                }
            }
            MapEntrance closestEntrance = worker0->script_chunk->map_entrances[closestEntranceIndex];
            logger.Debug($"Closest Entrance: pos:({closestEntrance.x}, {closestEntrance.y}, {closestEntrance.z}) distance:{minDistance}");



            //var region_to_id = ArchipelagoData.id_to_region.ToLookup(id => id.Value, id => id.Key);
            //if (current_region != RegionEnum.None && region_states.TryGetValue(current_region, out var current_state)) {
            //    _logger.Debug($"{current_region}: story_progress={current_state.story_progress}, room_id={current_state.room_id}, entrance={current_state.entrance}");
            //}
            //h_MsBattleLabelExe(0x00AC0000, 1, 1);


            //_SndSepPlay(81019, 63, 127);
            //_SndSepPlaySimple(80000009);
            //_Common_0043(worker0, &storage, &stack);
            //AtelBasicWorker* worker0 = Atel.controllers[0].worker(0);

            /*
            var SyncManager = FhUtil.ptr_at<byte>(0x008e9004);
            if (voice_bank != 0) Marshal.FreeHGlobal(voice_fev);
            var readBytes = allocate_file("../../../FFX_Data/GameData/PS3Data/Sound_PC/Voice/US/ffx_us_voice02.fev", out voice_fev);

            if (prev_fev == 0) {
                prev_fev = *(int*)(SyncManager + 0x2c);
                prev_length = *(int*)(SyncManager + 0x14);
                prev_bank = *(int*)(SyncManager + 0x3c);
            }

            *(nint*)(SyncManager + 0x2c) = voice_fev;
            *(uint*)(SyncManager + 0x14) = readBytes;


            if (voice_bank != 0) Marshal.FreeHGlobal(voice_bank);
            readBytes = allocate_file("../../../FFX_Data/GameData/PS3Data/Sound_PC/Voice/US/ffx_us_voice02_bank00.fsb", out voice_bank);

            *(nint*)(SyncManager + 0x3c) = voice_bank;
             */

            /*
            var FmodManager = FhUtil.get_at<nint>(0x008e9000);
            var SyncManager = FhUtil.get_at<nint>(0x008e9004);

            var ffxFmod = *(nint*)(FmodManager+0x8);

            var FmodVoice = *(nint*)(ffxFmod+0x10);

            var DAT_00ce8448 = FhUtil.ptr_at<nint>(0x008e8448);

            AtelStack stack = new AtelStack();
            stack.push_int(87262468);

            int work = 0;
            int[] storage_array = [0,0,0,0];
            
            fixed (int* storage = storage_array) {
                _Common_playFieldVoiceLineInit((AtelBasicWorker*)&work, storage, &stack);
                _Common_playFieldVoiceLineExec((AtelBasicWorker*)&work, &stack);
                _Common_playFieldVoiceLineResultInt((AtelBasicWorker*)&work, storage, &stack);

                _Common_00D6Init((AtelBasicWorker*)&work, storage, &stack);
                _Common_00D6eExec((AtelBasicWorker*)&work, &stack);
                _Common_00D6ResultInt((AtelBasicWorker*)&work, storage, &stack);

            }
             */


            //_ChN_ReadSystemMGRP(2, 2); // Load Swimming motion group

        }
        if (Globals.Input.select.held && Globals.Input.r1.just_pressed) {
            _logger.Info($"Resetting party");
            save_party();
            reset_party();
        }
        if (Globals.Input.select.held && Globals.Input.l2.just_pressed) {
            //foreach (var state in region_states) {
            //    _logger.Debug($"{state.Key}: story_progress={state.Value.Story_progress}, room_id={state.Value.room_id}, entrance={state.Value.entrance}");
            //}
            /*
            var SyncManager = FhUtil.get_at<int>(0x008e9004);
            var fev = *(int**)(SyncManager + 0x2c);
            var fev_length = *(int*)(SyncManager + 0x14);
            var bank = *(int**)(SyncManager + 0x3c);
            _logger.Debug($"fev_length: {fev_length}");
            _logger.Debug($"fev: {fev[0]} {fev[1]} {fev[2]} {fev[3]}");
            _logger.Debug($"bank: {bank[0]} {bank[1]} {bank[2]} {bank[3]}");
             */
            //get_party_frontline();

        }
        if (Globals.Input.select.held && Globals.Input.r2.just_pressed) {
            //_logger.Debug("Warp to Airship");
            //call_warp_to_map(382, 0);
        }
    }

    public static void call_warp_to_map(int map_id, int entrance_id) {
        AtelStack stack = new AtelStack();
        stack.push_int(map_id);
        stack.push_int(entrance_id);

        int work = 0;
        int storage = 0;

        h_Common_warpToMap((AtelBasicWorker*)&work, &storage, &stack);
    }
    public static void call_remove_party_member(int character_id, bool long_term = false) {
        AtelStack stack = new AtelStack();
        stack.push_int(character_id);

        int work = 0;
        int storage = 0;

        if (!long_term) h_Common_removePartyMember((AtelBasicWorker*)&work, &storage, &stack);
        else h_Common_removePartyMemberLongTerm((AtelBasicWorker*)&work, &storage, &stack);
    }

    public static void call_add_party_member(int character_id) {
        AtelStack stack = new AtelStack();
        stack.push_int(character_id);

        int work = 0;
        int storage = 0;

        h_Common_addPartyMember((AtelBasicWorker*)&work, &storage, &stack);
    }

    public static void call_put_party_member_in_slot(int slot, int character_id) {
        AtelStack stack = new AtelStack();
        stack.push_int(slot);
        stack.push_int(character_id);

        int work = 0;
        int storage = 0;

        h_Common_putPartyMemberInSlot((AtelBasicWorker*)&work, &storage, &stack);
    }

    public static uint[] get_party_frontline() {
        uint slot1 = 0;
        uint slot2 = 0;
        uint slot3 = 0;

        h_MsGetSavePartyMember(&slot1, &slot2, &slot3);

        return [slot1, slot2, slot3];
    }

    public static void save_party() {
        //Globals.save_data->atel_is_push_member = 1;

        for (int character = 0; character < NUM_CHARACTERS; character++) {
            if (is_character_unlocked(character)) {
                Globals.save_data->atel_push_party |= (byte)(1 << (byte)character);
            }
            else {
                Globals.save_data->atel_push_party &= (byte)(0xff ^ (1 << (byte)character));
            }
        }
        var party_formation = get_party_frontline();
        for (int i = 0; i < 3; i++) {
            Globals.save_data->atel_push_frontline[i] = (byte)party_formation[i];
        }
    }

    public static void reset_party() {
        //call_put_party_member_in_slot(0, (PlySaveId)0xff);
        //call_put_party_member_in_slot(1, (PlySaveId)0xff);
        //call_put_party_member_in_slot(2, (PlySaveId)0xff);
        //int slot = 0;
        byte[] unlocked = {0xff, 0xff, 0xff};
        int slot = 0;
        for (int character = 0; character < NUM_CHARACTERS; character++) {
            if (is_character_unlocked(character)) {
                call_add_party_member(character);
                if (slot < 3) unlocked[slot++] = (byte)character;
                /*
                if (slot < 3) {
                    call_put_party_member_in_slot(slot, pair.Key);
                    slot++;
                }
                 */
            } else {
                call_remove_party_member(character, !locked_characters[character]);
            }
        }
        slot = 0;
        List<byte> frontline = [];
        for (int i = 0; i < 3; i++) {
            byte character = Globals.save_data->atel_push_frontline[i];
            if (character == 0xff || !is_character_unlocked(character) || frontline.Contains(character)) {
                while (slot < 3 && (frontline.Contains(unlocked[slot]) || unlocked[slot] > 7)) slot++;
                if (slot < 3) character = unlocked[slot++];
                else character = 0xff;
            }
            call_put_party_member_in_slot(i, character);
            frontline.Add(character);
        }
        //Globals.save_data->atel_is_push_member = 0;
    }

    public static void set_party(List<int> characters, bool saveParty = true, bool onlyUnlocked = true) {
        logger.Debug($"Setting party to: {String.Join(", ", characters.Select(i => id_to_character[i]))}");
        party_overridden = true;
        if (saveParty) {
            save_party();
        }
        for (int chr = 0; chr <= PlySaveId.PC_MAGUS3; chr++) {
            call_remove_party_member(chr, true);
        }
        call_put_party_member_in_slot(0, 0xff);
        call_put_party_member_in_slot(1, 0xff);
        call_put_party_member_in_slot(2, 0xff);

        var slot = 0;
        foreach (int character in characters) {
            if (onlyUnlocked && !is_character_unlocked(character)) continue;
            call_add_party_member(character);
            if (slot < 3 && character <= PlySaveId.PC_SEYMOUR) {
                call_put_party_member_in_slot(slot++, character);
            }
        }
        foreach (var pair in locked_characters) {
            if (pair.Value) {
                call_add_party_member(pair.Key);
                call_remove_party_member(pair.Key, false);
            }
        }
    }

    public static void set_underwater_party(bool saveParty = true, bool onlyUnlocked = true) {
        set_party([PlySaveId.PC_TIDUS, PlySaveId.PC_WAKKA, PlySaveId.PC_RIKKU], saveParty, onlyUnlocked);
    }

    public static void set_summon_party(bool saveParty = true, bool onlyUnlocked = true) {
        set_party([ PlySaveId.PC_YUNA, PlySaveId.PC_VALEFOR, PlySaveId.PC_IFRIT, PlySaveId.PC_IXION,
                    PlySaveId.PC_SHIVA, PlySaveId.PC_BAHAMUT, PlySaveId.PC_ANIMA, PlySaveId.PC_YOJIMBO,
                    PlySaveId.PC_MAGUS1, PlySaveId.PC_MAGUS2, PlySaveId.PC_MAGUS3
                  ], saveParty, onlyUnlocked);
    }

    public static void set_character_model(int chr_id) {
        AtelStack stack = new AtelStack();
        stack.push_int(chr_id + 1);
        AtelBasicWorker* worker0 = Globals.Atel.controllers[0].worker(0);
        int storage = 0;
        _Common_loadModel(worker0, &storage, &stack);
        stack.push_int(0);
        _Common_linkFieldToBattleActor(worker0, &storage, &stack);
    }

    public static uint allocate_file(string filename, out nint file_ptr) {
        int[] fileStream = [0,0];
        uint readBytes = 0;
        file_ptr = 0;

        fixed (int* fs = fileStream) {
            nint pFilename = Marshal.StringToHGlobalAnsi(filename);
            h_openFile((nint)fs, pFilename, true, 0, 0, 1);
            Marshal.FreeHGlobal(pFilename);
            logger.Debug($"file_handle={fileStream[0]}, file_length={*(long*)(*(int*)(fileStream[1] + 0xc) + 8)}");
            if (fileStream[1] == 0) return 0;
            var file_length = *(long*)(*(int*)(fileStream[1] + 0xc) + 8);
            file_ptr = Marshal.AllocHGlobal((int)file_length);
            uint max_len = (uint)file_length;
            readBytes = _readFile((nint)fs, file_ptr, max_len);
        }
        logger.Debug($"read {readBytes}, beginning={((byte*)file_ptr)[0]} {((byte*)file_ptr)[1]} {((byte*)file_ptr)[2]} {((byte*)file_ptr)[3]}");
        return readBytes;
    }

    public override void render_imgui() {
        ArchipelagoGUI.render();
    }
}
