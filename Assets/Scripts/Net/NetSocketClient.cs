using UnityEngine;
using System.Collections;
using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Google.Protobuf;
using Newtonsoft.Json;
using System.Collections.Generic;
using PbDict;
using System.Threading;
using System.Reflection;
using System.Reflection.Emit;
using PbLogin;

public class NetSocketClient
{
    public CLIENT_ID ID;
    public bool Active = false;
    public float Lag = 0;
    private DateTime StartTickTime;
    private Socket m_Socket = null;
    NetMsgHandler msgHandler = null;

    private string m_IP;
    private int m_Port;
    //信息接收缓存(直接接收网络过来的信息)
    private Byte[] m_DataBuffer;
    // 现在存入Buffer的位置
    private int m_nNowInPos = 0;

    private NetLoopBuffer m_RecvLoopBuffer;

    //信息发送存储
    private Byte[] m_SendMsgBuffer;
    // 收到的信息，用于回调OnRecv
    //private Byte[] m_RecvMsgBuffer;

    private object locker = new object();
    private object sendlocker = new object();


    // 按次序存储收到的消息包的长度
    private ArrayList m_Events = new ArrayList();
    private Queue triggerQueue = new Queue();
    private const int BUF_LEN = 256 * 1024;
    private const int RECV_LOOPBUF_LEN = 512 * 1024;
    private const int PACK_HEADER_LEN = 8;
    private const int SEQ_LEN = 4;
    private const int RECONNECT_TIMES_LIMIT = 3;
    private int reconnectTimes = 0;
    private float tickTime;
    private float tickTimeInterval = 5f;
    private bool isReconnect = false;
    private float connectingTime;
    private System.Action<bool> connectCallback = null;

    protected MSG_HEAD sendHead = new MSG_HEAD();
    protected MSG_HEAD receiveHead = new MSG_HEAD();

    public bool isAuthed = false;

    public NetSocketClient()
    {

        m_DataBuffer = new Byte[BUF_LEN];
        m_nNowInPos = 0;
        //m_RecvLoopBuffer = new NetLoopBuffer(RECV_LOOPBUF_LEN);
        m_SendMsgBuffer = new Byte[BUF_LEN];
        //m_RecvMsgBuffer = new Byte[BUF_LEN];
    }

    private void Reset()
    {
        tickTime = 0;
        m_nNowInPos = 0;
        m_Events.Clear();
        triggerQueue.Clear();
        //m_RecvLoopBuffer.Reset();
    }

    public void Connect(string ip, int port, NetMsgHandler handler, System.Action<bool> cb)
    {
        Debug.Log("connnect to " + ip + " " + port);
        isReconnect = false;
        //        if (IsConnect())
        //        {
        Close();
        //        }
        //		m_Socket.EndConnect(
        m_IP = ip;
        m_Port = port;
        connectCallback = cb;
        msgHandler = handler;
        if (handler == null)
        {
            return;
        }
        connectingTime = 6f;
        msgHandler.init();
        StartConnect();

    }

    public void Reconnect()
    {
        isReconnect = true;
        if (IsConnect())
        {
            Close();
        }

        StartConnect();
    }

    public void StartConnect()
    {
        Reset();
        try
        {
            IPAddress[] addresses = Dns.GetHostAddresses(m_IP);
            if (addresses.Length > 0)
            {
                IPAddress ip = addresses[0];
                if (ip.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    m_Socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                    m_Socket.BeginConnect(ip, m_Port, new AsyncCallback(ConnectCallBack), m_Socket);
                }
                else
                {
                    m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    m_Socket.BeginConnect(ip, m_Port, new AsyncCallback(ConnectCallBack), m_Socket);
                }
            }
            else
            {
                Debug.LogError("dns parse error");
            }

            if (m_Socket != null)
            {
                Debug.Log("----- m_Socket is not null -------");
            } else
            {
                Debug.Log("----- m_Socket is Null -------");
            }
            
        }
        catch (SocketException ex)
        {
            Debug.LogError(ex.SocketErrorCode + " " + ex.StackTrace);
            PushEvent(ENNetEvent.EVENT_CONNECT, 0);
            //PushEvent(ENNetEvent.EVENT_ERROR, (int)(ex.SocketErrorCode));
        }
    }


