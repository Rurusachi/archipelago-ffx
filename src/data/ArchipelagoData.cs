using Fahrenheit.Core;
using Fahrenheit.Core.FFX;
using Fahrenheit.Core.FFX.Ids;
using Fahrenheit.Modules.ArchipelagoFFX.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TerraFX.Interop.DirectX;

namespace Fahrenheit.Modules.ArchipelagoFFX;

public static class ArchipelagoData {
    public static string[] id_to_character => [
        "TIDUS",
        "YUNA",
        "AURON",
        "KIMAHRI",
        "WAKKA",
        "LULU",
        "RIKKU",
        "SEYMOUR",
        "VALEFOR",
        "IFRIT",
        "IXION",
        "SHIVA",
        "BAHAMUT",
        "ANIMA",
        "YOJIMBO",
        "MAGUS1",
        "MAGUS2",
        "MAGUS3",
        "DUMMY",
        "DUMMY2",
    ];

    public enum RegionEnum {
        None = 0,
        DreamZanarkand,
        BaajTemple,
        Besaid,
        Kilika,
        Luca,
        MiihenHighroad,
        MushroomRockRoad,
        Djose,
        Moonflow,
        Guadosalam,
        ThunderPlains,
        Macalania,
        Bikanel,
        Bevelle,
        CalmLands,
        CavernOfTheStolenFayth,
        MtGagazet,
        ZanarkandRuins,
        Sin,
        Airship,
        OmegaRuins,
    }

    public enum GoalRequirement {
        None = 0,
        PartyMembers,
        Pilgrimage,
        PartyMembersAndAeons,
    }

    public static RegionEnum[] pilgrimageRegions = [
        RegionEnum.Besaid,
        RegionEnum.Kilika,
        RegionEnum.Djose,
        RegionEnum.Macalania,
        RegionEnum.Bevelle,
        RegionEnum.ZanarkandRuins,
    ];

    public static ushort[] pilgrimageStoryChecks = [
        182,    // Valefor
        348,    // Ifrit
        1010,   // Ixion
        1545,   // Shiva
        2220,   // Bahamut
        2850    // Yunalesca
    ];

    public static Dictionary<RegionEnum, List<int>> region_to_ids = new(){
        { RegionEnum.DreamZanarkand, [
            132,
            368,
            376,
            371,
            370,
            366,
            389,
            367,
            384,
            385,
            ] },
        { RegionEnum.BaajTemple, [
            48,
            49,
            50,
            63,
            52,
            74,
            295,
            196,
            298,
            51,
            71,
            288,
            64,
            380,
            ] },
        { RegionEnum.Besaid, [
            70,
            20,
            41,
            67,
            69,
            133,
            17,
            142,
            144,
            145,
            60,
            143,
            84,
            42,
            147,
            146,
            122,
            103,
            83,
            100,
            252,
            68,
            21,
            22,
            19,
            117,
            301,
            148,
            149,
            150,
            154,
            61,
            282,
            220,
            139,
            191, // 
            336, // Jecht Sphere
            337, // Jecht Sphere
            ] },
        { RegionEnum.Kilika, [
            43,
            53,
            152,
            46,
            16,
            151,
            293,
            18,
            65,
            78,
            155,
            156,
            96,
            44,
            108,
            45,
            98,
            47,
            118,
            167,
            237,
            168,
            94,
            169,
            191,
            297,
            185,
            294, // Maybe
            302, // Maybe
            ] },
        { RegionEnum.Luca, [
            267,
            377,
            268,
            355,
            72,
            123,
            73,
            85,
            86,
            87,
            88,
            89,
            77,
            157,
            170,
            158,
            184,
            104,
            107,
            159,
            57,
            121,
            299,
            113,
            124,
            62,
            212,
            347,
            250,
            125,
            55,
            335, // Jecht Sphere
            ] },
        { RegionEnum.MiihenHighroad, [
            95,
            120,
            127,
            58,
            171,
            112,
            115,
            116,
            59,
            334, // Jecht Sphere
            ] },
        { RegionEnum.MushroomRockRoad, [
            79,
            92,
            128,
            119,
            247,
            254,
            218,
            341,
            134,
            131,
            253, // Jecht Sphere?
            289, // Maybe
            ] },
        { RegionEnum.Djose, [
            93,
            76,
            82,
            210,
            81,
            161,
            160,
            214,
            90,
            91,
            245,
            ] },
        { RegionEnum.Moonflow, [
            75,
            105,
            187,
            188,
            235,
            99,
            291,
            236,
            190,
            189,
            109,
            97,
            234, // Maybe
            333, // Jecht Sphere
            ] },
        { RegionEnum.Guadosalam, [
            135,
            243,
            174,
            173,
            172,
            163,
            141,
            197,
            217,
            175,
            257,
            193,
            364,
            134,
            213, // Maybe?
            ] },
        { RegionEnum.ThunderPlains, [
            140,
            256,
            264,
            263,
            262,
            162,
            332, // Jecht Sphere
            ] },
        { RegionEnum.Macalania, [
            110,
            241,
            242,
            221,
            248,
            164,
            215,
            102,
            192,
            153,
            106,
            178,
            179,
            239,
            80,
            54,
            284,
            330,
            331,
            332,
            191,
            260, // Maybe
            365, // Maybe
            391, // Luca flashback
            338, // Jecht Sphere
            ] },
        { RegionEnum.Bikanel, [
            129,
            136,
            137,
            138,
            130,
            276,
            280,
            286,
            275,
            219,
            303,
            261,
            391, // Luca flashback
            ] },
        { RegionEnum.Bevelle, [
            205,
            180,
            181,
            182,
            306,
            226,
            198,
            209,
            208,
            183,
            274,
            206,
            177,
            176,
            329,
            227,
            305,
            238, // Maybe
            269, // Maybe
            273, // Maybe
            287, // Maybe
            339, // Jecht Sphere
            195,
            207,
            ] },
        { RegionEnum.CalmLands, [
            223,
            290,
            308,
            307,
            279,
            266,
            372,
            ] },
        { RegionEnum.CavernOfTheStolenFayth, [
            56,
            283,
            ] },
        { RegionEnum.MtGagazet, [
            259,
            244,
            285,
            309,
            134,
            165,
            249,
            272,
            310,
            311,
            312,
            361,
            381,
            383, // Braska Sphere
            362,
            ] },
        { RegionEnum.ZanarkandRuins, [
            132,
            363,
            225,
            314,
            222,
            316,
            320,
            317,
            318,
            224,
            270,
            319,
            315,
            313,
            ] },
        { RegionEnum.Sin, [
            322,
            203,
            296,
            204,
            327,
            324, // Point of no return : 3250
            325,
            326,
            386, // Ending
            387, // Ending
            388, // Ending
            390, // Ending
            ] },
        { RegionEnum.Airship, [
            194,
            265,
            351,
            211,
            277,
            255,
            392,
            //205,
            199,
            200,
            201,
            374, // Unsure
            375, // Unsure
            202, // Unsure
            ] },
        { RegionEnum.OmegaRuins, [
            258,
            271,
            ] },
    };

