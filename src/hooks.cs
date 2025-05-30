using Fahrenheit.Core;
using System.Runtime.InteropServices;
using System;
using static Fahrenheit.Modules.ArchipelagoFFX.delegates;
using Fahrenheit.Core.FFX;
using Fahrenheit.Modules.ArchipelagoFFX.Client;
using static Fahrenheit.Modules.ArchipelagoFFX.Client.ArchipelagoClient;
using Fahrenheit.Core.FFX.Battle;
using Fahrenheit.Core.FFX.Ids;
using Fahrenheit.Core.FFX.Atel;
using static Fahrenheit.Core.FFX.Globals;
using static Fahrenheit.Modules.ArchipelagoFFX.ArchipelagoData;
using System.Linq;
using System.Numerics;
using Fahrenheit.Core.ImGuiNET;
using System.Collections.Generic;
using System.Text;

//[assembly: DisableRuntimeMarshalling]
namespace Fahrenheit.Modules.ArchipelagoFFX;
public unsafe partial class ArchipelagoFFXModule {

    public static int* takara_pointer => FhUtil.ptr_at<int>(0xD35FEC);
    public static int* buki_get_pointer => FhUtil.ptr_at<int>(0xD35FF4);


    // AtelEventSetUp
    private static FhMethodHandle<FhCall.AtelEventSetUp> _AtelEventSetUp;

    public static char* get_event_name(uint event_id) => FhUtil.get_fptr<AtelGetEventName>(0x4796e0)(event_id);
    public static int atel_stack_pop(int* param_1, AtelStack* atelStack) => FhUtil.get_fptr<AtelStackPop>(0x0046de90)(param_1, atelStack);

    private static FhMethodHandle<Common_obtainTreasureInit> _Common_obtainTreasureInit;
    private static FhMethodHandle<Common_obtainTreasureSilentlyInit> _Common_obtainTreasureSilentlyInit;
    private static FhMethodHandle<Common_obtainBrotherhood> _Common_obtainBrotherhood;
    private static FhMethodHandle<Common_grantCelestialUpgrade> _Common_grantCelestialUpgrade;
    private static FhMethodHandle<Common_setPrimerCollected> _Common_setPrimerCollected;
    private static FhMethodHandle<Common_transitionToMap> _Common_transitionToMap;
    private static FhMethodHandle<Common_warpToMap> _Common_warpToMap;

    private static FhMethodHandle<SgEvent_showModularMenuInit> _SgEvent_showModularMenuInit;

    //private static FhMethodHandle<Common_playFieldVoiceLineInit> _Common_playFieldVoiceLineInit;
    //private static FhMethodHandle<Common_playFieldVoiceLineExec> _Common_playFieldVoiceLineExec;
    //private static FhMethodHandle<Common_playFieldVoiceLineResultInt> _Common_playFieldVoiceLineResultInt;



    private static Common_playFieldVoiceLineInit _Common_playFieldVoiceLineInit;
    private static Common_playFieldVoiceLineExec _Common_playFieldVoiceLineExec;
    private static Common_playFieldVoiceLineResultInt _Common_playFieldVoiceLineResultInt;

    private static Common_00D6Init _Common_00D6Init;
    private static Common_00D6eExec _Common_00D6eExec;
    private static Common_00D6ResultInt _Common_00D6ResultInt;


    // Common.01D1Init
    private static FhMethodHandle<FUN_0085fb60> _FUN_0085fb60;
    // Common.01D1Exec
    private static FhMethodHandle<FUN_0085fdb0> _FUN_0085fdb0;

    private static FhMethodHandle<Common_addPartyMember> _Common_addPartyMember;
    private static FhMethodHandle<Common_removePartyMember> _Common_removePartyMember;
    private static FhMethodHandle<Common_removePartyMemberLongTerm> _Common_removePartyMemberLongTerm;
    private static FhMethodHandle<Common_setWeaponInvisible> _Common_setWeaponVisibilty;
    private static FhMethodHandle<Common_putPartyMemberInSlot> _Common_putPartyMemberInSlot;

    private static FhMethodHandle<Common_pushParty> _Common_pushParty;
    private static FhMethodHandle<Common_popParty> _Common_popParty;

    // getCurrentPartySlots
    private static FhMethodHandle<FhCall.MsGetSavePartyMember> _MsGetSavePartyMember;

    private static FhCall.MsBtlListGroup _MsBtlListGroup;
    private static FhMethodHandle<FhCall.MsBattleExe> _MsBattleExe;
    private static FhMethodHandle<MsBattleLabelExe> _MsBattleLabelExe;
    private static FhMethodHandle<FUN_00791820> _FUN_00791820;

    private static FhMethodHandle<MsBtlGetPos> _MsBtlGetPos;

    private static FhMethodHandle<MsBtlReadSetScene> _MsBtlReadSetScene;

    // giveItem
    private static FhMethodHandle<FUN_007905a0> _FUN_007905a0;
    // giveKeyItem
    private static FhMethodHandle<FUN_0088e700> _FUN_0088e700;
    // takeGil
    private static FhMethodHandle<FUN_00785a60> _FUN_00785a60;


    // readFromBin
    private static FhMethodHandle<FUN_007ab890> _FUN_007ab890;

    // getWeaponName
    private static FhMethodHandle<FUN_007a0d10> _FUN_007a0d10;
    // getWeaponModel
    private static FhMethodHandle<FUN_007a0c70> _FUN_007a0c70;
    // giveWeapon
    private static FhMethodHandle<FUN_007ab930> _FUN_007ab930;
    // obtainTreasureCleanup
    private static FhMethodHandle<FUN_007993f0> _FUN_007993f0;



    // Overdrive Experiment
    private static FhMethodHandle<FhCall.MsCheckLeftWindow> _MsCheckLeftWindow;
    private static FhMethodHandle<FhCall.MsCheckUseCommand> _MsCheckUseCommand;
    private static FhMethodHandle<FhCall.TOBtlDrawStatusLimitGauge> _TOBtlDrawStatusLimitGauge;

    private static FhCall.MsGetChr _MsGetChr;
    private static FhCall.MsGetComData _MsGetComData;
    private static FhCall.MsGetCommandUse _MsGetCommandUse;
    private static FhCall.MsGetCommandMP _MsGetCommandMP;
    private static FhCall.MsGetRamChrMonster _MsGetRamChrMonster;
    private static FhCall.TODrawCrossBoxXYWHC2 _TODrawCrossBoxXYWHC2;


    // Sphere Grid Experiment
    private static FhMethodHandle<FhCall.eiAbmParaGet> _eiAbmParaGet;
    private static FhMethodHandle<FUN_00a48910> _FUN_00a48910;

    private static FhMethodHandle<FhCall.MsApUp> _MsApUp;


    private static FhMethodHandle<openFile> _openFile;
    private static readFile _readFile;
    private static FhMethodHandle<FUN_0070aec0> _FUN_0070aec0;

    private static FhCall.ChN_ReadSystemMGRP _ChN_ReadSystemMGRP;
    private static Common_loadModel _Common_loadModel;
    private static Common_0043 _Common_0043;
    private static Common_linkFieldToBattleActor _Common_linkFieldToBattleActor;
    private static FhCall.SndSepPlay _SndSepPlay;
    private static FhCall.SndSepPlaySimple _SndSepPlaySimple;


    private static FhMethodHandle<FhCall.MsSetSaveParam> _MsSetSaveParam;
    public static FhCall.MsGetExcelData _MsGetExcelData;
    public static FhCall.MsGetSaveWeapon _MsGetSaveWeapon;

    private static FhMethodHandle<Map_800F> _Map_800F;

    private static FhMethodHandle<FUN_0086bec0> _FUN_0086bec0;
    private static FhMethodHandle<FUN_0086bea0> _FUN_0086bea0;

