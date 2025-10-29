﻿using Fahrenheit.Core;
using Fahrenheit.Core.FFX;
using Fahrenheit.Core.Atel;
using Fahrenheit.Core.FFX.Battle;
using Hexa.NET.DirectXTex;
using System.Numerics;
using System.Runtime.InteropServices;
using static Fahrenheit.Core.FFX.FhCall;

namespace Fahrenheit.Modules.ArchipelagoFFX;
public static unsafe class delegates {


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void AtelInitTotal();
    public const nint __addr_AtelInitTotal = 0x0046d660;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void AtelSetUpCallFunc(int nameSpaceId, nint nameSpacePtr);
    public const nint __addr_AtelSetUpCallFunc = 0x00477800;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int   CT_Exec    (AtelBasicWorker* work, AtelStack* atelStack);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int   CT_RetInt  (AtelBasicWorker* work, int* storage, AtelStack* atelStack);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float CT_RetFloat(AtelBasicWorker* work, int* storage, AtelStack* atelStack);



    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate char* AtelGetEventName(uint event_id);

    // AtelStackPop 
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int AtelStackPop(int* param_1, AtelStack* atelStack);

    // Common.obtainTreasureInit
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void Common_obtainTreasureInit(AtelBasicWorker* work, int* storage, AtelStack* atelStack);

    // Common.obtainTreasureSilentlyInit
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void Common_obtainTreasureSilentlyInit(AtelBasicWorker* work, int* storage, AtelStack* atelStack);

    // Common.obtainBrotherhood
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_obtainBrotherhoodRetInt(AtelBasicWorker* work, int* storage, AtelStack* atelStack);

    // Common.grantCelestialUpgrade
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_grantCelestialUpgrade(AtelBasicWorker* work, int* storage, AtelStack* atelStack);

    // Common.setPrimerCollected
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_setPrimerCollected(AtelBasicWorker* work, int* storage, AtelStack* atelStack);

    // Common.transitionToMap
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_transitionToMap(AtelBasicWorker* work, int* storage, AtelStack* atelStack);

    // Common.warpToMap
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_warpToMap(AtelBasicWorker* work, int* storage, AtelStack* atelStack);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SgEvent_showModularMenuInit(AtelBasicWorker* work, int* storage, AtelStack* atelStack);
    public const nint __addr_SgEvent_showModularMenuInit = 0x00678210;


    // Common.playFieldVoiceLineInit
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_playFieldVoiceLineInit(AtelBasicWorker* work, int* storage, AtelStack* atelStack);
    // Common.playFieldVoiceLineExec
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_playFieldVoiceLineExec(AtelBasicWorker* param_1, AtelStack* param_2);
    // Common.playFieldVoiceLineResultInt
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_playFieldVoiceLineResultInt(AtelBasicWorker* work, int* storage, AtelStack* atelStack);


    // Common.00D6Init
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_00D6Init(AtelBasicWorker* work, int* storage, AtelStack* atelStack);
    // Common.00D6eExec
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_00D6eExec(AtelBasicWorker* param_1, AtelStack* param_2);
    // Common.00D6ResultInt
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_00D6ResultInt(AtelBasicWorker* work, int* storage, AtelStack* atelStack);



    // Common.01D1Init
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_01D1Init(AtelBasicWorker* work, int* storage, AtelStack* atelStack);
    public const nint __addr_Common_01D1Init = 0x0045fb60;
    // Common.01D1Exec
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool Common_01D1Exec(AtelBasicWorker* param_1, AtelStack* param_2);
    public const nint __addr_Common_01D1Exec = 0x0045fdb0;


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_addPartyMember(AtelBasicWorker* work, int* storage, AtelStack* atelStack);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_removePartyMember(AtelBasicWorker* work, int* storage, AtelStack* atelStack);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_removePartyMemberLongTerm(AtelBasicWorker* work, int* storage, AtelStack* atelStack);
    // Common.setWeaponVisibilty (Common.0240)
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_setWeaponInvisible(AtelBasicWorker* work, int* storage, AtelStack* atelStack);

    // Common.putPartyMemberInSlot
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_putPartyMemberInSlot(AtelBasicWorker* work, int* storage, AtelStack* atelStack);


    // Common.pushParty
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_pushParty(AtelBasicWorker* work, int* storage, AtelStack* atelStack);

    // Common.popParty
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_popParty(AtelBasicWorker* work, int* storage, AtelStack* atelStack);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsBattleLabelExe(uint encounter_id, byte param_2, byte screen_transition);
    public const nint __addr_MsBattleLabelExe = 0x00381d60;

    // EndOfBattle?
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void FUN_00791820();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsBtlGetPos(int param_1, Chr* chr, int btl_pos_a, int btl_pos_b, int btl_pos_c, Vector4* out_pos);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate byte MsBtlReadSetScene();


    // giveItem
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint FUN_007905a0(uint param_1, int param_2);


    // readFromBin
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* FUN_007ab890(int param_1, short* param_2, int param_3);

