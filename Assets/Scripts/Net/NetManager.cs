using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PbDict;

public enum CLIENT_STATE
{
    START = 0,
    OnConnectLS = 1,
    OnLoginSuc = 2,
    OnDisconnectLS = 3,
    OnConnectGS = 4,
    OnCreateRole = 5,
    OnInGS = 6,
}
public enum CLIENT_ID {
	Game = 1,
	Battle = 2
}

public class ServerConfigData {
	public string Name;
	public string Ip;
	public string Port;
	public ServerConfigData(string name, string ip, string port) {
		this.Name = name;
		this.Ip = ip;
		this.Port = port;
	}
}

public class NetManager : MonoBehaviour
{
    public bool Ticking = true;
    public CLIENT_STATE clientState = CLIENT_STATE.START;

    public NetSocketClient connect = new NetSocketClient();
    public UdpSocketClient battleConnect = new UdpSocketClient();

    public NetMsgHandler pro = new NetMsgHandler();
    public NetMsgHandler battleHandler = new NetMsgHandler();

    public delegate void NetConnectDelegate(bool success);

	public bool Filter;

    private Dictionary<PbDict.EDict, bool> NetLock = new Dictionary<PbDict.EDict, bool>();

    public bool IsLock(PbDict.EDict id)
    {
        if (NetLock.ContainsKey(id))
        {
            return NetLock[id];
        }
        return false;
    }

    public void Lock(PbDict.EDict id) {
        NetLock[id] = true;
    }

    public void Unlock(PbDict.EDict id)
    {
        if (NetLock.ContainsKey(id))
        {
            NetLock[id] = false;
        }
    }
    private static NetManager instance;
    public static NetManager Instance
    {
        get
        {
            if (instance != null)
            {
                instance = GameObject.FindObjectOfType<NetManager>();
            }
            return instance;
        }
    }

    void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
		connect.ID = CLIENT_ID.Game;
		//battleConnect.ID = CLIENT_ID.Battle;
    }

    void Update()
    {
        connect.Run();
        battleConnect.Run();
    }

    void OnDestroy()
    {
        connect.Close();
        //battleConnect.Close();
    }

	public void BeginHeartBeat () {
        connect.isAuthed = true;
	}

//	public virtual void HeartBeat() {
//		PhoenixProto.TickReq req = new PhoenixProto.TickReq ();
//		MemoryStream io = new MemoryStream();
//		ProtoBuf.Serializer.Serialize<PhoenixProto.TickReq>(io, req);
//		Send(io.GetBuffer(), (int)io.Length, (ushort)req.msgID);
//	}
//
//	public virtual void HeartHeatAck(ProtoBuf.IExtensible proto ) {
//		PhoenixProto.TickAck ack = proto as PhoenixProto.TickAck;
//
//	}


    public void ConnectToGSServer(string ip,int port,System.Action<bool> cb = null)
    {
        Debug.Log("connect to game server");
        this.NetLock.Clear();
		connect.Connect(ip, port, pro, cb);
    }

	public void ConnectToBSServer(string ip,int port)
    {
        Debug.Log("connect to battle server");
		//battleConnect.Active = true;
		battleConnect.InitServerInfo(ip, port, battleHandler);
    }

	public void ExitBattle() {
        battleConnect.Active = false;
        //battleConnect.Close ();
	}

    //??????????????????????????????
    public void CloseConnect()
    {
        connect.Close();
        //battleConnect.Close();
    }

    public void Reconnect() {
        if (connect != null) {
            connect.TryReconnect();
        }
    }

	//public bool Send<T>(T proto, Dict.EDict id) where T: Google.Protobuf.IMessage
 //   {
	//	return Send<T> (proto, (ushort)id);
	//}

	public bool Send<T>(T proto) where T : Google.Protobuf.IMessage
    {
		ushort id = NetMsgHandlerHelper.GetProtoID (proto);

        Debug.Log("msgID: " + id);

		if (id == 0) {
			Debug.LogError ("cant find proto" + proto);
		}
		return Send<T> (proto, id);
	}

    //public bool Send<T>(T proto, System.Action<Google.Protobuf.IMessage> ClineTest, Google.Protobuf.IMessage ack) where T : Google.Protobuf.IMessage
    //{
    //    if (ClineTest != null)
    //    {
    //        ClineTest(ack);
    //        return false;
    //    }
    //    return Send(proto);
    //}

    public bool Send<T>(T proto, ushort id) where T: Google.Protobuf.IMessage
    {
        if (!connect.IsConnect())
        {
//			connect.ShowReconnectAlert ();
            return false;
        }
        //        Debug.Log("[send]" + proto);
        //MemoryStream io = new MemoryStream();
        //ProtoBuf.Serializer.Serialize<T>(io, proto);
        return connect.Send(proto, id);
        //return connect.Send(io.GetBuffer(), (int)io.Length, (ushort)id);
    }

    public bool Send(byte[] data , ushort id)
    {
        if (!connect.IsConnect())
        {
            return false;
        }
        return connect.Send(data, id);
    }

    public bool BattleSend<T>(T proto, ushort id)
    {
        //if (!battleConnect.IsConnect())
        //{
        //    return false;
        //}
        //        Debug.Log("[battle send]" + proto);
        //MemoryStream io = new MemoryStream();
        //ProtoBuf.Serializer.Serialize<T>(io, proto);
        //return battleConnect.Send(io.GetBuffer(), (int)io.Length, (ushort)id);
        return false;
    }

	public void RegistListener(PbDict.EDict id, NetMsgHandler.NetEventDelegate cb)
	{
		RegistListener ((int)id, cb);
	}

	public bool RemoveListener(PbDict.EDict id, NetMsgHandler.NetEventDelegate cb)
	{
		return RemoveListener ((int)id, cb);
	}

    public void RegistListener(int id, NetMsgHandler.NetEventDelegate cb)
    {
        pro.RegistListener(id, cb);
        battleHandler.RegistListener(id, cb);
    }

    public bool RemoveListener(int id, NetMsgHandler.NetEventDelegate cb)
    {
        return pro.RemoveListener(id, cb) && battleHandler.RemoveListener(id, cb);
    }

	public bool IsConnected() {
		return connect.Active && connect.IsConnect ();
	}
}