    public void init_hooks() {
        const string game = "FFX.exe";

        _AtelEventSetUp = new FhMethodHandle<FhCall.AtelEventSetUp>(this, game, h_AtelEventSetUp, offset: FhCall.__addr_AtelEventSetUp);

        _Common_obtainTreasureInit = new FhMethodHandle<Common_obtainTreasureInit>(this, game, h_Common_obtainTreasureInit, offset: 0x0045a740);

        _Common_obtainTreasureSilentlyInit = new FhMethodHandle<Common_obtainTreasureSilentlyInit>(this, game, h_Common_obtainTreasureSilentlyInit, offset: 0x004579e0);

        _Common_obtainBrotherhood = new FhMethodHandle<Common_obtainBrotherhood>(this, game, h_Common_obtainBrotherhood, offset: 0x00459a40);

        _Common_grantCelestialUpgrade = new FhMethodHandle<Common_grantCelestialUpgrade>(this, game, h_Common_grantCelestialUpgrade, offset: 0x0045cfe0);

        _Common_setPrimerCollected = new FhMethodHandle<Common_setPrimerCollected>(this, game, h_Common_setPrimerCollected, offset: 0x0045ab30);

        _Common_transitionToMap = new FhMethodHandle<Common_transitionToMap>(this, game, h_Common_transitionToMap, offset: 0x004580c0);
        _Common_warpToMap = new FhMethodHandle<Common_warpToMap>(this, game, h_Common_warpToMap, offset: 0x00458370);

        _SgEvent_showModularMenuInit = new FhMethodHandle<SgEvent_showModularMenuInit>(this, game, h_SgEvent_showModularMenuInit, offset: 0x00678210);


        //_Common_playFieldVoiceLineInit = new FhMethodHandle<Common_playFieldVoiceLineInit>(this, game, h_Common_playFieldVoiceLineInit, offset: 0x0045cb70);
        //_Common_playFieldVoiceLineExec = new FhMethodHandle<Common_playFieldVoiceLineExec>(this, game, h_Common_playFieldVoiceLineExec, offset: 0x0045cd30);
        //_Common_playFieldVoiceLineResultInt = new FhMethodHandle<Common_playFieldVoiceLineResultInt>(this, game, h_Common_playFieldVoiceLineResultInt, offset: 0x0045d150);

        _Common_playFieldVoiceLineInit = FhUtil.get_fptr<Common_playFieldVoiceLineInit>(0x0045cb70);
        _Common_playFieldVoiceLineExec = FhUtil.get_fptr<Common_playFieldVoiceLineExec>(0x0045cd30);
        _Common_playFieldVoiceLineResultInt = FhUtil.get_fptr<Common_playFieldVoiceLineResultInt>(0x0045d150);

        _Common_00D6Init = FhUtil.get_fptr<Common_00D6Init>(0x0045d520);
        _Common_00D6eExec = FhUtil.get_fptr<Common_00D6eExec>(0x0045d820);
        _Common_00D6ResultInt = FhUtil.get_fptr<Common_00D6ResultInt>(0x0045dcf0);



        // Common.01D1Init
        //_FUN_0085fb60 = new FhMethodHandle<FUN_0085fb60>(this, game, h_after_voiceline_init, offset: 0x0045fb60);
        // Common.01D1Exec
        //_FUN_0085fdb0 = new FhMethodHandle<FUN_0085fdb0>(this, game, h_after_voiceline_exec, offset: 0x0045fdb0);




        _Common_addPartyMember = new FhMethodHandle<Common_addPartyMember>(this, game, h_Common_addPartyMember, offset: 0x0045b5a0);
        _Common_removePartyMember = new FhMethodHandle<Common_removePartyMember>(this, game, h_Common_removePartyMember, offset: 0x0045b6c0);
        _Common_removePartyMemberLongTerm = new FhMethodHandle<Common_removePartyMemberLongTerm>(this, game, h_Common_removePartyMemberLongTerm, offset: 0x0045aaf0);
        _Common_setWeaponVisibilty = new FhMethodHandle<Common_setWeaponInvisible>(this, game, h_Common_setWeaponInvisible, offset: 0x00456770);
        _Common_putPartyMemberInSlot = new FhMethodHandle<Common_putPartyMemberInSlot>(this, game, h_Common_putPartyMemberInSlot, offset: 0x0045bc90);


        _Common_pushParty = new FhMethodHandle<Common_pushParty>(this, game, h_Common_pushParty, offset: 0x0045b350);
        _Common_popParty = new FhMethodHandle<Common_popParty>(this, game, h_Common_popParty, offset: 0x0045b3c0);

        // getCurrentPartySlots
        _MsGetSavePartyMember = new FhMethodHandle<FhCall.MsGetSavePartyMember>(this, game, h_MsGetSavePartyMember, offset: FhCall.__addr_MsGetSavePartyMember);


        _MsBtlListGroup = FhUtil.get_fptr<FhCall.MsBtlListGroup>(FhCall.__addr_MsBtlListGroup);
        _MsBattleExe = new FhMethodHandle<FhCall.MsBattleExe>(this, game, h_MsBattleExe, offset: FhCall.__addr_MsBattleExe);
        _MsBattleLabelExe = new FhMethodHandle<MsBattleLabelExe>(this, game, h_MsBattleLabelExe, offset: 0x00381d60);
        _FUN_00791820 = new FhMethodHandle<FUN_00791820>(this, game, h_FUN_00791820, offset: 0x00391820);

        _MsBtlGetPos = new FhMethodHandle<MsBtlGetPos>(this, game, h_MsBtlGetPos, offset: 0x003ac000);

        _MsBtlReadSetScene = new FhMethodHandle<MsBtlReadSetScene>(this, game, h_MsBtlReadSetScene, offset: 0x00383ed0);




        // giveItem
        _FUN_007905a0 = new FhMethodHandle<FUN_007905a0>(this, game, h_give_item, offset: 0x003905a0);

        // giveKeyItem
        _FUN_0088e700 = new FhMethodHandle<FUN_0088e700>(this, game, h_give_key_item, offset: 0x0048e700);

        // takeGil
        _FUN_00785a60 = new FhMethodHandle<FUN_00785a60>(this, game, h_take_gil, offset: 0x00385a60);

        // giveWeapon
        _FUN_007ab930 = new FhMethodHandle<FUN_007ab930>(this, game, h_give_weapon, offset: 0x003ab930);

        // readFromBin
        _FUN_007ab890 = new FhMethodHandle<FUN_007ab890>(this, game, h_read_from_bin, offset: 0x003ab890);

        // getWeaponName
        _FUN_007a0d10 = new FhMethodHandle<FUN_007a0d10>(this, game, h_get_weapon_name, offset: 0x003a0d10);
        // getWeaponModel
        _FUN_007a0c70 = new FhMethodHandle<FUN_007a0c70>(this, game, h_get_weapon_model, offset: 0x003a0c70);
        // obtainTreasureCleanup
        _FUN_007993f0 = new FhMethodHandle<FUN_007993f0>(this, game, h_obtain_treasure_cleanup, offset: 0x003993f0);


        // Overdrive Experiment
        _MsCheckLeftWindow = new FhMethodHandle<FhCall.MsCheckLeftWindow>(this, game, h_MsCheckLeftWindow, offset: 0x0038f750);
        _MsCheckUseCommand = new FhMethodHandle<FhCall.MsCheckUseCommand>(this, game, h_MsCheckUseCommand, offset: 0x0038c750);
        _TOBtlDrawStatusLimitGauge = new FhMethodHandle<FhCall.TOBtlDrawStatusLimitGauge>(this, game, h_TOBtlDrawStatusLimitGauge, offset: 0x00496050);

        _MsGetChr = FhUtil.get_fptr<FhCall.MsGetChr>(0x00394030);
        _MsGetComData = FhUtil.get_fptr<FhCall.MsGetComData>(0x0039a4c0);
        _MsGetCommandUse = FhUtil.get_fptr<FhCall.MsGetCommandUse>(0x0039a5c0);
        _MsGetCommandMP = FhUtil.get_fptr<FhCall.MsGetCommandMP>(0x0038d030);
        _MsGetRamChrMonster = FhUtil.get_fptr<FhCall.MsGetRamChrMonster>(0x0039af00);
        _TODrawCrossBoxXYWHC2 = FhUtil.get_fptr<FhCall.TODrawCrossBoxXYWHC2>(0x004f4b20);

        _eiAbmParaGet = new FhMethodHandle<FhCall.eiAbmParaGet>(this, game, h_eiAbmParaGet, offset: FhCall.__addr_eiAbmParaGet);
        _FUN_00a48910 = new FhMethodHandle<FUN_00a48910>(this, game, h_FUN_00a48910, offset: 0x00648910);

        _MsApUp = new FhMethodHandle<FhCall.MsApUp>(this, game, h_MsApUp, offset: 0x00398a10);

        //delegate* unmanaged[Thiscall]<nint, nint, bool, bool> ph_openFile = &h_openFile;
        //openFile p_openFile = (x, y, z) => d_openFile(ph_openFile, x, y, z);
        //openFile temp = Marshal.GetDelegateForFunctionPointer<openFile>((nint)ph_openFile);
        _openFile = new FhMethodHandle<openFile>(this, game, h_openFile, offset: 0x00208100);
        _readFile = FhUtil.get_fptr<readFile>(0x00208250);

        _FUN_0070aec0 = new FhMethodHandle<FUN_0070aec0>(this, game, h_FUN_0070aec0, offset: 0x0030aec0);


        _ChN_ReadSystemMGRP = FhUtil.get_fptr<FhCall.ChN_ReadSystemMGRP>(FhCall.__addr_ChN_ReadSystemMGRP);

        _Common_loadModel = FhUtil.get_fptr<Common_loadModel>(0x0045ce70);
        _Common_0043 = FhUtil.get_fptr<Common_0043>(0x0045c810);
        _Common_linkFieldToBattleActor = FhUtil.get_fptr<Common_linkFieldToBattleActor>(0x0045ca00);

        _SndSepPlay = FhUtil.get_fptr<FhCall.SndSepPlay>(FhCall.__addr_SndSepPlay);
        _SndSepPlaySimple = FhUtil.get_fptr<FhCall.SndSepPlaySimple>(FhCall.__addr_SndSepPlaySimple);


        _Map_800F = new FhMethodHandle<Map_800F>(this, game, h_Map_800F, offset: 0x0051b1a0);


        _MsSetSaveParam = new FhMethodHandle<FhCall.MsSetSaveParam>(this, game, h_MsSetSaveParam, offset: FhCall.__addr_MsSetSaveParam);
        _MsGetExcelData = FhUtil.get_fptr<FhCall.MsGetExcelData>(FhCall.__addr_MsGetExcelData);
        _MsGetSaveWeapon = FhUtil.get_fptr<FhCall.MsGetSaveWeapon>(FhCall.__addr_MsGetSaveWeapon);


        _FUN_0086bec0 = new FhMethodHandle<FUN_0086bec0>(this, game, h_FUN_0086bec0, offset: 0x0046bec0);
        _FUN_0086bea0 = new FhMethodHandle<FUN_0086bea0>(this, game, h_FUN_0086bea0, offset: 0x0046bea0);

        foreach (byte[] script in customScripts) {
            customScriptHandles.Add(GCHandle.Alloc(script, GCHandleType.Pinned));
        }
        //customScriptHandles.Add(GCHandle.Alloc(customScriptHook, GCHandleType.Pinned));
        //customScriptHandles.Add(GCHandle.Alloc(customScriptSin, GCHandleType.Pinned));
        //customScriptHandle = GCHandle.Alloc(customScript, GCHandleType.Pinned);

        foreach (byte[] text in customStrings) {
            customStringHandles.Add(GCHandle.Alloc(text, GCHandleType.Pinned));
        }
        //customStringHandles.Add(GCHandle.Alloc(customStrings[0], GCHandleType.Pinned));
    }

    public static int ignore_this = 11;
    public static int h_Map_800F(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int param_1 = atelStack->values_as_int[0];
        if (param_1 == ignore_this) {
            atelStack->pop_int();
            return 1;
        }
        return _Map_800F.orig_fptr(work, storage, atelStack);
    }

