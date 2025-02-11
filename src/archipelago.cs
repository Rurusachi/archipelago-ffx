using Fahrenheit.Core;
using Fahrenheit.Core.FFX;
using Fahrenheit.Core.FFX.Atel;
using Fahrenheit.Core.FFX.Ids;
using Fahrenheit.Modules.Archipelago.Client;
using Fahrenheit.Modules.Archipelago.GUI;
using Fahrenheit.Core.ImGuiNET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;


//using Fahrenheit.Modules.Archipelago.GUI;
using System.Text.Json.Serialization;
using static Fahrenheit.Modules.Archipelago.ArchipelagoData;
using static Fahrenheit.Modules.Archipelago.Client.ArchipelagoClient;
using static Fahrenheit.Modules.Archipelago.delegates;

namespace Fahrenheit.Modules.Archipelago;
public sealed record ArchipelagoModuleConfig : FhModuleConfig {
    [JsonConstructor]
    public ArchipelagoModuleConfig(string name)
        : base(name) { }

    public override FhModule SpawnModule() {
        return new ArchipelagoModule(this);
    }
}

public unsafe partial class ArchipelagoModule : FhModule {
    private readonly ArchipelagoModuleConfig _moduleConfig;

    private ushort last_story_progress = 0;
    private ushort last_room_id = 0;

    public static RegionEnum current_region = RegionEnum.None;
    public static Dictionary<RegionEnum, bool> region_is_unlocked = [];
    public static Dictionary<RegionEnum, ArchipelagoRegion> region_states = [];

    public static Dictionary<PlySaveId, bool> character_is_unlocked = [];
    public static bool party_overridden = false;

    public static uint ap_multiplier = 1;

    public static nint voice_fev = 0;
    public static nint voice_bank = 0;

    public static nint prev_fev = 0;
    public static nint prev_length = 0;
    public static nint prev_bank = 0;

    public ArchipelagoModule(ArchipelagoModuleConfig moduleConfig) : base(moduleConfig) {
        _moduleConfig = moduleConfig;

        init_hooks();
    }

    public override bool init() {
        // Initialize Archipelago Client
        if (region_is_unlocked.Count == 0) { 
            foreach (var region in region_to_ids) {
                FhLog.Debug(region.Key.ToString());
                region_is_unlocked.Add(region.Key, region.Key == RegionEnum.DreamZanarkand);
            }
            foreach (var region in region_starting_state) {
                FhLog.Debug(region.Key.ToString());
                region_states.Add(region.Key, region.Value);
            }
            for (int i = 0; i < 0x12; i++) {
                character_is_unlocked.Add((PlySaveId)i, false);
            }
            // Until Archipelago handles this
            character_is_unlocked[PlySaveId.PC_TIDUS] = true;
        }
        return hook();
    }

    public override void pre_update() {
        // Update Archipelago Client
        ArchipelagoClient.update();
        if (last_story_progress != Globals.save_data->story_progress) {
            FhLog.Info($"story_progress changed: {last_story_progress} -> {Globals.save_data->story_progress}");

            update_region_state(false);
            last_story_progress = Globals.save_data->story_progress;
        }
        if (last_room_id != Globals.save_data->current_room_id) {
            FhLog.Info($"Room changed: Entered {Globals.save_data->current_room_id} at entrance {Globals.save_data->current_spawnpoint}");
            
            on_map_change();
            last_room_id = Globals.save_data->current_room_id;

            /*
            if (Globals.save_data->last_room_id == 368 && Globals.save_data->current_room_id == 376) {
                FhLog.Debug("Skip Dream Zanarkand");
                call_warp_to_map(382, 0);
            }
             */
        }
        /*
         */
        //h_eiAbmParaGet();
    }

