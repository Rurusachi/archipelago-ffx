using Fahrenheit.Core;
using Fahrenheit.Core.Atel;
using Fahrenheit.Core.FFX;
using Fahrenheit.Core.FFX.Battle;
using Fahrenheit.Core.FFX.Ids;
using Fahrenheit.Modules.ArchipelagoFFX.Client;
using Fahrenheit.Modules.ArchipelagoFFX.GUI;
using Hexa.NET.DirectXTex;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
//using Fahrenheit.Core.ImGuiNET;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TerraFX.Interop.Windows;
using static Fahrenheit.Core.FFX.Globals;
using static Fahrenheit.Modules.ArchipelagoFFX.ArchipelagoData;
using static Fahrenheit.Modules.ArchipelagoFFX.Client.FFXArchipelagoClient;
using static Fahrenheit.Modules.ArchipelagoFFX.delegates;

//[assembly: DisableRuntimeMarshalling]
namespace Fahrenheit.Modules.ArchipelagoFFX;
public unsafe partial class ArchipelagoFFXModule {

    public static int* takara_pointer => FhUtil.ptr_at<int>(0xD35FEC);
    public static int* buki_get_pointer => FhUtil.ptr_at<int>(0xD35FF4);


    // AtelEventSetUp
    private static FhMethodHandle<AtelEventSetUp> _AtelEventSetUp;

    public static char* get_event_name(uint event_id) => FhUtil.get_fptr<AtelGetEventName>(0x4796e0)(event_id);
    public static int atel_stack_pop(int* param_1, AtelStack* atelStack) => FhUtil.get_fptr<AtelStackPop>(0x0046de90)(param_1, atelStack);

    private static FhMethodHandle<Common_obtainTreasureInit> _Common_obtainTreasureInit;
    private static FhMethodHandle<Common_obtainTreasureSilentlyInit> _Common_obtainTreasureSilentlyInit;
    private static FhMethodHandle<CT_RetInt_01B6> _Common_isBrotherhoodUnpoweredRetInt;
    private static FhMethodHandle<CT_RetInt_01B7> _Common_upgradeBrotherhoodRetInt;
    private static FhMethodHandle<Common_obtainBrotherhoodRetInt> _Common_obtainBrotherhoodRetInt;
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
    private static Common_01D1Init _Common_01D1Init;
    // Common.01D1Exec
    private static Common_01D1Exec _Common_01D1Exec;

    private static FhMethodHandle<Common_addPartyMember> _Common_addPartyMember;
    private static FhMethodHandle<Common_removePartyMember> _Common_removePartyMember;
    private static FhMethodHandle<Common_removePartyMemberLongTerm> _Common_removePartyMemberLongTerm;
    private static FhMethodHandle<Common_setWeaponInvisible> _Common_setWeaponVisibilty;
    private static FhMethodHandle<Common_putPartyMemberInSlot> _Common_putPartyMemberInSlot;

    private static FhMethodHandle<Common_pushParty> _Common_pushParty;
    private static FhMethodHandle<Common_popParty> _Common_popParty;

    // getCurrentPartySlots
    private static FhMethodHandle<MsGetSavePartyMember> _MsGetSavePartyMember;

    private static MsBtlListGroup _MsBtlListGroup;
    private static FhMethodHandle<MsBattleExe> _MsBattleExe;
    public static MsBattleLabelExe _MsBattleLabelExe;
    private static FhMethodHandle<FUN_00791820> _FUN_00791820;

    private static FhMethodHandle<MsBtlGetPos> _MsBtlGetPos;

    private static FhMethodHandle<MsBtlReadSetScene> _MsBtlReadSetScene;

    // giveItem
    private static FhMethodHandle<FUN_007905a0> _FUN_007905a0;
    // giveKeyItem
    // takeGil


    // readFromBin
    private static FhMethodHandle<FUN_007ab890> _FUN_007ab890;

    // getWeaponName
    private static FhMethodHandle<FUN_007a0d10> _FUN_007a0d10;
    // getWeaponModel
    private static FhMethodHandle<FUN_007a0c70> _FUN_007a0c70;
    // obtainTreasureCleanup
    private static FhMethodHandle<FUN_007993f0> _FUN_007993f0;

    private static FhCall.MsGetChr _MsGetChr;
    private static FhCall.MsGetComData _MsGetComData;
    private static FhCall.MsGetCommandUse _MsGetCommandUse;
    private static FhCall.MsGetCommandMP _MsGetCommandMP;
    private static FhCall.MsGetRamChrMonster _MsGetRamChrMonster;
    private static FhCall.TODrawCrossBoxXYWHC2 _TODrawCrossBoxXYWHC2;


    // Sphere Grid Experiment
    private static FhMethodHandle<eiAbmParaGet> _eiAbmParaGet;
    private static FhMethodHandle<FUN_00a48910> _FUN_00a48910;

    private static FhMethodHandle<MsApUp> _MsApUp;


    private static FhMethodHandle<openFile> _openFile;
    private static readFile _readFile;
    private static FhMethodHandle<FUN_0070aec0> _FUN_0070aec0;

    private static FhCall.ChN_ReadSystemMGRP _ChN_ReadSystemMGRP;
    private static Common_loadModel _Common_loadModel;
    private static Common_0043 _Common_0043;
    private static Common_linkFieldToBattleActor _Common_linkFieldToBattleActor;
    private static FhCall.SndSepPlay _SndSepPlay;


    private static FhMethodHandle<MsSetSaveParam> _MsSetSaveParam;
    public static MsGetExcelData _MsGetExcelData;

    private static FhMethodHandle<Map_800F> _Map_800F;

    private static FhMethodHandle<FUN_0086bec0> _FUN_0086bec0;
    private static FhMethodHandle<FUN_0086bea0> _FUN_0086bea0;

    private static FhMethodHandle<graphicInitFMVPlayer> _graphicInitFMVPlayer;

    private static FhMethodHandle<FUN_00656c90> _FUN_00656c90;
    private static FUN_0065ee30 _FUN_0065ee30;
    private static ClusterManager_loadPCluster _ClusterManager_loadPCluster;
    private static Phyre_PFramework_PApplication_FixupClusters _Phyre_PFramework_PApplication_FixupClusters;
    private static ClusterManager_releasePCluster _ClusterManager_releasePCluster;


    // ObtainTreasure related
    private static FhMethodHandle<TkMsImportantSet> _TkMsImportantSet;
    private static MsPayGIL _MsPayGIL;
    private static SndSepPlaySimple _SndSepPlaySimple;
    private static MsGetSaveWeapon _MsGetSaveWeapon;
    private static FUN_007ab930 _FUN_007ab930; // giveWeapon?
    private static AtelGetCurCtrlWork _AtelGetCurCtrlWork;
    private static MsFieldItemGet _MsFieldItemGet;
    private static TkMsGetRomItem _TkMsGetRomItem;
    private static MsSaveItemUse _MsSaveItemUse;
    private static MsImportantName _MsImportantName;
    private static CT_RetInt_0065 _CT_RetInt_0065;
    private static CT_RetInt_006A _CT_RetInt_006A;
    private static FhCall.MsBtlGetInit _MsBtlGetInit;
    private static AtelGetMesWinWork _AtelGetMesWinWork;
    private static FUN_008b8910 _FUN_008b8910; // setMessageWindowVariableType? (0: text*, 1: int)
    private static FUN_008bda20 _FUN_008bda20; // getMenuText
    private static FUN_008b8930 _FUN_008b8930; // setMessageWindowVariable
    private static FUN_0086a0c0 _FUN_0086a0c0;


    // Voice related
    private static FhMethodHandle<FmodVoice_dataChange> _FmodVoice_dataChange;
    private static FMOD_EventSystem_load _FMOD_EventSystem_load;

    private static FhMethodHandle<FfxFmod_soundInit_setLang> _FfxFmod_soundInit_setLang;
    private static FhMethodHandle<LocalizationManager_Initialize> _LocalizationManager_Initialize;
    public static LocalizationManager_GetInstance _LocalizationManager_GetInstance;
    public static FfxFmod_soundInit _FfxFmod_soundInit;
    public static FmodVoice_initList _FmodVoice_initList;


    // Custom namespace
    private static FhMethodHandle<AtelInitTotal> _AtelInitTotal;
    public static void AtelSetUpCallFunc(int id, nint nameSpacePtr) => FhUtil.get_fptr<AtelSetUpCallFunc>(__addr_AtelSetUpCallFunc)(id, nameSpacePtr);