    public static bool h_openFile(nint _this, nint filename, bool readOnly, nint unknown_1, nint unknown_2, nint unknown_3) {
        string x = Marshal.PtrToStringAnsi(filename);
        logger.Debug($"{_this}, {x}, {readOnly}, {unknown_1}, {unknown_2}, {unknown_3}");
        var result = _openFile.orig_fptr(_this, filename, readOnly, unknown_1, unknown_2, unknown_3);
        logger.Debug($"{result}, {*(int*)_this}, {*(int*)((int)_this + 4)}");
        return result;
    }

    public static uint h_FUN_0070aec0(nint _this, uint voice_id, uint param_2) {

        return _FUN_0070aec0.orig_fptr(_this, voice_id, param_2);
    }

    public bool hook() {

        return _Common_obtainTreasureInit.hook() && _Common_obtainTreasureSilentlyInit.hook() && _Common_obtainBrotherhood.hook()
            && _Common_grantCelestialUpgrade.hook() && _Common_setPrimerCollected.hook()
            && _AtelEventSetUp.hook() && _Common_transitionToMap.hook() && _Common_warpToMap.hook()
            && _SgEvent_showModularMenuInit.hook()
            && _Common_addPartyMember.hook() && _Common_removePartyMember.hook() && _Common_removePartyMemberLongTerm.hook() && _Common_setWeaponVisibilty.hook()
            && _Common_putPartyMemberInSlot.hook() && _Common_pushParty.hook() && _Common_popParty.hook() && _MsBattleExe.hook() && _FUN_00791820.hook()
            && _MsApUp.hook() && _MsBtlReadSetScene.hook() //&& _MsSetSaveParam.hook() // && _Map_800F.hook() //_MsBtlGetPos.hook()
            && _eiAbmParaGet.hook() // && _FUN_00a48910.hook()
            && _FUN_0086bec0.hook() && _FUN_0086bea0.hook(); // Custom strings
        //&& _openFile.hook() && _FUN_0070aec0.hook();
        //&& _MsCheckLeftWindow.hook() && _MsCheckUseCommand.hook() && _TOBtlDrawStatusLimitGauge.hook();

    }

    private static void set(byte* code_ptr, int offset, AtelOpCode opcode) {
        byte* ptr = code_ptr + offset;
        foreach (byte b in opcode.to_bytes()) {
            *ptr = b;
            ptr++;
        }
    }
    private static void set(byte* code_ptr, int offset, AtelOpCode[] opcodes) {
        byte* ptr = code_ptr + offset;
        foreach (AtelOpCode op in opcodes) {
            foreach (byte b in op.to_bytes()) {
                *ptr = b;
                ptr++;
            }
        }
    }

    private static byte* h_FUN_0086bec0(int param_1) {
        byte* result = _FUN_0086bec0.orig_fptr(param_1);

        if ((param_1 & 0x8000) != 0) {
            int custom_index = param_1 & 0x7FFF;
            result = (byte*)customStringHandles[custom_index].AddrOfPinnedObject();
            string decoded = FhCharset.Us.to_string(result);
            logger.Debug(decoded);
        }

        return result;
    }
    private static short h_FUN_0086bea0(int param_1) {
        short result;
        if ((param_1 & 0x8000) != 0) {
            int custom_index = param_1 & 0x7FFF;
            result = customStringFlags[custom_index];
        } else {
            result = _FUN_0086bea0.orig_fptr(param_1);
        }

        return result;
    }

    private static readonly byte NEWLINE = 0x03;
    private static readonly byte TIME    = 0x09;
    private static readonly byte CHOICE  = 0x10;

    private static readonly List<GCHandle> customStringHandles = [];
    private static readonly byte[][] customStrings = {
        new byte[][] {
            [TIME, 0x30],
            FhCharset.Us.to_bytes("Ready to fight Sin?"),
            [NEWLINE, CHOICE, 0x30],
            FhCharset.Us.to_bytes("Yes"),
            [NEWLINE, CHOICE, 0x31],
            FhCharset.Us.to_bytes("No"),
        }.SelectMany(x => x).ToArray(),
    };
    // Choices << 8 | Flags
    private static readonly short[] customStringFlags = {
        (02 << 8) | 00,

    };

    private static readonly List<GCHandle> customScriptHandles = [];
    private static readonly byte[][] customScripts = {
        // Cid talk hook
        new AtelOpCode[]{
            // If GameMoment < 2970 or GameMoment >= 3120: Jump to j01 (return)
            AtelInst.PUSHV   .build(0x0000),
            AtelInst.POPY    .build(      ),

            AtelInst.PUSHY   .build(      ),
            AtelInst.PUSHII  .build(2970  ),
            AtelInst.LS      .build(      ),

            AtelInst.PUSHY   .build(      ),
            AtelInst.PUSHII  .build(3210  ),
            AtelInst.GTE     .build(      ),

            AtelInst.LOR     .build(      ),
            AtelInst.POPXCJMP.build(0x0001),


            // call Common.disablePlayerControl? [005Eh]();
            AtelInst.CALLPOPA.build(0x005e),
            // call Common.displayFieldChoice [013Bh](boxIndex=1 [01h], string="{TIME:00}{CHOICE:00}Save{\n}{CHOICE:01}Cancel" [D4h], p3=0 [00h], p4=1 [01h], x=280 [0118h], y=224 [E0h], align=Center [04h]);
            AtelInst.PUSHII  .build(0x0001),
            AtelInst.PUSHII  .build(0x8000),
            AtelInst.PUSHII  .build(0x0000),
            AtelInst.PUSHII  .build(0x0001),
            AtelInst.PUSHII  .build(0x0100),
            AtelInst.PUSHII  .build(0x00e0),
            AtelInst.PUSHII  .build(0x0004),
            AtelInst.CALLPOPA.build(0x013b),
            AtelInst.PUSHA.build(),
            // call Common.waitForText [0084h](boxIndex=1 [01h], p2=1 [01h]);
            AtelInst.PUSHII  .build(0x0001),
            AtelInst.PUSHII  .build(0x0001),
            AtelInst.CALLPOPA.build(0x0084),
            // call Common.enablePlayerControl? [005Dh]();
            AtelInst.CALLPOPA.build(0x005d),
            // call Common.obtainBrotherhood(choice, 1, 3) i.e. if !choice jump to customscripts[1]
            AtelInst.PUSHII  .build(0x0103),
            AtelInst.CALLPOPA.build(0x01B8),

            
            //// Jump to j01 (return)
            AtelInst.JMP     .build(0x0001),
        }.SelectMany(x => x.to_bytes()).ToArray(),

        // Start Airship Sin fights
        new AtelOpCode[]{
            // Set GameMoment = 3085
            AtelInst.PUSHII  .build(  3085),
            AtelInst.POPV    .build(0x0000),
            // call Common.transitionToMap? [0011h](map=199, entranceIndex=0);
            AtelInst.PUSHII  .build(   199),
            AtelInst.PUSHII  .build(     0),
            AtelInst.CALLPOPA.build(0x0011),
            // Return;
            AtelInst.RET.build(),

        }.SelectMany(x => x.to_bytes()).ToArray(),
    };

    private static readonly AtelOpCode[] save_sphere_load_model = [
        AtelInst.PUSHII  .build(0x5001),
        AtelInst.CALLPOPA.build(0x0001),
    ];

    private static string current_event_name = "";
    private static void h_AtelEventSetUp(int event_id) {
        _AtelEventSetUp.orig_fptr(event_id);

        string event_name = Marshal.PtrToStringAnsi((nint)get_event_name((uint)event_id))!;
        logger.Debug($"atel_event_setup: {event_name}");
        byte* code_ptr = Atel.controllers[0].worker(0)->code_ptr;
        switch (event_name) {
            case "hiku2100":
                logger.Debug($"atel_event_setup: Inject set_airship_destinations call");
                set(code_ptr, 0x26D1, [
                    AtelInst.PUSHII  .build(0x0001),
                    AtelInst.CALLPOPA.build(0x01B8) // Common.obtainBrotherhood(1) = set_airship_destinations
                    ]);

                set(code_ptr, 0x4028, AtelInst.JMP.build(0x013A));
                break;
            case "hiku0801":
                logger.Debug($"atel_event_setup: Inject Cid talk hook");
                set(code_ptr, 0x4DC5, [
                    AtelInst.PUSHII  .build(0x0002),
                    AtelInst.CALLPOPA.build(0x01B8), // Common.obtainBrotherhood(0, 2) = jump to customScripts[0]
                ]);
                break;
            case "ssbt0300":
                logger.Debug($"atel_event_setup: Redirect Overdrive Sin post-battle warp");
                set(code_ptr, 0x500E, AtelInst.PUSHII.build(382));
                break;
        }
        // Inject save sphere hook
        {
            AtelOpCode? previous_op = null;
            AtelOpCode? current_op = null;
            uint code_length = Atel.controllers[0].worker(0)->script_chunk->code_length;
            int i = 0;
            while (i < code_length) {
                previous_op = current_op;
                AtelInst inst = (AtelInst)code_ptr[i];
                if (inst.has_operand()) {
                    current_op = inst.build(*(ushort*)(code_ptr + i + 1));
                } else {
                    current_op = inst.build();
                }
                if (current_op == save_sphere_load_model[1] && previous_op == save_sphere_load_model[0]) {
                    logger.Info($"Detected save sphere init at {i-6}");
                    int save_sphere_offset = i - 6;
                    set(code_ptr, save_sphere_offset + 0x48, AtelInst.JMP.build(0x0007)); // Always all options

                    // Update region state. Also skips save sphere tutorial
                    set(code_ptr, save_sphere_offset + 0x571, [
                        AtelInst.PUSHII  .build(0x0007),
                        AtelInst.CALLPOPA.build(0x01B8), // Common.obtainBrotherhood(7) = update_region_state

                        AtelInst.JMP     .build(0x0023),
                        ]);

                    // Board Airship option
                    set(code_ptr, save_sphere_offset + 0x657, [
                        // call Common.00BB(0);
                        AtelInst.PUSHII  .build(0x0000),
                        AtelInst.CALLPOPA.build(0x00BB),
                        // call Common.00BC(0);
                        AtelInst.PUSHII  .build(0x0000),
                        AtelInst.CALLPOPA.build(0x00BC),
                        // call Common.warpToMap?[010Bh](382, 0); (Airship Menu)
                        AtelInst.PUSHII  .build(   382),
                        AtelInst.PUSHII  .build(     0),
                        AtelInst.CALLPOPA.build(0x010B),
                        ]);


                }
                i += inst.has_operand() ? 3 : 1;
            }
        }

        current_event_name = event_name;
    }

