using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net;

public class MSG_HEAD
{
    public uint ID;
    public uint Len;
    protected byte[] c = new byte[8];

    public byte[] GetSendbuf()
    {
        

        //var bytes32ID = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((Int32)ID));
        //var bytes32Len = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((Int32)Len));

        var bytes32ID = BitConverter.GetBytes(ID);
        var bytes32Len = BitConverter.GetBytes(Len);

        this.c[0] = bytes32ID[0];
        this.c[1] = bytes32ID[1];
        this.c[2] = bytes32ID[2];
        this.c[3] = bytes32ID[3];
        this.c[4] = bytes32Len[0];
        this.c[5] = bytes32Len[1];
        this.c[6] = bytes32Len[2];
        this.c[7] = bytes32Len[3];
  //      Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((Int16)ID)), 0, c, 0, sizeof(ushort));
		//Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((Int32)Len)), 0, c, sizeof(ushort), sizeof(uint));
        return c;
    }

    public int Parse(byte[] recvBuf)
    {
		int indent = 0;
		ID = BitConverter.ToUInt32(recvBuf, indent);
		//ID = (ushort)IPAddress.NetworkToHostOrder((Int32)ID);
		indent += sizeof(uint);

        //ID = BitConverter.ToUInt16(recvBuf, indent);
        //ID = (ushort)IPAddress.NetworkToHostOrder((Int16)ID);
        //indent += sizeof(ushort);

        Len = BitConverter.ToUInt32(recvBuf, indent);
		//Len = (uint)IPAddress.NetworkToHostOrder((Int32)Len);
		indent += sizeof(uint);

		return indent;
    }
}