    public virtual void Close()
    {

        if (m_Socket != null)
        {
            Debug.Log("----------  Socket Close     ---------");
            m_Socket.Close();
            m_Socket = null;
            PushEvent(ENNetEvent.EVENT_CLOSE, 0);
        }
        Active = false;
    }

    public bool IsConnect()
    {
        if (null == m_Socket)
        {
            return false;
        }
        return m_Socket.Connected;
    }


    private byte[] bytes = new byte[1024];
    private CodedOutputStream outstream = null;
    private FieldInfo field = null;
    public bool Send(IMessage data, int msgID)
    {
        //if (stream == null)
        //{
        //    stream = new MemoryStream(bytes);
        //}
        if (outstream == null)
        {
            outstream = new CodedOutputStream(bytes);
            Type t = typeof(CodedOutputStream);
            field = t.GetField("position", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        else
        {
            outstream.Flush();
            if (field != null) {
                field.SetValue(outstream, 0);
            }
        }
        if (true)
        {
            data.WriteTo(outstream);
            if (MessageLogFilter.Filter(msgID))
            {
                Debug.Log("<color=#6699ff>sendMessage -></color>"+ " " + msgID +" " + Enum.GetName(typeof(EDict), msgID) + " " +JsonConvert.SerializeObject(data));
            }
            
            
            return Send(bytes, (int)data.CalculateSize(), (ushort)msgID);
        }
        return false;
    }

    public bool Send(byte[] data, int msgID)
    {
        return Send(data, (int)data.Length, (ushort)msgID);
    }


    public bool Send(byte[] Data, int Length, ushort MsgId)
    {
        if (null == m_Socket)
        {
            return false;
        }
        lock (sendlocker) {
            // Check if connection is on.
            if (!m_Socket.Connected)
            {
                return false;
            }

            int seq = 0; //TODO: this seq need be generate and store by sequency,inorder to track the system error msg from server
 
            sendHead.ID = (uint)MsgId;
            sendHead.Len = (uint)Length + SEQ_LEN; // +4 for the seq combined in the data
            byte[] headbuf = sendHead.GetSendbuf();


            // Check data length.
            if (sendHead.Len > BUF_LEN - PACK_HEADER_LEN)
            {
                // 因为Send是在主线程被调用，所以不需要加锁
                PushEvent(ENNetEvent.EVENT_DISCONNECT, (int)(SelfDefSocketError.SEND_OVERFLOW));
                return false;
            }

            Array.Copy(headbuf, 0, m_SendMsgBuffer, 0, PACK_HEADER_LEN);
            Array.Copy(BitConverter.GetBytes(seq), 0, m_SendMsgBuffer, PACK_HEADER_LEN, SEQ_LEN); // add seq before the data
            Array.Copy(Data, 0, m_SendMsgBuffer, PACK_HEADER_LEN + SEQ_LEN, Length);

            int Size = PACK_HEADER_LEN + Length + SEQ_LEN;

            int timeout = 1000;
            //        m_Socket.SendTimeout = 0;
            int startTickCount = Environment.TickCount;
            int Sent = 0;
            do
            {
                if (Environment.TickCount > startTickCount + timeout)
                {
                    PushEvent(ENNetEvent.EVENT_DISCONNECT, (int)(SelfDefSocketError.SEND_OVERFLOW));
                    return false;
                }
                Sent += m_Socket.Send(m_SendMsgBuffer, Sent, Size - Sent, SocketFlags.None);
            } while (Sent < Size);
        }

        if (m_Socket == null)
        {
            Debug.LogError("After send, m_Socket is null");
        }
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

    private void ConnectCallBack(IAsyncResult AR)
    {
        try
        {
            if (null == m_Socket)
            {
                return;
            }
            m_Socket.EndConnect(AR);
            // 推入事件

            Debug.Log(" ------  ConnectCallBack  ------- ");
            //开始从连接的Socket异步读取数据。接收来自服务器的信息
            //AsyncCallback引用在异步操作完成时调用的回调方法
            m_Socket.BeginReceive(m_DataBuffer, m_nNowInPos, m_DataBuffer.Length, SocketFlags.None, ReceiveCallBack, null);
            PushEvent(ENNetEvent.EVENT_CONNECT, m_Socket.Connected ? 1 : 0);
            
            this.Active = true;
            //TODO: where to add heartbeat
            msgHandler.RemoveListener((ushort)EDict.PbLoginS2CHeartBeat, receiveTick);
            msgHandler.RegistListener((ushort)EDict.PbLoginS2CHeartBeat, receiveTick);
        }
        catch (SocketException ex)
        {
            m_Socket.EndConnect(AR);
            PushEvent(ENNetEvent.EVENT_CONNECT, 0);
        }
    }

    private void receiveTick(IMessage proto)
    {
        this.tickTime = 0;
        if (StartTickTime != null) {
            Lag = (float)(DateTime.UtcNow - StartTickTime).TotalSeconds;
        }
    }

    private void CheckTick()
    {
        if (Active)
        {
            float now = Time.realtimeSinceStartup;
            if (!NetManager.Instance.Ticking) {
                tickTime = 0;
                return;
            }
            if (IsConnect())
            {
                tickTime += Time.deltaTime;
                if (tickTime > 2.5 * tickTimeInterval)
                {
                    tickTime = 0;
                    TryReconnect(true);
                }
                //else if (tickTime > tickTimeInterval)
                //{
                //    startTickTime = now;
                //    PhoenixProto.TickReq req = new PhoenixProto.TickReq();
                //    Send(req, (ushort)PhoenixProto.MSGID.TickReqId);
                //}
            }
            else
            {
                TryReconnect();
            }
        }
    }

    private void ReceiveCallBack(IAsyncResult result)
    {
        Debug.Log("---- ReceiveCallBack --------");
        if (null == m_Socket)
        {
            Debug.Log("---- null == m_Socket --------");
            return;
        }
        try
        { 
            //结束挂起的异步读取，返回接收到的字节数。 AR，它存储此异步操作的状态信息以及所有用户定义数据
            int REnd = m_Socket.EndReceive(result);
            result.AsyncWaitHandle.Close();
            
            
            // 修改位置
            m_nNowInPos += REnd;
            

            // Got Message, lock
            do
            {
                if (m_nNowInPos >= PACK_HEADER_LEN)
                {
                    int indent = 0;
                    indent = receiveHead.Parse(m_DataBuffer);

                    //Debug.Log("---- cmd id --------: " + receiveHead.ID);
                    //Debug.Log("---- cmd len --------: " + receiveHead.Len);

                    
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
                        PushEvent(ENNetEvent.EVENT_DISCONNECT, (int)(SelfDefSocketError.PACKET_ERROR));
                        return;
                    }
                    else if (len > m_nNowInPos)
                    {
                        //没收完
                        break;
                    }
                    else
                    {
                        NetMessage msg = msgHandler.Read(m_DataBuffer, len);
                        if (msg != null)
                        {
                            lock (locker)
                            {
                                triggerQueue.Enqueue(msg);
                            }
                        }

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

            m_Socket.BeginReceive(m_DataBuffer, m_nNowInPos, m_DataBuffer.Length - m_nNowInPos, 0, ReceiveCallBack, null);
        }
        catch (Exception e)
        {
            Debug.Log(e + " " + e.StackTrace);
        }
    }

    public void StartTick()
    {
        Debug.Log("StartTick....");
        if (isAuthed) {
            Debug.Log("StartTick - C2S_HeartBeat");
            StartTickTime = DateTime.UtcNow;
            C2S_HeartBeat req = new C2S_HeartBeat();
            
            Send(req, (ushort)EDict.PbLoginC2SHeartBeat);
        }
        
    }


    /// <summary>
    public void Run()
    {
        if (connectingTime > 0)
        {
            connectingTime -= Time.deltaTime;
            if (connectingTime <= 0 && !this.IsConnect())
            {
                this.Close();
                PushEvent(ENNetEvent.EVENT_CONNECT, 0);
            }
        }
        CheckTick();
        lock (locker)
        {
            while (triggerQueue.Count > 0)
            {
                NetMessage msg = (NetMessage)triggerQueue.Dequeue();
                if (msg != null)
                {

                    if (MessageLogFilter.Filter(msg.ID))
                        Debug.Log("<color=#FFFF00>receive msg -></color>" + msg.ID + " " + Enum.GetName(typeof(EDict), msg.ID) + " " + JsonConvert.SerializeObject(msg.Proto));


                    //if (msg.Proto != null)
                    //{
                    msgHandler.TriggerEvent(msg.ID, msg.Proto);
                    //}
                    //else if (msg.LuaBytes !=null)
                    //{
                    //    LuaNetHandle.ReceiveNetMsg(msg.LuaBytes, msg.ID);
                    //}
                }
                tickTime = 0;
            }
        }

        if (m_Events.Count == 0)
        {
            return;
        }

        for (int i = 0; i < m_Events.Count; i++)
        {
            SNetEvent sEvent = (SNetEvent)m_Events[i];
            switch (sEvent.enEvent)
            {
                case ENNetEvent.EVENT_CONNECT:
                    {
                        bool success = sEvent.nParam > 0;
                        Debug.Log("OnConnect" + success);
                        if (success)
                        {
                            OnConnect();
                        }
                        if (connectCallback != null)
                        {
                            connectCallback(success);
                        }
                    }
                    break;
                case ENNetEvent.EVENT_CLOSE:
                    {
                        Debug.Log("OnClose");
                        OnClose();
                    }
                    break;

                case ENNetEvent.EVENT_ERROR:
                    {
                        Debug.Log("OnError");
                        OnError(sEvent.nParam);
                    }
                    break;
                case ENNetEvent.EVENT_DISCONNECT:
                    {
                        Debug.Log("OnDisconnect");
                        OnDisconnect();
                    }
                    break;
                default:
                    {
                    }
                    break;
            }
        }
        m_Events.Clear();
    }

    private static Thread tickThread;
    //连接服务器成功
    public void OnConnect()
    {
        connectingTime = 0;
        reconnectTimes = 0;
        if (tickThread != null) {
            tickThread.Abort();
        }
        tickThread = new Thread(ThreadTick);
        if (tickThread.ThreadState != ThreadState.Running)
        {
            tickThread.Start();
        }
        
    }

    private void ThreadTick() {
        try {
            while (true)
            {
                if (this.IsConnect())
                {
                    StartTick();
                }
                Thread.Sleep((int)(this.tickTimeInterval * 1000f));
            }
        }
        catch (Exception e) 
        {

        }
    }

    public void OnError(int ErrorCode)
    {
        this.Close();
    }

    public void OnDisconnect()
    {
        this.Close();
    }

    public bool TryReconnect(bool isTickTimeOut = false)
    {
        return true;
    }


    public void OnClose()
    {

    }
}

public class MessageLogFilter
{
    static public bool LogEnabled = true;

    static private HashSet<int> filterIds = new HashSet<int> {
        //不想打印出log的消息Id在这里加入
        (int)EDict.PbLoginC2SHeartBeat,
        (int)EDict.PbLoginS2CHeartBeat,
    };

    static public bool Filter(int msgId)
    {
        if (LogEnabled == false)
            return false;
        return !filterIds.Contains(msgId);
    }
}