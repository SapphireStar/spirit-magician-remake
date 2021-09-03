using UnityEngine;
using System.Collections;
using System.Threading;
using System;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.IO;

public enum EBattleState
{
    EBattleState_Init = 0,
    EBattleState_EnterBattle = 1,
    EBattleState_LoadBattle = 2,
    EBattleState_ExitBattle = 3,
};

public class UdpSocketClient
{
    private Socket m_Socket = null;
    NetMsgHandler msgHandler = null;

    public EBattleState battleState;
    public float stateBeginTime;

    private string m_IP;
    private int m_Port;

    public bool Active;
    public float lastTickTime;

    private IPEndPoint serverInfo;
    //信息接收缓存(直接接收网络过来的信息)
    private Byte[] m_DataBuffer;
    // 现在存入Buffer的位置
    private int m_nNowInPos = 0;

    private NetLoopBuffer m_RecvLoopBuffer;

    //信息发送存储
    private Byte[] m_SendMsgBuffer;
    // 收到的信息，用于回调OnRecv
    private Byte[] m_RecvMsgBuffer;

    private object locker = new object();

    // 按次序存储收到的消息包的长度
    private ArrayList m_Events = new ArrayList();
	private Queue triggerQueue = new Queue();
    private const int BUF_LEN = 256 * 1024;
    private const int RECV_LOOPBUF_LEN = 512 * 1024;
    private const int PACK_HEADER_LEN = 6;

	protected MSG_HEAD sendHead = new MSG_HEAD();
	protected MSG_HEAD receiveHead = new MSG_HEAD();
    MemoryStream io = new MemoryStream();
    public void Run()
    {     
        if (Active)
        {
            while (triggerQueue.Count > 0)
            {
                NetMessage msg = (NetMessage)triggerQueue.Dequeue();
                msgHandler.TriggerEvent(msg.ID, msg.Proto);
            }

            //tick数据包
            if (Time.realtimeSinceStartup > lastTickTime + 1)
            {
                //PhoenixProto.TickReq req = new PhoenixProto.TickReq();
                //ProtoBuf.Serializer.Serialize<PhoenixProto.MSGID.TickReqId>(io, req);
                //Send(io.GetBuffer(), (int)io.Length, (ushort)PhoenixProto.MSGID.TickReqId);
                //lastTickTime = Time.realtimeSinceStartup;
            }

            if (battleState == EBattleState.EBattleState_Init)
            {
//                if (Time.realtimeSinceStartup > stateBeginTime + 1)
//                {
//                    PhoenixProto.EnterBattleReq req = new PhoenixProto.EnterBattleReq();
//                    req.token = UserDataManager.Instance.AccountInfo.UserID;
//                    req.RoomUniqueID = LBattle.BattleMgr.Ins.roomInsID;
//                    NetManager.Instance.BattleSend<PhoenixProto.EnterBattleReq>(req, (ushort)PhoenixProto.ToyWorldMSGID.Enter_Battle_Req_ID);
//
//                    stateBeginTime = Time.realtimeSinceStartup;
//                }
            }
            else if (battleState == EBattleState.EBattleState_EnterBattle)
            {
//                if (LBattle.BattleMgr.Ins.BL.levelState == LBattle.eLoadState.loadFinished)
//                {
//                    if (Time.realtimeSinceStartup > stateBeginTime + 1)
//                    {
//                        PhoenixProto.LoadSceneFinishRpt rpt = new PhoenixProto.LoadSceneFinishRpt();
//                        NetManager.Instance.BattleSend<PhoenixProto.LoadSceneFinishRpt>(rpt, (ushort)PhoenixProto.ToyWorldMSGID.Load_Scene_Finish_Rpt_ID);
//                        stateBeginTime = Time.realtimeSinceStartup;
//                    }
//                }
            }
            else if (battleState == EBattleState.EBattleState_LoadBattle)
            {
                //正式战斗状态，多久没收到数据包。。。处理
                
            }
        }
    }

    private void Reset()
    {
        lastTickTime = Time.realtimeSinceStartup;
        Active = true;
        battleState = EBattleState.EBattleState_Init;
        stateBeginTime = Time.realtimeSinceStartup;
        m_nNowInPos = 0;
        m_Events.Clear();
        m_RecvLoopBuffer.Reset();
    }