    public void init_hooks() {
        const string game = "FFX.exe";

        _AtelEventSetUp = new FhMethodHandle<AtelEventSetUp>(this, game, __addr_AtelEventSetUp, h_AtelEventSetUp);

        _Common_obtainTreasureInit = new FhMethodHandle<Common_obtainTreasureInit>(this, game, 0x0045a740, h_Common_obtainTreasureInit);

        _Common_obtainTreasureSilentlyInit = new FhMethodHandle<Common_obtainTreasureSilentlyInit>(this, game, 0x004579e0, h_Common_obtainTreasureSilentlyInit);

        _Common_isBrotherhoodUnpoweredRetInt = new FhMethodHandle<CT_RetInt_01B6>(this, game, __addr_CT_RetInt_01B6, h_Common_isBrotherhoodUnpoweredRetInt);
        _Common_upgradeBrotherhoodRetInt = new FhMethodHandle<CT_RetInt_01B7>(this, game, __addr_CT_RetInt_01B7, h_Common_upgradeBrotherhoodRetInt);
        _Common_obtainBrotherhoodRetInt = new FhMethodHandle<Common_obtainBrotherhoodRetInt>(this, game, 0x00459a40, h_Common_obtainBrotherhoodRetInt);

        _Common_grantCelestialUpgrade = new FhMethodHandle<Common_grantCelestialUpgrade>(this, game, 0x0045cfe0, h_Common_grantCelestialUpgrade);

        _Common_setPrimerCollected = new FhMethodHandle<Common_setPrimerCollected>(this, game, 0x0045ab30, h_Common_setPrimerCollected);

        _Common_transitionToMap = new FhMethodHandle<Common_transitionToMap>(this, game, 0x004580c0, h_Common_transitionToMap);
        _Common_warpToMap = new FhMethodHandle<Common_warpToMap>(this, game, 0x00458370, h_Common_warpToMap);

        _SgEvent_showModularMenuInit = new FhMethodHandle<SgEvent_showModularMenuInit>(this, game, __addr_SgEvent_showModularMenuInit, h_SgEvent_showModularMenuInit);


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

        // Common.01D1Init
        _Common_01D1Init = FhUtil.get_fptr<Common_01D1Init>(__addr_Common_01D1Init);
        // Common.01D1Exec
        _Common_01D1Exec = FhUtil.get_fptr<Common_01D1Exec>(__addr_Common_01D1Exec);




        _Common_addPartyMember = new FhMethodHandle<Common_addPartyMember>(this, game, 0x0045b5a0, h_Common_addPartyMember);
        _Common_removePartyMember = new FhMethodHandle<Common_removePartyMember>(this, game, 0x0045b6c0, h_Common_removePartyMember);
        _Common_removePartyMemberLongTerm = new FhMethodHandle<Common_removePartyMemberLongTerm>(this, game, 0x0045aaf0, h_Common_removePartyMemberLongTerm);
        _Common_setWeaponVisibilty = new FhMethodHandle<Common_setWeaponInvisible>(this, game, 0x00456770, h_Common_setWeaponInvisible);
        _Common_putPartyMemberInSlot = new FhMethodHandle<Common_putPartyMemberInSlot>(this, game, 0x0045bc90, h_Common_putPartyMemberInSlot);


        _Common_pushParty = new FhMethodHandle<Common_pushParty>(this, game, 0x0045b350, h_Common_pushParty);
        _Common_popParty = new FhMethodHandle<Common_popParty>(this, game, 0x0045b3c0, h_Common_popParty);

        // getCurrentPartySlots
        _MsGetSavePartyMember = new FhMethodHandle<MsGetSavePartyMember>(this, game, __addr_MsGetSavePartyMember, h_MsGetSavePartyMember);


        _MsBtlListGroup = FhUtil.get_fptr<MsBtlListGroup>(__addr_MsBtlListGroup);
        _MsBattleExe = new FhMethodHandle<MsBattleExe>(this, game, __addr_MsBattleExe, h_MsBattleExe);
        _MsBattleLabelExe = FhUtil.get_fptr<MsBattleLabelExe>(__addr_MsBattleLabelExe);
        _FUN_00791820 = new FhMethodHandle<FUN_00791820>(this, game, 0x00391820, h_FUN_00791820);

        _MsBtlGetPos = new FhMethodHandle<MsBtlGetPos>(this, game, 0x003ac000, h_MsBtlGetPos);

        _MsBtlReadSetScene = new FhMethodHandle<MsBtlReadSetScene>(this, game, 0x00383ed0, h_MsBtlReadSetScene);




        // giveItem
        _FUN_007905a0 = new FhMethodHandle<FUN_007905a0>(this, game, 0x003905a0, h_give_item);



        // readFromBin
        _FUN_007ab890 = new FhMethodHandle<FUN_007ab890>(this, game, 0x003ab890, h_read_from_bin);

        // getWeaponName
        _FUN_007a0d10 = new FhMethodHandle<FUN_007a0d10>(this, game, 0x003a0d10, h_get_weapon_name);
        // getWeaponModel
        _FUN_007a0c70 = new FhMethodHandle<FUN_007a0c70>(this, game, 0x003a0c70, h_get_weapon_model);
        // obtainTreasureCleanup
        _FUN_007993f0 = new FhMethodHandle<FUN_007993f0>(this, game, 0x003993f0, h_obtain_treasure_cleanup);


        _MsGetChr = FhUtil.get_fptr<FhCall.MsGetChr>(0x00394030);
        _MsGetComData = FhUtil.get_fptr<FhCall.MsGetComData>(0x0039a4c0);
        _MsGetCommandUse = FhUtil.get_fptr<FhCall.MsGetCommandUse>(0x0039a5c0);
        _MsGetCommandMP = FhUtil.get_fptr<FhCall.MsGetCommandMP>(0x0038d030);
        _MsGetRamChrMonster = FhUtil.get_fptr<FhCall.MsGetRamChrMonster>(0x0039af00);
        _TODrawCrossBoxXYWHC2 = FhUtil.get_fptr<FhCall.TODrawCrossBoxXYWHC2>(0x004f4b20);

        _eiAbmParaGet = new FhMethodHandle<eiAbmParaGet>(this, game, __addr_eiAbmParaGet, h_eiAbmParaGet);
        _FUN_00a48910 = new FhMethodHandle<FUN_00a48910>(this, game, 0x00648910, h_FUN_00a48910);

        _MsApUp = new FhMethodHandle<MsApUp>(this, game, 0x00398a10, h_MsApUp);

        //delegate* unmanaged[Thiscall]<nint, nint, bool, bool> ph_openFile = &h_openFile;
        //openFile p_openFile = (x, y, z) => d_openFile(ph_openFile, x, y, z);
        //openFile temp = Marshal.GetDelegateForFunctionPointer<openFile>((nint)ph_openFile);
        _openFile = new FhMethodHandle<openFile>(this, game, 0x00208100, h_openFile);
        _readFile = FhUtil.get_fptr<readFile>(0x00208250);

        _FUN_0070aec0 = new FhMethodHandle<FUN_0070aec0>(this, game, 0x0030aec0, h_FUN_0070aec0);


        _ChN_ReadSystemMGRP = FhUtil.get_fptr<FhCall.ChN_ReadSystemMGRP>(FhCall.__addr_ChN_ReadSystemMGRP);

        _Common_loadModel = FhUtil.get_fptr<Common_loadModel>(0x0045ce70);
        _Common_0043 = FhUtil.get_fptr<Common_0043>(0x0045c810);
        _Common_linkFieldToBattleActor = FhUtil.get_fptr<Common_linkFieldToBattleActor>(0x0045ca00);

        _SndSepPlay = FhUtil.get_fptr<FhCall.SndSepPlay>(FhCall.__addr_SndSepPlay);


        _Map_800F = new FhMethodHandle<Map_800F>(this, game, 0x0051b1a0, h_Map_800F);


        _MsSetSaveParam = new FhMethodHandle<MsSetSaveParam>(this, game, __addr_MsSetSaveParam, h_MsSetSaveParam);
        _MsGetExcelData = FhUtil.get_fptr<MsGetExcelData>(__addr_MsGetExcelData);


        // obtainTreasure related
        _TkMsImportantSet = new FhMethodHandle<TkMsImportantSet>(this, game, __addr_TkMsImportantSet, h_TkMsImportantSet);
        _MsPayGIL = FhUtil.get_fptr<MsPayGIL>(__addr_MsPayGIL); // takeGil
        _SndSepPlaySimple = FhUtil.get_fptr<SndSepPlaySimple>(__addr_SndSepPlaySimple);
        _MsGetSaveWeapon = FhUtil.get_fptr<MsGetSaveWeapon>(__addr_MsGetSaveWeapon);
        _FUN_007ab930 = FhUtil.get_fptr<FUN_007ab930>(0x003ab930); // giveWeapon
        _AtelGetCurCtrlWork = FhUtil.get_fptr<AtelGetCurCtrlWork>(__addr_AtelGetCurCtrlWork);
        _MsFieldItemGet = FhUtil.get_fptr<MsFieldItemGet>(__addr_MsFieldItemGet);
        _TkMsGetRomItem = FhUtil.get_fptr<TkMsGetRomItem>(__addr_TkMsGetRomItem);
        _MsSaveItemUse = FhUtil.get_fptr<MsSaveItemUse>(__addr_MsSaveItemUse);
        _MsImportantName = FhUtil.get_fptr<MsImportantName>(__addr_MsImportantName);
        _CT_RetInt_0065 = FhUtil.get_fptr<CT_RetInt_0065>(__addr_CT_RetInt_0065);
        _CT_RetInt_006A = FhUtil.get_fptr<CT_RetInt_006A>(__addr_CT_RetInt_006A);
        _MsBtlGetInit = FhUtil.get_fptr<FhCall.MsBtlGetInit>(FhCall.__addr_MsBtlGetInit);
        _AtelGetMesWinWork = FhUtil.get_fptr<AtelGetMesWinWork>(__addr_AtelGetMesWinWork);
        _FUN_008b8910 = FhUtil.get_fptr<FUN_008b8910>(__addr_FUN_008b8910);
        _FUN_008bda20 = FhUtil.get_fptr<FUN_008bda20>(__addr_FUN_008bda20);
        _FUN_008b8930 = FhUtil.get_fptr<FUN_008b8930>(__addr_FUN_008b8930);
        _FUN_0086a0c0 = FhUtil.get_fptr<FUN_0086a0c0>(__addr_FUN_0086a0c0);


        _FUN_0086bec0 = new FhMethodHandle<FUN_0086bec0>(this, game, 0x0046bec0, h_FUN_0086bec0);
        _FUN_0086bea0 = new FhMethodHandle<FUN_0086bea0>(this, game, 0x0046bea0, h_FUN_0086bea0);


        _FUN_00656c90 = new FhMethodHandle<FUN_00656c90>(this, game, 0x00256c90, h_FUN_00656c90);

        // For loading texture from game
        _FUN_0065ee30 = FhUtil.get_fptr<FUN_0065ee30>(__addr_ClusterManager_FUN_0065ee30);
        _ClusterManager_loadPCluster = FhUtil.get_fptr<ClusterManager_loadPCluster>(__addr_ClusterManager_loadPCluster);
        _Phyre_PFramework_PApplication_FixupClusters = FhUtil.get_fptr<Phyre_PFramework_PApplication_FixupClusters>(__addr_Phyre_PFramework_PApplication_FixupClusters);
        _ClusterManager_releasePCluster = FhUtil.get_fptr<ClusterManager_releasePCluster>(__addr_ClusterManager_releasePCluster);

        // Non-loading FMV
        _graphicInitFMVPlayer = new FhMethodHandle<graphicInitFMVPlayer>(this, game, __addr_graphicInitFMVPlayer, h_graphicInitFMVPlayer);

        foreach (byte[] script in customScripts) {
            customScriptHandles.Add(GCHandle.Alloc(script, GCHandleType.Pinned));
        }

        //for (int i = 0; i < rawCustomStrings.Length; i++) {
        //    byte[] text = rawCustomStrings[i];
        //    customStrings[i] = new CustomString(text);
        //}

        _FmodVoice_dataChange = new FhMethodHandle<FmodVoice_dataChange>(this, game, __addr_FmodVoice_dataChange, h_FmodVoice_dataChange);
        var _FMOD_EventSystem_load_pointer = FhUtil.get_at<nint>(__addr_FMOD_EventSystem_load);
        _FMOD_EventSystem_load = Marshal.GetDelegateForFunctionPointer<FMOD_EventSystem_load>(_FMOD_EventSystem_load_pointer);
        //_FMOD_EventSystem_load = new FhMethodHandle<FMOD_EventSystem_load>(this, _FMOD_EventSystem_load_pointer, h_FMOD_EventSystem_load);

        //_FfxFmod_soundInit_setLang = new FhMethodHandle<FfxFmod_soundInit_setLang>(this, game, __addr_FfxFmod_soundInit_setLang, h_FfxFmod_soundInit_setLang);

        _LocalizationManager_Initialize = new FhMethodHandle<LocalizationManager_Initialize>(this, game, __addr_LocalizationManager_Initialize, h_LocalizationManager_Initialize);
        _LocalizationManager_GetInstance = FhUtil.get_fptr<LocalizationManager_GetInstance>(__addr_LocalizationManager_GetInstance);
        _FfxFmod_soundInit = FhUtil.get_fptr<FfxFmod_soundInit>(__addr_FfxFmod_soundInit);
        _FmodVoice_initList = FhUtil.get_fptr<FmodVoice_initList>(__addr_FmodVoice_initList);


        // Custom namespace
        _AtelInitTotal = new FhMethodHandle<AtelInitTotal>(this, game, __addr_AtelInitTotal, h_AtelInitTotal);

    }

    public static int ignore_this = 11;
    public static int h_Map_800F(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int param_1 = atelStack->values.as_int()[0];
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

        return _Common_obtainTreasureInit.hook() && _Common_obtainTreasureSilentlyInit.hook() && _Common_obtainBrotherhoodRetInt.hook()
            && _Common_grantCelestialUpgrade.hook() && _Common_setPrimerCollected.hook()
            && _AtelEventSetUp.hook() && _Common_transitionToMap.hook() && _Common_warpToMap.hook()
            && _SgEvent_showModularMenuInit.hook()
            && _Common_addPartyMember.hook() && _Common_removePartyMember.hook() && _Common_removePartyMemberLongTerm.hook() && _Common_setWeaponVisibilty.hook()
            && _Common_putPartyMemberInSlot.hook() && _Common_pushParty.hook() && _Common_popParty.hook() && _MsBattleExe.hook() && _FUN_00791820.hook()
            && _MsApUp.hook() //&& _MsBtlReadSetScene.hook() //&& _MsSetSaveParam.hook() // && _Map_800F.hook() //_MsBtlGetPos.hook()
            && _eiAbmParaGet.hook() // && _FUN_00a48910.hook()
            && _FUN_0086bec0.hook() && _FUN_0086bea0.hook() // Custom strings
            && _graphicInitFMVPlayer.hook() && _FmodVoice_dataChange.hook()
            && _AtelInitTotal.hook();
            //&& _LocalizationManager_Initialize.hook();
            //&& _FUN_00656c90.hook() && _FUN_0065ee30.hook();
            //&& _openFile.hook() && _FUN_0070aec0.hook();
            //&& _MsCheckLeftWindow.hook() && _MsCheckUseCommand.hook() && _TOBtlDrawStatusLimitGauge.hook();

    }

    private static void set(byte* code_ptr, int offset, AtelInst opcode) {
        byte* ptr = code_ptr + offset;
        foreach (byte b in opcode.to_bytes()) {
            *ptr = b;
            ptr++;
        }
    }
    private static void set(byte* code_ptr, int offset, AtelInst[] opcodes) {
        byte* ptr = code_ptr + offset;
        foreach (AtelInst op in opcodes) {
            foreach (byte b in op.to_bytes()) {
                *ptr = b;
                ptr++;
            }
        }
    }

    private static byte* h_FUN_0086bec0(int param_1) {
        byte* result;
        if ((param_1 & 0x8000) != 0) {
            int custom_index = param_1 & 0x7FFF;
            result = customStrings[custom_index].encoded;
            //result = (byte*)customStringHandles[custom_index].AddrOfPinnedObject();
            //FhCharset.compute_decode_buffer_size(result);
            //string decoded = FhCharset.Us.to_string(result);
            logger.Debug(customStrings[custom_index].decoded);
        } else {
            // May crash if called with invalid index
            result = _FUN_0086bec0.orig_fptr(param_1);
        }

        return result;
    }
    private static short h_FUN_0086bea0(int param_1) {
        short result;
        if ((param_1 & 0x8000) != 0) {
            int custom_index = param_1 & 0x7FFF;
            result = customStrings[custom_index].metadata;
        }
        else {
            result = _FUN_0086bea0.orig_fptr(param_1);
        }

        return result;
    }

    private static readonly byte NEWLINE = 0x03;
    private static readonly byte TIME    = 0x09;
    private static readonly byte CHOICE  = 0x10;

    //private static readonly List<GCHandle> customStringHandles = [];
    //public static readonly byte[][] customStrings = {
    //    //new byte[][] {
    //    //    [TIME, 0x30],
    //    //    FhCharset.Us.to_bytes("Ready to fight Sin?"),
    //    //    [NEWLINE, CHOICE, 0x30],
    //    //    FhCharset.Us.to_bytes("Yes"),
    //    //    [NEWLINE, CHOICE, 0x31],
    //    //    FhCharset.Us.to_bytes("No"),
    //    //}.SelectMany(x => x).ToArray(),
    //    //FhCharset.Us.to_bytes("{TIME:0}Ready to fight {COLOR:BLUE}Sin{COLOR:WHITE}?\n{CHOICE:0}Yes\n{CHOICE:1}No"),
    //};
    public struct CustomString {
        public byte* encoded;
        public int encodedLength;
        public string decoded;
        public int choices;
        public int flags;