    private static void h_Common_obtainTreasureInit(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int treasure_id = atelStack->values_as_int[1];
        //ArchipelagoGUI.lastTreasure = treasure_id;
        logger.Debug($"obtain_treasure: {treasure_id}");
        ArchipelagoClient.sendTreasureLocation(treasure_id);

        _Common_obtainTreasureInit.orig_fptr(work, storage, atelStack);
    }

    private static void h_Common_obtainTreasureSilentlyInit(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int treasure_id = atelStack->values_as_int[0];
        //ArchipelagoGUI.lastTreasure = treasure_id;
        logger.Debug($"obtain_treasure_silently: {treasure_id}");
        ArchipelagoClient.sendTreasureLocation(treasure_id);
        _Common_obtainTreasureSilentlyInit.orig_fptr(work, storage, atelStack);
    }

    private static void h_Common_obtainBrotherhood(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        logger.Debug($"obtain_brotherhood");
        if (atelStack->size > 0) {
            int param = atelStack->pop_int();
            int call_type = param & 0xff;
            if (call_type == 1) {
                logger.Info($"obtain_brotherhood: Set Airship Destinations");
                set_airship_destinations();
            } else if (call_type == 2) { // Jump
                int jump_index = param >> 8;
                work->current_thread.pc = (byte*)customScriptHandles[jump_index].AddrOfPinnedObject() - 3;
            } else if (call_type == 3) { // Jump if false
                int jump_index = param >> 8;
                int val = atelStack->pop_int();
                if (val == 0) {
                    work->current_thread.pc = (byte*)customScriptHandles[jump_index].AddrOfPinnedObject() - 3;
                }
            } else if (call_type == 4) { // Jump if true
                int jump_index = param >> 8;
                int val = atelStack->pop_int();
                if (val == 1) {
                    work->current_thread.pc = (byte*)customScriptHandles[jump_index].AddrOfPinnedObject() - 3;
                }
            } else if (call_type == 5) { // Offset
                int offset = atelStack->pop_int();
                work->current_thread.pc += offset - 3;
            } else if (call_type == 6) { // Offset if false
                int offset = atelStack->pop_int();
                int val = atelStack->pop_int();
                if (val == 0) {
                    work->current_thread.pc += offset - 3;
                }
            } else if (call_type == 6) { // Offset if true
                int offset = atelStack->pop_int();
                int val = atelStack->pop_int();
                if (val == 1) {
                    work->current_thread.pc += offset - 3;
                }
            } else if (call_type == 7) { // Update region state
                update_region_state(false);
            }
            return;
        }

        _Common_obtainBrotherhood.orig_fptr(work, storage, atelStack);
    }
    private static int h_Common_grantCelestialUpgrade(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int character = atelStack->values_as_int[0];
        int level = atelStack->values_as_int[1];
        logger.Debug($"grant_celestial_upgrade: character={id_to_character[character]}, level={level}");

        return _Common_grantCelestialUpgrade.orig_fptr(work, storage, atelStack);
    }
    private static int h_Common_setPrimerCollected(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int primer = atelStack->values_as_int[0];
        logger.Debug($"set_primer_collected: Al Bhed Primer {primer + 1}");

        return _Common_setPrimerCollected.orig_fptr(work, storage, atelStack);
    }

    private static void update_region_state(bool map_changed) {
        logger.Info($"Update region state");
        if (skip_state_updates) {
            return;
        }

        // Update region state
        if (current_region != RegionEnum.None && region_states.TryGetValue(current_region, out ArchipelagoData.ArchipelagoRegion? current_state)) {
            current_state = region_states[current_region];
            current_state.room_id  = map_changed ? last_room_id     : save_data->current_room_id;
            current_state.entrance = map_changed ? last_entrance_id : save_data->current_spawnpoint;
            current_state.story_progress = save_data->story_progress;
        }
    }

    private static void on_map_change() {
        if (id_to_regions.Contains(save_data->current_room_id)) {

            // New Game
            if (save_data->last_room_id == 0 && save_data->current_room_id == 132) current_region = RegionEnum.DreamZanarkand;

            var regions = id_to_regions[save_data->current_room_id];
            RegionEnum region = regions.Any(r => current_region == r) ? current_region : regions.Last();

            if (save_data->current_room_id == 205 && save_data->story_progress != 2000) region = RegionEnum.Airship;

            if (!region_is_unlocked[region]) {
                // Reroute to current room
                logger.Info($"{region} is locked!");
                //call_warp_to_map(save_data->last_room_id, save_data->last_spawnpoint);
                save_data->current_room_id = save_data->last_room_id;
                save_data->current_spawnpoint = save_data->last_spawnpoint;
            }
            else {
                if (current_region != region) {
                    update_region_state(true);
                    if (current_region != RegionEnum.None) skip_state_updates = false;
                    current_region = region;
                    ArchipelagoRegion current_state = region_states[region];
                    save_data->story_progress = current_state.story_progress;

                    //call_warp_to_map(current_state.room_id, current_state.entrance);
                    save_data->current_room_id = current_state.room_id;
                    save_data->current_spawnpoint = (byte)current_state.entrance;
                    logger.Debug($"transition_to_map: Rerouted to map={current_state.room_id}, entrance={current_state.entrance}");

                    /*
                    // No party members in early Baaj
                    if (current_region == RegionEnum.BaajTemple && current_state.Story_progress < 3000) {
                        party_overridden = true;

                        save_party();
                        
                        for (int i = current_state.Story_progress < 60 ? 1 : 2; i < 7; i++) {
                            call_remove_party_member((PlySaveId)i);
                        }
                        call_add_party_member(PlySaveId.PC_TIDUS);
                        call_put_party_member_in_slot(0, PlySaveId.PC_TIDUS);
                        call_put_party_member_in_slot(1, current_state.Story_progress < 60 ? (PlySaveId)0xff : PlySaveId.PC_RIKKU);
                        call_put_party_member_in_slot(2, (PlySaveId)0xff);
                    } else {
                        party_overridden = false;
                        reset_party();
                    }
                     */
                    reset_party();

                    /*
                    if (save_data->last_room_id == 382) {
                    }
                     */
                }

            }
        }
        else {
            if (save_data->current_room_id == 382) update_region_state(true);
            current_region = RegionEnum.None;
            skip_state_updates = false;
        }
    }

    private static int h_Common_transitionToMap(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int map = atelStack->values_as_int[0];
        int entrance = atelStack->values_as_int[1];
        logger.Debug($"transition_to_map: map={map}, entrance={entrance}");

        //update_region_state(atelStack);

        return _Common_transitionToMap.orig_fptr(work, storage, atelStack);
    }
    private static int h_Common_warpToMap(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int map = atelStack->values_as_int[0];
        int entrance = atelStack->values_as_int[1];
        logger.Debug($"warp_to_map: map={map}, entrance={entrance}");

        // New Game
        //if (save_data->current_room_id == 0 && map == 132) current_region = RegionEnum.DreamZanarkand;

        // Skip most of Dream Zanarkand
        /*
        if (save_data->current_room_id == 376 && map == 371) {
            atelStack->values_as_int[0] = 382;
            map = atelStack->values_as_int[0];
            AtelStack tempStack = new();
            tempStack.push_int(1);
            remove_party_member(param_1, param_2, &tempStack);
            tempStack.push_int(4);
            remove_party_member(param_1, param_2, &tempStack);
        }
         */

        //update_region_state(atelStack);

        // Skip intro
        if (map == 348) {
            logger.Debug("Skip intro");
            atelStack->values_as_int[0] = 23;
        }

        return _Common_warpToMap.orig_fptr(work, storage, atelStack);
    }

    private static void h_SgEvent_showModularMenuInit(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int menu = atelStack->values_as_int[0];
        int unknown1 = menu >> 24 & 0xFF;
        int menuType = menu >> 16 & 0xFF;
        int unknown2 = menu >> 8 & 0xFF;
        int index = menu & 0xFF;

        string menuTypeString = menuType switch {
            0x00 => "Menu",
            0x01 => "BattleRewards.",
            0x02 => "ItemShop",
            0x04 => "WeaponShop",
            0x08 => "EnterName.",
            0x10 => "LoadGame",
            0x20 => "SaveGame",
            0x40 => "SphereMonitor",
            0x80 => "Tutorial",
            int x => $"Unknown({x})",
        };

        if (menu == 0x40800001 || menu == 0x40800002 || menu == 0x40800003 || menu == 0x40800004 || menu == 0x40800005) {
            logger.Debug($"Skipping menu: type={menuTypeString}, index={index} {(unknown1 == 0x40 ? "" : $", Unknown1={unknown1}")} {(unknown2 == 0x00 ? "" : $", Unknown2={unknown2}")}");
            //FhUtil.set_at(0x00efbbf0, 0x40080000);
            //FhUtil.set_at(0x00efbbf4, 0xffffffff);
            atelStack->pop_int();
            FhUtil.set_at(0x01efb4d4, 0); // Don't wait for menu
            return;
        }
        if (menuType == 0x80) {
            logger.Info($"Unknown tutorial?");
        }
        logger.Info($"Opening menu: type={menuTypeString}, index={index} {(unknown1 == 0x40 ? "" : $", Unknown1={unknown1}")} {(unknown2 == 0x00 ? "" : $", Unknown2={unknown2}")}");
        _SgEvent_showModularMenuInit.orig_fptr(work, storage, atelStack);
    }