    public UdpSocketClient()
    {
        Active = false;
        m_DataBuffer = new Byte[BUF_LEN];
        m_nNowInPos = 0;
        m_RecvLoopBuffer = new NetLoopBuffer(RECV_LOOPBUF_LEN);

        m_SendMsgBuffer = new Byte[BUF_LEN];
        m_RecvMsgBuffer = new Byte[BUF_LEN];
    }

    public bool BindClientInfo()
    {
        m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        return true;
    }

    public bool InitServerInfo(string ip, int port, NetMsgHandler handler)
    {
        //client udp socket
        BindClientInfo();
        //重置UDP处理器
        Reset();
        //初始化协议处理器
        msgHandler = handler;
        msgHandler.init();
        //服务器地址信息
        m_IP = ip;
        m_Port = port;
        //服务器udp收包地址
        serverInfo = new IPEndPoint(IPAddress.Parse(m_IP), m_Port);
        //准备收包
        StartRecv();
        return true;
    }

    public bool StartRecv()
    {
        m_Socket.BeginReceive(m_DataBuffer, m_nNowInPos, m_DataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), null);
        return true;
    }

    private void ReceiveCallBack(IAsyncResult AR)
    {
        if (null == m_Socket)
        {
            return;
        }
        //结束挂起的异步读取，返回接收到的字节数。 AR，它存储此异步操作的状态信息以及所有用户定义数据
        int REnd = m_Socket.EndReceive(AR);
        // 修改位置
        m_nNowInPos += REnd;

        do
        {
            if (m_nNowInPos >= PACK_HEADER_LEN)
            {
                int indent = 0;
                indent = receiveHead.Parse(m_DataBuffer);
                //Debug.LogWarning("recv " + receiveHead.ID);
                int len = 0;
                if (indent == -1)
                {
                    len = -1;
                }
                else
                {
                    len = (int)receiveHead.Len + PACK_HEADER_LEN;
                }

                if (len < 0)
                {
                    return;
                }
                else if (len > m_nNowInPos)
                {
                    break;
                }
                else
                {
                    bool bRes = false;
                    lock (locker)
                    {
                        bRes = m_RecvLoopBuffer.Push(m_DataBuffer, 0, len);
                    }
                    if (!bRes)
                    {
                        PushEvent(ENNetEvent.EVENT_DISCONNECT, (int)(SelfDefSocketError.RECV_OVERFLOW));

                        return;
                    }


                    readBuffer(len);

                    m_nNowInPos -= len;
                    if (m_nNowInPos > 0)
                    {
                        // 包没有解完，所以把刚才结尾的数据拷贝到从头开始，然后继续收包
                        Array.Copy(m_DataBuffer, len, m_DataBuffer, 0, m_nNowInPos);
                    }
                }
            }
            else
            {
                // == 0说明还有没收完的包
                break;
            }
        } while (m_nNowInPos > 0);

        m_Socket.BeginReceive(m_DataBuffer, m_nNowInPos, m_DataBuffer.Length - m_nNowInPos, 0, new AsyncCallback(ReceiveCallBack), null);
    }

    public bool Send(Byte[] Data, int Length, ushort MsgId)
    {
        int headsize = sizeof(ushort) + sizeof(uint);
        sendHead.ID = MsgId;
        sendHead.Len = (uint)Length;
        byte[] headbuf = sendHead.GetSendbuf();
        Array.Copy(headbuf, 0, m_SendMsgBuffer, 0, headsize);
        Array.Copy(Data, 0, m_SendMsgBuffer, headsize, Length);

        int Size = headsize + Length;
        m_Socket.SendTo(m_SendMsgBuffer, 0, Size, SocketFlags.None, serverInfo);
        return true;
    }

    // 此方法必须在加锁的区域进行调用
    private void PushEvent(ENNetEvent enEvent, int nParam)
    {
        SNetEvent netEvent = new SNetEvent();

        netEvent.enEvent = enEvent;
        netEvent.nParam = nParam;

        m_Events.Add(netEvent);
    }

    public void readBuffer(int length)
    {
        if (!m_RecvLoopBuffer.Pop(ref m_RecvMsgBuffer, length))
        {
            //OnClose();
        }
        else
        {
            NetMessage msg = msgHandler.Read(m_RecvMsgBuffer, length);
            triggerQueue.Enqueue(msg);
        }
    }
}
