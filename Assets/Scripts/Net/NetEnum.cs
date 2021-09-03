using UnityEngine;
using System.Collections;

enum SelfDefSocketError
{
    RECV_OVERFLOW = -3,
    SEND_OVERFLOW = -2,
    PACKET_ERROR = -1,
}

public enum ENNetEvent
{
    EVENT_CONNECT = 0,
    EVENT_RECV,
    EVENT_CLOSE,
    EVENT_ERROR,
    EVENT_DISCONNECT,
}

public struct SNetEvent
{
    // 事件类型
    public ENNetEvent enEvent;
    // 时间相应的参数
    public int nParam;
}