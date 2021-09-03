using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using Google.Protobuf;
using PbDict;

public class NetMessage {
	public int ID;
	public IMessage Proto;
    public byte[] LuaBytes;
    public int seq;
}

public class NetMsgHandler
{
    private const int PACK_HEADER_LEN = 8;
    private const int SEQ_LEN = 4;

    public delegate void NetEventDelegate(IMessage obj);

    private Dictionary<int, List<NetEventDelegate>> callbackInfo = new Dictionary<int, List<NetEventDelegate>>();


    //public Dictionary<int, T> quickHandle = new Dictionary<int, T>();

    public void RegistListener(int id, NetEventDelegate cb)
    {
        if (!callbackInfo.ContainsKey(id))
        {
            callbackInfo[id] = new List<NetEventDelegate>();
        }
        callbackInfo[id].Add(cb);
    }

	public void AddListener(int id, NetEventDelegate cb)
	{
		if (!callbackInfo.ContainsKey(id))
		{
			callbackInfo[id] = new List<NetEventDelegate>();
		}
		callbackInfo[id].Add(cb);
	}

    public bool RemoveListener(int id, NetEventDelegate cb)
    {
        if (callbackInfo.ContainsKey(id))
        {
            return callbackInfo[id].Remove(cb);
        }
        return false;
    }

    public void TriggerEvent(int id, Google.Protobuf.IMessage protoData)
    {
        Debug.Log("TriggerEvent list, cmd id:" + id);
        if (callbackInfo.ContainsKey(id))
        {
            List<NetEventDelegate> list = callbackInfo[id];
//			#if UNITY_EDITOR
//            if (list.Count != 1)
//            {
//                Debug.LogError("TriggerEvent list:" + list.Count + " " + id);
//            }
//			#endif

            //??????????????????
            for (int i = 0; i < list.Count; i++)
            {
                list[i](protoData);
            }
        }
        else
        {
            //if (id == (uint)PhoenixProto.MSGID.Role_Appear_Ntf_ID ||
            //    id == (uint)PhoenixProto.ToyWorldMSGID.Self_Info_Ntf_ID ||
            //    id == (uint)PhoenixProto.ToyWorldMSGID.Monster_Appear_Ntf_ID ||
            //    id == (uint)PhoenixProto.ToyWorldMSGID.MagicItem_Appear_Ntf_ID ||
            //    id == (uint)PhoenixProto.ToyWorldMSGID.Obj_DisAppear_Ntf_ID ||
            //    id == (uint)PhoenixProto.ToyWorldMSGID.Obj_Move_Ntf_ID ||
            //    id == (uint)PhoenixProto.ToyWorldMSGID.Creature_Die_Buff_Ntf_ID ||
            //    id == (uint)PhoenixProto.ToyWorldMSGID.Creature_HP_Review_Ntf_ID ||
            //    id == (uint)PhoenixProto.ToyWorldMSGID.Creature_Damage_Ntf_ID)
            //{
            //    Debug.LogError("cant find TriggerEvent " + id);
            //}
        }
    }

    protected MSG_HEAD head = new MSG_HEAD();
    //    public int Handle(byte[] recvbuf, int size, NetSocketClient peer)
    //    {
    //        int indent = 0;
    //		if (size < 6)
    //		{
    //			return 0;
    //		}
    //		//MSG_HEAD head = new MSG_HEAD();
    //		indent = head.Parse(recvbuf);
    //		if (indent == -1)
    //		{
    //			//UnityEngine.Debug.Log("parse head failed");
    //			return -1;
    //		}

    //		if (head.Len + 6 > size)
    //		{
    //			return 0;
    //		}

    //		using (MemoryStream stream = new MemoryStream (recvbuf, 6, (int)head.Len)) {

    ////			Debug.LogError ("protoData" + protoData + " " + recvbuf.Length + " " + head.Len);

    //			var protoData = NetMsgHandlerHelper.Deserialize(head.ID, stream);
    //			TriggerEvent (head.ID, protoData);
    //			stream.Dispose();
    //			stream.Close ();
    //		}
    //		indent = (int)(head.Len + 6);
    //        return indent;
    //    }


    public NetMessage Read(byte[] recvbuf, int size)
    {

        if (size < PACK_HEADER_LEN)
        {
            UnityEngine.Debug.LogError("read buffer error 1");
            return default(NetMessage);
        }

        head.Parse(recvbuf);

        if (head.Len + PACK_HEADER_LEN > size)
        {
            UnityEngine.Debug.LogError("read buffer error 2");
            return default(NetMessage);
        }


        if (head.Len + SEQ_LEN > size)
        {
            UnityEngine.Debug.LogError("read buffer error 3");
            return default(NetMessage);
        }

        //Special case for server side HeartBeat
        if (head.Len < SEQ_LEN && head.ID == (int)PbInternal.CMD.InternalServiceToServiceHeartbeat)
        {
            NetMessage netMsg = new NetMessage();
            netMsg.ID = (int)PbDict.EDict.PbLoginS2CHeartBeat;
            netMsg.seq = 0;
            netMsg.Proto = new PbLogin.S2C_HeartBeat();

            return netMsg;
        }


        byte[] seqBytes = new byte[SEQ_LEN];

        byte[] pbBytes = new byte[head.Len - SEQ_LEN];
        Array.Copy(recvbuf, PACK_HEADER_LEN, seqBytes, 0, SEQ_LEN);
        Array.Copy(recvbuf, PACK_HEADER_LEN + SEQ_LEN, pbBytes, 0, head.Len - SEQ_LEN);

        NetMessage nm = new NetMessage();

        nm.seq = BitConverter.ToInt32(seqBytes,0);
        IMessage protoData = null;
        protoData = NetMsgHandlerHelper.Deserialize((EDict)head.ID, pbBytes);

        nm.ID = (int)head.ID;
        nm.Proto = protoData;

        return nm;

        //using (MemoryStream stream = new MemoryStream(recvbuf, PACK_HEADER_LEN, (int)head.Len))
        //{
        //    NetMessage nm = new NetMessage();
        //    IMessage protoData = null;

        //    Debug.Log("MemoryStream stream len: " + stream.Length);

        //    protoData = NetMsgHandlerHelper.Deserialize((EDict)head.ID, stream);

        //    nm.ID = (int)head.ID;
        //    nm.Proto = protoData;
        //    stream.Dispose();
        //    stream.Close();
        //    return nm;
        //}
    }

    public void init()
    {


    }

}