    /*
    private static int h_Common_playFieldVoiceLineInit(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        var voice_line = atelStack->values_as_int[0];
        logger.Debug($"play_field_voice_line_init: voice_line={voice_line}");

        return _Common_playFieldVoiceLineInit.orig_fptr(work, storage, atelStack);
    }
    private static int h_Common_playFieldVoiceLineExec(AtelBasicWorker* work, AtelStack* atelStack) {
        logger.Debug($"play_field_voice_line_exec: param_1={atelStack->values_as_int[0]}");

        return _Common_playFieldVoiceLineExec.orig_fptr(work, atelStack);
    }
    private static int h_Common_playFieldVoiceLineResultInt(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        var voice_line = atelStack->values_as_int[0];
        logger.Debug($"play_field_voice_line_result_int: param_1={voice_line}");

        return _Common_playFieldVoiceLineResultInt.orig_fptr(work, storage, atelStack);
    }
    private static int h_after_voiceline_init(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        var input = atelStack->values_as_int[0];
        logger.Debug($"after_voiceline_init: input={input}");

        return _FUN_0085fb60.orig_fptr(work, storage, atelStack);
    }
    private static bool h_after_voiceline_exec(int* param_1, int* param_2) {
        logger.Debug($"after_voiceline_exec: param_1={*param_2}");

        return _FUN_0085fdb0.orig_fptr(param_1, param_2);
    }

    public static void play_voice_line(int voice_line) {
        AtelStack stack = new AtelStack();
        stack.push_int(voice_line);
        logger.Debug($"stack_size = {stack.size}, voice_line = {stack.values_as_int[0]}");

        int param_1 = 0;
        int param_2 = 0;
        h_Common_playFieldVoiceLineInit((AtelBasicWorker*)&param_1, &param_2, &stack);
        logger.Debug($"play_voice_line: after play_field_voice_line_init");

        var result = h_Common_playFieldVoiceLineExec((AtelBasicWorker*)&param_1, &stack);
        logger.Debug($"play_voice_line: after exec, result={result}");

        h_Common_playFieldVoiceLineResultInt((AtelBasicWorker*)&param_1, &param_2, &stack);
        logger.Debug($"play_voice_line: after play_field_voice_line_init");
        logger.Debug($"play_field_voice_line_result_int: Stack={stack.size}");
        stack.push_int(5);

        h_after_voiceline_init((AtelBasicWorker*)&param_1, &param_2, &stack);
        logger.Debug($"play_voice_line: after after_voiceline_init");

        h_after_voiceline_exec(&param_1, &param_2);
        logger.Debug($"play_voice_line: after after_voiceline_exec");
    }
     */


    public static int h_Common_addPartyMember(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int character = atelStack->values_as_int[0];

        //if (!party_overridden && !(save_data->atel_is_push_member == 1)) {
        if (!party_overridden) {
            if (character_is_unlocked.TryGetValue(character, out bool is_unlocked) && !is_unlocked) {
                atelStack->pop_int();
                return 1;
            }
        }
        logger.Debug($"add_party_member: character={id_to_character[character]}");

        return _Common_addPartyMember.orig_fptr(work, storage, atelStack);
    }
    public static int h_Common_removePartyMember(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int character = atelStack->values_as_int[0];

        //if (!party_overridden && !(save_data->atel_is_push_member == 1)) {
        if (!party_overridden) {
            if (character_is_unlocked.TryGetValue(character, out bool is_unlocked) && is_unlocked) {
                atelStack->pop_int();
                return 1;
            }
        }
        logger.Debug($"remove_party_member: character={id_to_character[character]}");

        return _Common_removePartyMember.orig_fptr(work, storage, atelStack);
    }
    public static int h_Common_removePartyMemberLongTerm(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int character = atelStack->values_as_int[0];

        //if (!party_overridden && !(save_data->atel_is_push_member == 1)) {
        if (!party_overridden) {
            if (character_is_unlocked.TryGetValue(character, out bool is_unlocked) && is_unlocked) {
                atelStack->pop_int();
                return 1;
            }
        }
        logger.Debug($"remove_party_member_long_term: character={id_to_character[character]}");

        return _Common_removePartyMemberLongTerm.orig_fptr(work, storage, atelStack);
    }
    public static int h_Common_setWeaponInvisible(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int character = (byte)atelStack->values_as_int[0];
        int state = atelStack->values_as_int[1];
        logger.Debug($"character={id_to_character[character]}, state={state}");
        if (state != 0 && character_is_unlocked.TryGetValue(character, out bool is_unlocked) && is_unlocked) {
            atelStack->pop_int();
            atelStack->pop_int();
            return 0;
        }
        return _Common_setWeaponVisibilty.orig_fptr(work, storage, atelStack);
    }
    public static int h_Common_putPartyMemberInSlot(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int slot = atelStack->values_as_int[0];
        int character = (byte)atelStack->values_as_int[1];
        //if (!party_overridden && !(save_data->atel_is_push_member == 1)) {
        if (!party_overridden) {
            if (character == 0xff || (character_is_unlocked.TryGetValue(character, out bool is_unlocked) && !is_unlocked)) {
                // Ignore call
                atelStack->pop_int();
                atelStack->pop_int();
                return (int)get_party_frontline()[slot];
            }
        }
        logger.Debug($"put_party_member_in_slot: slot={slot}, character={(character != 0xff ? id_to_character[character] : "Empty")}");

        return _Common_putPartyMemberInSlot.orig_fptr(work, storage, atelStack);
    }
    public static int h_Common_pushParty(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        logger.Debug($"push_party");

        //if (party_overridden) return _Common_pushParty.orig_fptr(work, storage, atelStack);
        //save_party();
        return 0;
    }
    public static int h_Common_popParty(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        logger.Debug($"pop_party");

        //if (party_overridden) return _Common_popParty.orig_fptr(work, storage, atelStack);
        //reset_party();
        return 1;
    }

    // TODO: Get function pointer instead of hook for most of these
    public static void h_MsGetSavePartyMember(uint* param_1, uint* param_2, uint* param_3) {
        logger.Debug($"get_current_party_slots");
        _MsGetSavePartyMember.orig_fptr(param_1, param_2, param_3);

        logger.Debug($"get_current_party_slots: slot_0={*param_1}, slot_1={*param_2}, slot_2={*param_3}");
    }

    // Pre-battle
    public static void h_MsBattleExe(uint param_1, int field_idx, int group_idx, int formation_idx) {
        var field_ptr = btl->ptr_btl_bin_fields + field_idx * 0xe;
        string field_name = Marshal.PtrToStringAnsi((nint)(field_ptr+6));

        var group_ptr = _MsBtlListGroup(field_idx, group_idx);
        byte group_name = *(byte*)(group_ptr+5 + formation_idx * 2);

        string encounter_name = $"{field_name}_{group_name:00}";
        logger.Debug($"{encounter_name}: param_1={param_1}");
        /*
        if (encounterToPartyDict.TryGetValue(encounter_name, out List<PlySaveId> characters)) {
            if (characters.Count > 0) {
                save_party();
                set_party(characters);
            }
            else {
                reset_party();
            }
        }
         */
        if (encounterToActionDict.TryGetValue(encounter_name, out Action action)) action();
        else reset_party();
        _MsBattleExe.orig_fptr(param_1, field_idx, group_idx, formation_idx);
    }

    // Pre-battle (only battles launched from Atel script?)
    public static int h_MsBattleLabelExe(uint encounter_id, byte param_2, byte screen_transition) {
        var result = _MsBattleLabelExe.orig_fptr(encounter_id, param_2, screen_transition);

        var field_ptr = btl->ptr_btl_bin_fields + btl->field_idx * 0xe;
        string field_name = Marshal.PtrToStringAnsi((nint)(field_ptr+6));

        var group_ptr = _MsBtlListGroup(btl->field_idx, btl->group_idx);
        byte group_name = *(byte*)(group_ptr+5 + btl->formation_idx * 2);

        string encounter_name = $"{field_name}_{group_name:00}";
        logger.Debug($"{encounter_name}: encounter ={encounter_id}, transition={screen_transition}, param_2={param_2}");
        /*
        if (encounterToPartyDict.TryGetValue(encounter_name, out List<PlySaveId> characters)) {
            if (characters.Count > 0) {
                save_party();
                set_party(characters);
            } else {
                reset_party();
            }
        }
         */
        if (encounterToActionDict.TryGetValue(encounter_name, out Action action)) action();
        else reset_party();

        return result;
    }

    // Battle loop?
    public static void h_FUN_00791820() {
        _FUN_00791820.orig_fptr();
        string encounter_name = Marshal.PtrToStringAnsi((nint)btl->field_name);
        if (btl->battle_end_type > 1 && btl->battle_state == 0x21) {
            logger.Info($"Victory: type={btl->battle_end_type}, encounter={encounter_name}");
            //if (save_data->atel_is_push_member == 1) {
            if (party_overridden) {
                reset_party();
                // Battle frontline gets copied after this so have to set here
                for (int i = 0; i < 3; i++) {
                    btl->frontline[i] = save_data->party_order[i];
                }
                for (int i = 0; i < 4; i++) {
                    btl->backline[i] = save_data->party_order[i+3];
                }
                party_overridden = false;
            }
            if (encounterVictoryActions.TryGetValue(encounter_name, out Action action)) {
                action();
            }
            if (encounterToLocationDict.TryGetValue(encounter_name, out int location_id)) {
                sendLocation(location_id);
            }
        }
    }

