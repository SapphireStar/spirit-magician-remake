using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using Google.Protobuf;
using System.Net;
using PbLogin;
using PbDict;
using PbBattle;
public class NetMsgHandlerHelper {

    public static IMessage Deserialize(EDict protoID, byte [] data)
	{
		IMessage result = null;

		
		//Debug.Log("Deserialize(EDict protoID, byte [] data: "+ data.Length);
		switch (protoID)
		{
			case EDict.PbLoginS2CHeartBeat:
				result = S2C_HeartBeat.Parser.ParseFrom(data);
				break;
			case EDict.PbLoginS2CAuth:
				
				result = S2C_Auth.Parser.ParseFrom(data);

				break;
			case EDict.PbLoginS2CCreateRole:

				result = S2C_CreateRole.Parser.ParseFrom(data);
				break;
			case EDict.SharedServerMessage:
				result = Shared.ServerMessage.Parser.ParseFrom(data);
				break;
            case EDict.PbLoginS2CEnterGame:
                result = S2C_EnterGame.Parser.ParseFrom(data);
                break;
            case EDict.PbBattleS2CUpdateBattleAction:
                result = PbBattle.S2C_UpdateBattleAction.Parser.ParseFrom(data);
                break;
            case EDict.PbMatchS2CBattleMatch:
                result = PbMatch.S2C_BattleMatch.Parser.ParseFrom(data);
                break;
            case EDict.PbBattleS2CStartBattle:
                result = S2C_StartBattle.Parser.ParseFrom(data);
                break;
            case EDict.PbBattleS2CEnterBattle:
                result = S2C_EnterBattle.Parser.ParseFrom(data);
                break;
            case EDict.PbBattleS2CBattleAction:
                result = S2C_BattleAction.Parser.ParseFrom(data);
                break;
            case EDict.PbBattleS2CBattleEnd:
                result = S2C_BattleEnd.Parser.ParseFrom(data);
                break;


        }
		return result;
	}

	public static ushort GetProtoID(object obj)
	{
		
		if (obj is C2S_HeartBeat) { return (ushort)EDict.PbLoginC2SHeartBeat; }
		if (obj is C2S_Auth) { return (ushort)EDict.PbLoginC2SAuth; }
		if (obj is C2S_CreateRole) { return (ushort)EDict.PbLoginC2SCreateRole; }
		if (obj is C2S_EnterGame) { return (ushort)EDict.PbLoginC2SEnterGame; }
        if (obj is C2S_BattleAction) { return (ushort)EDict.PbBattleC2SBattleAction; }
        if (obj is PbMatch.C2S_BattleMatch) { return (ushort)EDict.PbMatchC2SBattleMatch; }
        if (obj is C2S_EnterBattle) { return (ushort)EDict.PbBattleC2SEnterBattle; }
        return 0;
	}
}
			