        public CustomString(ReadOnlySpan<byte> text, int choices = 0, int flags = 0, FhEncodingFlags encodingFlags = default) {
            this.choices = choices;
            this.flags = flags;

            this.decoded = Encoding.UTF8.GetString(text);

            this.encodedLength = FhCharset.compute_encode_buffer_size(text, flags: encodingFlags);
            this.encoded = (byte*)NativeMemory.AllocZeroed((nuint)encodedLength+1);
            int actual_size = FhCharset.encode(text, new Span<byte>(encoded, encodedLength), flags: encodingFlags);
        }

        public CustomString(string text, int choices = 0, int flags = 0, FhEncodingFlags encodingFlags = default) {
            this.choices = choices;
            this.flags = flags;
            this.decoded = text;

            ReadOnlySpan<byte> utf8String = Encoding.UTF8.GetBytes(text);

            this.encodedLength = FhCharset.compute_encode_buffer_size(utf8String, flags: encodingFlags);
            this.encoded = (byte*)NativeMemory.AllocZeroed((nuint)encodedLength+1);
            int actual_size = FhCharset.encode(utf8String, new Span<byte>(encoded, encodedLength), flags: encodingFlags);
        }

        public readonly void Free() {
            NativeMemory.Free(encoded);
        }

        public readonly short metadata => (short)((choices << 8) | flags);
    }

    //private const byte WHITE     = 0x41;
    //private const byte YELLOW    = 0x43;
    //private const byte GREY      = 0x52;
    //private const byte BLUE      = 0x88;
    //private const byte RED       = 0x94;
    //private const byte PINK      = 0x97;
    //private const byte OL_PURPLE = 0xA1;
    //private const byte OL_CYAN   = 0xB1;

    public static readonly CustomString[] customStrings = [
        new CustomString("{TIME:00}Ready to fight {COLOR:88}Sin{COLOR:41}?{LF}{CHOICE:00}Yes{LF}{CHOICE:01}No"u8, 2, 0),
        new CustomString("{TIME:00}Ready to fight {COLOR:88}Jecht{COLOR:41}?{LF}{CHOICE:00}Yes{LF}{CHOICE:01}No"u8, 2, 0),
        new CustomString("{TIME:00}Locked"u8, 0, 0),
    ];
    //public static readonly byte[][] rawCustomStrings = [
    //    "{TIME:0}Ready to fight {COLOR:BLUE}Sin{COLOR:WHITE}?\n{CHOICE:0}Yes\n{CHOICE:1}No"u8.ToArray()
    //];


    // Choices << 8 | Flags
    //private static readonly short[] customStringFlags = {
    //    (02 << 8) | 00,
    //
    //};


    //private static readonly byte[] sinString = new byte[][] {
    //        [TIME, 0x30],
    //        FhCharset.Us.to_bytes("Ready to fight Sin?"),
    //        [NEWLINE, CHOICE, 0x30],
    //        FhCharset.Us.to_bytes("Yes"),
    //        [NEWLINE, CHOICE, 0x31],
    //        FhCharset.Us.to_bytes("No"),
    //    }.SelectMany(x => x).ToArray();

    private static readonly GCHandle APItemString;
    //private static readonly GCHandle APItemString = GCHandle.Alloc(FhCharset.encode("Archipelago Item"), GCHandleType.Pinned);

    private static AtelInst[] atelDisplayFieldString(ushort boxIndex, ushort stringIndex, ushort x, ushort y, ushort align = 4, ushort textFlags = 0, ushort transparent = 0) {
        return [
            // call Common.positionText [0065h](boxIndex=1 [01h], x=256 [0100h], y=224 [E0h], align=Center [04h]);
            AtelOp.PUSHII   .build(boxIndex),
            AtelOp.PUSHII   .build(x),
            AtelOp.PUSHII   .build(y),
            AtelOp.PUSHII   .build(align),
            AtelOp.CALLPOPA .build(0x0065),
            // call Common.setTextHasTransparentBackdrop [0066h](boxIndex=1 [01h], transparent=false [00h]);
            AtelOp.PUSHII   .build(boxIndex),
            AtelOp.PUSHII   .build(transparent),
            AtelOp.CALLPOPA .build(0x0066),
            // call Common.displayFieldString [0064h](boxIndex=1 [01h], string=customString[2] [00h]);
            AtelOp.PUSHII   .build(boxIndex),
            AtelOp.PUSHII   .build(stringIndex),
            AtelOp.CALLPOPA .build(0x0064),
            // call Common.setTextFlags [009Dh](boxIndex=1 [01h], textFlags=[] [00h]);
            AtelOp.PUSHII   .build(boxIndex),
            AtelOp.PUSHII   .build(textFlags),
            AtelOp.CALLPOPA .build(0x009D),
            // call Common.006A(boxIndex=1 [01h], p2=0 [00h]);
            AtelOp.PUSHII   .build(boxIndex),
            AtelOp.PUSHII   .build(0x0000),
            AtelOp.CALLPOPA .build(0x006A),
        ];
    }

    private static readonly List<GCHandle> customScriptHandles = [];
    private static readonly byte[][] customScripts = {
        // Cid talk hook
        ((AtelInst[])[
            // If GameMoment < 2970 or GameMoment >= 3120: Jump to j01 (return)
            AtelOp.PUSHV   .build(0x0000),
            AtelOp.POPY    .build(      ),

            AtelOp.PUSHY   .build(      ),
            AtelOp.PUSHII  .build(2970  ),
            AtelOp.LS      .build(      ),

            AtelOp.PUSHY   .build(      ),
            AtelOp.PUSHII  .build(3210  ),
            AtelOp.GTE     .build(      ),

            AtelOp.LOR     .build(      ),
            AtelOp.POPXCJMP.build(0x0001),


            // call Common.disablePlayerControl? [005Eh]();
            AtelOp.CALLPOPA.build(0x005e),
            // call Common.displayFieldChoice [013Bh](boxIndex=1 [01h], string=customString[0], p3=0 [00h], p4=1 [01h], x=280 [0118h], y=224 [E0h], align=Center [04h]);
            AtelOp.PUSHII  .build(0x0001),
            AtelOp.PUSHII  .build(0x8000),
            AtelOp.PUSHII  .build(0x0000),
            AtelOp.PUSHII  .build(0x0001),
            AtelOp.PUSHII  .build(0x0100),
            AtelOp.PUSHII  .build(0x00e0),
            AtelOp.PUSHII  .build(0x0004),
            AtelOp.CALLPOPA.build(0x013b),
            AtelOp.PUSHA.build(),
            // call Common.waitForText [0084h](boxIndex=1 [01h], p2=1 [01h]);
            AtelOp.PUSHII  .build(0x0001),
            AtelOp.PUSHII  .build(0x0001),
            AtelOp.CALLPOPA.build(0x0084),
            // call Common.enablePlayerControl? [005Dh]();
            AtelOp.CALLPOPA.build(0x005d),
            // call Common.obtainBrotherhood(choice, 1, 3) i.e. if !choice jump to customscripts[1]
            AtelOp.PUSHII  .build(0x0103),
            AtelOp.CALLPOPA.build(0x01B8),

            
            //// Jump to j01 (return)
            AtelOp.JMP     .build(0x0001),
        ]).SelectMany(x => x.to_bytes()).ToArray(),

        // Start Airship Sin fights
        ((AtelInst[])[
            // Set GameMoment = 3085
            AtelOp.PUSHII  .build(  3085),
            AtelOp.POPV    .build(0x0000),
            // call Common.transitionToMap? [0011h](map=199, entranceIndex=0);
            AtelOp.PUSHII  .build(   199),
            AtelOp.PUSHII  .build(     0),
            AtelOp.CALLPOPA.build(0x0011),
            // Return;
            AtelOp.RET.build(),

        ]).SelectMany(x => x.to_bytes()).ToArray(),

        // Remove defeated Aeon
        ((AtelInst[])[
            // call Common.obtainBrotherhood(privBA98, 9) = lock party member privBA98
            AtelOp.PUSHV   .build(0x000B),
            AtelOp.PUSHII  .build(0x0009),
            AtelOp.CALLPOPA.build(0x01B8),

            // switch privBA98
            AtelOp.PUSHV   .build(0x000B),
            AtelOp.POPY    .build(),
            // Jump to j04 (6758)
            AtelOp.JMP     .build(0x0004),

        ]).SelectMany(x => x.to_bytes()).ToArray(),

        // Check Goal Requirement (Falling tower)
        ((AtelInst[])[
            // If !(GameMoment < 3250): Jump to j01 (return)
            AtelOp.PUSHV    .build(0x0000),
            AtelOp.PUSHII   .build(3250  ),
            AtelOp.LS       .build(      ),
            AtelOp.POPXNCJMP.build(0x0001),
            // call Common.obtainBrotherhood(privBA98, A) = isGoalUnlocked
            AtelOp.PUSHII   .build(0x000A),
            AtelOp.CALLPOPA .build(0x01B8),
            // call Common.obtainBrotherhood(isGoalUnlocked, 0x0404) i.e. if isGoalUnlocked jump to customScripts[4]
            AtelOp.PUSHA    .build(),
            AtelOp.PUSHII   .build(0x0404),
            AtelOp.CALLPOPA .build(0x01B8),
            
            // call Common.disablePlayerControl? [005Eh]();
            AtelOp.CALLPOPA.build(0x005e),
            // display customStrings[2]
            .. atelDisplayFieldString(1, 0x8002, 256, 224, 4, 0, 0),

            // walk away
            // call Common.obtainBrotherhood(w0, 6, 0x050b) i.e. workers[0].entry_points[6] = customScripts[5]
            AtelOp.PUSHII  .build(0x0000),
            AtelOp.PUSHII  .build(0x0006),
            AtelOp.PUSHII  .build(0x050B),
            AtelOp.CALLPOPA.build(0x01B8),

            // runAndAwaitEnd w00e06 (Level 1)
            AtelOp.PUSHII  .build(0x0001),
            AtelOp.PUSHII  .build(0x0000),
            AtelOp.PUSHII  .build(0x0006),
            AtelOp.REQEW   .build(      ),
            AtelOp.POPA    .build(      ),

            // call Common.obtainBrotherhood(w0, 6, 0xb) i.e. workers[0].entry_points[6] = original value
            AtelOp.PUSHII  .build(0x0000),
            AtelOp.PUSHII  .build(0x0006),
            AtelOp.PUSHII  .build(0x000C),
            AtelOp.CALLPOPA.build(0x01B8),

            // call Common.waitForText [0084h](boxIndex=1 [01h], p2=1 [01h]);
            AtelOp.PUSHII  .build(0x0001),
            AtelOp.PUSHII  .build(0x0001),
            AtelOp.CALLPOPA.build(0x0084),
            // call Common.enablePlayerControl? [005Dh]();
            AtelOp.CALLPOPA.build(0x005d),

            // Jump to j01 (return)
            AtelOp.JMP      .build(0x0001),

        ]).SelectMany(x => x.to_bytes()).ToArray(),

        ((AtelInst[])[
            // call Common.disablePlayerControl? [005Eh]();
            AtelOp.CALLPOPA.build(0x005e),
            // call Common.displayFieldChoice [013Bh](boxIndex=1 [01h], string=customString[1], p3=0 [00h], p4=1 [01h], x=280 [0118h], y=224 [E0h], align=Center [04h]);
            AtelOp.PUSHII    .build(0x0001),
            AtelOp.PUSHII    .build(0x8001),
            AtelOp.PUSHII    .build(0x0000),
            AtelOp.PUSHII    .build(0x0001),
            AtelOp.PUSHII    .build(0x0100),
            AtelOp.PUSHII    .build(0x00e0),
            AtelOp.PUSHII    .build(0x0004),
            AtelOp.CALLPOPA  .build(0x013b),
            AtelOp.PUSHA     .build(),
            // call Common.waitForText [0084h](boxIndex=1 [01h], p2=1 [01h]);
            AtelOp.PUSHII    .build(0x0001),
            AtelOp.PUSHII    .build(0x0001),
            AtelOp.CALLPOPA  .build(0x0084),
            // call Common.enablePlayerControl? [005Dh]();
            AtelOp.CALLPOPA  .build(0x005d),
            // If !choice Jump to j00 (continue)
            AtelOp.POPXNCJMP  .build(0x0000),

            // walk away
            // call Common.obtainBrotherhood(w0, 6, 0x050b) i.e. workers[0].entry_points[6] = customScripts[5]
            AtelOp.PUSHII  .build(0x0000),
            AtelOp.PUSHII  .build(0x0006),
            AtelOp.PUSHII  .build(0x050B),
            AtelOp.CALLPOPA.build(0x01B8),

            // runAndAwaitEnd w00e06 (Level 1)
            AtelOp.PUSHII  .build(0x0001),
            AtelOp.PUSHII  .build(0x0000),
            AtelOp.PUSHII  .build(0x0006),
            AtelOp.REQEW   .build(      ),
            AtelOp.POPA    .build(      ),

            // call Common.obtainBrotherhood(w0, 6, 0xb) i.e. workers[0].entry_points[6] = original value
            AtelOp.PUSHII  .build(0x0000),
            AtelOp.PUSHII  .build(0x0006),
            AtelOp.PUSHII  .build(0x000C),
            AtelOp.CALLPOPA.build(0x01B8),

            // call Common.enablePlayerControl? [005Dh]();
            AtelOp.CALLPOPA.build(0x005d),

            // Jump to j01 (return)
            AtelOp.JMP       .build(0x0001),
        ]).SelectMany(x => x.to_bytes()).ToArray(),

        ((AtelInst[])[
            //call Common.disablePlayerControl? [005Eh]();
            AtelOp.CALLPOPA.build(0x005E),
            //call Common.stopWorkerMotion [00D1h]();
            AtelOp.CALLPOPA.build(0x00D1),
            //call Common.setMovementSpeed [006Ch](speed=34.0 [42080000h]);
            AtelOp.PUSHF   .build(0x0008),
            AtelOp.CALLPOPA.build(0x006C),
            //call Common.0017(p1=0 [00h]);
            AtelOp.PUSHII  .build(0x0000),
            AtelOp.CALLPOPA.build(0x0017),
            //call Common.setRotationSpeed1 [002Eh](float=0.10471976 [3DD67750h]);
            AtelOp.PUSHF   .build(0x0006),
            AtelOp.CALLPOPA.build(0x002E),
            //call Common.setRotationSpeed2 [002Fh](float=0.17453294 [3E32B8C3h]);
            AtelOp.PUSHF   .build(0x0007),
            AtelOp.CALLPOPA.build(0x002F),
            //call Common.setDestination [0015h](x=-96.33675 [C2C0AC6Ah], y=25.666155 [41CD5449h], z=-526.34314 [C40395F6h]);
            AtelOp.PUSHII  .build((ushort)(0xc0800000 & 0xffff)),
            AtelOp.PUSHII  .build((ushort)(0xc0800000 >>    16)),
            AtelOp.CALLPOPA.build(0xF001),
            AtelOp.PUSHII  .build((ushort)(0x40ae56e5 & 0xffff)),
            AtelOp.PUSHII  .build((ushort)(0x40ae56e5 >>    16)),
            AtelOp.CALLPOPA.build(0xF001),
            AtelOp.PUSHII  .build((ushort)(0xc3af0000 & 0xffff)),
            AtelOp.PUSHII  .build((ushort)(0xc3af0000 >>    16)),
            AtelOp.CALLPOPA.build(0xF001),
            AtelOp.CALLPOPA.build(0x0015),
            //call Common.setRotationTarget1 [0028h](angle=Common.destinationToYaw [001Fh]());
            AtelOp.CALL    .build(0x001F),
            AtelOp.CALLPOPA.build(0x0028),
            //call Common.startRotation [0019h](activeBits=0 [00h], flags=[b0002, b1000, b4000, b8000] [0000D002h], targetWorker=w00 [00h]);
            AtelOp.PUSHII  .build(0x0000),
            AtelOp.PUSHI   .build(0x0000),
            AtelOp.PUSHII  .build(0x0000),
            AtelOp.CALLPOPA.build(0x0019),
            //call Common.startMotion [0018h](activeBits=0 [00h], flags=[b0001, b4000, b8000] [0000C001h], targetWorker=w00 [00h]);
            AtelOp.PUSHII  .build(0x0000),
            AtelOp.PUSHI   .build(0x0001),
            AtelOp.PUSHII  .build(0x0000),
            AtelOp.CALLPOPA.build(0x0018),
            //call Common.waitForMotion [001Ah]();
            AtelOp.CALLPOPA.build(0x001A),
            //call Common.stopWorkerRotation [0078h](worker=Common.getWorkerIndex [0033h](worker=<Self> [FFFFh]));
            AtelOp.PUSHII  .build(0xFFFF),
            AtelOp.CALL    .build(0x0033),
            AtelOp.CALLPOPA.build(0x0078),
            //return;
            AtelOp.RET     .build(      ),

        ]).SelectMany(x => x.to_bytes()).ToArray(),


    };