    private static void* curr_pos_area_ptr = null;
    public static byte h_MsBtlReadSetScene() {
        byte result = _MsBtlReadSetScene.orig_fptr();
        ref BtlAreas original_pos_struct = ref *btl->ptr_pos_def;
        if (original_pos_struct.area_count == 0) return result;
        if (curr_pos_area_ptr != null) {
            NativeMemory.Free(curr_pos_area_ptr);
            curr_pos_area_ptr = null;
            //Marshal.FreeHGlobal((nint)curr_pos_area);
        }
        int total_size = 0;
        int total_info_count = 0;
        for (int i = 0; i < original_pos_struct.area_count; i++) {
            total_size += 0x60;
            total_size += Math.Max(original_pos_struct[i].count_party_pos, (byte)3) * 0x20;
            total_size += Math.Max(original_pos_struct[i].count_aeon_pos, (byte)0xC) * 0x20;
            total_size += original_pos_struct[i].count_enemy_pos * 0x20;
            total_size += original_pos_struct[i].count_some_info * 0x10; // some_info
            total_info_count += original_pos_struct[i].count_some_info;
            for (int j = 0; j < original_pos_struct[i].count_some_info; j++) {
                total_size += original_pos_struct.some_info(i)[j].count_something * 0x10;
            }
        }
        total_size += 0x10; //chunk_end
        curr_pos_area_ptr = NativeMemory.AllocZeroed((uint)total_size);
        //curr_pos_area = (PosArea*)Marshal.AllocHGlobal(total_size);

        BtlAreas* curr_pos_struct_ptr = (BtlAreas*)curr_pos_area_ptr;

        ref BtlAreas curr_pos_struct = ref *curr_pos_struct_ptr;

        // Copy
        int some_info_start = original_pos_struct.area_count * 0x60;
        int curr_info_count = 0;
        int positions_start = some_info_start + total_info_count * 0x10;
        int curr_position_count = 0;
        for (int i = 0; i < original_pos_struct.area_count; i++) {
            curr_pos_struct[i] = original_pos_struct[i];

            curr_pos_struct[i].count_party_pos = Math.Max(curr_pos_struct[i].count_party_pos, (byte)3);
            curr_pos_struct[i].count_aeon_pos = Math.Max(curr_pos_struct[i].count_aeon_pos, (byte)0xc);
            curr_pos_struct[i].count_enemy_pos = curr_pos_struct[i].count_enemy_pos;

            curr_pos_struct[i].offset_party_pos = (uint)(positions_start + curr_position_count * 0x10);
            for (int j = 0; j < curr_pos_struct[i].count_party_pos; j++) {

                if (original_pos_struct[i].count_party_pos <= 2 || (original_pos_struct.party_pos(i)[0] - original_pos_struct.party_pos(i)[original_pos_struct[i].count_party_pos - 1]).Length() < 10) {
                    // TODO: Calculate positions
                    if (original_pos_struct[i].count_party_pos == 1 || original_pos_struct[i].count_party_pos >= 3) {
                        if (j > 2) {
                            //logger.Warning($"Unexpected position: {param_1}, {btl_pos_a}, {btl_pos_b}, {btl_pos_c}");
                        }
                        curr_pos_struct.party_pos(i)[j] = original_pos_struct.party_pos(i)[0];
                        if (j != 1) {
                            Vector4 enemy_pos = original_pos_struct.enemy_pos(i)[0];
                            Vector4 enemy_to_party = curr_pos_struct.party_pos(i)[j] - enemy_pos;
                            Vector3 enemy_to_party_3d = new(enemy_to_party.X, enemy_to_party.Y, enemy_to_party.Z);
                            Vector3 rotation_axis = new(0, 1, 0);
                            double angle = j == 0 ? -(Math.PI / 2) : (Math.PI / 2);

                            Vector3 x = Vector3.Cross(rotation_axis, enemy_to_party_3d);
                            Vector3 y = Vector3.Cross(rotation_axis, x);
                            Vector3 rotated = enemy_to_party_3d + (float)Math.Sin(angle) * x + (float)(1 - Math.Cos(angle)) * y;
                            curr_pos_struct.party_pos(i)[j] += new Vector4(Vector3.Normalize(rotated) * 30, curr_pos_struct.party_pos(i)[j].W);
                        }
                    }
                    else {
                        //if (j > 2) return -1;
                        Vector4 ally_1 = original_pos_struct.party_pos(i)[0];
                        Vector4 line = curr_pos_struct.party_pos(i)[1] - ally_1;
                        if (line.Length() > 10) {
                            if (j == 2) {
                                curr_pos_struct.party_pos(i)[j] = original_pos_struct.party_pos(i)[1];
                                curr_pos_struct.party_pos(i)[j] += line;
                            }
                            else {
                                curr_pos_struct.party_pos(i)[j] = original_pos_struct.party_pos(i)[j];
                            }
                        }
                        else if (j != 1) {
                            Vector4 enemy_pos = original_pos_struct.enemy_pos(i)[0];
                            Vector4 enemy_to_party = curr_pos_struct.party_pos(i)[j] - enemy_pos;
                            Vector3 enemy_to_party_3d = new(enemy_to_party.X, enemy_to_party.Y, enemy_to_party.Z);
                            Vector3 rotation_axis = new(0, 1, 0);
                            double angle = j == 0 ? -(Math.PI / 2) : (Math.PI / 2);

                            Vector3 x = Vector3.Cross(rotation_axis, enemy_to_party_3d);
                            Vector3 y = Vector3.Cross(rotation_axis, x);
                            Vector3 rotated = enemy_to_party_3d + (float)Math.Sin(angle) * x + (float)(1 - Math.Cos(angle)) * y;
                            curr_pos_struct.party_pos(i)[j] += new Vector4(Vector3.Normalize(rotated) * 30, curr_pos_struct.party_pos(i)[j].W);
                        }
                    }
                }
                else {
                    // Copy
                    curr_pos_struct.party_pos(i)[j] = original_pos_struct.party_pos(i)[j];
                }
                curr_position_count++;
            }
            curr_pos_struct[i].offset_party_run_pos = (uint)(positions_start + curr_position_count * 0x10);
            for (int j = 0; j < curr_pos_struct[i].count_party_pos; j++) {
                if (original_pos_struct[i].count_party_pos <= j) {
                    curr_pos_struct.party_run_pos(i)[j] = original_pos_struct.party_run_pos(i)[0];
                }
                else {
                    curr_pos_struct.party_run_pos(i)[j] = original_pos_struct.party_run_pos(i)[j];
                }
                if (curr_pos_struct.party_run_pos(i)[j] == Vector4.Zero) {
                    curr_pos_struct.party_run_pos(i)[j] = original_pos_struct.party_pos(i)[0];
                    Vector4 enemy_pos = original_pos_struct.enemy_pos(i)[0];
                    Vector4 enemy_to_party = curr_pos_struct.party_run_pos(i)[j] - enemy_pos;
                    curr_pos_struct.party_run_pos(i)[j] += Vector4.Normalize(enemy_to_party) * 0x30;
                }
                curr_position_count++;
            }

            curr_pos_struct[i].offset_aeon_pos = (uint)(positions_start + curr_position_count * 0x10);
            for (int j = 0; j < curr_pos_struct[i].count_aeon_pos; j++) {
                if (j == 7) {
                    curr_pos_struct.aeon_pos(i)[j] = curr_pos_struct.party_pos(i)[0];
                }
                else if (j == 9) {
                    curr_pos_struct.aeon_pos(i)[j] = curr_pos_struct.party_pos(i)[2];
                }
                else {
                    curr_pos_struct.aeon_pos(i)[j] = curr_pos_struct.party_pos(i)[1];
                }
                curr_position_count++;
            }
            curr_pos_struct[i].offset_aeon_run_pos = (uint)(positions_start + curr_position_count * 0x10);
            for (int j = 0; j < curr_pos_struct[i].count_aeon_pos; j++) {
                curr_pos_struct.aeon_run_pos(i)[j] = curr_pos_struct.party_run_pos(i)[0];
                curr_position_count++;
            }

            curr_pos_struct[i].offset_enemy_pos = (uint)(positions_start + curr_position_count * 0x10);
            for (int j = 0; j < curr_pos_struct[i].count_enemy_pos; j++) {
                curr_pos_struct.enemy_pos(i)[j] = original_pos_struct.enemy_pos(i)[j];
                curr_position_count++;
            }
            curr_pos_struct[i].offset_enemy_run_pos = (uint)(positions_start + curr_position_count * 0x10);
            for (int j = 0; j < curr_pos_struct[i].count_enemy_pos; j++) {
                curr_pos_struct.enemy_run_pos(i)[j] = original_pos_struct.enemy_run_pos(i)[j];
                curr_position_count++;
            }

            curr_pos_struct[i].offset_some_info = (uint)(some_info_start + curr_info_count * 0x10);
            for (int j = 0; j < curr_pos_struct[i].count_some_info; j++) {
                curr_pos_struct.some_info(i)[j] = original_pos_struct.some_info(i)[j];
                curr_info_count++;
            }
        }

        for (int i = 0; i < original_pos_struct.area_count; i++) {
            for (int j = 0; j < curr_pos_struct[i].count_some_info; j++) {
                curr_pos_struct.some_info(i)[j].offset_something = (uint)(positions_start + curr_position_count * 0x10);
                for (int k = 0; k < curr_pos_struct.some_info(i)[j].count_something; k++) {
                    curr_pos_struct.something(i, j)[k] = original_pos_struct.something(i, j)[k];
                    curr_position_count++;
                }
            }
        }
        for (int i = 0; i < original_pos_struct.area_count; i++) {
            curr_pos_struct[i].offset_chunk_end = (uint)(positions_start + curr_position_count * 0x10);
        }
        *curr_pos_struct.chunk_end = *original_pos_struct.chunk_end;


        btl->ptr_pos_def = curr_pos_struct_ptr;

        return result;
    }

    public static int h_MsBtlGetPos(int param_1, Chr* chr, int btl_pos_a, int btl_pos_b, int btl_pos_c, Vector4* out_pos) {
        // btl_pos_a: position group/set/area
        // btl_pos_b = 3: party_pos[btl_pos_c]
        // btl_pos_b = 4: aeon_pos[btl_pos_c]
        // btl_pos_b = 5: enemy_pos[btl_pos_c]
        // btl_pos_b = 6: party_run_pos[btl_pos_c]?
        // btl_pos_b = 7: aeon_run_pos[btl_pos_c]?
        // btl_pos_b = 8: enemy_run_pos[btl_pos_c]?
        // btl_pos_b = 9: b?
        // btl_pos_b = 10: c?
        return _MsBtlGetPos.orig_fptr(param_1, chr, btl_pos_a, btl_pos_b, btl_pos_c, out_pos);
    }

