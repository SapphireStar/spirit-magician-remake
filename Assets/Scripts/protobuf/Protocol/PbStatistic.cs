// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: pb_statistic.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace PbStatistic {

  /// <summary>Holder for reflection information generated from pb_statistic.proto</summary>
  public static partial class PbStatisticReflection {

    #region Descriptor
    /// <summary>File descriptor for pb_statistic.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PbStatisticReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChJwYl9zdGF0aXN0aWMucHJvdG8SDHBiX3N0YXRpc3RpYyIgChFDMlNfU3Rh",
            "dGlzdGljRGF0YRILCgNpZHMYASADKAUiLgoRU3RhdGlzdGljVW5pdEluZm8S",
            "CgoCaWQYASABKAUSDQoFdmFsdWUYAiABKAUiQwoRUzJDX1N0YXRpc3RpY0Rh",
            "dGESLgoFaW5mb3MYASADKAsyHy5wYl9zdGF0aXN0aWMuU3RhdGlzdGljVW5p",
            "dEluZm8qsRIKDFNUQVRJU1RJQ19JRBIICgROT05FEAASDAoIR09MRF9TVU0Q",
            "ARISCg5NQUdJQ19EVVNUX1NVTRACEiAKHEJPVU5UWV9UQVNLX0ZJTklTSF9U",
            "SU1FU19TVU0QAxIWChJLSUxMX01PTlNURVJfQ09VTlQQBBIRCg1QT1dFUl9D",
            "T05TVU1FEAUSEwoPT05MSU5FX1RJTUVfU1VNEAYSHgoaRFVOR0VPTl9MVUNL",
            "Qk9YX09QRU5fVElNRVMQBxIdChlOT1JNQUxfRFVOR0VPTl9QQVNTX1RJTUVT",
            "EAgSIAocRElGRklDVUxUX0RVTkdFT05fUEFTU19USU1FUxAJEiAKHFNQRUVE",
            "X1RZUEVfR09MRF9DT1BQRVJfVElNRVMQChIcChhBUkVOQV9MVUNLQk9YX09Q",
            "RU5fVElNRVMQCxITCg9BUkVOQV9XSU5fVElNRVMQDBIYChRTVEFSX0NIRVNT",
            "X1dJTl9USU1FUxANEhMKD01FTEVFX1dJTl9USU1FUxAOEh0KGU1JUkFDTEVf",
            "VEhFQVRSRV9XSU5fVElNRVMQDxIdChlFVkVOVF9MVUNLX0JPWF9PUEVOX1RJ",
            "TUVTEBASGAoUTElPTl9IRUFSVF9XSU5fVElNRVMQERIUChBaT01CSUVfV0lO",
            "X1RJTUVTEBISEQoNS09GX1dJTl9USU1FUxATEhkKFUdSQVlfQkFUVExFX1dJ",
            "Tl9USU1FUxAUEh8KG1NQRUVEX0RVTkdFT05fUEFTU19USU1FXzIwMRAVEh8K",
            "G1NQRUVEX0RVTkdFT05fUEFTU19USU1FXzIwMhAWEh8KG1NQRUVEX0RVTkdF",
            "T05fUEFTU19USU1FXzIwMxAXEh8KG1NQRUVEX0RVTkdFT05fUEFTU19USU1F",
            "XzIwNBAYEh8KG1NQRUVEX0RVTkdFT05fUEFTU19USU1FXzIwNRAZEiYKIlNQ",
            "RUVEX0RVTkdFT05fUEFTU19USU1FXzIwMV9TRUFTT04QGhImCiJTUEVFRF9E",
            "VU5HRU9OX1BBU1NfVElNRV8yMDJfU0VBU09OEBsSJgoiU1BFRURfRFVOR0VP",
            "Tl9QQVNTX1RJTUVfMjAzX1NFQVNPThAcEiYKIlNQRUVEX0RVTkdFT05fUEFT",
            "U19USU1FXzIwNF9TRUFTT04QHRImCiJTUEVFRF9EVU5HRU9OX1BBU1NfVElN",
            "RV8yMDVfU0VBU09OEB4SGwoXVE9VUk5BTUVOVF9TRUFTT05fR1JBR0UQHxIb",
            "ChdFVkVOVF9HT0xEX0NPUFBFUl9USU1FUxAgEhQKEEJBVFRMRV9XSU5fVElN",
            "RVMQIRIQCgxHT0xEX0NPTlNVTUUQIhIWChJNQUdJQ19EVVNUX0NPTlNVTUUQ",
            "IxIPCgtIT1JTRV9DT1VOVBAkEhEKDVBBUlRORVJfQ09VTlQQJRIVChFNQU5V",
            "RkFDVFVSRV9DT1VOVBAmEhIKDlBJQ0tfT1JFX1RJTUVTECcSFAoQUElDS19H",
            "UkFTU19USU1FUxAoEhMKD1BJQ0tfRklTSF9USU1FUxApEhsKF0RBSUxZX1RB",
            "U0tfRklOSVNIX1RJTUVTECoSEQoNRlJJRU5EU19DT1VOVBArEh8KG05PUk1B",
            "TF9EVU5HRU9OX1BBU1NfVElNRVNfMRAsEh8KG05PUk1BTF9EVU5HRU9OX1BB",
            "U1NfVElNRVNfMxAtEh8KG05PUk1BTF9EVU5HRU9OX1BBU1NfVElNRVNfNRAu",
            "Eh8KG05PUk1BTF9EVU5HRU9OX1BBU1NfVElNRVNfNxAvEh8KG05PUk1BTF9E",
            "VU5HRU9OX1BBU1NfVElNRVNfORAwEiIKHkRJRkZJQ1VMVF9EVU5HRU9OX1BB",
            "U1NfVElNRVNfMhAxEiIKHkRJRkZJQ1VMVF9EVU5HRU9OX1BBU1NfVElNRVNf",
            "NBAyEiIKHkRJRkZJQ1VMVF9EVU5HRU9OX1BBU1NfVElNRVNfNhAzEiIKHkRJ",
            "RkZJQ1VMVF9EVU5HRU9OX1BBU1NfVElNRVNfOBA0EiMKH0RJRkZJQ1VMVF9E",
            "VU5HRU9OX1BBU1NfVElNRVNfMTAQNRIcChhTUEVFRF9EVU5HRU9OX1BBU1Nf",
            "VElNRVMQNhIgChxTUEVFRF9EVU5HRU9OX1BBU1NfVElNRVNfMjAxEDcSIAoc",
            "U1BFRURfRFVOR0VPTl9QQVNTX1RJTUVTXzIwMhA4EiAKHFNQRUVEX0RVTkdF",
            "T05fUEFTU19USU1FU18yMDMQORIgChxTUEVFRF9EVU5HRU9OX1BBU1NfVElN",
            "RVNfMjA0EDoSIAocU1BFRURfRFVOR0VPTl9QQVNTX1RJTUVTXzIwNRA7EhsK",
            "F1RFQU1fRFVOR0VPTl9QQVNTX1RJTUVTEDwSHwobVEVBTV9EVU5HRU9OX1BB",
            "U1NfVElNRVNfMTAxED0SGAoUVE9VUk5BTUVOVF9XSU5fVElNRVMQPhIYChRF",
            "VkVOVF9QTEFZX1dJTl9USU1FUxA/EhkKFUVRVUlQX1NUUkVOR1RIX0xWX1NV",
            "TRBAEhAKDEpFV0VMX0xWX1NVTRBBEhoKFkFMTF9EVU5HRU9OX1BBU1NfVElN",
            "RVMQQhIPCgtDT01CQVRQT1dFUhBDEg4KCkxPR0lOX0RBWVMQRBIfChtTUEVF",
            "RF9EVU5HRU9OX1BBU1NfVElNRV8yMDYQRRIfChtTUEVFRF9EVU5HRU9OX1BB",
            "U1NfVElNRV8yMDcQRhImCiJTUEVFRF9EVU5HRU9OX1BBU1NfVElNRV8yMDZf",
            "U0VBU09OEEcSJgoiU1BFRURfRFVOR0VPTl9QQVNTX1RJTUVfMjA3X1NFQVNP",
            "ThBIEiAKHFNQRUVEX0RVTkdFT05fUEFTU19USU1FU18yMDYQSRIgChxTUEVF",
            "RF9EVU5HRU9OX1BBU1NfVElNRVNfMjA3EEoSIAocTk9STUFMX0RVTkdFT05f",
            "UEFTU19USU1FU18xMxBLEiAKHE5PUk1BTF9EVU5HRU9OX1BBU1NfVElNRVNf",
            "MTUQTBIjCh9ESUZGSUNVTFRfRFVOR0VPTl9QQVNTX1RJTUVTXzE0EE0SIwof",
            "RElGRklDVUxUX0RVTkdFT05fUEFTU19USU1FU18xNhBOEh8KG1RFQU1fRFVO",
            "R0VPTl9QQVNTX1RJTUVTXzEwMhBPQhFaD3BiL3BiX3N0YXRpc3RpY2IGcHJv",
            "dG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::PbStatistic.STATISTIC_ID), }, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::PbStatistic.C2S_StatisticData), global::PbStatistic.C2S_StatisticData.Parser, new[]{ "Ids" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::PbStatistic.StatisticUnitInfo), global::PbStatistic.StatisticUnitInfo.Parser, new[]{ "Id", "Value" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::PbStatistic.S2C_StatisticData), global::PbStatistic.S2C_StatisticData.Parser, new[]{ "Infos" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum STATISTIC_ID {
    [pbr::OriginalName("NONE")] None = 0,
    /// <summary>
    /// ????????????????????????
    /// </summary>
    [pbr::OriginalName("GOLD_SUM")] GoldSum = 1,
    /// <summary>
    /// ????????????????????????
    /// </summary>
    [pbr::OriginalName("MAGIC_DUST_SUM")] MagicDustSum = 2,
    /// <summary>
    /// ????????????????????????
    /// </summary>
    [pbr::OriginalName("BOUNTY_TASK_FINISH_TIMES_SUM")] BountyTaskFinishTimesSum = 3,
    /// <summary>
    /// ??????????????????
    /// </summary>
    [pbr::OriginalName("KILL_MONSTER_COUNT")] KillMonsterCount = 4,
    /// <summary>
    /// ?????????????????????????????????
    /// </summary>
    [pbr::OriginalName("POWER_CONSUME")] PowerConsume = 5,
    /// <summary>
    /// ???????????????
    /// </summary>
    [pbr::OriginalName("ONLINE_TIME_SUM")] OnlineTimeSum = 6,
    /// <summary>
    /// ????????????????????????
    /// </summary>
    [pbr::OriginalName("DUNGEON_LUCKBOX_OPEN_TIMES")] DungeonLuckboxOpenTimes = 7,
    /// <summary>
    /// ????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("NORMAL_DUNGEON_PASS_TIMES")] NormalDungeonPassTimes = 8,
    /// <summary>
    /// ????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("DIFFICULT_DUNGEON_PASS_TIMES")] DifficultDungeonPassTimes = 9,
    /// <summary>
    /// ??????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_TYPE_GOLD_COPPER_TIMES")] SpeedTypeGoldCopperTimes = 10,
    /// <summary>
    /// ???????????????????????????
    /// </summary>
    [pbr::OriginalName("ARENA_LUCKBOX_OPEN_TIMES")] ArenaLuckboxOpenTimes = 11,
    /// <summary>
    /// ?????????????????????
    /// </summary>
    [pbr::OriginalName("ARENA_WIN_TIMES")] ArenaWinTimes = 12,
    /// <summary>
    /// ????????????????????????
    /// </summary>
    [pbr::OriginalName("STAR_CHESS_WIN_TIMES")] StarChessWinTimes = 13,
    /// <summary>
    /// ???????????????????????????
    /// </summary>
    [pbr::OriginalName("MELEE_WIN_TIMES")] MeleeWinTimes = 14,
    /// <summary>
    /// ???????????????????????????
    /// </summary>
    [pbr::OriginalName("MIRACLE_THEATRE_WIN_TIMES")] MiracleTheatreWinTimes = 15,
    /// <summary>
    /// ????????????????????????
    /// </summary>
    [pbr::OriginalName("EVENT_LUCK_BOX_OPEN_TIMES")] EventLuckBoxOpenTimes = 16,
    /// <summary>
    /// ????????????????????????
    /// </summary>
    [pbr::OriginalName("LION_HEART_WIN_TIMES")] LionHeartWinTimes = 17,
    /// <summary>
    /// ??????????????????????????????
    /// </summary>
    [pbr::OriginalName("ZOMBIE_WIN_TIMES")] ZombieWinTimes = 18,
    /// <summary>
    /// ????????????????????????
    /// </summary>
    [pbr::OriginalName("KOF_WIN_TIMES")] KofWinTimes = 19,
    /// <summary>
    /// ????????????????????????
    /// </summary>
    [pbr::OriginalName("GRAY_BATTLE_WIN_TIMES")] GrayBattleWinTimes = 20,
    /// <summary>
    /// ?????????????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIME_201")] SpeedDungeonPassTime201 = 21,
    /// <summary>
    /// ?????????????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIME_202")] SpeedDungeonPassTime202 = 22,
    /// <summary>
    /// ????????????????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIME_203")] SpeedDungeonPassTime203 = 23,
    /// <summary>
    /// ?????????????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIME_204")] SpeedDungeonPassTime204 = 24,
    /// <summary>
    /// ?????????????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIME_205")] SpeedDungeonPassTime205 = 25,
    /// <summary>
    /// ????????????????????????????????????(????????????)
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIME_201_SEASON")] SpeedDungeonPassTime201Season = 26,
    /// <summary>
    /// ????????????????????????????????????(????????????)
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIME_202_SEASON")] SpeedDungeonPassTime202Season = 27,
    /// <summary>
    /// ???????????????????????????????????????(????????????)
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIME_203_SEASON")] SpeedDungeonPassTime203Season = 28,
    /// <summary>
    /// ????????????????????????????????????(????????????)
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIME_204_SEASON")] SpeedDungeonPassTime204Season = 29,
    /// <summary>
    /// ????????????????????????????????????(????????????)
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIME_205_SEASON")] SpeedDungeonPassTime205Season = 30,
    /// <summary>
    /// ?????????????????????
    /// </summary>
    [pbr::OriginalName("TOURNAMENT_SEASON_GRAGE")] TournamentSeasonGrage = 31,
    /// <summary>
    /// ????????????????????????
    /// </summary>
    [pbr::OriginalName("EVENT_GOLD_COPPER_TIMES")] EventGoldCopperTimes = 32,
    /// <summary>
    /// ??????????????????
    /// </summary>
    [pbr::OriginalName("BATTLE_WIN_TIMES")] BattleWinTimes = 33,
    /// <summary>
    /// ??????????????????
    /// </summary>
    [pbr::OriginalName("GOLD_CONSUME")] GoldConsume = 34,
    /// <summary>
    /// ??????????????????
    /// </summary>
    [pbr::OriginalName("MAGIC_DUST_CONSUME")] MagicDustConsume = 35,
    /// <summary>
    /// ??????????????????
    /// </summary>
    [pbr::OriginalName("HORSE_COUNT")] HorseCount = 36,
    /// <summary>
    /// ??????????????????
    /// </summary>
    [pbr::OriginalName("PARTNER_COUNT")] PartnerCount = 37,
    /// <summary>
    /// ??????????????????
    /// </summary>
    [pbr::OriginalName("MANUFACTURE_COUNT")] ManufactureCount = 38,
    /// <summary>
    /// ????????????
    /// </summary>
    [pbr::OriginalName("PICK_ORE_TIMES")] PickOreTimes = 39,
    /// <summary>
    /// ????????????
    /// </summary>
    [pbr::OriginalName("PICK_GRASS_TIMES")] PickGrassTimes = 40,
    /// <summary>
    /// ????????????
    /// </summary>
    [pbr::OriginalName("PICK_FISH_TIMES")] PickFishTimes = 41,
    /// <summary>
    /// ????????????????????????
    /// </summary>
    [pbr::OriginalName("DAILY_TASK_FINISH_TIMES")] DailyTaskFinishTimes = 42,
    /// <summary>
    /// ????????????
    /// </summary>
    [pbr::OriginalName("FRIENDS_COUNT")] FriendsCount = 43,
    /// <summary>
    /// ????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("NORMAL_DUNGEON_PASS_TIMES_1")] NormalDungeonPassTimes1 = 44,
    /// <summary>
    /// ????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("NORMAL_DUNGEON_PASS_TIMES_3")] NormalDungeonPassTimes3 = 45,
    /// <summary>
    /// ???????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("NORMAL_DUNGEON_PASS_TIMES_5")] NormalDungeonPassTimes5 = 46,
    /// <summary>
    /// ????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("NORMAL_DUNGEON_PASS_TIMES_7")] NormalDungeonPassTimes7 = 47,
    /// <summary>
    /// ????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("NORMAL_DUNGEON_PASS_TIMES_9")] NormalDungeonPassTimes9 = 48,
    /// <summary>
    /// ????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("DIFFICULT_DUNGEON_PASS_TIMES_2")] DifficultDungeonPassTimes2 = 49,
    /// <summary>
    /// ????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("DIFFICULT_DUNGEON_PASS_TIMES_4")] DifficultDungeonPassTimes4 = 50,
    /// <summary>
    /// ???????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("DIFFICULT_DUNGEON_PASS_TIMES_6")] DifficultDungeonPassTimes6 = 51,
    /// <summary>
    /// ????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("DIFFICULT_DUNGEON_PASS_TIMES_8")] DifficultDungeonPassTimes8 = 52,
    /// <summary>
    /// ????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("DIFFICULT_DUNGEON_PASS_TIMES_10")] DifficultDungeonPassTimes10 = 53,
    /// <summary>
    /// ?????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIMES")] SpeedDungeonPassTimes = 54,
    /// <summary>
    /// ?????????????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIMES_201")] SpeedDungeonPassTimes201 = 55,
    /// <summary>
    /// ?????????????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIMES_202")] SpeedDungeonPassTimes202 = 56,
    /// <summary>
    /// ????????????????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIMES_203")] SpeedDungeonPassTimes203 = 57,
    /// <summary>
    /// ?????????????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIMES_204")] SpeedDungeonPassTimes204 = 58,
    /// <summary>
    /// ?????????????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIMES_205")] SpeedDungeonPassTimes205 = 59,
    /// <summary>
    /// ?????????????????????????????????
    /// </summary>
    [pbr::OriginalName("TEAM_DUNGEON_PASS_TIMES")] TeamDungeonPassTimes = 60,
    /// <summary>
    /// (????????????)????????????????????????
    /// </summary>
    [pbr::OriginalName("TEAM_DUNGEON_PASS_TIMES_101")] TeamDungeonPassTimes101 = 61,
    /// <summary>
    /// ?????????????????????
    /// </summary>
    [pbr::OriginalName("TOURNAMENT_WIN_TIMES")] TournamentWinTimes = 62,
    /// <summary>
    /// ??????????????????????????????
    /// </summary>
    [pbr::OriginalName("EVENT_PLAY_WIN_TIMES")] EventPlayWinTimes = 63,
    /// <summary>
    /// ????????????????????????
    /// </summary>
    [pbr::OriginalName("EQUIP_STRENGTH_LV_SUM")] EquipStrengthLvSum = 64,
    /// <summary>
    /// ????????????????????????
    /// </summary>
    [pbr::OriginalName("JEWEL_LV_SUM")] JewelLvSum = 65,
    /// <summary>
    /// ????????????????????????
    /// </summary>
    [pbr::OriginalName("ALL_DUNGEON_PASS_TIMES")] AllDungeonPassTimes = 66,
    /// <summary>
    /// ?????????
    /// </summary>
    [pbr::OriginalName("COMBATPOWER")] Combatpower = 67,
    /// <summary>
    /// ??????????????????
    /// </summary>
    [pbr::OriginalName("LOGIN_DAYS")] LoginDays = 68,
    /// <summary>
    /// ?????????????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIME_206")] SpeedDungeonPassTime206 = 69,
    /// <summary>
    /// ?????????????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIME_207")] SpeedDungeonPassTime207 = 70,
    /// <summary>
    /// ????????????????????????????????????(????????????)
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIME_206_SEASON")] SpeedDungeonPassTime206Season = 71,
    /// <summary>
    /// ????????????????????????????????????(????????????)
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIME_207_SEASON")] SpeedDungeonPassTime207Season = 72,
    /// <summary>
    /// ?????????????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIMES_206")] SpeedDungeonPassTimes206 = 73,
    /// <summary>
    /// ?????????????????????????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("SPEED_DUNGEON_PASS_TIMES_207")] SpeedDungeonPassTimes207 = 74,
    /// <summary>
    /// ????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("NORMAL_DUNGEON_PASS_TIMES_13")] NormalDungeonPassTimes13 = 75,
    /// <summary>
    /// ????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("NORMAL_DUNGEON_PASS_TIMES_15")] NormalDungeonPassTimes15 = 76,
    /// <summary>
    /// ????????????????????????????????????
    /// </summary>
    [pbr::OriginalName("DIFFICULT_DUNGEON_PASS_TIMES_14")] DifficultDungeonPassTimes14 = 77,
    /// <summary>
    /// ????????????????????????????????????	
    /// </summary>
    [pbr::OriginalName("DIFFICULT_DUNGEON_PASS_TIMES_16")] DifficultDungeonPassTimes16 = 78,
    /// <summary>
    /// (????????????)????????????????????????	
    /// </summary>
    [pbr::OriginalName("TEAM_DUNGEON_PASS_TIMES_102")] TeamDungeonPassTimes102 = 79,
  }

  #endregion

  #region Messages
  public sealed partial class C2S_StatisticData : pb::IMessage<C2S_StatisticData> {
    private static readonly pb::MessageParser<C2S_StatisticData> _parser = new pb::MessageParser<C2S_StatisticData>(() => new C2S_StatisticData());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<C2S_StatisticData> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PbStatistic.PbStatisticReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public C2S_StatisticData() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public C2S_StatisticData(C2S_StatisticData other) : this() {
      ids_ = other.ids_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public C2S_StatisticData Clone() {
      return new C2S_StatisticData(this);
    }

    /// <summary>Field number for the "ids" field.</summary>
    public const int IdsFieldNumber = 1;
    private static readonly pb::FieldCodec<int> _repeated_ids_codec
        = pb::FieldCodec.ForInt32(10);
    private readonly pbc::RepeatedField<int> ids_ = new pbc::RepeatedField<int>();
    /// <summary>
    /// ???????????????id(?????????????????????id)
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<int> Ids {
      get { return ids_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as C2S_StatisticData);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(C2S_StatisticData other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!ids_.Equals(other.ids_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= ids_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      ids_.WriteTo(output, _repeated_ids_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += ids_.CalculateSize(_repeated_ids_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(C2S_StatisticData other) {
      if (other == null) {
        return;
      }
      ids_.Add(other.ids_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10:
          case 8: {
            ids_.AddEntriesFrom(input, _repeated_ids_codec);
            break;
          }
        }
      }
    }

  }

  /// <summary>
  /// ??????????????????
  /// </summary>
  public sealed partial class StatisticUnitInfo : pb::IMessage<StatisticUnitInfo> {
    private static readonly pb::MessageParser<StatisticUnitInfo> _parser = new pb::MessageParser<StatisticUnitInfo>(() => new StatisticUnitInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<StatisticUnitInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PbStatistic.PbStatisticReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public StatisticUnitInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public StatisticUnitInfo(StatisticUnitInfo other) : this() {
      id_ = other.id_;
      value_ = other.value_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public StatisticUnitInfo Clone() {
      return new StatisticUnitInfo(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    /// <summary>
    /// ??????id
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "value" field.</summary>
    public const int ValueFieldNumber = 2;
    private int value_;
    /// <summary>
    /// ?????????
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Value {
      get { return value_; }
      set {
        value_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as StatisticUnitInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(StatisticUnitInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Value != other.Value) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (Value != 0) hash ^= Value.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (Value != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Value);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (Value != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Value);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(StatisticUnitInfo other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.Value != 0) {
        Value = other.Value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 16: {
            Value = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class S2C_StatisticData : pb::IMessage<S2C_StatisticData> {
    private static readonly pb::MessageParser<S2C_StatisticData> _parser = new pb::MessageParser<S2C_StatisticData>(() => new S2C_StatisticData());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<S2C_StatisticData> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PbStatistic.PbStatisticReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public S2C_StatisticData() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public S2C_StatisticData(S2C_StatisticData other) : this() {
      infos_ = other.infos_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public S2C_StatisticData Clone() {
      return new S2C_StatisticData(this);
    }

    /// <summary>Field number for the "infos" field.</summary>
    public const int InfosFieldNumber = 1;
    private static readonly pb::FieldCodec<global::PbStatistic.StatisticUnitInfo> _repeated_infos_codec
        = pb::FieldCodec.ForMessage(10, global::PbStatistic.StatisticUnitInfo.Parser);
    private readonly pbc::RepeatedField<global::PbStatistic.StatisticUnitInfo> infos_ = new pbc::RepeatedField<global::PbStatistic.StatisticUnitInfo>();
    /// <summary>
    /// ????????????
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::PbStatistic.StatisticUnitInfo> Infos {
      get { return infos_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as S2C_StatisticData);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(S2C_StatisticData other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!infos_.Equals(other.infos_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= infos_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      infos_.WriteTo(output, _repeated_infos_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += infos_.CalculateSize(_repeated_infos_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(S2C_StatisticData other) {
      if (other == null) {
        return;
      }
      infos_.Add(other.infos_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            infos_.AddEntriesFrom(input, _repeated_infos_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