    public override void handle_input() {
        /*
        if (Globals.Input.select.held) {
            if (Globals.Input.l1.just_pressed) {
                transitionsEnabled = !transitionsEnabled;
                FhLog.Debug($"transitionsEnableD = {transitionsEnabled}");
            }
        }
         */
        if (Globals.Input.l1.held && Globals.Input.r1.held && Globals.Input.start.just_pressed) {
            FhLog.Debug("Soft Reset");
            //Globals.save_data->current_room_id = 23;
            if (Globals.btl->battle_state != 0) {
                Globals.btl->battle_end_type = 1;
            } else {
                call_warp_to_map(23, 0);
            }

        }
        if (Globals.Input.select.held && Globals.Input.l1.just_pressed) {
            //var region_to_id = ArchipelagoData.id_to_region.ToLookup(id => id.Value, id => id.Key);
            if (current_region != RegionEnum.None && region_states.TryGetValue(current_region, out var current_state)) {
                FhLog.Debug($"{current_region}: story_progress={current_state.Story_progress}, room_id={current_state.room_id}, entrance={current_state.entrance}");
            }
            h_MsBattleLabelExe(0x00AC0000, 1, 1);


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
            FhLog.Info($"Resetting party");

            reset_party();
        }
        if (Globals.Input.select.held && Globals.Input.l2.just_pressed) {
            //foreach (var state in region_states) {
            //    FhLog.Debug($"{state.Key}: story_progress={state.Value.Story_progress}, room_id={state.Value.room_id}, entrance={state.Value.entrance}");
            //}
            /*
            var SyncManager = FhUtil.get_at<int>(0x008e9004);
            var fev = *(int**)(SyncManager + 0x2c);
            var fev_length = *(int*)(SyncManager + 0x14);
            var bank = *(int**)(SyncManager + 0x3c);
            FhLog.Debug($"fev_length: {fev_length}");
            FhLog.Debug($"fev: {fev[0]} {fev[1]} {fev[2]} {fev[3]}");
            FhLog.Debug($"bank: {bank[0]} {bank[1]} {bank[2]} {bank[3]}");
             */

        }
        if (Globals.Input.select.held && Globals.Input.r2.just_pressed) {
            FhLog.Debug("Warp to Airship");
            call_warp_to_map(382, 0);
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
    public static void call_remove_party_member(PlySaveId character_id, bool long_term = false) {
        AtelStack stack = new AtelStack();
        stack.push_int((int)character_id);

        int work = 0;
        int storage = 0;

        if (!long_term) h_Common_removePartyMember((AtelBasicWorker*)&work, &storage, &stack);
        else h_Common_removePartyMemberLongTerm((AtelBasicWorker*)&work, &storage, &stack);
    }

    public static void call_add_party_member(PlySaveId character_id) {
        AtelStack stack = new AtelStack();
        stack.push_int((int)character_id);

        int work = 0;
        int storage = 0;

        h_Common_addPartyMember((AtelBasicWorker*)&work, &storage, &stack);
    }

    public static void call_put_party_member_in_slot(int slot, PlySaveId character_id) {
        AtelStack stack = new AtelStack();
        stack.push_int(slot);
        stack.push_int((int)character_id);

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
        Globals.save_data->atel_is_push_member = 1;
        foreach (var pair in character_is_unlocked) {
            if (pair.Value) {
                Globals.save_data->atel_push_party |= (byte)(1 << (byte)pair.Key);
            }
            else {
                Globals.save_data->atel_push_party &= (byte)(0xff ^ (1 << (byte)pair.Key));
            }
        }
        var party_formation = get_party_frontline();
        for (int i = 0; i < 3; i++) {
            Globals.save_data->atel_push_frontline[i] = (byte)party_formation[i];
        }
    }

    public static void reset_party() {
        Globals.save_data->atel_is_push_member = 0;
        //call_put_party_member_in_slot(0, (PlySaveId)0xff);
        //call_put_party_member_in_slot(1, (PlySaveId)0xff);
        //call_put_party_member_in_slot(2, (PlySaveId)0xff);
        //int slot = 0;
        byte[] unlocked = {0xff, 0xff, 0xff};
        int slot = 0;
        foreach (var pair in character_is_unlocked) {
            if (pair.Value) {
                call_add_party_member(pair.Key);
                if (slot < 3) unlocked[slot++] = (byte)pair.Key;
                /*
                if (slot < 3) {
                    call_put_party_member_in_slot(slot, pair.Key);
                    slot++;
                }
                 */
            } else {
                call_remove_party_member(pair.Key, true);
            }
        }
        slot = 0;
        List<byte> frontline = [];
        for (int i = 0; i < 3; i++) {
            byte character = Globals.save_data->atel_push_frontline[i];
            if (character == 0xff || !character_is_unlocked[(PlySaveId)character] || frontline.Contains(character)) {
                while (slot < 3 && (frontline.Contains(unlocked[slot]) || unlocked[slot] > 7)) slot++;
                if (slot < 3) character = unlocked[slot++];
            }
            call_put_party_member_in_slot(i, (PlySaveId)character);
            frontline.Add(character);
        }
    }

    public static void set_party(List<PlySaveId> characters) {
        for (PlySaveId chr = 0; chr <= PlySaveId.PC_MAGUS3; chr++) {
            call_remove_party_member(chr, true);
        }
        call_put_party_member_in_slot(0, (PlySaveId)0xff);
        call_put_party_member_in_slot(1, (PlySaveId)0xff);
        call_put_party_member_in_slot(2, (PlySaveId)0xff);

        var slot = 0;
        foreach (PlySaveId character in characters) {
            call_add_party_member(character);
            if (slot < 3 && character <= PlySaveId.PC_SEYMOUR) {
                call_put_party_member_in_slot(slot++, character);
            }
        }
    }

    public static void set_character_model(PlySaveId chr_id) {
        AtelStack stack = new AtelStack();
        stack.push_int((int)chr_id + 1);
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
            h_openFile((nint)fs, Marshal.StringToHGlobalAnsi(filename), true, 0, 0, 1);
            FhLog.Debug($"file_handle={fileStream[0]}, file_length={*(long*)(*(int*)(fileStream[1] + 0xc) + 8)}");
            if (fileStream[1] == 0) return 0;
            var file_length = *(long*)(*(int*)(fileStream[1] + 0xc) + 8);
            file_ptr = Marshal.AllocHGlobal((int)file_length);
            uint max_len = (uint)file_length;
            readBytes = _readFile((nint)fs, file_ptr, max_len);
        }
        FhLog.Debug($"read {readBytes}, beginning={((byte*)file_ptr)[0]} {((byte*)file_ptr)[1]} {((byte*)file_ptr)[2]} {((byte*)file_ptr)[3]}");
        return readBytes;
    }

    public override void render_imgui() {
        ArchipelagoGUI.render();
    }
}