    private static uint h_give_item(uint param_1, int param_2) {
        logger.Debug($"give_item: {param_2} x {param_1}");

        return _FUN_007905a0.orig_fptr(param_1, param_2);
    }
    private static void h_give_key_item(uint param_1) {
        logger.Debug($"give_key_item: {param_1}");

        _FUN_0088e700.orig_fptr(param_1);
    }
    private static void h_take_gil(int param_1) {
        logger.Debug($"take_gil: {param_1}");

        _FUN_00785a60.orig_fptr(param_1);
    }
    private static int h_give_weapon(Equipment* param_1) {
        logger.Debug($"give_weapon: {(int)param_1}");

        return _FUN_007ab930.orig_fptr(param_1);
    }
    private static byte* h_read_from_bin(int param_1, short* param_2, int param_3) {
        logger.Debug($"get_from_bin: {param_1}, {(int)param_2}, {param_3}");
        logger.Debug($"bin_pointers: {(uint)param_2}, {(uint)(*takara_pointer)}, {(uint)(*buki_get_pointer)}");
        if (param_2 == (short*)*takara_pointer) {
            logger.Debug($"get_from_bin: takara.bin");
        } else if (param_2 == (short*)*buki_get_pointer) {
            logger.Debug($"get_from_bin: buki_get.bin");
        }
        var result = _FUN_007ab890.orig_fptr(param_1, param_2, param_3);
        logger.Debug($"get_from_bin: result = {((uint)result).ToString("X")}");
        return result;
    }
    private static ushort h_get_weapon_name(Equipment* param_1) {
        logger.Debug($"get_weapon_name: {(int)param_1}");

        return _FUN_007a0d10.orig_fptr(param_1);
    }
    private static void h_get_weapon_model(ushort name_id, byte owner, int unknown, ushort* model_id_pointer) {
        logger.Debug($"get_weapon_model: {name_id}, {owner}, {unknown}, {*model_id_pointer}, ");

        _FUN_007a0c70.orig_fptr(name_id, owner, unknown, model_id_pointer);
    }
    private static void h_obtain_treasure_cleanup(BtlRewardData* param_1, int param_2) {
        logger.Debug($"obtain_treasure_cleanup");

        _FUN_007993f0.orig_fptr(param_1, param_2);
    }


    public static void obtain_item(uint item_id, int amount) {
        var item_type = (item_id & 0xF000) >> 12;
        switch (item_type) {
            case 0xA:
                // Key Item
                h_give_key_item(item_id);
                // TODO: Handle Jecht Spheres and Al Bhed Primers
                break;
            case 0x2:
                // Item
                h_give_item(item_id, amount);
                break;
            case 0x5:
                // Equipment
                item_id &= 0xff;
                UnownedEquipment* weapon_data = (UnownedEquipment*)h_read_from_bin((int)item_id, (short*)(*buki_get_pointer), 0);
                //var data = get_from_bin((int)item_id, (short*)0x12000C00, 0);
                logger.Debug($"obtain_item: Weapon {(int)weapon_data}");
                BtlRewardData rewardData = new BtlRewardData();
                rewardData.gear_count = 1;
                Equipment new_weapon = rewardData.gear[0];
                new_weapon.exists = true;
                new_weapon.flags = weapon_data->flags;
                new_weapon.owner = weapon_data->owner;
                new_weapon.type = weapon_data->type;
                new_weapon.__0x6 = 0xff;
                new_weapon.dmg_formula = weapon_data->dmg_formula;
                new_weapon.power = weapon_data->power;
                new_weapon.crit_bonus = weapon_data->crit_bonus;
                int count = 0;
                for (int i = 0; i < 4; i++) {
                    if (weapon_data->abilities[i] == 0) {
                        new_weapon.abilities[i] = 0xff;
                    }
                    else {
                        new_weapon.abilities[i] = weapon_data->abilities[i];
                        count++;
                    }
                }
                new_weapon.slot_count = (byte)Math.Max(weapon_data->slot_count, count);
                new_weapon.name_id = h_get_weapon_name(&new_weapon);
                h_get_weapon_model(new_weapon.name_id, new_weapon.owner, 0, &new_weapon.model_id);
                var result = h_give_weapon(&new_weapon);
                if (result != 0) {
                    h_obtain_treasure_cleanup(&rewardData, 7);
                }
                break;
            case 0x1:
                // Gil
                h_take_gil(-1000 * amount);
                break;
            case 0xE:
                // Region Unlock
                item_id &= 0xff;
                logger.Debug($"Region: {(RegionEnum)item_id}");
                region_is_unlocked[(RegionEnum)item_id] = true;
                break;
            case 0xF:
                // Party Member
                int char_id = (int)(item_id & 0xFF);
                logger.Debug($"Character: {id_to_character[char_id]}");
                character_is_unlocked[char_id] = true;
                if (char_id == 0xF) {
                    // Magus sisters
                    character_is_unlocked[PlySaveId.PC_MAGUS2] = true;
                    character_is_unlocked[PlySaveId.PC_MAGUS3] = true;
                }
                break;
        }
        /*
        if (item_type == 0xA000) {
            give_key_item(item_id);
        }
        else if (item_type == 0x2000) {
            give_item(item_id, amount);
        }
        else if (item_type == 0x5000) {
            item_id &= 0xff;
            UnownedEquipment* weapon_data = (UnownedEquipment*)read_from_bin((int)item_id, (short*)(*buki_get_pointer), 0);
            //var data = get_from_bin((int)item_id, (short*)0x12000C00, 0);
            logger.Debug($"obtain_item: Weapon {(int)weapon_data}");
            BtlRewardData rewardData = new BtlRewardData();
            rewardData.gear_count = 1;
            Equipment new_weapon = rewardData.gear[0];
            new_weapon.exists = true;
            new_weapon.flags = weapon_data->flags;
            new_weapon.owner = weapon_data->owner;
            new_weapon.type = weapon_data->type;
            new_weapon.__0x6 = 0xff;
            new_weapon.dmg_formula = weapon_data->dmg_formula;
            new_weapon.power = weapon_data->power;
            new_weapon.crit_bonus = weapon_data->crit_bonus;
            int count = 0;
            for (int i = 0; i < 4; i++) {
                if (weapon_data->abilities[i] == 0) {
                    new_weapon.abilities[i] = 0xff;
                } else {
                    new_weapon.abilities[i] = weapon_data->abilities[i];
                    count++;
                }
            }
            new_weapon.slot_count = (byte)Math.Max(weapon_data->slot_count, count);
            new_weapon.name_id = get_weapon_name(&new_weapon);
            get_weapon_model(new_weapon.name_id, new_weapon.owner, 0, &new_weapon.model_id);
            var result = give_weapon(&new_weapon);
            if (result != 0) {
                obtain_treasure_cleanup(&rewardData, 7);
            }
        } else if (item_type == 0x1000) {
            // Gil
            take_gil(-1000 * amount);
        } else if (item_type == 0xE000) {
            // Region unlock
            item_id &= 0xff;
            logger.Debug($"Region: {(RegionEnum)item_id}");
            region_is_unlocked[(RegionEnum)item_id] = true;
        } else if (item_type == 0xF000) {
            // Party member
            var char_id = item_id & 0xFF;
            logger.Debug($"Character: {(PlySaveId)char_id}");
            character_is_unlocked[(PlySaveId)char_id] = true;
            if (char_id == 0xF) {
                // Magus sisters
                character_is_unlocked[PlySaveId.PC_MAGUS2] = true;
                character_is_unlocked[PlySaveId.PC_MAGUS3] = true;
            }
        }
         */
    }

    public static void call_obtain_brotherhood() {
        AtelStack stack = new AtelStack();

        int param_1 = 0;
        int param_2 = 0;

        h_Common_obtainBrotherhood((AtelBasicWorker*)&param_1, &param_2, &stack);
    }

    public static void receive_treasure(int id) {
        AtelStack stack = new AtelStack();
        stack.push_int(id);

        int param_1 = 0;
        int param_2 = 0;

        _Common_obtainTreasureSilentlyInit.orig_fptr((AtelBasicWorker*)&param_1, &param_2, &stack);
    }

    public static void set_airship_destinations() {
        logger.Debug("set_airship_destinations");
        AtelBasicWorker* worker = Atel.controllers[0].worker(0);
        var var_table = worker->table_event_data;
        uint* airshipDestinationCount = &var_table[11];
        uint* airshipDestinationLength = &var_table[12];
        uint* airshipDestinations = &var_table[13];

        for (int i = 0; i < 31; i++) {
            airshipDestinations[i] = 0;
        }
        uint curr = 0;
        foreach (var region in region_is_unlocked) {
            if (region.Value) {
                var state = region_states[region.Key];
                if (state.airship_destination_index != 99) {
                    airshipDestinations[curr] = state.airship_destination_index;
                    curr++;
                }
            }
        }
        *airshipDestinationCount = curr-1;
        *airshipDestinationLength = curr-1;
        /*
        airshipDestinations[0] = 2;
        airshipDestinations[1] = 6;
        *airshipDestinationCount = 1;
        *airshipDestinationLength = 1;
         */
    }


    // Sphere Grid Experiment

    public static void h_eiAbmParaGet() {
        // Replace normal calculation with custom Archipelago-based calculation (if option enabled)
        logger.Debug("Calculating stats");
        _eiAbmParaGet.orig_fptr(); // security_cookie doesn't matter?
    }

