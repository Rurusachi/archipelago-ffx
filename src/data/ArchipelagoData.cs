using Fahrenheit.Core;
using Fahrenheit.Core.FFX.Ids;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fahrenheit.Modules.Archipelago;

public class ArchipelagoData {
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
            199,
            200,
            201,
            374,
            375,
            202,
            322,
            203,
            296,
            204,
            327,
            324,
            325,
            326,
            386,
            ] },
        { RegionEnum.Airship, [
            194,
            265,
            351,
            211,
            277,
            255,
            392,
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
        public RegionEnum return_if_locked;
        public ArchipelagoStoryCheckDelegate? check_delegate;
    }
    public unsafe class ArchipelagoRegion {
        public Dictionary<ushort, ArchipelagoStoryCheck> story_checks = [];
        private ushort story_progress;
        public ushort Story_progress 
        {
            get => story_progress;
            set 
            {
                if (story_progress == value) return;
                if (story_checks.TryGetValue(value, out var storyCheck)) {
                    if (ArchipelagoModule.region_is_unlocked.TryGetValue(storyCheck.return_if_locked, out var is_unlocked) && !is_unlocked) {
                        ArchipelagoModule.call_warp_to_map(382, 0);
                    }
                    story_progress = storyCheck.next_story_progress ?? value;
                    room_id = storyCheck.next_room_id ?? room_id;
                    entrance = storyCheck.next_entrance ?? entrance;

                    storyCheck.check_delegate?.Invoke(this);
                } else {
                    story_progress = value;
                }
                    /*
                    if (story_checks.TryGetValue(value, out var checkDelegate)) {
                        checkDelegate(this);
                    }
                    //story_progress = story_transitions.GetValueOrDefault(value, value);
                     */
                } 
        }
        public ushort room_id;
        public ushort entrance;
        public uint airship_destination_index;
    }

    public static Dictionary<RegionEnum, ArchipelagoRegion> region_starting_state => new(){
        {RegionEnum.DreamZanarkand, new(){ Story_progress = 0, room_id = 132, entrance = 0, airship_destination_index = 99,
            story_checks = {
                { 5, new() {check_delegate = (r) => {
                    FhLog.Info("Dream Zanarkand complete");
                    ArchipelagoModule.save_party();
                    ArchipelagoModule.reset_party();
                    ArchipelagoModule.call_warp_to_map(382, 0);
                }} }
            } } },
        {RegionEnum.BaajTemple, new(){ Story_progress = 30, room_id = 48, entrance = 0, airship_destination_index = 1, 
            story_checks = {
                { 60, new() {check_delegate = (r) => {FhLog.Info("Baaj Temple visit 1 complete"); } } },
                { 110, new() {next_story_progress = 3000, next_room_id = 49, next_entrance = 2, return_if_locked = RegionEnum.Besaid, check_delegate = (r) => {FhLog.Info("Al Bhed Ship complete"); } } },
            } } },
        {RegionEnum.Besaid, new(){ Story_progress = 111, room_id = 70, entrance = 0, airship_destination_index = 2,
            story_checks = {
                { 228, new() {check_delegate = (r) => {FhLog.Info("Besaid visit 1 complete"); } } },
                { 290, new() {next_story_progress = 3000, next_room_id = 19, next_entrance = 1, return_if_locked = RegionEnum.Kilika, check_delegate = (r) => {FhLog.Info("S.S Liki visit complete"); } } },
            } } },
        {RegionEnum.Kilika, new(){ Story_progress = 290, room_id = 43, entrance = 0, airship_destination_index = 3,
            story_checks = {
                { 372, new() {check_delegate = (r) => {FhLog.Info("Kilika visit 1 complete"); } } },
                { 400, new() {next_story_progress = 3000, next_room_id = 98, next_entrance = 3, return_if_locked = RegionEnum.Luca, check_delegate = (r) => {FhLog.Info("S.S Winno visit complete"); } } },
                { 402, new() {next_story_progress = 3000, next_room_id = 98, next_entrance = 3, return_if_locked = RegionEnum.Luca, check_delegate = (r) => {FhLog.Info("S.S Winno visit complete"); } } },
            } } },
        {RegionEnum.Luca, new(){ Story_progress = 402, room_id = 267, entrance = 0, airship_destination_index = 4,
            story_checks = {
                { 730, new() {next_story_progress = 3000, next_room_id = 123, next_entrance = 6, return_if_locked = RegionEnum.MiihenHighroad, check_delegate = (r) => {FhLog.Info("Luca visit complete"); } } },
            } } },
        {RegionEnum.MiihenHighroad, new(){ Story_progress = 730, room_id = 95, entrance = 0, airship_destination_index = 5,
            story_checks = {
                { 787, new() {next_story_progress = 3000, next_room_id = 171, next_entrance = 1, return_if_locked = RegionEnum.MushroomRockRoad, check_delegate = (r) => {FhLog.Info("Mi'ihen Highroad visit complete"); } } },
            } } },
        {RegionEnum.MushroomRockRoad, new(){ Story_progress = 787, room_id = 79, entrance = 0, airship_destination_index = 6,
            story_checks = {
                { 960, new() {next_story_progress = 3000, next_room_id = 131, next_entrance = 3, return_if_locked = RegionEnum.Djose, check_delegate = (r) => {FhLog.Info("Mushroom Rock Road visit complete"); } } },
            } } },
        {RegionEnum.Djose, new(){ Story_progress = 960, room_id = 93, entrance = 0, airship_destination_index = 99,
            story_checks = {
                { 1030, new() {next_story_progress = 3000, next_room_id = 82, next_entrance = 0, return_if_locked = RegionEnum.Moonflow, check_delegate = (r) => {FhLog.Info("Djose visit 1 complete"); } } },
            } } },
        {RegionEnum.Moonflow, new(){ Story_progress = 1030, room_id = 75, entrance = 0, airship_destination_index = 7,
            story_checks = {
                { 1085, new() {next_story_progress = 3000, next_room_id = 235, next_entrance = 1, return_if_locked = RegionEnum.Guadosalam, check_delegate = (r) => {FhLog.Info("Moonflow visit complete"); } } },
            } } },
        {RegionEnum.Guadosalam, new(){ Story_progress = 1085, room_id = 135, entrance = 0, airship_destination_index = 8,
            story_checks = {
                { 1210, new() {next_story_progress = 3000, next_room_id = 243, next_entrance = 1, return_if_locked = RegionEnum.ThunderPlains, check_delegate = (r) => {FhLog.Info("Guadosalam visit complete"); } } },
            } } },
        {RegionEnum.ThunderPlains, new(){ Story_progress = 1210, room_id = 140, entrance = 0, airship_destination_index = 9,
            story_checks = {
                { 1375, new() {next_story_progress = 3000, next_room_id = 263, next_entrance = 2, return_if_locked = RegionEnum.Macalania, check_delegate = (r) => {FhLog.Info("Thunder Plains visit complete"); } } },
            } } },
        {RegionEnum.Macalania, new(){ Story_progress = 1400, room_id = 110, entrance = 0, airship_destination_index = 10,
            story_checks = {
                { 1470, new() {check_delegate = (r) => {FhLog.Info("Macalania Woods visit complete"); } } },
                { 1704, new() {next_story_progress = 3000, next_room_id = 215, next_entrance = 1, return_if_locked = RegionEnum.Bikanel, check_delegate = (r) => {FhLog.Info("Lake Macalania visit 1 complete"); } } },
            } } },
        {RegionEnum.Bikanel, new(){ Story_progress = 1704, room_id = 129, entrance = 0, airship_destination_index = 11,
            story_checks = {
                { 1950, new() {next_story_progress = 3000, next_room_id = 129, next_entrance = 2, return_if_locked = RegionEnum.Airship, check_delegate = (r) => {FhLog.Info("Bikanel visit complete"); } } },
            } } },
        {RegionEnum.Airship, new(){ Story_progress = 1950, room_id = 194, entrance = 1, airship_destination_index = 99,
            story_checks = {
                { 2075, new() {next_story_progress = 2970, next_room_id = 255, next_entrance = 0, return_if_locked = RegionEnum.Bevelle, check_delegate = (r) => {FhLog.Info("Airship visit 1 complete"); } } },
            } } },
        {RegionEnum.Bevelle, new(){ Story_progress = 2040, room_id = 205, entrance = 0, airship_destination_index = 18, // Destination 12 doesn't work (12 = Bevelle but doesn't have destination, 18 = Highbridge) 
            story_checks = {
                { 2385, new() {next_story_progress = 2920, next_room_id = 227, next_entrance = 0, return_if_locked = RegionEnum.CalmLands, check_delegate = (r) => {FhLog.Info("Bevelle visit 1 complete"); } } },
                { 2945, new() {next_story_progress = 3000, next_room_id = 208, next_entrance = 1, check_delegate = (r) => {FhLog.Info("Bevelle visit 2 complete"); } } },
            } } }, 
        {RegionEnum.CalmLands, new(){ Story_progress = 2385, room_id = 223, entrance = 0, airship_destination_index = 13,
            story_checks = {
                { 2440, new() {next_story_progress = 3000, next_room_id = 223, next_entrance = 4, return_if_locked = RegionEnum.MtGagazet, check_delegate = (r) => {FhLog.Info("Calm Lands complete"); } } }, // Normally ends at 2440, but CSR skips from 2420 to 2510
                { 2510, new() {next_story_progress = 3000, next_room_id = 223, next_entrance = 4, return_if_locked = RegionEnum.MtGagazet, check_delegate = (r) => {FhLog.Info("Calm Lands complete"); } } }, // Normally ends at 2440, but CSR skips from 2420 to 2510
            } } },
        {RegionEnum.CavernOfTheStolenFayth, new(){ Story_progress = 2385, room_id = 56, entrance = 0, airship_destination_index = 99 } },
        {RegionEnum.MtGagazet, new(){ Story_progress = 2440, room_id = 259, entrance = 0, airship_destination_index = 14,
            story_checks = {
                { 2680, new() {next_story_progress = 3000, next_room_id = 259, next_entrance = 2, return_if_locked = RegionEnum.ZanarkandRuins, check_delegate = (r) => {FhLog.Info("Mt. Gagazet complete"); } } },
            } } },
        {RegionEnum.ZanarkandRuins, new(){ Story_progress = 2680, room_id = 132, entrance = 0, airship_destination_index = 15,
            story_checks = {
                { 2900, new() {next_story_progress = 3000, next_room_id = 313, next_entrance = 3, return_if_locked = RegionEnum.Airship, check_delegate = (r) => {FhLog.Info("Zanarkand Ruins complete"); } } },
            } } },
        {RegionEnum.Sin, new(){ Story_progress = 3085, room_id = 199, entrance = 0, airship_destination_index = 16,
            story_checks = {
                { 3400, new() {next_room_id = 199, next_entrance = 0, check_delegate = (r) => {FhLog.Info("Game Complete"); } } },
            } } },
        {RegionEnum.OmegaRuins, new(){ Story_progress = 3000, room_id = 258, entrance = 2, airship_destination_index = 17 } }, // Story_progress?
    };

    // For battle that don't push/pop but should
    public static Dictionary<string, List<PlySaveId>> encounterToPartyDict => new(){
        {"bjyt02_00", [PlySaveId.PC_TIDUS,
            PlySaveId.PC_WAKKA,
            PlySaveId.PC_RIKKU,
        ]},
        {"bjyt02_01", [PlySaveId.PC_TIDUS,
            PlySaveId.PC_WAKKA,
            PlySaveId.PC_RIKKU,
        ]},
        {"lchb07_00", [PlySaveId.PC_AURON,
            PlySaveId.PC_TIDUS,
            PlySaveId.PC_YUNA,
            PlySaveId.PC_KIMAHRI,
            PlySaveId.PC_WAKKA,
            PlySaveId.PC_LULU,
            PlySaveId.PC_RIKKU,
            PlySaveId.PC_VALEFOR,
            PlySaveId.PC_IFRIT,
            PlySaveId.PC_IXION,
            PlySaveId.PC_SHIVA,
            PlySaveId.PC_BAHAMUT,
            PlySaveId.PC_ANIMA,
            PlySaveId.PC_YOJIMBO,
            PlySaveId.PC_MAGUS1,
            PlySaveId.PC_MAGUS2,
            PlySaveId.PC_MAGUS3,
        ]},
    };







    // TODO: Populate battle checks
    public static Dictionary<string, int> encounterToLocationDict => new() {

    };
    
}