    private static readonly AtelInst[] save_sphere_load_model = [
        AtelOp.PUSHII  .build(0x5001),
        AtelOp.CALLPOPA.build(0x0001),
    ];


    private static readonly AtelInst[] save_sphere_load_underwater_model = [
        AtelOp.PUSHII  .build(0x50AB),
        AtelOp.CALLPOPA.build(0x0001),
    ];

    private static Dictionary<(int, int), uint> originalEntryPoints = new();
    private static string current_event_name = "";
    private static void h_AtelEventSetUp(int event_id) {
        _AtelEventSetUp.orig_fptr(event_id);

        foreach (var handle in cached_strings) {
            handle.Free();
        }
        cached_strings.Clear();

        originalEntryPoints.Clear();

        string event_name = Marshal.PtrToStringAnsi((nint)get_event_name((uint)event_id))!;
        logger.Debug($"atel_event_setup: {event_name}");
        byte* code_ptr = Atel.controllers[0].worker(0)->code_ptr;
        switch (event_name) {
            case "hiku2100":
                logger.Debug($"atel_event_setup: Inject set_airship_destinations call");
                set(code_ptr, 0x26D1, [
                    AtelOp.PUSHII  .build(0x0001),
                    AtelOp.CALLPOPA.build(0x01B8) // Common.obtainBrotherhood(1) = set_airship_destinations
                    ]);

                set(code_ptr, 0x4028, AtelOp.JMP.build(0x013A));


                // Fix Bevelle destination
                set(code_ptr, 0x4326, AtelOp.PUSHII.build(0x000C)); // Point destination 12 (Bevelle Temple) to destination 18's warp (Highbridge) 
                set(code_ptr, 0x4118, AtelOp.JMP.build(0x0141)); // Skip setting GameMoment when choosing destination 18

                break;
            case "hiku0801":
                logger.Debug($"atel_event_setup: Inject Cid talk hook");
                set(code_ptr, 0x4DC5, [
                    AtelOp.PUSHII  .build(0x0002),
                    AtelOp.CALLPOPA.build(0x01B8), // Common.obtainBrotherhood(0002) = jump to customScripts[0]
                ]);
                break;
            case "ssbt0300":
                logger.Debug($"atel_event_setup: Redirect Overdrive Sin post-battle warp");
                set(code_ptr, 0x500E, AtelOp.PUSHII.build(382));
                break;
            case "sins0700":
                logger.Debug($"atel_event_setup: Handle removing Aeons");
                set(code_ptr, 0x5972, [
                    AtelOp.PUSHII  .build(0x0202),
                    AtelOp.CALLPOPA.build(0x01B8), // Common.obtainBrotherhood(0202) = jump to customScripts[2]
                    ]);
                break;

            case "luca0400":
                logger.Debug($"atel_event_setup: Wait longer");
                set(code_ptr, 0x68F9, [
                    AtelOp.PUSHII  .build(10),
                    AtelOp.CALLPOPA.build( 0),
                    ]);
                break;

            case "sins0900":
                // Tower falling down
                Globals.Atel.current_controller->worker(0xD)->table_jump[0] = 0x1951;
                AtelBasicWorker* _0xd = Globals.Atel.current_controller->worker(0xD);
                set(code_ptr, 0x1947, [
                    AtelOp.PUSHII.build(0x0302),
                    AtelOp.CALLPOPA.build(0x01B8), // Common.obtainBrotherhood(0302) = jump to customScripts[3]
                    ]);

                // Look up at tower
                //set(code_ptr, 0x1761, [
                //    AtelOp.RET.build(),
                //    ]);
                break;
        }

        // Inject save sphere hook
        if (event_name == "nagi0000") {
            int save_sphere_offset = 0x1BB69;
            logger.Info($"Save sphere init at {save_sphere_offset}");
            set(code_ptr, save_sphere_offset + 0x48, AtelOp.JMP.build(0x0007)); // Always all options

            // Update region state. Also skips save sphere tutorial
            set(code_ptr, save_sphere_offset + 0x59B, [
                AtelOp.PUSHII  .build(0x0008),
                AtelOp.CALLPOPA.build(0x01B8), // Common.obtainBrotherhood(8) = update_region_state

                AtelOp.JMP     .build(0x0023),
                ]);

            // Board Airship option
            set(code_ptr, save_sphere_offset + 0x695, [
                // call Common.00BB(0);
                AtelOp.PUSHII  .build(0x0000),
                AtelOp.CALLPOPA.build(0x00BB),
                // call Common.00BC(0);
                AtelOp.PUSHII  .build(0x0000),
                AtelOp.CALLPOPA.build(0x00BC),
                // call Common.warpToMap?[010Bh](382, 0); (Airship Menu)
                AtelOp.PUSHII  .build(   382),
                AtelOp.PUSHII  .build(     0),
                AtelOp.CALLPOPA.build(0x010B),
                ]);

        }
        else if (event_name == "cdsp0700") {
            int save_sphere_offset = 0x2AA3;
            logger.Info($"Underwater save sphere init at {save_sphere_offset}");
            set(code_ptr, save_sphere_offset + 0x5A, AtelOp.JMP.build(0x0002)); // Always all options

            // Update region state. Also skips save sphere tutorial
            set(code_ptr, save_sphere_offset + 0x542, [
                AtelOp.PUSHII  .build(0x0008),
                AtelOp.CALLPOPA.build(0x01B8), // Common.obtainBrotherhood(8) = update_region_state

                AtelOp.JMP     .build(0x001D),
                ]);

            // Board Airship option
            set(code_ptr, save_sphere_offset + 0x628, [
                // call Common.00BB(0);
                AtelOp.PUSHII  .build(0x0000),
                AtelOp.CALLPOPA.build(0x00BB),
                // call Common.00BC(0);
                AtelOp.PUSHII  .build(0x0000),
                AtelOp.CALLPOPA.build(0x00BC),
                // call Common.warpToMap?[010Bh](382, 0); (Airship Menu)
                AtelOp.PUSHII  .build(   382),
                AtelOp.PUSHII  .build(     0),
                AtelOp.CALLPOPA.build(0x010B),
                ]);

        }
        else if (event_name == "stbv0000") {
            int save_sphere_offset = 0x2982;
            logger.Info($"Underwater save sphere init at {save_sphere_offset}");
            set(code_ptr, save_sphere_offset + 0x5A, AtelOp.JMP.build(0x0002)); // Always all options

            // Update region state. Also skips save sphere tutorial
            set(code_ptr, save_sphere_offset + 0x542, [
                AtelOp.PUSHII  .build(0x0008),
                AtelOp.CALLPOPA.build(0x01B8), // Common.obtainBrotherhood(8) = update_region_state

                AtelOp.JMP     .build(0x001D),
                ]);

            // Board Airship option
            set(code_ptr, save_sphere_offset + 0x628, [
                // call Common.00BB(0);
                AtelOp.PUSHII  .build(0x0000),
                AtelOp.CALLPOPA.build(0x00BB),
                // call Common.00BC(0);
                AtelOp.PUSHII  .build(0x0000),
                AtelOp.CALLPOPA.build(0x00BC),
                // call Common.warpToMap?[010Bh](382, 0); (Airship Menu)
                AtelOp.PUSHII  .build(   382),
                AtelOp.PUSHII  .build(     0),
                AtelOp.CALLPOPA.build(0x010B),
                ]);


            save_sphere_offset = 0x3168;
            logger.Info($"Underwater save sphere init at {save_sphere_offset}");
            set(code_ptr, save_sphere_offset + 0x5A, AtelOp.JMP.build(0x0002)); // Always all options

            // Update region state. Also skips save sphere tutorial
            set(code_ptr, 0x3746, [
                AtelOp.PUSHII  .build(0x0008),
                AtelOp.CALLPOPA.build(0x01B8), // Common.obtainBrotherhood(8) = update_region_state

                AtelOp.JMP     .build(0x001E),
                ]);

            // Board Airship option
            set(code_ptr, 0x382C, [
                // call Common.00BB(0);
                AtelOp.PUSHII  .build(0x0000),
                AtelOp.CALLPOPA.build(0x00BB),
                // call Common.00BC(0);
                AtelOp.PUSHII  .build(0x0000),
                AtelOp.CALLPOPA.build(0x00BC),
                // call Common.warpToMap?[010Bh](382, 0); (Airship Menu)
                AtelOp.PUSHII  .build(   382),
                AtelOp.PUSHII  .build(     0),
                AtelOp.CALLPOPA.build(0x010B),
                ]);
        }
        else if (false && event_name == "stbv0100") {
            int save_sphere_offset = 0xF07F;
            logger.Info($"Save sphere init at {save_sphere_offset}");
            set(code_ptr, save_sphere_offset + 0x48, AtelOp.JMP.build(0x0007)); // Always all options

            // Update region state. Also skips save sphere tutorial
            set(code_ptr, save_sphere_offset + 0x571, [
                AtelOp.PUSHII  .build(0x0008),
                        AtelOp.CALLPOPA.build(0x01B8), // Common.obtainBrotherhood(8) = update_region_state

                        AtelOp.JMP     .build(0x0023),
                        ]);

            // Board Airship option
            set(code_ptr, save_sphere_offset + 0x657, [
                // call Common.00BB(0);
                AtelOp.PUSHII  .build(0x0000),
                        AtelOp.CALLPOPA.build(0x00BB),
                        // call Common.00BC(0);
                        AtelOp.PUSHII  .build(0x0000),
                        AtelOp.CALLPOPA.build(0x00BC),
                        // call Common.warpToMap?[010Bh](382, 0); (Airship Menu)
                        AtelOp.PUSHII  .build(   382),
                        AtelOp.PUSHII  .build(     0),
                        AtelOp.CALLPOPA.build(0x010B),
                        ]);


            save_sphere_offset = 0xFCF1;
            logger.Info($"Save sphere init at {save_sphere_offset}");
            set(code_ptr, save_sphere_offset + 0x48, AtelOp.JMP.build(0x0007)); // Always all options

            // Update region state. Also skips save sphere tutorial
            set(code_ptr, 0x10278, [
                AtelOp.PUSHII  .build(0x0008),
                AtelOp.CALLPOPA.build(0x01B8), // Common.obtainBrotherhood(8) = update_region_state

                AtelOp.JMP     .build(0x0025),
                ]);

            // Board Airship option
            set(code_ptr, 0x1035E, [
                // call Common.00BB(0);
                AtelOp.PUSHII  .build(0x0000),
                AtelOp.CALLPOPA.build(0x00BB),
                // call Common.00BC(0);
                AtelOp.PUSHII  .build(0x0000),
                AtelOp.CALLPOPA.build(0x00BC),
                // call Common.warpToMap?[010Bh](382, 0); (Airship Menu)
                AtelOp.PUSHII  .build(   382),
                AtelOp.PUSHII  .build(     0),
                AtelOp.CALLPOPA.build(0x010B),
                ]);

        }
        else {
            AtelInst? previous_op = null;
            AtelInst? current_op = null;
            uint code_length = Atel.controllers[0].worker(0)->script_chunk->code_length;
            int i = 0;
            int save_spheres_detected = 0;
            int sphere_level_offset = 0x48;
            int tutorial_offset = 0x571;
            ushort tutorial_jump = 0x23;
            int airship_warp_offset = 0x657;
            if (event_name == "mihn0000" || event_name == "mihn0200") {
                tutorial_offset = 0x59B;
                airship_warp_offset = 0x695;
            }
            while (i < code_length) {
                previous_op = current_op;
                AtelOp inst = (AtelOp)code_ptr[i];
                if (inst.has_operand()) {
                    current_op = inst.build(*(ushort*)(code_ptr + i + 1));
                }
                else {
                    current_op = inst.build();
                }
                if (current_op == save_sphere_load_model[1] && previous_op == save_sphere_load_model[0]) {
                    if (save_spheres_detected == 1) {
                        // Additional save spheres usually have different offsets
                        sphere_level_offset = 0x48;
                        tutorial_offset = 0x587;
                        tutorial_jump = 0x25;
                        airship_warp_offset = 0x66D;
                    }
                    else if (save_spheres_detected > 1) {
                        // Potentially incorrect offsets
                        logger.Info("Potentially incorrect Save Sphere offsets");
                    }
                    save_spheres_detected++;
                    logger.Info($"Detected save sphere init at {i - 6}");
                    int save_sphere_offset = i - 6;

                    AtelOp someInst = (AtelOp)code_ptr[save_sphere_offset + sphere_level_offset];
                    if (!someInst.has_operand() || someInst.build(*(ushort*)(code_ptr + save_sphere_offset + sphere_level_offset + 1)) != AtelOp.PUSHV.build(0x0000)) {
                        logger.Warning($"Unexpected instruction at {save_sphere_offset + sphere_level_offset}");
                    }
                    set(code_ptr, save_sphere_offset + sphere_level_offset, AtelOp.JMP.build(0x0007)); // Always all options



                    someInst = (AtelOp)code_ptr[save_sphere_offset + tutorial_offset + 13];
                    if (!someInst.has_operand() || someInst.build(*(ushort*)(code_ptr + save_sphere_offset + tutorial_offset + 13 + 1)) != AtelOp.POPXNCJMP.build(tutorial_jump)) {
                        logger.Warning($"Unexpected instruction at {save_sphere_offset + tutorial_offset + 13}");
                    }
                    // Update region state. Also skips save sphere tutorial
                    set(code_ptr, save_sphere_offset + tutorial_offset, [
                        AtelOp.PUSHII  .build(0x0008),
                        AtelOp.CALLPOPA.build(0x01B8), // Common.obtainBrotherhood(8) = update_region_state

                        AtelOp.JMP     .build(tutorial_jump),
                        ]);


                    someInst = (AtelOp)code_ptr[save_sphere_offset + airship_warp_offset];
                    if (!someInst.has_operand() || someInst.build(*(ushort*)(code_ptr + save_sphere_offset + airship_warp_offset + 1)) != AtelOp.PUSHV.build(0x0000)) {
                        logger.Warning($"Unexpected instruction at {save_sphere_offset + airship_warp_offset}");
                    }
                    // Board Airship option
                    set(code_ptr, save_sphere_offset + airship_warp_offset, [
                        // call Common.00BB(0);
                        AtelOp.PUSHII  .build(0x0000),
                        AtelOp.CALLPOPA.build(0x00BB),
                        // call Common.00BC(0);
                        AtelOp.PUSHII  .build(0x0000),
                        AtelOp.CALLPOPA.build(0x00BC),
                        // call Common.warpToMap?[010Bh](382, 0); (Airship Menu)
                        AtelOp.PUSHII  .build(   382),
                        AtelOp.PUSHII  .build(     0),
                        AtelOp.CALLPOPA.build(0x010B),
                        ]);
                }

                i += inst.has_operand() ? 3 : 1;
            }
        }

        current_event_name = event_name;
    }