    public static readonly Lookup<int, RegionEnum> id_to_regions = (Lookup<int, RegionEnum>)region_to_ids.SelectMany(region => region.Value, (region, id) => new{region.Key, id}).ToLookup(pair => pair.id, pair => pair.Key);

    public delegate void ArchipelagoStoryCheckDelegate(ArchipelagoRegion region);

    public class ArchipelagoStoryCheck {
        public ushort? next_story_progress;
        public ushort? next_room_id;
        public ushort? next_entrance;
        public bool    visit_complete = false;
        public bool    pilgrimage = false;
        public RegionEnum return_if_locked;
        public ArchipelagoStoryCheckDelegate? check_delegate;
    }
    public unsafe class ArchipelagoRegion {
        public Dictionary<ushort, ArchipelagoStoryCheck> story_checks = [];

        public int completed_visits { get; set; }
        public ushort story_progress { get; set; }
        public ushort room_id { get; set; }
        public ushort entrance { get; set; }
        public bool pilgrimage_completed { get; set; }
        public uint airship_destination_index;
    }

    public static unsafe Dictionary<RegionEnum, ArchipelagoRegion> region_starting_state => new(){
        {RegionEnum.DreamZanarkand, new(){ story_progress = 0, room_id = 132, entrance = 0, airship_destination_index = 99,
            story_checks = {
                // Dream Zanarkand region does not exist in Apworld, so no place to put Tidus location. Revisit when starting party member rando is added.
                //{ 4,  new() {check_delegate = (r) => {
                //    // Tidus
                //    int partyMember_id = 0;
                //    if (!FFXArchipelagoClient.local_checked_locations.Contains(partyMember_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                //        if (ArchipelagoFFXModule.item_locations.party_member.TryGetValue(partyMember_id, out var item)) {
                //            if (FFXArchipelagoClient.sendLocation(partyMember_id, FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                //                ArchipelagoFFXModule.obtain_item(item.id);
                //            }
                //        }
                //    }
                //} } },
                { 5, new() {check_delegate = (r) => {
                    ArchipelagoFFXModule.logger.Info("Dream Zanarkand complete");
                    ArchipelagoFFXModule.save_party();
                    ArchipelagoFFXModule.reset_party();
                    ArchipelagoFFXModule.call_warp_to_map(382, 0);
                }} }
            } } },
        {RegionEnum.BaajTemple, new(){ story_progress = 30, room_id = 48, entrance = 0, airship_destination_index = 1, 
            story_checks = {
                { 60, new() {check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Baaj Temple visit 1 complete"); } } },
                { 110, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 49, next_entrance = 2, return_if_locked = RegionEnum.Besaid, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Al Bhed Ship complete"); } } },
            } } },
        {RegionEnum.Besaid, new(){ story_progress = 111, room_id = 70, entrance = 0, airship_destination_index = 2,
            story_checks = {
                { 119,  new() {check_delegate = (r) => {
                    // Wakka
                    int partyMember_id = 4;
                    if (!FFXArchipelagoClient.local_checked_locations.Contains(partyMember_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                        if (ArchipelagoFFXModule.item_locations.party_member.TryGetValue(partyMember_id, out var item)) {
                            if (FFXArchipelagoClient.sendLocation(partyMember_id, FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                                ArchipelagoFFXModule.obtain_item(item.id);
                            }
                        }
                    }
                } } },
                { 182,  new() {pilgrimage = true, check_delegate = (r) => {
                    // Valefor
                    int partyMember_id = 8;
                    if (!FFXArchipelagoClient.local_checked_locations.Contains(partyMember_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                        if (ArchipelagoFFXModule.item_locations.party_member.TryGetValue(partyMember_id, out var item)) {
                            if (FFXArchipelagoClient.sendLocation(partyMember_id, FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                                ArchipelagoFFXModule.obtain_item(item.id);
                            }
                        }
                    }
                } } },
                { 210, new() {check_delegate = (r) => {
                    // Send and obtain Map and Brotherhood locations if skipped by CSR
                    int treasure_id = 459;
                    if (!FFXArchipelagoClient.local_checked_locations.Contains(treasure_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.Treasure)) {
                        if (ArchipelagoFFXModule.item_locations.treasure.TryGetValue(treasure_id, out var item)) {
                            if (FFXArchipelagoClient.sendLocation(treasure_id, FFXArchipelagoClient.ArchipelagoLocationType.Treasure)) {
                                ArchipelagoFFXModule.obtain_item(item.id);
                            }
                        }
                    }
                    int other_id = 0;
                    if (!FFXArchipelagoClient.local_checked_locations.Contains(other_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.Other)) {
                        if (ArchipelagoFFXModule.item_locations.other.TryGetValue(other_id, out var item)) {
                            if (FFXArchipelagoClient.sendLocation(other_id, FFXArchipelagoClient.ArchipelagoLocationType.Other)) {
                                ArchipelagoFFXModule.obtain_item(item.id);
                            }
                        }
                    }
                } } },
                { 228, new() {check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Besaid visit 1 complete"); } } },
                { 248,  new() {check_delegate = (r) => {
                    int partyMember_id = 3; // Kimahri
                    if (!FFXArchipelagoClient.local_checked_locations.Contains(partyMember_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                        if (ArchipelagoFFXModule.item_locations.party_member.TryGetValue(partyMember_id, out var item)) {
                            if (FFXArchipelagoClient.sendLocation(partyMember_id, FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                                ArchipelagoFFXModule.obtain_item(item.id);
                            }
                        }
                    }
                } } },
                { 290, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 19, next_entrance = 1, return_if_locked = RegionEnum.Kilika, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("S.S Liki visit complete"); } } },
            } } },
        {RegionEnum.Kilika, new(){ story_progress = 290, room_id = 43, entrance = 0, airship_destination_index = 3,
            story_checks = {
                { 348,  new() {pilgrimage = true, check_delegate = (r) => {
                    // Ifrit
                    int partyMember_id = 9;
                    if (!FFXArchipelagoClient.local_checked_locations.Contains(partyMember_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                        if (ArchipelagoFFXModule.item_locations.party_member.TryGetValue(partyMember_id, out var item)) {
                            if (FFXArchipelagoClient.sendLocation(partyMember_id, FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                                ArchipelagoFFXModule.obtain_item(item.id);
                            }
                        }
                    }
                } } },
                { 372, new() {check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Kilika visit 1 complete"); } } },
                { 400, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 98, next_entrance = 3, return_if_locked = RegionEnum.Luca, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("S.S Winno visit complete"); } } },
                { 402, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 98, next_entrance = 3, return_if_locked = RegionEnum.Luca, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("S.S Winno visit complete"); } } },
            } } },
        {RegionEnum.Luca, new(){ story_progress = 402, room_id = 267, entrance = 0, airship_destination_index = 4,
            story_checks = {
                { 600,  new() {check_delegate = (r) => {
                    // Auron
                    int partyMember_id = 2;
                    if (!FFXArchipelagoClient.local_checked_locations.Contains(partyMember_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                        if (ArchipelagoFFXModule.item_locations.party_member.TryGetValue(partyMember_id, out var item)) {
                            if (FFXArchipelagoClient.sendLocation(partyMember_id, FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                                ArchipelagoFFXModule.obtain_item(item.id);
                            }
                        }
                    }
                } } },
                { 730, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 123, next_entrance = 6, return_if_locked = RegionEnum.MiihenHighroad, 
                    check_delegate = (r) => {
                        ArchipelagoFFXModule.logger.Info("Luca visit complete"); 
                        // CSR workaround
                        Globals.save_data->current_room_id = 123;
                        Globals.save_data->current_spawnpoint = 6;
                    } } },
            } } },
        {RegionEnum.MiihenHighroad, new(){ story_progress = 730, room_id = 95, entrance = 0, airship_destination_index = 5,
            story_checks = {
                { 787, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 171, next_entrance = 1, return_if_locked = RegionEnum.MushroomRockRoad, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Mi'ihen Highroad visit complete"); } } },
            } } },
        {RegionEnum.MushroomRockRoad, new(){ story_progress = 787, room_id = 79, entrance = 0, airship_destination_index = 6,
            story_checks = {
                { 960, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 131, next_entrance = 3, return_if_locked = RegionEnum.Djose, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Mushroom Rock Road visit complete"); } } },
            } } },
        {RegionEnum.Djose, new(){ story_progress = 960, room_id = 93, entrance = 0, airship_destination_index = 99,
            story_checks = {
                { 1010,  new() {pilgrimage = true, check_delegate = (r) => {
                    // Ixion
                    int partyMember_id = 10;
                    if (!FFXArchipelagoClient.local_checked_locations.Contains(partyMember_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                        if (ArchipelagoFFXModule.item_locations.party_member.TryGetValue(partyMember_id, out var item)) {
                            if (FFXArchipelagoClient.sendLocation(partyMember_id, FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                                ArchipelagoFFXModule.obtain_item(item.id);
                            }
                        }
                    }
                } } },
                { 1030, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 82, next_entrance = 0, return_if_locked = RegionEnum.Moonflow, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Djose visit 1 complete"); } } },
            } } },
        {RegionEnum.Moonflow, new(){ story_progress = 1030, room_id = 75, entrance = 0, airship_destination_index = 7,
            story_checks = {
                { 1085,  new() {visit_complete = true, next_story_progress = 3210, next_room_id = 235, next_entrance = 1, return_if_locked = RegionEnum.Guadosalam, 
                    check_delegate = (r) => {
                    // Rikku
                    int partyMember_id = 6;
                    if (!FFXArchipelagoClient.local_checked_locations.Contains(partyMember_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                        if (ArchipelagoFFXModule.item_locations.party_member.TryGetValue(partyMember_id, out var item)) {
                            if (FFXArchipelagoClient.sendLocation(partyMember_id, FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                                ArchipelagoFFXModule.obtain_item(item.id);
                            }
                        }
                    }
                    ArchipelagoFFXModule.logger.Info("Moonflow visit complete");
                } } },
            } } },
        {RegionEnum.Guadosalam, new(){ story_progress = 1085, room_id = 135, entrance = 0, airship_destination_index = 8,
            story_checks = {
                { 1170, new() {check_delegate = (r) => {
                    // Send and obtain Brotherhood upgrade location if skipped by CSR
                    int other_id = 37;
                    if (!FFXArchipelagoClient.local_checked_locations.Contains(other_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.Other)) {
                        if (ArchipelagoFFXModule.item_locations.other.TryGetValue(other_id, out var item)) {
                            if (FFXArchipelagoClient.sendLocation(other_id, FFXArchipelagoClient.ArchipelagoLocationType.Other)) {
                                ArchipelagoFFXModule.obtain_item(item.id);
                            }
                        }
                    }
                } } },
                { 1210, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 243, next_entrance = 1, return_if_locked = RegionEnum.ThunderPlains, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Guadosalam visit complete"); } } },
            } } },
        {RegionEnum.ThunderPlains, new(){ story_progress = 1210, room_id = 140, entrance = 0, airship_destination_index = 9,
            story_checks = {
                { 1375, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 263, next_entrance = 2, return_if_locked = RegionEnum.Macalania, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Thunder Plains visit complete"); } } },
            } } },
        {RegionEnum.Macalania, new(){ story_progress = 1400, room_id = 110, entrance = 0, airship_destination_index = 10,
            story_checks = {
                { 1430, new() {check_delegate = (r) => {
                    // Send and obtain Jecht Sphere location if skipped by CSR
                    int treasure_id = 177;
                    if (!FFXArchipelagoClient.local_checked_locations.Contains(treasure_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.Treasure)) {
                        if (ArchipelagoFFXModule.item_locations.treasure.TryGetValue(treasure_id, out var item)) {
                            if (FFXArchipelagoClient.sendLocation(treasure_id, FFXArchipelagoClient.ArchipelagoLocationType.Treasure)) {
                                ArchipelagoFFXModule.obtain_item(item.id);
                            }
                        }
                    }
                } } },
                { 1470, new() {check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Macalania Woods visit complete"); } } },
                { 1530, new() {check_delegate = (r) => {
                    int treasure_id = 85;
                    if (!FFXArchipelagoClient.local_checked_locations.Contains(treasure_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.Treasure)) {
                        if (ArchipelagoFFXModule.item_locations.treasure.TryGetValue(treasure_id, out var item)) {
                            if (FFXArchipelagoClient.sendLocation(treasure_id, FFXArchipelagoClient.ArchipelagoLocationType.Treasure)) {
                                ArchipelagoFFXModule.obtain_item(item.id);
                            }
                        }
                    } } } },
                { 1545,  new() {pilgrimage = true, check_delegate = (r) => {
                    // Shiva
                    int partyMember_id = 11;
                    if (!FFXArchipelagoClient.local_checked_locations.Contains(partyMember_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                        if (ArchipelagoFFXModule.item_locations.party_member.TryGetValue(partyMember_id, out var item)) {
                            if (FFXArchipelagoClient.sendLocation(partyMember_id, FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                                ArchipelagoFFXModule.obtain_item(item.id);
                            }
                        }
                    }
                } } },
                { 1704, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 215, next_entrance = 1, return_if_locked = RegionEnum.Bikanel, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Lake Macalania visit 1 complete"); } } },
            } } },
        {RegionEnum.Bikanel, new(){ story_progress = 1704, room_id = 129, entrance = 0, airship_destination_index = 11,
            story_checks = {
                { 1940, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 129, next_entrance = 2, return_if_locked = RegionEnum.Airship, check_delegate = (r) => {
                    ArchipelagoFFXModule.logger.Info("Bikanel visit complete");
                    int[] treasure_ids = [362, 363, 364];
                    foreach (int treasure_id in treasure_ids) {
                        if (!FFXArchipelagoClient.local_checked_locations.Contains(treasure_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.Treasure)) {
                            if (ArchipelagoFFXModule.item_locations.treasure.TryGetValue(treasure_id, out var item)) {
                                if (FFXArchipelagoClient.sendLocation(treasure_id, FFXArchipelagoClient.ArchipelagoLocationType.Treasure)) {
                                    ArchipelagoFFXModule.obtain_item(item.id);
                                }
                            }
                        }
                    }
                } } },
                { 1950, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 129, next_entrance = 2, return_if_locked = RegionEnum.Airship, check_delegate = (r) => {
                    ArchipelagoFFXModule.logger.Info("Bikanel visit complete");
                    int[] treasure_ids = [362, 363, 364];
                    foreach (int treasure_id in treasure_ids) {
                        if (!FFXArchipelagoClient.local_checked_locations.Contains(treasure_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.Treasure)) {
                            if (ArchipelagoFFXModule.item_locations.treasure.TryGetValue(treasure_id, out var item)) {
                                if (FFXArchipelagoClient.sendLocation(treasure_id, FFXArchipelagoClient.ArchipelagoLocationType.Treasure)) {
                                    ArchipelagoFFXModule.obtain_item(item.id);
                                }
                            }
                        }
                    }
                } } },
            } } },
        {RegionEnum.Airship, new(){ story_progress = 1950, room_id = 194, entrance = 1, airship_destination_index = 99,
            story_checks = {
                { 2075, new() {visit_complete = true, next_story_progress = 2970, next_room_id = 255, next_entrance = 0, return_if_locked = RegionEnum.Bevelle, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Airship visit 1 complete"); } } },
                //{ 3135, new() {next_story_progress = 3210, next_room_id = 255, next_entrance = 0, return_if_locked = RegionEnum.Sin, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Airship visit 2 complete"); } } },
            } } },
        {RegionEnum.Bevelle, new(){ story_progress = 2040, room_id = 205, entrance = 0, airship_destination_index = 12, // Destination 12 doesn't work (12 = Bevelle but doesn't have destination, 18 = Highbridge) 
            story_checks = {
                { 2220,  new() {pilgrimage = true, check_delegate = (r) => {
                    // Bahamut
                    int partyMember_id = 12;
                    if (!FFXArchipelagoClient.local_checked_locations.Contains(partyMember_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                        if (ArchipelagoFFXModule.item_locations.party_member.TryGetValue(partyMember_id, out var item)) {
                            if (FFXArchipelagoClient.sendLocation(partyMember_id, FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                                ArchipelagoFFXModule.obtain_item(item.id);
                            }
                        }
                    }
                } } },
                { 2385, new() {visit_complete = true, next_story_progress = 2920, next_room_id = 208, next_entrance = 1, return_if_locked = RegionEnum.CalmLands, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Bevelle visit 1 complete"); } } },
                { 2945, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 208, next_entrance = 1, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Bevelle visit 2 complete"); } } },
            } } }, 
        {RegionEnum.CalmLands, new(){ story_progress = 2385, room_id = 223, entrance = 0, airship_destination_index = 13,
            story_checks = {
                { 2440, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 223, next_entrance = 4, return_if_locked = RegionEnum.MtGagazet, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Calm Lands complete"); } } }, // Normally ends at 2440, but CSR skips from 2420 to 2510
                { 2510, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 223, next_entrance = 4, return_if_locked = RegionEnum.MtGagazet, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Calm Lands complete"); } } }, // Normally ends at 2440, but CSR skips from 2420 to 2510
            } } },
        {RegionEnum.CavernOfTheStolenFayth, new(){ story_progress = 2385, room_id = 56, entrance = 0, airship_destination_index = 99 } },
        {RegionEnum.MtGagazet, new(){ story_progress = 2440, room_id = 259, entrance = 0, airship_destination_index = 14,
            story_checks = {
                { 2680, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 259, next_entrance = 2, return_if_locked = RegionEnum.ZanarkandRuins, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Mt. Gagazet complete"); } } },
            } } },
        {RegionEnum.ZanarkandRuins, new(){ story_progress = 2680, room_id = 132, entrance = 0, airship_destination_index = 15,
            story_checks = {
                { 2850, new() {pilgrimage = true } },
                { 2875, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 313, next_entrance = 3, return_if_locked = RegionEnum.Airship, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Zanarkand Ruins complete"); } } },
                { 2900, new() {visit_complete = true, next_story_progress = 3210, next_room_id = 313, next_entrance = 3, return_if_locked = RegionEnum.Airship, check_delegate = (r) => {ArchipelagoFFXModule.logger.Info("Zanarkand Ruins complete"); } } },
            } } },
        {RegionEnum.Sin, new(){ story_progress = 3125, room_id = 322, entrance = 2, airship_destination_index = 16,
            story_checks = {
                { 3400, new() {visit_complete = true, next_room_id = 322, next_entrance = 2, check_delegate = (r) => {
                    ArchipelagoFFXModule.logger.Info("Game Complete");
                    foreach (var character in ArchipelagoFFXModule.locked_characters) {
                        ArchipelagoFFXModule.locked_characters[character.Key] = false;
                    }
                } } },
                { 11000, new() {next_story_progress = 3210, next_room_id = 327, next_entrance = 0, check_delegate = (r) => {
                    ArchipelagoFFXModule.call_warp_to_map(382, 0);
                } } },
            } } },
        {RegionEnum.OmegaRuins, new(){ story_progress = 3210, room_id = 258, entrance = 2, airship_destination_index = 17 } }, // Story_progress?
    };

    // For battles that don't push/pop but should
    public static Dictionary<string, List<int>> encounterToPartyDict => new(){
        {"bjyt02_00", [PlySaveId.PC_TIDUS,
            PlySaveId.PC_WAKKA,
            PlySaveId.PC_RIKKU,
        ]},
        {"bjyt02_01", [PlySaveId.PC_TIDUS,
            PlySaveId.PC_WAKKA,
            PlySaveId.PC_RIKKU,
        ]},
        {"lchb07_00", [ // Auron solo
        ]},
        {"lchb08_00", [ // Luca post-blitzball sahagins
        ]},
    };

    public static Dictionary<string, Action> encounterVictoryActions => new() {
        // Evrae airship battle
        //{"hiku15_00", () => {
        //    ArchipelagoFFXModule.logger.Info("Defeated Evrae. Airship visit 1 complete");
        //    ArchipelagoFFXModule.region_states[RegionEnum.Airship].story_progress = 2970;
        //    ArchipelagoFFXModule.region_states[RegionEnum.Airship].room_id = 255;
        //    ArchipelagoFFXModule.region_states[RegionEnum.Airship].entrance = 0;
        //    ArchipelagoFFXModule.skip_state_updates = true;
        //    }
        //},
        // Overdrive Sin
        {
            "ssbt03_00", () => {
            ArchipelagoFFXModule.logger.Info("Defeated Overdrive Sin. Airship visit 2 complete");
            ArchipelagoFFXModule.region_states[RegionEnum.Airship].story_progress = 3210;
            ArchipelagoFFXModule.region_states[RegionEnum.Airship].room_id = 374;
            ArchipelagoFFXModule.region_states[RegionEnum.Airship].entrance = 1;
            ArchipelagoFFXModule.skip_state_updates = true;
            }
        },
        // Yojimbo (Maybe on recruiting Yojimbo instead?)
        //{"nagi05_10", () => {
        //    ArchipelagoFFXModule.logger.Info("Defeated Yojimbo. Cavern of the Stolen Fayth visit 1 complete");
        //    ArchipelagoFFXModule.region_states[RegionEnum.CavernOfTheStolenFayth].story_progress = 3210;
        //    ArchipelagoFFXModule.region_states[RegionEnum.CavernOfTheStolenFayth].room_id = 56;
        //    ArchipelagoFFXModule.region_states[RegionEnum.CavernOfTheStolenFayth].entrance = 0;
        //    ArchipelagoFFXModule.skip_state_updates = true;
        //    }
        //},
        // Yu Yevon
        //{
        //    "sins07_10", () => {
        //
        //    }
        //}
    };

    public static Dictionary<string, Action> encounterToActionDict => new(){
        //{"bjyt04_00", ArchipelagoFFXModule.reset_party }, // Klikk (solo Tidus)
        //{"bjyt04_01", ArchipelagoFFXModule.reset_party }, // Klikk (Tidus + Rikku)
        
        // Auron solo
        {"lchb07_00", () => ArchipelagoFFXModule.set_party([PlySaveId.PC_AURON], true, false) },
        // Yenke and Biran. Can only target Kimahri and scale on his stats.
        {"mtgz01_10", () => ArchipelagoFFXModule.set_party([PlySaveId.PC_KIMAHRI], true, false) },

        // Bikanel forced Zu fight. Maybe not needed?
        //{"bika00_10", () => ArchipelagoFFXModule.set_party([PlySaveId.PC_TIDUS, PlySaveId.PC_LULU, PlySaveId.PC_AURON], true, false) },

        // Tutorials
        // Will softlock without Lulu
        {"bsil07_51", () => {
            ArchipelagoFFXModule.set_party([PlySaveId.PC_TIDUS, PlySaveId.PC_WAKKA, PlySaveId.PC_LULU], true, false);
            int partyMember_id = 5; // Lulu
            if (!FFXArchipelagoClient.local_checked_locations.Contains(partyMember_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                if (ArchipelagoFFXModule.item_locations.party_member.TryGetValue(partyMember_id, out var item)) {
                    if (FFXArchipelagoClient.sendLocation(partyMember_id, FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                        ArchipelagoFFXModule.obtain_item(item.id);
                    }
                }
            }
        } },
        // Yuna is unable to act without an aeon, causing a softlock.
        {"bsil05_50", () => {
            ArchipelagoFFXModule.set_party([PlySaveId.PC_LULU, PlySaveId.PC_TIDUS, PlySaveId.PC_WAKKA, PlySaveId.PC_YUNA, PlySaveId.PC_VALEFOR], true, false);
            int partyMember_id = 1; // Yuna
            if (!FFXArchipelagoClient.local_checked_locations.Contains(partyMember_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                if (ArchipelagoFFXModule.item_locations.party_member.TryGetValue(partyMember_id, out var item)) {
                    if (FFXArchipelagoClient.sendLocation(partyMember_id, FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                        ArchipelagoFFXModule.obtain_item(item.id);
                    }
                }
            }
        } },

        // Gui 2
        {"kino03_10", () => {
            int partyMember_id = 7; // Seymour
            if (!FFXArchipelagoClient.local_checked_locations.Contains(partyMember_id | (long)FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                if (ArchipelagoFFXModule.item_locations.party_member.TryGetValue(partyMember_id, out var item)) {
                    if (FFXArchipelagoClient.sendLocation(partyMember_id, FFXArchipelagoClient.ArchipelagoLocationType.PartyMember)) {
                        ArchipelagoFFXModule.obtain_item(item.id);
                    }
                }
            }
        } },

        // Tidus only gets 1 turn?
        {"klyt00_50", () => ArchipelagoFFXModule.set_party([PlySaveId.PC_TIDUS, PlySaveId.PC_KIMAHRI, PlySaveId.PC_LULU], true, false) },

        // Piercing tutorial. Tidus only gets 1 turn. Enemy only attacks 3rd character?
        {"mihn00_50", () => ArchipelagoFFXModule.set_party([PlySaveId.PC_TIDUS, PlySaveId.PC_AURON, PlySaveId.PC_WAKKA], true, false) },

        // Rikku tutorial
        {"genk16_50", () => ArchipelagoFFXModule.set_party([PlySaveId.PC_RIKKU], true, false) },

        // Summon fights
        // Belgemine
        {"genk00_40",  () => ArchipelagoFFXModule.set_summon_party()},
        {"kino04_40",  () => ArchipelagoFFXModule.set_summon_party()},
        {"lmyt01_00",  () => ArchipelagoFFXModule.set_summon_party()},
        {"lmyt01_01",  () => ArchipelagoFFXModule.set_summon_party()},
        {"lmyt01_02",  () => ArchipelagoFFXModule.set_summon_party()},
        {"lmyt01_03",  () => ArchipelagoFFXModule.set_summon_party()},
        {"lmyt01_04",  () => ArchipelagoFFXModule.set_summon_party()},
        {"lmyt01_05",  () => ArchipelagoFFXModule.set_summon_party()},
        {"lmyt01_06",  () => ArchipelagoFFXModule.set_summon_party()},
        {"lmyt01_07",  () => ArchipelagoFFXModule.set_summon_party()},
        {"mihn00_60",  () => ArchipelagoFFXModule.set_summon_party()},
        {"nagi00_40",  () => ArchipelagoFFXModule.set_summon_party()},
        {"zzzz00_250", () => ArchipelagoFFXModule.set_summon_party()},
        {"zzzz02_85",  () => ArchipelagoFFXModule.set_summon_party()},
        // Isaaru
        {"bvyt09_10",  () => ArchipelagoFFXModule.set_summon_party()},
        {"bvyt09_11",  () => ArchipelagoFFXModule.set_summon_party()},
        {"bvyt09_12",  () => ArchipelagoFFXModule.set_summon_party()},
        {"zzzz00_248", () => ArchipelagoFFXModule.set_summon_party()},


        // Underwater fights
        // Luca post-blitzball underwater fight
        {"lchb08_00", () => {
            ArchipelagoFFXModule.set_party([PlySaveId.PC_TIDUS, PlySaveId.PC_WAKKA]); // Only 2. Third character stays in original spot when battle moves forward
        } },
        // Extractor. May not work correctly without Yuna?
        {"genk09_00", () => {
            ArchipelagoFFXModule.set_party([PlySaveId.PC_TIDUS, PlySaveId.PC_WAKKA, PlySaveId.PC_YUNA, PlySaveId.PC_RIKKU]);
        } },
        // Baaj. Should work with 3
        {"bjyt02_00", () => ArchipelagoFFXModule.set_underwater_party()},
        {"bjyt02_01", () => ArchipelagoFFXModule.set_underwater_party()},
        {"bjyt02_02", () => ArchipelagoFFXModule.set_underwater_party()},
        // Al Bhed Ship. May only work with 2
        {"cdsp00_00", () => ArchipelagoFFXModule.set_underwater_party()},
        {"cdsp00_01", () => ArchipelagoFFXModule.set_underwater_party()},
        {"cdsp00_02", () => ArchipelagoFFXModule.set_underwater_party()},
        {"cdsp01_00", () => ArchipelagoFFXModule.set_underwater_party()},
        {"cdsp01_01", () => ArchipelagoFFXModule.set_underwater_party()},
        {"cdsp01_02", () => ArchipelagoFFXModule.set_underwater_party()}, // Not confirmed to exist but cdsp01_XX are probably copies of cdsp00_XX
        {"cdsp07_00", () => ArchipelagoFFXModule.set_underwater_party()},
        {"cdsp07_01", () => ArchipelagoFFXModule.set_underwater_party()},
        // Gagazet. Probably the underwater fights
        {"mtgz07_00", () => ArchipelagoFFXModule.set_underwater_party()},
        {"mtgz07_01", () => ArchipelagoFFXModule.set_underwater_party()},
        {"mtgz07_02", () => ArchipelagoFFXModule.set_underwater_party()},
        {"mtgz07_03", () => ArchipelagoFFXModule.set_underwater_party()},
        // Via Purifico
        {"stbv00_00", () => ArchipelagoFFXModule.set_underwater_party()},
        {"stbv00_01", () => ArchipelagoFFXModule.set_underwater_party()},
        {"stbv00_02", () => ArchipelagoFFXModule.set_underwater_party()},
        {"stbv00_03", () => ArchipelagoFFXModule.set_underwater_party()},
        {"stbv00_04", () => ArchipelagoFFXModule.set_underwater_party()},
        {"stbv00_10", () => ArchipelagoFFXModule.set_underwater_party()},
        {"stbv00_11", () => ArchipelagoFFXModule.set_underwater_party()},
        {"stbv00_12", () => ArchipelagoFFXModule.set_underwater_party()},
        // Besaid with Wakka
        {"bsil03_00", () => ArchipelagoFFXModule.set_underwater_party()},
        {"bsil03_01", () => ArchipelagoFFXModule.set_underwater_party()},
        {"bsil03_02", () => ArchipelagoFFXModule.set_underwater_party()},
        {"bsil03_03", () => ArchipelagoFFXModule.set_underwater_party()},
        // S.S Liki
        {"slik02_01", () => ArchipelagoFFXModule.set_underwater_party()},
    };


    public static Dictionary<string, int[]> encounterToLocationDict = new(){
        {"bjyt04_01",  [0]}, // Baaj Temple: Klikk Defeated
        {"cdsp07_00",  [1]}, // Al Bhed Ship: Tros Defeated
        {"bsil07_70",  [2]}, // Besaid: Dark Valefor
        {"slik02_00",  [3]}, // S.S. Liki: Sin Fin
        {"slik02_01",  [4]}, // S.S. Liki: Sinspawn Echuilles
        {"klyt00_00",  [5]}, // Kilika: Lord Ochu
        {"klyt01_00",  [6]}, // Kilika: Sinspawn Geneaux
        {"cdsp02_00",  [7]}, // Luca: Oblitzerator defeated
        {"mihn02_00",  [8]}, // Mi'Hen Highroad: Chocobo Eater
        {"kino02_00",  [9]}, // Mushroom Rock Road: Sinspawn Gui 2
        {"kino03_10", [10]}, // Mushroom Rock Road: Sinspawn Gui
        {"genk09_00", [12]}, // Moonflow: Extractor
        {"kami03_71", [13]}, // Thunder Plains: Dark Ixion
        {"mcfr03_00", [14]}, // Macalania Woods: Spherimorph
        {"maca02_00", [15]}, // Lake Macalania: Crawler
        {"mcyt06_00", [16]}, // Lake Macalania: Seymour/Anima
        {"maca02_01", [17]}, // Lake Macalania: Wendigo
        {"mcyt00_70", [18]}, // Lake Macalania: Dark Shiva
        {"bika03_70", [19]}, // Bikanel: Dark Ifrit
        {"hiku15_00", [20]}, // Airship: Evrae
        {"ssbt00_00", [21]}, // Airship: Sin Left Fin
        {"ssbt01_00", [22]}, // Airship: Sin Right Fin
        {"ssbt02_00", [23]}, // Airship: Sinspawn Genais and Core
        {"ssbt03_00", [24]}, // Airship: Overdrive Sin
        {"hiku15_70", [25]}, // Airship: Penance
        {"bvyt09_12", [26]}, // Bevelle: Isaaru (probably?)
        {"stbv00_10", [27]}, // Bevelle: Evrae Altana
        {"stbv01_10", [28]}, // Bevelle: Seymour Natus
        {"nagi01_00", [29]}, // Calm Lands: Defender X
        {"zzzz02_76", [30]}, // Monster Arena: Nemesis
        {"nagi05_74", [31]}, // Cavern of the Stolen Fayth: Dark Yojimbo
        {"mtgz01_10", [32]}, // Gagazet (Outside): Biran and Yenke
        {"mtgz02_00", [33]}, // Gagazet (Outside): Seymour Flux
        {"mtgz01_70", [34]}, // Gagazet (Outside): Dark Anima
        {"mtgz08_00", [35]}, // Gagazet: Sanctuary Keeper
        {"dome02_00", [36]}, // Zanarkand: Spectral Keeper
        {"dome06_00", [37]}, // Zanarkand: Yunalesca
        {"dome06_70", [38]}, // Zanarkand: Dark Bahamut
        {"sins03_00", [39]}, // Sin: Seymour Omnis
        {"sins06_00", [40]}, // Sin: Braska's Final Aeon
        {"sins07_0x", [41]}, // Sin: Contest of Aeons
        {"sins07_10", [42]}, // Sin: Yu Yevon
        {"omeg00_10", [43]}, // Omega Ruins: Ultima Weapon
        {"omeg01_10", [44]}, // Omega Ruins: Omega Weapon
        {"kino00_70", [45, 46, 47]}, // Dark Mindy, Dark Sandy, Dark Cindy
        {"kino01_70", [45, 46, 47]}, // Dark Mindy, Dark Sandy, Dark Cindy
        {"kino01_72", [45, 46]}, // Dark Mindy, Dark Sandy
        {"kino05_71", [45]}, // Dark Mindy
        {"kino05_70", [46]}, // Dark Sandy
        {"kino01_71", [47]}, // Dark Cindy
        {"bjyt02_02", [48]}, // Geosgaeno
        

        //{"kino00_70", 45}, {"kino01_70", 45}, {"kino01_72", 45}, {"kino05_71", 45}, // Dark Mindy
        //{"kino00_70", 46}, {"kino01_70", 46}, {"kino01_72", 46}, {"kino05_70", 46}, // Dark Sandy
        //{"kino00_70", 47}, {"kino01_70", 47}, {"kino01_71", 47},                    // Dark Cindy
    };
}
