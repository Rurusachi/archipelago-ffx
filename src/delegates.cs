using Fahrenheit.Core;
using Fahrenheit.Core.FFX;
using Fahrenheit.Core.FFX.Atel;
using Fahrenheit.Core.FFX.Battle;
using System.Numerics;
using System.Runtime.InteropServices;
using static Fahrenheit.Modules.ArchipelagoFFX.Client.ArchipelagoClient;

namespace Fahrenheit.Modules.ArchipelagoFFX;
public static unsafe class delegates {

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
    public delegate void Common_obtainBrotherhood(AtelBasicWorker* work, int* storage, AtelStack* atelStack);

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
    public delegate int FUN_0085fb60(AtelBasicWorker* work, int* storage, AtelStack* atelStack);
    // Common.01D1Exec
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool FUN_0085fdb0(int* param_1, int* param_2);


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

    // giveKeyItem
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_0088e700(uint param_1);

    // takeGil
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00785a60(int param_1);


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
}