    private static void h_MsSetSaveParam(uint chr_id) {
        logger.Debug("Calculating base stats and equipment");
        _MsSetSaveParam.orig_fptr(chr_id);

        return;
        PlySave ply = save_data->ply_arr[(int)chr_id];
        Equipment*[] equips =
        [
            (Equipment*)_MsGetSaveWeapon(ply.wpn_inv_idx, 0x0),
            (Equipment*)_MsGetSaveWeapon(ply.arm_inv_idx, 0x0),
        ];
        int strength_mult = 0;
        int defense_mult = 0;
        int magic_mult = 0;
        int magic_defense_mult = 0;
        int agility_mult = 0;
        int luck_mult = 0;
        int evasion_mult = 0;
        int accuracy_mult = 0;
        int hp_mult = 0;
        int mp_mult = 0;
        int strength_bonus_mult = 0;
        int defense_bonus_mult = 0;
        int magic_bonus_mult = 0;
        int magic_defense_bonus_mult = 0;

        foreach (Equipment* equip in equips) {
            for (int i = 0; i < 4; i++) {
                if (equip->abilities[i] == 0 || equip->abilities[i] == 0xFF) continue;
                AutoAbility* a_ability = (AutoAbility*)_MsGetExcelData(equip->abilities[i] & 0xFFF, btl->ptr_a_ability_bin, (int*)0x0);
                strength_mult            += a_ability->stat_inc_flags.strength()            ? a_ability->stat_inc_amount : 0;
                defense_mult             += a_ability->stat_inc_flags.defense()             ? a_ability->stat_inc_amount : 0;
                magic_mult               += a_ability->stat_inc_flags.magic()               ? a_ability->stat_inc_amount : 0;
                magic_defense_mult       += a_ability->stat_inc_flags.magic_defense()       ? a_ability->stat_inc_amount : 0;
                agility_mult             += a_ability->stat_inc_flags.agility()             ? a_ability->stat_inc_amount : 0;
                luck_mult                += a_ability->stat_inc_flags.luck()                ? a_ability->stat_inc_amount : 0;
                evasion_mult             += a_ability->stat_inc_flags.evasion()             ? a_ability->stat_inc_amount : 0;
                accuracy_mult            += a_ability->stat_inc_flags.accuracy()            ? a_ability->stat_inc_amount : 0;
                hp_mult                  += a_ability->stat_inc_flags.hp()                  ? a_ability->stat_inc_amount : 0;
                mp_mult                  += a_ability->stat_inc_flags.mp()                  ? a_ability->stat_inc_amount : 0;
                strength_bonus_mult      += a_ability->stat_inc_flags.strength_bonus()      ? a_ability->stat_inc_amount : 0;
                defense_bonus_mult       += a_ability->stat_inc_flags.defense_bonus()       ? a_ability->stat_inc_amount : 0;
                magic_bonus_mult         += a_ability->stat_inc_flags.magic_bonus()         ? a_ability->stat_inc_amount : 0;
                magic_defense_bonus_mult += a_ability->stat_inc_flags.magic_defense_bonus() ? a_ability->stat_inc_amount : 0;
            }
        }

        ply.strength =      (byte)Math.Clamp(ply.strength      * strength_mult      / 100, 0, 255);
        ply.defense =       (byte)Math.Clamp(ply.defense       * defense_mult       / 100, 0, 255);
        ply.magic =         (byte)Math.Clamp(ply.magic         * magic_mult         / 100, 0, 255);
        ply.magic_defense = (byte)Math.Clamp(ply.magic_defense * magic_defense_mult / 100, 0, 255);
        ply.agility =       (byte)Math.Clamp(ply.agility       * agility_mult       / 100, 0, 255);
        ply.luck =          (byte)Math.Clamp(ply.luck          * luck_mult          / 100, 0, 255);
        ply.evasion =       (byte)Math.Clamp(ply.evasion       * evasion_mult       / 100, 0, 255);
        ply.accuracy =      (byte)Math.Clamp(ply.accuracy      * accuracy_mult      / 100, 0, 255);
        ply.hp =            (uint)Math.Clamp(ply.hp            * hp_mult            / 100, 0, ply.auto_ability_effects.has_break_hp_limit ? 99999 : 9999);
        ply.mp =            (uint)Math.Clamp(ply.mp            * mp_mult            / 100, 0, ply.auto_ability_effects.has_break_mp_limit ?  9999 :  999);
    }

    public static void h_FUN_00a48910(uint chr_id, int node_idx) {
        // TODO: Send Archipelago location when node is unlocked (if option enabled)
        logger.Debug($"Unlock node {node_idx} for {id_to_character[chr_id]}");
        _FUN_00a48910.orig_fptr(chr_id, node_idx);
    }

    private static uint h_MsApUp(int chr_id, Chr* chr, int base_ap_add, uint param_4) {
        logger.Debug($"AP gain: character={id_to_character[chr_id]}, ap={base_ap_add}");
        int new_ap = (int)Math.Min(Math.BigMul(base_ap_add, ap_multiplier), 999_999_999);
        return _MsApUp.orig_fptr(chr_id, chr, new_ap, param_4);
    }



    // Overdrive Experiment

    private static uint h_MsCheckLeftWindow(uint chr_id) {
        bool bVar1;
        Chr* chr;
        PCommand* command;
        uint com_id;
        uint uVar2;
        int iVar3;
        int _fullOverdrive = FhUtil.get_at<int>(0xD333E8);
        byte* btl_ply_win = (byte*)FhUtil.get_at<uint>(0x1F10CD8);


        chr = _MsGetChr(chr_id);
        if (_fullOverdrive != 0) {
            chr->limit_charge = chr->limit_charge_max;
        }

        uVar2 = 0;
        iVar3 = (int)chr_id * 0x478 + 0x28;
        for (int i = 0; i < 8; i++) {
            com_id = *(ushort*)(btl_ply_win + iVar3);
            if (com_id != 0xff) {
                command = _MsGetComData(com_id, (int*)0x0);
                bVar1 = _MsGetCommandUse(chr_id, com_id) != 0;
                if (!bVar1) {
                    if (command->command.limit_cost == 0) {
                        uVar2 = uVar2 | 1;
                    }
                    else {
                        uVar2 = uVar2 | 2;
                    }
                }
            }
            iVar3 = iVar3 + 2;
        }
        return uVar2;
    }

    private static uint h_MsCheckUseCommand(uint chr_id, PCommand* command, int param_3) {
        Chr* chr;
        uint uVar1;
        uint uVar2;

        if (command == (Command*)0x0) {
            return 0xffffffff;
        }
        chr = _MsGetChr(chr_id);
        if ( (command->command.limit_cost == 0 || ((command->command.limit_cost <= chr->limit_charge && (*(ushort*)((int)chr + 0x616) & 0x400) == 0 ) || btl->debug.always_available_overdrive)
             ) && ((command->command.flags_misc & 0x20000) == 0 || chr->status_suffer_turns_left.silence == 0)) {
            uVar1 = _MsGetCommandMP(chr_id, (uint)command);
            uVar2 = _MsGetRamChrMonster(chr_id);
            if (uVar2 == 1 || btl->debug.no_mp_cost) {
                uVar1 = 0;
                param_3 = 0;
            }
            if ((int)(param_3 + uVar1) <= chr->mp) {
                return uVar1;
            }
        }
        return 0xffffffff;
    }

    private static float _graphicUiRemapX2(float x) {
        return (x * 512.0f) / 1920.0f;
    }
    private static float _graphicUiRemapY2(float y) {
        return (y * 416.0f) / 1080.0f;
    }
    private static void h_TOBtlDrawStatusLimitGauge(int current_parts, float param_2, float param_3, float param_4, float param_5) {
        _TOBtlDrawStatusLimitGauge.orig_fptr(current_parts, param_2, param_3, param_4, param_5);

        if (cur_btl_window == null || cur_btl_window->command_list_len == 0) return;
        int curr_draw_char = ((int)param_3 - 335) / 20; // 335.1111, 355.14075, 375.17038
        if (curr_draw_char < 0 || curr_draw_char > 3) return; // Not a player
        if (btl->frontline[curr_draw_char] != cur_btl_window->cur_chr_id && (cur_btl_window->cur_chr_id < 8 || cur_btl_window->cur_chr_id > 0x11)) return; // Not current character and not Aeon

        PCommand* hovered_command = _MsGetComData(cur_btl_window->command_list[cur_btl_window->sel_idx], (int*)0x0);
        if (hovered_command->command.limit_cost > 0) {
            short gauge_parts = FhUtil.get_at<short>(0x21d0aa4);
            Chr* chr = _MsGetChr(cur_btl_window->cur_chr_id);
            float parts_ratio = (float)gauge_parts / chr->limit_charge_max;

            float gauge_start = _graphicUiRemapX2(1f) + param_2;
            float y = _graphicUiRemapY2(1f) + param_3;
            float h = param_5 - _graphicUiRemapY2(2f);
            if (chr->limit_charge >= hovered_command->command.limit_cost) {
                var variance = ((Math.Sin(cur_btl_window->seconds_spent_in_menu * 4) + 1) / 2);
                byte alpha = (byte)(0x80 * variance);
                int rgba = 0xff | (0xff << 8) | (0xff << 16) | (alpha << 24); // RGBA

                float used_start = gauge_start + ((param_4 - _graphicUiRemapX2(2f)) * (chr->limit_charge - hovered_command->command.limit_cost) * parts_ratio) / gauge_parts;
                float used_end = gauge_start + ((param_4 - _graphicUiRemapX2(2f)) * current_parts) / gauge_parts;
                float used_width = used_end - used_start;
                _TODrawCrossBoxXYWHC2(used_start, y, used_width, h, (uint)rgba, (uint)rgba);
            }
            else {
                int rgba = 128 | (16 << 8) | (16 << 16) | (0x80 << 24); // RGBA

                float overflow_start = gauge_start + ((param_4 - _graphicUiRemapX2(2f)) * (current_parts+1)) / gauge_parts;
                float overflow_end = gauge_start + ((param_4 - _graphicUiRemapX2(2f)) * hovered_command->command.limit_cost * parts_ratio) / gauge_parts;
                float overflow_width = overflow_end - overflow_start;
                _TODrawCrossBoxXYWHC2(overflow_start, y, overflow_width, h, (uint)rgba, (uint)rgba);
            }
        }
    }


}