    // getWeaponName
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort FUN_007a0d10(Equipment* param_1);
    // getWeaponModel
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_007a0c70(ushort name_id, byte owner, int unknown, ushort* model_id_pointer);
    // giveWeapon
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_007ab930(Equipment* param_1);
    // obtainTreasureCleanup
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_007993f0(BtlRewardData* param_1, int param_2);


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00a48910(uint chr_id, int node_idx);



    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate bool openFile(nint _this, nint filename, bool readOnly, nint unknown_1, nint unknown_2, nint unknown_3);

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate uint readFile(nint _this, nint buffer, uint max_len);

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate uint FUN_0070aec0(nint _this, uint voice_id, uint param_2);
    


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_loadModel(AtelBasicWorker* work, int* storage, AtelStack* atelStack);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_linkFieldToBattleActor(AtelBasicWorker* work, int* storage, AtelStack* atelStack);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Common_0043(AtelBasicWorker* work, int* storage, AtelStack* atelStack);






    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Map_800F(AtelBasicWorker* work, int* storage, AtelStack* atelStack);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* FUN_0086bec0(int param_1);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate short FUN_0086bea0(int param_1);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void FUN_00656c90(int param_1, int param_2, char* fileName);

    [StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x74)]
    public struct PTexture2DBase {
        [FieldOffset(0x00)] public nint            unknown1;
        [FieldOffset(0x0c)] public nint            unknown2;
        [FieldOffset(0x10)] public int             mipCount;
        [FieldOffset(0x14)] public int             flags;
        [FieldOffset(0x1c)] public uint            width;
        [FieldOffset(0x20)] public uint            height;
        [FieldOffset(0x24)] public int             num_buffers; //??
        [FieldOffset(0x28)] public ID3D11Resource* buffer;
        [FieldOffset(0x70)] public int             isBound;

    }

    [StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x800)]
    public struct PhyFMVPlayerManager {
        [FieldOffset(0x38c)] public PTexture2DBase fmv_texture1;
        [FieldOffset(0x400)] public PTexture2DBase fmv_texture2;
        [FieldOffset(0x474)] public PTexture2DBase fmv_texture3;
        [FieldOffset(0x4f0)] public nint                           fmv_path;

        [FieldOffset(0x6e4)] public byte                           _0x6e4;
        [FieldOffset(0x6fc)] public byte                           _0x6fc;
        [FieldOffset(0x6fd)] public byte                           _0x6fd;
    }

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate int PhyFMVPlayerManager_initialize(PhyFMVPlayerManager* phyFMVPlayerManager, int movie_id, byte zero, byte param_3);
    public const    nint __addr_PhyFMVPlayerManager_initialize = 0x002d9db0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool graphicInitFMVPlayer(int movie_id, int param_2);
    public const    nint __addr_graphicInitFMVPlayer = 0x00241840;


    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate int PhyreScene_loadVFXTexture(nint phyreScene, int param_1, byte* param_2, char param_3);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PhyreScene_onVFXTextureLoaded(nint param_1, int* param_2);

    public struct returnedTexData {
        public short unknown1;
        public short type;
        public nint  dataName;
        public short offset;
        public short size;
        public nint  unknown2;
    }
    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate returnedTexData* FUN_0056cd50(nint _this, nint dataName);

    public struct FixedClusterData {
        public nint _0x00; // Texture data offsets?
        public nint _0x04; // Texture data?
        public nint _0x08;
        public nint _0x0c;
        public nint _0x10;
        public nint _0x14;
        public nint _0x18;
        public nint _0x1c;
    }

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate void FUN_0065ee30(FixedClusterData* param_1);
    public const    nint __addr_ClusterManager_FUN_0065ee30 = 0x0025ee30;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Phyre_PFramework_PApplication_FixupClusters(nint cluster, int param_1);
    public const    nint __addr_Phyre_PFramework_PApplication_FixupClusters = 0x00223740;

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate nint ClusterManager_loadPCluster(nint _this, nint filePath);
    public const    nint __addr_ClusterManager_loadPCluster = 0x0029ba80;

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate void ClusterManager_releasePCluster(nint _this, nint cluster);
    public const    nint __addr_ClusterManager_releasePCluster = 0x0029bef0;

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate nint ClusterManager_getPClusterByName(nint _this, nint filePath);











    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x2c)]
    public struct TOMesWinWork {
        [FieldOffset(0x08)] public byte* text;
        [FieldOffset(0x0c)] public byte* _0xc;
        [FieldOffset(0x14)] public short status;

        [FieldOffset(0x16)] public short _0x16;

        [FieldOffset(0x18)] public int   _0x18;
        [FieldOffset(0x1d)] public byte  _0x1d;
        [FieldOffset(0x20)] public byte  _0x20;

    }

    // obbtainTreasure related
    public delegate void          FUN_008b8910(int window_idx, int variable_idx, int type); // setMessageWindowVariableType (0: text*, 1: int)
    public const    nint          __addr_FUN_008b8910 = 0x004b8910;

    public delegate byte*         FUN_008bda20(uint window_idx); // getMenuText
    public const    nint          __addr_FUN_008bda20 = 0x004bda20;

    public delegate void          FUN_008b8930(int window_idx, int variable_idx, int value); // setMessageWindowVariable
    public const    nint          __addr_FUN_008b8930 = 0x004b8930;

    public delegate void          FUN_0086a0c0();
    public const    nint          __addr_FUN_0086a0c0 = 0x0046a0c0;

    public delegate TOMesWinWork* AtelGetMesWinWork(int idx);
    public const    nint          __addr_AtelGetMesWinWork = 0x0046be20;


    // Voice related
    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate int FmodVoice_dataChange(nint FmodVoice, int event_id, nint param_2);
    public const nint __addr_FmodVoice_dataChange = 0x0030a720;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate nint FMOD_EventSystem_load(nint param_1, nint file_path, nint param_3, nint bank);
    public const nint __addr_FMOD_EventSystem_load = 0x70C75C;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate nint FMOD_Bank_Post_Load(nint param_1, nint param_2, nint param_3, nint param_4);


    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate void FfxFmod_soundInit_setLang(nint ffxFmod, int lang);
    public const nint __addr_FfxFmod_soundInit_setLang = 0x0030b4e0;


    [StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x1C)]
    public struct FFXLocalizationManager {
        [FieldOffset(0x00)] public int video; // Also voice?
        [FieldOffset(0x04)] public int text;  // Probably
        [FieldOffset(0x08)] public int voice; // Unused?
    }

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate void LocalizationManager_Initialize(FFXLocalizationManager* localizationManager);
    public const nint __addr_LocalizationManager_Initialize = 0x002db1c0;

    public unsafe delegate FFXLocalizationManager* LocalizationManager_GetInstance();
    public const nint __addr_LocalizationManager_GetInstance = 0x002db1a0;


    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate void FfxFmod_soundInit(nint ffxFmod);
    public const nint __addr_FfxFmod_soundInit = 0x00307170;

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate void FmodVoice_initList(nint fmodVoice);
    public const nint __addr_FmodVoice_initList = 0x0030ac80;




    // Temporary
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void AtelEventSetUp(int event_id);
    public const nint __addr_AtelEventSetUp = 0x472E90;

    // getCurrentPartySlots
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MsGetSavePartyMember(uint* param_1, uint* param_2, uint* param_3);
    public const nint __addr_MsGetSavePartyMember = 0x3853B0;


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CT_RetInt_01B6(nint work, int* storage, nint atelStack);

    public static int __addr_CT_RetInt_01B6 = 0x004594d0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CT_RetInt_01B7(nint work, int* storage, nint atelStack);

    public static int __addr_CT_RetInt_01B7 = 0x004596a0;


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void eiAbmParaGet();

    public static int __addr_eiAbmParaGet = 0x00654860;


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsApUp(int chr_id, Chr* chr, int base_ap_add, uint param_4);
    public const nint __addr_MsApUp = 0x00398A10;


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TkMsImportantSet(uint param_1);
    public const nint __addr_TkMsImportantSet = 0x0048e700;


    public delegate void MsFieldItemGet(int treasure_id);
    public const nint __addr_MsFieldItemGet = 0x00398fe0;

    public delegate void CT_RetInt_0065(nint work, int* storage, nint atelStack);
    public const nint __addr_CT_RetInt_0065 = 0x00457f60;

    public delegate void CT_RetInt_006A(nint work, int* storage, nint atelStack);
    public const nint __addr_CT_RetInt_006A = 0x004589f0;


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate nint AtelGetCurCtrlWork();
    public const nint __addr_AtelGetCurCtrlWork = 0x46AF80;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int MsPayGIL(int change);
    public const nint __addr_MsPayGIL = 0x385A60;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void SndSepPlaySimple(uint param_1);
    public const nint __addr_SndSepPlaySimple = 0x486DE0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate nint MsGetSaveWeapon(uint gear_inv_idx, nint ref_name);
    public const nint __addr_MsGetSaveWeapon = 0x3ABBF0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void MsSetSaveParam(uint chr_id);
    public const nint __addr_MsSetSaveParam = 0x3861B0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void MsBattleExe(uint param_1, int field_idx, int group_idx, int formation_idx);
    public const nint __addr_MsBattleExe = 0x3810F0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void TkMsGetRomItem(uint param_1, int* param_2);
    public const nint __addr_TkMsGetRomItem = 0x4AB230;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void MsSaveItemUse(uint item_id, int amount);
    public const nint __addr_MsSaveItemUse = 0x3905A0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte* MsImportantName(uint key_item_idx);
    public const nint __addr_MsImportantName = 0x3908B0;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate nint MsBtlListGroup(int field_idx, int group_idx);
    public const nint __addr_MsBtlListGroup = 0x39D230;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate nint MsGetExcelData(int req_elem_idx, nint excel_data_ptr, int* ref_data_end);
    public const nint __addr_MsGetExcelData = 0x3AB890;
}

