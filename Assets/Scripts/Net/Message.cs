using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

public class Message
{
    byte[] data = new byte[1024];
    int startIndex = 0;

    public byte[] Data
    {
        get { return data; }
        set { data = value; }
    }
    public int StartIndex
    {
        get { return startIndex; }
    }
    public int RemainSize
    {
        get { return data.Length - startIndex; }
    }

    public void AddCount(int count)
    {
        startIndex += count;
    }
    public void ReadMessage(int newDataAmount, Action<ActionCode, string> processDataCallback)
    {
        startIndex += newDataAmount;
        while (true)
        {
            if (startIndex <= 4) return;
            int count = BitConverter.ToInt32(data, 0);
            if ((startIndex - 4) >= count)//当startIndex-4>=count时，说明服务器已经收到了足够的数据量来解析这条数据，因此开始解析
            {
                /*                    string s = Encoding.UTF8.GetString(data, 4, count);
                                    Console.WriteLine("解析出来一条数据： " + s);*/
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
                string s = Encoding.UTF8.GetString(data, 8, count - 4);
                processDataCallback(actionCode, s);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);//考虑到粘包的情况，若TCP自动进行了粘包，则读取完一条数据后，将这条数据从data中
                                                                             //去除，这里的Array.Copy方法可以看作是将刚刚读取的数据从data数组中移除，然后将后面的数据向前移动至数组的开头
                startIndex -= (count + 4);
            }
            else return;
        }
    }

    /// <summary>
    /// 对向服务器发送的请求进行封装
    /// </summary>
    /// <param name="requestCode"></param>
    /// <param name="actionCode"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static byte[] PackData(RequestCode requestCode, ActionCode actionCode, string data)
    {

        byte[] requestCodeBytes = BitConverter.GetBytes((int)requestCode);
        byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataLength = requestCodeBytes.Length + dataBytes.Length + actionCodeBytes.Length;
        byte[] dataLengthBytes = BitConverter.GetBytes(dataLength);
    
        try
        {
           byte[] Data = dataLengthBytes.Concat(requestCodeBytes).ToArray<byte>().
                            Concat(actionCodeBytes).ToArray<byte>().
                            Concat(dataBytes).ToArray<byte>();
            return Data;
        }
        catch (Exception e)
        {
            Console.WriteLine("警告:在Message中合并字节数组出现错误" + e);
            return null;
        }


    }

    public static byte[] PackData(ActionCode actionCode, string data)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataLength = requestCodeBytes.Length + dataBytes.Length;
        byte[] dataLengthBytes = BitConverter.GetBytes(dataLength);
        try
        {
            dataLengthBytes.Concat(requestCodeBytes.Concat(dataBytes.ToArray<byte>()).ToArray<byte>()).ToArray<byte>();
            return dataLengthBytes;
        }
        catch (Exception e)
        {
            Console.WriteLine("警告:在Message中合并字节数组出现错误" + e);
            return null;
        }


    }

}