    private static void h_Common_obtainTreasureInit(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int treasure_id = atelStack->values.as_int()[1];
        //ArchipelagoGUI.lastTreasure = treasure_id;
        logger.Debug($"obtain_treasure: {treasure_id}");
        //ArchipelagoClient.sendTreasureLocation(treasure_id);
        FFXArchipelagoClient.sendLocation(treasure_id, ArchipelagoLocationType.Treasure);

        //_Common_obtainTreasureInit.orig_fptr(work, storage, atelStack);
        obtainTreasureInitReimplement(work, storage, atelStack);
    }


    private static void obtainTreasureInitReimplement(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        byte* item_name = (byte*)0;

        int treasure_id = atelStack->pop_int();
        int window_id = atelStack->pop_int();
        logger.Debug($"window_id:{window_id}, treasure_id:{treasure_id}");

        _SndSepPlaySimple(0x80000026);
        AtelWorkerController* pAVar2 = (AtelWorkerController*)_AtelGetCurCtrlWork();
        ((byte*)pAVar2)[3] |= 4;
        _MsFieldItemGet(treasure_id);
        _FUN_008b8910(window_id, 0, 0);
        _FUN_008b8910(window_id, 1, 1);
        bool gear_inv_is_full = false;
        uint weapon_id = 0;
        byte* message_text = _FUN_008bda20(0x401d); // "Nothing"


        if (item_locations.treasure.TryGetValue(treasure_id, out var item)) {
            obtain_item(item.id);

            CustomString name = new CustomString(item.id != 0 ? item.name : $"{item.name} to {item.player}", encodingFlags: FhEncodingFlags.IGNORE_EXPRESSIONS);
            logger.Info(item.name);

            cached_strings.Add(name);
            item_name = name.encoded;
            if (item.id != 0) {
                message_text = _FUN_008bda20(0x4018); // "Obtained %0!"
            } else {
                CustomString sent_text = new CustomString("Sent {VAR:00}!");
                cached_strings.Add(sent_text);
                message_text = sent_text.encoded;
            }
        }
        else
        if (Battle.reward_data->item_count != 0) {
            _TkMsGetRomItem(Battle.reward_data->items[0], (int*)&item_name);
            _FUN_008b8930(window_id, 1, Battle.reward_data->items_amounts[0]);
            if (Battle.reward_data->items_amounts[0] == 1) {
                message_text = _FUN_008bda20(0x4018); // "Obtained %0!"
            }
            else {
                message_text = _FUN_008bda20(0x4019); // "Obtained %0 x%1!"
            }

            byte[] decoded = new byte[FhCharset.compute_decode_buffer_size(new ReadOnlySpan<byte>(item_name, 1000))];

            FhCharset.decode(new ReadOnlySpan<byte>(item_name, 1000), decoded);
            //string decoded = FhCharset.Us.to_string(item_name);
            logger.Info(Encoding.UTF8.GetString(decoded));

            _MsSaveItemUse(Battle.reward_data->items[0], Battle.reward_data->items_amounts[0]);
        }
        else if (Battle.reward_data->key_item_count != 0) {
            item_name = _MsImportantName(Battle.reward_data->key_item);
            message_text = _FUN_008bda20(0x4017); // "Obtained %0!"
            _TkMsImportantSet.hook_fptr(Battle.reward_data->key_item);
        }
        else if (Battle.reward_data->gear_count != 0) {
            weapon_id = Battle.reward_data->gear_inv_idx;
            message_text = _FUN_008bda20(0x4016); // "Obtained %0!"
            Equipment* weapon = (Equipment*)_MsGetSaveWeapon(weapon_id, (nint)(&item_name));
            int inv_id = _FUN_007ab930(weapon); // giveWeapon?
            gear_inv_is_full = inv_id == 0;
        }
        else if (Battle.reward_data->gil != 0) {
            _FUN_008b8930(window_id, 1, (int)Battle.reward_data->gil);
            message_text = _FUN_008bda20(0x401a); // "Obtained %1 Gil!"
            _MsPayGIL(-(int)Battle.reward_data->gil);
        }

        _FUN_008b8930(window_id, 0, (int)item_name);

        atelStack->push_int(window_id);
        atelStack->push_int(0x100);
        atelStack->push_int(0xd0);
        atelStack->push_int(4);
        _CT_RetInt_0065((nint)work, storage, (nint)atelStack);

        TOMesWinWork* mesageWindowWorker = _AtelGetMesWinWork(window_id);
        mesageWindowWorker->_0x20 = 0;
        mesageWindowWorker->text = message_text;
        mesageWindowWorker->_0xc = message_text;
        mesageWindowWorker->_0x16 = 0;
        mesageWindowWorker->_0x18 = 0;

        atelStack->push_int(window_id);
        atelStack->push_int(0);
        _CT_RetInt_006A((nint)work, storage, (nint)atelStack);

        *storage = window_id;
        storage[1] = 1;
        storage[2] = gear_inv_is_full ? 1 : 0;
        storage[3] = (int)weapon_id;

        if (!gear_inv_is_full) {
            _MsBtlGetInit();
        }
        _FUN_0086a0c0();
        mesageWindowWorker->_0x1d |= 0x10;
    }

    private static void h_Common_obtainTreasureSilentlyInit(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int treasure_id = atelStack->values.as_int()[0];
        //ArchipelagoGUI.lastTreasure = treasure_id;
        logger.Debug($"obtain_treasure_silently: {treasure_id}");
        FFXArchipelagoClient.sendLocation(treasure_id, ArchipelagoLocationType.Treasure);
        //_Common_obtainTreasureSilentlyInit.orig_fptr(work, storage, atelStack);
        obtainTreasureSilentlyInitReimplement(work, storage, atelStack);
    }

