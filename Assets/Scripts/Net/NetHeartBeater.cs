using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;


public class NetHeartBeater {
	public NetSocketClient Master;

	public NetMsgHandler MasterHandler;


	public void SetUp() {
		MasterHandler.RegistListener ((ushort)PbDict.EDict.PbLoginS2CHeartBeat, masterTick);
	}

	public void Send() {
		SendTick (Master);
	}

	public void SendTick(NetSocketClient client){
		if (client != null) {
			Debug.Log("NetHeartBeater.SendTick - C2S_HeartBeat");
			PbLogin.C2S_HeartBeat req = new PbLogin.C2S_HeartBeat();
			client.Send(req, (ushort)PbDict.EDict.PbLoginC2SHeartBeat);
		}
	}

	private void masterTick(Google.Protobuf.IMessage proto){
		
	}
}