    private static void obtainTreasureSilentlyInitReimplement(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {

        int treasure_id = atelStack->pop_int();
        logger.Debug($"treasure_id:{treasure_id}");
        _MsFieldItemGet(treasure_id);
        bool gear_inv_is_full = false;
        uint weapon_id = 0;

        if (item_locations.treasure.TryGetValue(treasure_id, out var item)) {
            obtain_item(item.id);
        }
        else
        if (Battle.reward_data->item_count != 0) {
            _MsSaveItemUse(Battle.reward_data->items[0], Battle.reward_data->items_amounts[0]);
        }
        else if (Battle.reward_data->key_item_count != 0) {
            _TkMsImportantSet.hook_fptr(Battle.reward_data->key_item);
        }
        else if (Battle.reward_data->gear_count != 0) {
            weapon_id = Battle.reward_data->gear_inv_idx;
            Equipment* weapon = (Equipment*)_MsGetSaveWeapon(weapon_id, 0);
            int inv_id = _FUN_007ab930(weapon); // giveWeapon?
            gear_inv_is_full = inv_id == 0;
        }
        else if (Battle.reward_data->gil != 0) {
            _MsPayGIL(-(int)Battle.reward_data->gil);
        }

        storage[2] = gear_inv_is_full ? 1 : 0;
        storage[3] = (int)weapon_id;

        if (!gear_inv_is_full) {
            _MsBtlGetInit();
        }
    }

    private static int h_Common_obtainBrotherhoodRetInt(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        logger.Debug($"obtain_brotherhoodRetInt");
        if (atelStack->size > 0) {
            int param = atelStack->pop_int();
            int call_type = param & 0xff;
            if (call_type == 1) {
                logger.Info($"obtain_brotherhood: Set Airship Destinations");
                set_airship_destinations();
            }
            else if (call_type == 2) { // Jump
                int jump_index = param >> 8;
                work->current_thread.pc = (byte*)customScriptHandles[jump_index].AddrOfPinnedObject() - 3;
            }
            else if (call_type == 3) { // Jump if false
                int jump_index = param >> 8;
                int val = atelStack->pop_int();
                if (val == 0) {
                    work->current_thread.pc = (byte*)customScriptHandles[jump_index].AddrOfPinnedObject() - 3;
                }
            }
            else if (call_type == 4) { // Jump if true
                int jump_index = param >> 8;
                int val = atelStack->pop_int();
                if (val == 1) {
                    work->current_thread.pc = (byte*)customScriptHandles[jump_index].AddrOfPinnedObject() - 3;
                }
            }
            else if (call_type == 5) { // Offset
                int offset = atelStack->pop_int();
                work->current_thread.pc += offset - 3;
            }
            else if (call_type == 6) { // Offset if false
                int offset = atelStack->pop_int();
                int val = atelStack->pop_int();
                if (val == 0) {
                    work->current_thread.pc += offset - 3;
                }
            }
            else if (call_type == 7) { // Offset if true
                int offset = atelStack->pop_int();
                int val = atelStack->pop_int();
                if (val == 1) {
                    work->current_thread.pc += offset - 3;
                }
            }
            else if (call_type == 8) { // Update region state
                Vector3 playerPos = Globals.actors->chr_pos_vec.AsVector3();
                var closestEntrance = Atel.controllers[0].worker(0)->script_chunk->map_entrances.ToArray()
                    .Select((entrance, index) => new {Index=index, Entrance=entrance, Distance=(playerPos - entrance.pos).Length()})
                    .MinBy(tuple => tuple.Distance);
                if (closestEntrance?.Distance < 100) {
                    last_entrance_id = (ushort)closestEntrance.Index;
                    logger.Debug($"Entrance within 100: pos:({closestEntrance.Entrance.x}, {closestEntrance.Entrance.y}, {closestEntrance.Entrance.z}) distance:{closestEntrance.Distance}");
                }

                update_region_state(false);
            }
            else if (call_type == 9) { // Lock party member
                int party_member = atelStack->pop_int();
                locked_characters[party_member] = true;
            }
            else if (call_type == 0xA) { // Check if final battle is unlocked
                switch (seed.GoalRequirement) {
                    case GoalRequirement.None:
                        return 1;
                    case GoalRequirement.PartyMembers:
                        if (unlocked_characters.All(c => c.Value)) {
                            return 1;
                        }
                        break;
                    case GoalRequirement.Pilgrimage:
                        if (pilgrimageRegions.All(region => region_states[region].completed_visits > 0)) {
                            return 1;
                        }
                        break;
                }
                return 0;
            }
            else if (call_type == 0xB) { // Replace worker entry point
                int jump_index = param >> 8;
                int entryPoint = atelStack->pop_int();
                int workerIndex = atelStack->pop_int();

                AtelBasicWorker* targetWorker = Atel.controllers[0].worker(workerIndex);

                if (!originalEntryPoints.ContainsKey((workerIndex, entryPoint))) {
                    originalEntryPoints[(workerIndex, entryPoint)] = targetWorker->table_entry_points[entryPoint];
                }

                int targetAddress = (int)customScriptHandles[jump_index].AddrOfPinnedObject();

                int addressOffset = targetAddress - (int)targetWorker->code_ptr;

                targetWorker->table_entry_points[entryPoint] = (uint)addressOffset;
            }
            else if (call_type == 0xC) { // Restore worker entry point
                int entryPoint = atelStack->pop_int();
                int workerIndex = atelStack->pop_int();
                if (originalEntryPoints.TryGetValue((workerIndex, entryPoint), out uint value)) {
                    Atel.controllers[0].worker(workerIndex)->table_entry_points[entryPoint] = value;
                }
            }

            return 1;
        }
        if (item_locations.other.TryGetValue(0, out var item)) {
            obtain_item(item.id);
            return 1;
        }
        return _Common_obtainBrotherhoodRetInt.orig_fptr(work, storage, atelStack);
    }
    private static int h_Common_upgradeBrotherhoodRetInt(nint work, int* storage, nint atelStack) {
        logger.Debug($"upgrade_brotherhoodRetInt");
        if (item_locations.other.TryGetValue(37, out var item)) {
            obtain_item(item.id);
            return 1;
        }

        return _Common_upgradeBrotherhoodRetInt.orig_fptr(work, storage, atelStack);
    }
    private static int h_Common_isBrotherhoodUnpoweredRetInt(nint work, int* storage, nint atelStack) {
        logger.Debug($"isBrotherhoodUnpoweredRetInt");

        return _Common_isBrotherhoodUnpoweredRetInt.orig_fptr(work, storage, atelStack);
    }
    private static int h_Common_grantCelestialUpgrade(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int character = atelStack->values.as_int()[0];
        int level = atelStack->values.as_int()[1];
        logger.Debug($"grant_celestial_upgrade: character={id_to_character[character]}, level={level}");

        return _Common_grantCelestialUpgrade.orig_fptr(work, storage, atelStack);
    }
    private static int h_Common_setPrimerCollected(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int primer = atelStack->values.as_int()[0];
        logger.Debug($"set_primer_collected: Al Bhed Primer {primer + 1}");

        if (item_locations.other.TryGetValue(primer + 1, out var item)) {
            obtain_item(item.id);
        }

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
            current_state.room_id = map_changed ? last_room_id : save_data->current_room_id;
            current_state.entrance = map_changed ? last_entrance_id : save_data->current_spawnpoint;
            current_state.story_progress = save_data->story_progress;
        }
    }

    private static void on_map_change() {
        if (id_to_regions.Contains(save_data->current_room_id)) {

            // New Game
            if (save_data->last_room_id == 0 && save_data->current_room_id == 132) {
                current_region = RegionEnum.DreamZanarkand;
                // Load seed here?
                if (!loadSeed()) {
                    save_data->current_room_id = 23;
                    on_map_change();
                    return;
                }
                foreach (uint item in seed.StartingItems) obtain_item(item);
            }

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
                    save_party();
                    reset_party();

                    /*
                    if (save_data->last_room_id == 382) {
                    }
                     */
                }

            }
        }
        else {
            if (save_data->current_room_id == 23) {
                logger.Debug("Enter main menu");
                // Main Menu
                initalize_states();
                seed = default;
            }
            if (save_data->current_room_id == 382) update_region_state(true); // Airship Menu
            current_region = RegionEnum.None;
            skip_state_updates = false;
        }
    }

    private static int h_Common_transitionToMap(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int map = atelStack->values.as_int()[0];
        int entrance = atelStack->values.as_int()[1];
        logger.Debug($"transition_to_map: map={map}, entrance={entrance}");

        //update_region_state(atelStack);

        return _Common_transitionToMap.orig_fptr(work, storage, atelStack);
    }
    private static int h_Common_warpToMap(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int map = atelStack->values.as_int()[0];
        int entrance = atelStack->values.as_int()[1];
        logger.Debug($"warp_to_map: map={map}, entrance={entrance}");

        // New Game
        //if (save_data->current_room_id == 0 && map == 132) current_region = RegionEnum.DreamZanarkand;

        // Skip most of Dream Zanarkand
        /*
        if (save_data->current_room_id == 376 && map == 371) {
            atelStack->values.as_int()[0] = 382;
            map = atelStack->values.as_int()[0];
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
            atelStack->values.as_int()[0] = 23;
        }

        return _Common_warpToMap.orig_fptr(work, storage, atelStack);
    }

    private static void h_SgEvent_showModularMenuInit(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int menu = atelStack->values.as_int()[0];
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


    public static int h_Common_addPartyMember(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int character = atelStack->values.as_int()[0];

        //if (!party_overridden && !(save_data->atel_is_push_member == 1)) {
        if (!party_overridden) {
            if (!is_character_unlocked(character)) {
                atelStack->pop_int();
                return 1;
            }
        }
        logger.Debug($"add_party_member: character={id_to_character[character]}");

        return _Common_addPartyMember.orig_fptr(work, storage, atelStack);
    }
    public static int h_Common_removePartyMember(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int character = atelStack->values.as_int()[0];

        //if (!party_overridden && !(save_data->atel_is_push_member == 1)) {
        if (!party_overridden) {
            if (is_character_unlocked(character)) {
                atelStack->pop_int();
                return 1;
            }
        }
        logger.Debug($"remove_party_member: character={id_to_character[character]}");

        return _Common_removePartyMember.orig_fptr(work, storage, atelStack);
    }
    public static int h_Common_removePartyMemberLongTerm(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int character = atelStack->values.as_int()[0];

        //if (!party_overridden && !(save_data->atel_is_push_member == 1)) {
        if (!party_overridden) {
            if (is_character_unlocked(character)) {
                atelStack->pop_int();
                return 1;
            }
        }
        logger.Debug($"remove_party_member_long_term: character={id_to_character[character]}");

        return _Common_removePartyMemberLongTerm.orig_fptr(work, storage, atelStack);
    }
    public static int h_Common_setWeaponInvisible(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int character = (byte)atelStack->values.as_int()[0];
        int state = atelStack->values.as_int()[1];
        logger.Debug($"character={id_to_character[character]}, state={state}");
        if (state != 0 && is_character_unlocked(character)) {
            atelStack->pop_int();
            atelStack->pop_int();
            return 0;
        }
        return _Common_setWeaponVisibilty.orig_fptr(work, storage, atelStack);
    }
    public static int h_Common_putPartyMemberInSlot(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        int slot = atelStack->values.as_int()[0];
        int character = (byte)atelStack->values.as_int()[1];
        //if (!party_overridden && !(save_data->atel_is_push_member == 1)) {
        if (!party_overridden) {
            if (character == 0xff || (!is_character_unlocked(character))) {
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
        var field_ptr = Battle.btl->ptr_btl_bin_fields + field_idx * 0xe;
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
        if (encounterToActionDict.TryGetValue(encounter_name, out Action? action)) {
            action();
        }
        else {
            save_party(); // Probably?
            reset_party();
        }
        _MsBattleExe.orig_fptr(param_1, field_idx, group_idx, formation_idx);
    }

    // Battle loop?
    public static void h_FUN_00791820() {
        _FUN_00791820.orig_fptr();
        string encounter_name = Marshal.PtrToStringAnsi((nint)Battle.btl->field_name);
        if (Battle.btl->battle_end_type > 1 && Battle.btl->battle_state == 0x21) {
            logger.Info($"Victory: type={Battle.btl->battle_end_type}, encounter={encounter_name}");
            //if (save_data->atel_is_push_member == 1) {
            if (party_overridden) {
                reset_party();
                // Battle frontline gets copied after this so have to set here
                for (int i = 0; i < 3; i++) {
                    Battle.btl->frontline[i] = save_data->party_order[i];
                }
                for (int i = 0; i < 4; i++) {
                    Battle.btl->backline[i] = save_data->party_order[i + 3];
                }
                party_overridden = false;
            }
            if (encounterVictoryActions.TryGetValue(encounter_name, out Action? action)) {
                action();
            }
            if (encounterToLocationDict.TryGetValue(encounter_name, out int[]? boss_locations)) {
                foreach (int location_id in boss_locations) {
                    sendLocation(location_id, ArchipelagoLocationType.Boss);
                }
            }
        }
    }

    private static void* curr_pos_area_ptr = null;
    public static byte h_MsBtlReadSetScene() {
        byte result = _MsBtlReadSetScene.orig_fptr();
        //ref BtlAreas original_pos_struct = ref *Battle.btl->ptr_pos_def;
        BtlAreasHelper original_pos_struct = new BtlAreasHelper(Battle.btl->ptr_pos_def);
        if (original_pos_struct.areas.Length == 0) return result;
        if (curr_pos_area_ptr != null) {
            NativeMemory.Free(curr_pos_area_ptr);
            curr_pos_area_ptr = null;
            //Marshal.FreeHGlobal((nint)curr_pos_area);
        }
        int total_size = 0;
        int total_info_count = 0;
        for (int i = 0; i < original_pos_struct.areas.Length; i++) {
            total_size += 0x60;
            total_size += Math.Max(original_pos_struct.areas[i].party_pos.Length, (byte)3) * 0x20;
            total_size += Math.Max(original_pos_struct.areas[i].aeon_pos.Length, (byte)0xC) * 0x20;
            total_size += original_pos_struct.areas[i].enemy_pos.Length * 0x20;
            total_size += original_pos_struct.areas[i].some_info.Length * 0x10; // some_info
            total_info_count += original_pos_struct.areas[i].some_info.Length;
            for (int j = 0; j < original_pos_struct.areas[i].some_info.Length; j++) {
                total_size += original_pos_struct.areas[i].something[j].Length * 0x10;
            }
        }
        total_size += 0x10; //chunk_end
        curr_pos_area_ptr = NativeMemory.AllocZeroed((uint)total_size);
        //curr_pos_area = (PosArea*)Marshal.AllocHGlobal(total_size);

        BtlArea* curr_pos_struct_ptr = (BtlArea*)curr_pos_area_ptr;
        BtlAreasHelper curr_pos_struct = new BtlAreasHelper(curr_pos_struct_ptr);
        //
        //ref BtlAreas curr_pos_struct = ref *curr_pos_struct_ptr;
        curr_pos_struct.area_count = original_pos_struct.area_count;
        curr_pos_struct.area_type = original_pos_struct.area_type;

        // Copy
        int some_info_start = original_pos_struct.areas.Length * 0x60;
        int curr_info_count = 0;
        int positions_start = some_info_start + total_info_count * 0x10;
        int curr_position_count = 0;
        for (int i = 0; i < original_pos_struct.areas.Length; i++) {
            //curr_pos_struct.areas[i] = original_pos_struct.areas[i];
            curr_pos_struct.areas[i].btlArea = original_pos_struct.areas[i].btlArea;

            curr_pos_struct.areas[i].btlArea.count_party_pos = Math.Max(curr_pos_struct.areas[i].btlArea.count_party_pos, (byte)3);
            curr_pos_struct.areas[i].btlArea.count_aeon_pos = Math.Max(curr_pos_struct.areas[i].btlArea.count_aeon_pos, (byte)0xc);
            curr_pos_struct.areas[i].btlArea.count_enemy_pos = curr_pos_struct.areas[i].btlArea.count_enemy_pos;

            curr_pos_struct.areas[i].btlArea.offset_party_pos = (uint)(positions_start + curr_position_count * 0x10);
            for (int j = 0; j < curr_pos_struct.areas[i].party_pos.Length; j++) {

                if (original_pos_struct.areas[i].party_pos.Length <= 2 || (original_pos_struct.areas[i].party_pos[0] - original_pos_struct.areas[i].party_pos[original_pos_struct.areas[i].party_pos.Length - 1]).Length() < 10) {
                    // TODO: Calculate positions
                    if (original_pos_struct.areas[i].party_pos.Length == 1 || original_pos_struct.areas[i].party_pos.Length >= 3) {
                        if (j > 2) {
                            //logger.Warning($"Unexpected position: {param_1}, {btl_pos_a}, {btl_pos_b}, {btl_pos_c}");
                        }
                        curr_pos_struct.areas[i].party_pos[j] = original_pos_struct.areas[i].party_pos[0];
                        if (j != 1) {
                            Vector4 enemy_pos = original_pos_struct.areas[i].enemy_pos[0];
                            Vector4 enemy_to_party = curr_pos_struct.areas[i].party_pos[j] - enemy_pos;
                            Vector3 enemy_to_party_3d = new(enemy_to_party.X, enemy_to_party.Y, enemy_to_party.Z);
                            Vector3 rotation_axis = new(0, 1, 0);
                            double angle = j == 0 ? -(Math.PI / 2) : (Math.PI / 2);

                            Vector3 x = Vector3.Cross(rotation_axis, enemy_to_party_3d);
                            Vector3 y = Vector3.Cross(rotation_axis, x);
                            Vector3 rotated = enemy_to_party_3d + (float)Math.Sin(angle) * x + (float)(1 - Math.Cos(angle)) * y;
                            curr_pos_struct.areas[i].party_pos[j] += new Vector4(Vector3.Normalize(rotated) * 30, curr_pos_struct.areas[i].party_pos[j].W);
                        }
                    }
                    else {
                        //if (j > 2) return -1;
                        Vector4 ally_1 = original_pos_struct.areas[i].party_pos[0];
                        Vector4 line = curr_pos_struct.areas[i].party_pos[1] - ally_1;
                        if (line.Length() > 10) {
                            if (j == 2) {
                                curr_pos_struct.areas[i].party_pos[j] = original_pos_struct.areas[i].party_pos[1];
                                curr_pos_struct.areas[i].party_pos[j] += line;
                            }
                            else {
                                curr_pos_struct.areas[i].party_pos[j] = original_pos_struct.areas[i].party_pos[j];
                            }
                        }
                        else if (j != 1) {
                            Vector4 enemy_pos = original_pos_struct.areas[i].enemy_pos[0];
                            Vector4 enemy_to_party = curr_pos_struct.areas[i].party_pos[j] - enemy_pos;
                            Vector3 enemy_to_party_3d = new(enemy_to_party.X, enemy_to_party.Y, enemy_to_party.Z);
                            Vector3 rotation_axis = new(0, 1, 0);
                            double angle = j == 0 ? -(Math.PI / 2) : (Math.PI / 2);

                            Vector3 x = Vector3.Cross(rotation_axis, enemy_to_party_3d);
                            Vector3 y = Vector3.Cross(rotation_axis, x);
                            Vector3 rotated = enemy_to_party_3d + (float)Math.Sin(angle) * x + (float)(1 - Math.Cos(angle)) * y;
                            curr_pos_struct.areas[i].party_pos[j] += new Vector4(Vector3.Normalize(rotated) * 30, curr_pos_struct.areas[i].party_pos[j].W);
                        }
                    }
                }
                else {
                    // Copy
                    curr_pos_struct.areas[i].party_pos[j] = original_pos_struct.areas[i].party_pos[j];
                }
                curr_position_count++;
            }
            curr_pos_struct.areas[i].btlArea.offset_party_run_pos = (uint)(positions_start + curr_position_count * 0x10);
            for (int j = 0; j < curr_pos_struct.areas[i].party_pos.Length; j++) {
                if (original_pos_struct.areas[i].party_pos.Length <= j) {
                    curr_pos_struct.areas[i].party_run_pos[j] = original_pos_struct.areas[i].party_run_pos[0];
                }
                else {
                    curr_pos_struct.areas[i].party_run_pos[j] = original_pos_struct.areas[i].party_run_pos[j];
                }
                if (curr_pos_struct.areas[i].party_run_pos[j] == Vector4.Zero) {
                    curr_pos_struct.areas[i].party_run_pos[j] = original_pos_struct.areas[i].party_pos[0];
                    Vector4 enemy_pos = original_pos_struct.areas[i].enemy_pos[0];
                    Vector4 enemy_to_party = curr_pos_struct.areas[i].party_run_pos[j] - enemy_pos;
                    curr_pos_struct.areas[i].party_run_pos[j] += Vector4.Normalize(enemy_to_party) * 0x30;
                }
                curr_position_count++;
            }

            curr_pos_struct.areas[i].btlArea.offset_aeon_pos = (uint)(positions_start + curr_position_count * 0x10);
            for (int j = 0; j < curr_pos_struct.areas[i].aeon_pos.Length; j++) {
                if (j == 7) {
                    curr_pos_struct.areas[i].aeon_pos[j] = curr_pos_struct.areas[i].party_pos[0];
                }
                else if (j == 9) {
                    curr_pos_struct.areas[i].aeon_pos[j] = curr_pos_struct.areas[i].party_pos[2];
                }
                else {
                    curr_pos_struct.areas[i].aeon_pos[j] = curr_pos_struct.areas[i].party_pos[1];
                }
                curr_position_count++;
            }
            curr_pos_struct.areas[i].btlArea.offset_aeon_run_pos = (uint)(positions_start + curr_position_count * 0x10);
            for (int j = 0; j < curr_pos_struct.areas[i].aeon_pos.Length; j++) {
                curr_pos_struct.areas[i].aeon_run_pos[j] = curr_pos_struct.areas[i].party_run_pos[0];
                curr_position_count++;
            }

            curr_pos_struct.areas[i].btlArea.offset_enemy_pos = (uint)(positions_start + curr_position_count * 0x10);
            for (int j = 0; j < curr_pos_struct.areas[i].enemy_pos.Length; j++) {
                curr_pos_struct.areas[i].enemy_pos[j] = original_pos_struct.areas[i].enemy_pos[j];
                curr_position_count++;
            }
            curr_pos_struct.areas[i].btlArea.offset_enemy_run_pos = (uint)(positions_start + curr_position_count * 0x10);
            for (int j = 0; j < curr_pos_struct.areas[i].enemy_pos.Length; j++) {
                curr_pos_struct.areas[i].enemy_run_pos[j] = original_pos_struct.areas[i].enemy_run_pos[j];
                curr_position_count++;
            }

            curr_pos_struct.areas[i].btlArea.offset_some_info = (uint)(some_info_start + curr_info_count * 0x10);
            for (int j = 0; j < curr_pos_struct.areas[i].some_info.Length; j++) {
                curr_pos_struct.areas[i].some_info[j] = original_pos_struct.areas[i].some_info[j];
                curr_info_count++;
            }
        }

        for (int i = 0; i < original_pos_struct.areas.Length; i++) {
            for (int j = 0; j < curr_pos_struct.areas[i].some_info.Length; j++) {
                curr_pos_struct.areas[i].some_info[j].offset_something = (uint)(positions_start + curr_position_count * 0x10);
                for (int k = 0; k < curr_pos_struct.areas[i].something[j].Length; k++) {
                    curr_pos_struct.areas[i].something[j][k] = original_pos_struct.areas[i].something[j][k];
                    curr_position_count++;
                }
            }
        }
        for (int i = 0; i < original_pos_struct.areas.Length; i++) {
            curr_pos_struct.areas[i].btlArea.offset_chunk_end = (uint)(positions_start + curr_position_count * 0x10);
        }
        *curr_pos_struct.chunk_end = *original_pos_struct.chunk_end;


        Battle.btl->ptr_pos_def = curr_pos_struct_ptr;

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
    private static void h_TkMsImportantSet(uint param_1) {
        logger.Debug($"give_key_item: {param_1}");

        _TkMsImportantSet.orig_fptr(param_1);
    }
    private static byte* h_read_from_bin(int param_1, short* param_2, int param_3) {
        logger.Debug($"get_from_bin: {param_1}, {(int)param_2}, {param_3}");
        logger.Debug($"bin_pointers: {(uint)param_2}, {(uint)(*takara_pointer)}, {(uint)(*buki_get_pointer)}");
        if (param_2 == (short*)*takara_pointer) {
            logger.Debug($"get_from_bin: takara.bin");
        }
        else if (param_2 == (short*)*buki_get_pointer) {
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


    public static void obtain_item(uint item_id, int amount=-1) {
        if (item_id == 0) return;
        var item_type = (item_id & 0xF000) >> 12;
        if (amount == -1) amount = (int)((item_id & 0xFF0000) >> 16);
        item_id &= 0xFFFF;
        switch (item_type) {
            case 0xA:
                // Key Item
                h_TkMsImportantSet(item_id);
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
                new_weapon.equipped_by = 0xff;
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
                var result = _FUN_007ab930(&new_weapon); // giveWeapon?
                if (result != 0) {
                    h_obtain_treasure_cleanup(&rewardData, 7);
                }
                break;
            case 0x1:
                // Gil
                if (amount == -1) amount = (int)((item_id & 0xFF0000) >> 16) * 1000;
                _MsPayGIL(-amount);
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
                unlocked_characters[char_id] = true;
                if (char_id == 0xF) {
                    // Magus sisters
                    unlocked_characters[PlySaveId.PC_MAGUS2] = true;
                    unlocked_characters[PlySaveId.PC_MAGUS3] = true;
                }
                break;
            case 0x9:
                // Trap
                item_id &= 0xfff;
                logger.Debug($"Trap: {item_id}");
                if (item_id == 0) {
                    play_voice_line(136815042); // "Stay away from the summoner"
                }
                break;
        }
    }

    public static void call_obtain_brotherhood() {
        AtelStack stack = new AtelStack();

        int param_1 = 0;
        int param_2 = 0;

        h_Common_obtainBrotherhoodRetInt((AtelBasicWorker*)&param_1, &param_2, &stack);
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
        *airshipDestinationCount = curr - 1;
        *airshipDestinationLength = curr - 1;
        /*
        airshipDestinations[0] = 2;
        airshipDestinations[1] = 6;
        *airshipDestinationCount = 1;
        *airshipDestinationLength = 1;
         */
    }


    // Sphere Grid Experiment

    public static void h_eiAbmParaGet() {
        // TODO: Replace normal calculation with custom Archipelago-based calculation (if option enabled)
        logger.Debug("Calculating stats");
        _eiAbmParaGet.orig_fptr();

        // Guaranteed access to all sphere types
        for (int i = 0; i < 18; i++) {
            save_data->ply_arr[i].abi_map.has_extract_power = true;
            save_data->ply_arr[i].abi_map.has_extract_mana = true;
            save_data->ply_arr[i].abi_map.has_extract_speed = true;
            save_data->ply_arr[i].abi_map.has_extract_ability = true;
        }
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
                AutoAbility* a_ability = (AutoAbility*)_MsGetExcelData(equip->abilities[i] & 0xFFF, Battle.btl->ptr_a_ability_bin, (int*)0x0);
                strength_mult += a_ability->stat_inc_flags.strength() ? a_ability->stat_inc_amount : 0;
                defense_mult += a_ability->stat_inc_flags.defense() ? a_ability->stat_inc_amount : 0;
                magic_mult += a_ability->stat_inc_flags.magic() ? a_ability->stat_inc_amount : 0;
                magic_defense_mult += a_ability->stat_inc_flags.magic_defense() ? a_ability->stat_inc_amount : 0;
                agility_mult += a_ability->stat_inc_flags.agility() ? a_ability->stat_inc_amount : 0;
                luck_mult += a_ability->stat_inc_flags.luck() ? a_ability->stat_inc_amount : 0;
                evasion_mult += a_ability->stat_inc_flags.evasion() ? a_ability->stat_inc_amount : 0;
                accuracy_mult += a_ability->stat_inc_flags.accuracy() ? a_ability->stat_inc_amount : 0;
                hp_mult += a_ability->stat_inc_flags.hp() ? a_ability->stat_inc_amount : 0;
                mp_mult += a_ability->stat_inc_flags.mp() ? a_ability->stat_inc_amount : 0;
                strength_bonus_mult += a_ability->stat_inc_flags.strength_bonus() ? a_ability->stat_inc_amount : 0;
                defense_bonus_mult += a_ability->stat_inc_flags.defense_bonus() ? a_ability->stat_inc_amount : 0;
                magic_bonus_mult += a_ability->stat_inc_flags.magic_bonus() ? a_ability->stat_inc_amount : 0;
                magic_defense_bonus_mult += a_ability->stat_inc_flags.magic_defense_bonus() ? a_ability->stat_inc_amount : 0;
            }
        }

        ply.strength = (byte)Math.Clamp(ply.strength * strength_mult / 100, 0, 255);
        ply.defense = (byte)Math.Clamp(ply.defense * defense_mult / 100, 0, 255);
        ply.magic = (byte)Math.Clamp(ply.magic * magic_mult / 100, 0, 255);
        ply.magic_defense = (byte)Math.Clamp(ply.magic_defense * magic_defense_mult / 100, 0, 255);
        ply.agility = (byte)Math.Clamp(ply.agility * agility_mult / 100, 0, 255);
        ply.luck = (byte)Math.Clamp(ply.luck * luck_mult / 100, 0, 255);
        ply.evasion = (byte)Math.Clamp(ply.evasion * evasion_mult / 100, 0, 255);
        ply.accuracy = (byte)Math.Clamp(ply.accuracy * accuracy_mult / 100, 0, 255);
        ply.hp = (uint)Math.Clamp(ply.hp * hp_mult / 100, 0, ply.auto_ability_effects.has_break_hp_limit ? 99999 : 9999);
        ply.mp = (uint)Math.Clamp(ply.mp * mp_mult / 100, 0, ply.auto_ability_effects.has_break_mp_limit ? 9999 : 999);
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



    // Non-loading FMV
    private static bool h_graphicInitFMVPlayer(int movie_id, int param_2) {
        bool result = _graphicInitFMVPlayer.orig_fptr(movie_id, param_2);
        return !result;
    }

    // Texture experiments
    private static void h_FUN_00656c90(int param_1, int param_2, char* fileName) {
        //logger.Debug($"{param_1}, {param_2}, {(nint)fileName}");
        //
        //string nameString = Marshal.PtrToStringAnsi((nint)fileName);
        //
        //logger.Debug(nameString);


        _FUN_00656c90.orig_fptr(param_1, param_2, fileName);
    }

    //private static void h_FUN_0065ee30(FixedClusterData* data) {
    //    _FUN_0065ee30.orig_fptr(data);
    //
    //    //if (data->_0x0c != 0) {
    //    //    logger.Debug($"Data exists {(uint)data}");
    //    //
    //    //    nint* _this = (nint*)(data->_0x00 + data->_0x10);
    //    //
    //    //    var g_mainTexString = Marshal.StringToHGlobalAnsi("g_mainTex");
    //    //    var g_widthSizeString = Marshal.StringToHGlobalAnsi("g_widthSize");
    //    //    var g_heightSizeString = Marshal.StringToHGlobalAnsi("g_heightSize");
    //    //
    //    //    var g_mainTex    = h_FUN_0056cd50(*_this, g_mainTexString);
    //    //    var g_widthSize  = h_FUN_0056cd50(*_this, g_widthSizeString);
    //    //    var g_heightSize = h_FUN_0056cd50(*_this, g_heightSizeString);
    //    //
    //    //    if (g_mainTex != null) {
    //    //        logger.Debug($"g_mainTex exists {*g_mainTex}");
    //    //        if (g_widthSize != null) {
    //    //            logger.Debug($"g_widthSize exists {*g_widthSize}");
    //    //            if (g_heightSize != null) {
    //    //                logger.Debug($"g_heightSize exists {*g_heightSize}");
    //    //                if (_this[1] != 0) {
    //    //                    nint* _this2 = (nint*)_this[1];
    //    //                    byte[] widthBytes = new byte[g_widthSize->size];
    //    //                    for (int i = 0; i < widthBytes.Length; i++) {
    //    //                        widthBytes[i] = *(byte*)(_this2 + g_widthSize->offset + i);
    //    //                    }
    //    //                    logger.Debug("Copied width");
    //    //                    //FhModule.some_image_tex = g_mainTex;
    //    //                    //FhModule.some_image_dimensions.X = g_widthSize;
    //    //                    //FhModule.some_image_dimensions.Y = g_heightSize;
    //    //                }
    //    //            }
    //    //        }
    //    //    }
    //    //
    //    //    Marshal.FreeHGlobal(g_mainTexString);
    //    //    Marshal.FreeHGlobal(g_widthSizeString);
    //    //    Marshal.FreeHGlobal(g_heightSizeString);
    //    //}
    //}

    public static List<nint> loaded_clusters = [];
    public static PTexture2DBase* loadCluster(string file) {
        //var filePath = Marshal.StringToHGlobalAnsi("/FFX_Data/GameData/PS3Data/map/hiku/hiku22/2d/tex/D3D11/0_11_132_16_12.dds.phyre");
        if (file == "") return null;
        var filePath = Marshal.StringToHGlobalAnsi(file);
        nint clusterManager = FhUtil.get_at<nint>(0x008cca44);
        var cluster = _ClusterManager_loadPCluster(clusterManager, filePath);
        Marshal.FreeHGlobal(filePath);
        if (cluster == 0) return null;
        loaded_clusters.Add(cluster);

        int fixedClusterResult = _Phyre_PFramework_PApplication_FixupClusters(cluster, 1);
        if (fixedClusterResult != 0) return null;

        FixedClusterData data = new();
        data._0x14 = *(nint*)cluster + 0x1c;
        data._0x18 = *(nint*)data._0x14 != data._0x14 ? *(nint*)data._0x14 : 0;
        data._0x1c = (nint)FhUtil.ptr_at<nint>(0x008a3738); // PTexture2DBase vftable?
        _FUN_0065ee30(&data);
        if (data._0x0c == 0) {
            return null;
        }

        PTexture2DBase* _this = (PTexture2DBase*)(data._0x00 + data._0x10);
        return _this;
    }

    public static void releaseCluster(nint cluster) {
        nint clusterManager = FhUtil.get_at<nint>(0x008cca44); //var clusterManager = getClusterManager();
        _ClusterManager_releasePCluster(clusterManager, cluster);

    }


    // Voice related
    //public static nint h_FMOD_EventSystem_load(nint param_1, nint file_path, nint param_3, nint param_4) {
    //    string path = Marshal.PtrToStringAnsi(file_path);
    //    nint result = _FMOD_EventSystem_load.orig_fptr(param_1, file_path, param_3, param_4);
    //    logger.Debug($"{path}, {param_1}, {param_3} -> {result}");

    //    return result;
    //}

    public static void h_FfxFmod_soundInit_setLang(nint ffxFmod, int lang) {
        _FfxFmod_soundInit_setLang.orig_fptr(ffxFmod, lang);
        *(byte*)(ffxFmod + 4) = 0;
    }

    // Unsure if there are side effects
    public static void h_LocalizationManager_Initialize(FFXLocalizationManager* localizationManager) {
        _LocalizationManager_Initialize.orig_fptr(localizationManager);
        localizationManager->video = 0;
        localizationManager->voice = 0;
    }

    public static int h_FmodVoice_dataChange(nint FmodVoice, int event_id, nint param_2) {
        logger.Debug($"{FmodVoice}, {event_id}, {param_2}");
        int result = _FmodVoice_dataChange.orig_fptr(FmodVoice, event_id, param_2);

        string bank_name = "ffx_us_voice03"; // Contains "Stay away from the summoner!" (136815042)
        string path = $"../../../FFX_Data/GameData/PS3Data/Sound_PC/Voice/US/{bank_name}.fev";
        nint file_path = Marshal.StringToHGlobalAnsi(path);

        int bank_index = bank_name[^1] + (bank_name[^2] * 5 - 0x108)*2;
        nint bank = *(int*)(FmodVoice+0x18) + bank_index*4;

        nint load_result = _FMOD_EventSystem_load(param_2, file_path, 0, bank);
        Marshal.FreeHGlobal(file_path);

        if (load_result == 0) {
            int* piVar5 = *(int**)bank;
            if (piVar5 != null) {
                FMOD_Bank_Post_Load _FMOD_Bank_Post_Load = Marshal.GetDelegateForFunctionPointer<FMOD_Bank_Post_Load>(*(nint*)(*piVar5 + 8));

                nint s_voice = Marshal.StringToHGlobalAnsi("voice");
                load_result = _FMOD_Bank_Post_Load((nint)piVar5, s_voice, 0, *(int*)(FmodVoice + 0xc) + bank_index*4);
                Marshal.FreeHGlobal(s_voice);
                logger.Debug($"{bank_name}: {load_result}");
            }
        }

        return result;
    }

    public static void play_voice_line(int voice_line) {
        AtelStack stack = new AtelStack();
        stack.push_int(voice_line);
        logger.Debug($"stack_size = {stack.size}, voice_line = {stack.values.as_int()[0]}");

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

    }


    private static Dictionary<int, CT_Exec>     cached_CT_Execs     = new();
    private static Dictionary<int, CT_RetInt>   cached_CT_RetInts   = new();
    private static Dictionary<int, CT_RetFloat> cached_CT_RetFloats = new();
    public static CT_Exec get_CT_Exec(int id) {
        if (cached_CT_Execs.TryGetValue(id, out var result)) {
            return result;
        }
        AtelCallTargetNamespace nmsp = (AtelCallTargetNamespace)(id >> 0xC);
        AtelCallTarget* internal_ct = nmsp.get_internal() + (id & 0xFFF);

        CT_Exec ct = Marshal.GetDelegateForFunctionPointer<CT_Exec>(internal_ct->ret_float_func);
        cached_CT_Execs[id] = ct;
        return ct;
    }
    public static CT_RetInt get_CT_RetInt(int id) {
        if (cached_CT_RetInts.TryGetValue(id, out var result)) {
            return result;
        }
        AtelCallTargetNamespace nmsp = (AtelCallTargetNamespace)(id >> 0xC);
        AtelCallTarget* internal_ct = nmsp.get_internal() + (id & 0xFFF);

        CT_RetInt ct = Marshal.GetDelegateForFunctionPointer<CT_RetInt>(internal_ct->ret_float_func);
        cached_CT_RetInts[id] = ct;
        return ct;
    }
    public static CT_RetFloat get_CT_RetFloat(int id) {
        if (cached_CT_RetFloats.TryGetValue(id, out var result)) {
            return result;
        }
        AtelCallTargetNamespace nmsp = (AtelCallTargetNamespace)(id >> 0xC);
        AtelCallTarget* internal_ct = nmsp.get_internal() + (id & 0xFFF);

        CT_RetFloat ct = Marshal.GetDelegateForFunctionPointer<CT_RetFloat>(internal_ct->ret_float_func);
        cached_CT_RetFloats[id] = ct;
        return ct;
    }


    // Custom namespace
    public void h_AtelInitTotal() {
        _logger.Debug("Initializing Atel namespaces");
        _AtelInitTotal.orig_fptr();

        AtelSetUpCallFunc(0xF, customNameSpaceHandle.AddrOfPinnedObject());
    }


    static AtelCallTarget[] customNameSpace = {
        new() { exec_func = (nint)(delegate*<AtelBasicWorker*, AtelStack*, int>)(&CT_Exec_F000)},
        new() { ret_int_func = (nint)(delegate* unmanaged[Cdecl]<AtelBasicWorker*, int*, AtelStack*, int>)(&CT_RetInt_F001)},
    };
    static GCHandle customNameSpaceHandle = GCHandle.Alloc(customNameSpace, GCHandleType.Pinned);

    public static int CT_Exec_F000(AtelBasicWorker* work, AtelStack* atelStack) {
        logger.Debug("Call target F000");
        return 0;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static int CT_RetInt_F001(AtelBasicWorker* work, int* storage, AtelStack* atelStack) {
        logger.Debug("Call target F001");
        int high = atelStack->pop_int();
        int low  = atelStack->pop_int();

        atelStack->push_int((high << 16) | low);

        atelStack->types[atelStack->size - 1] = AtelStackType.F32;

        return 1;
    }
}

