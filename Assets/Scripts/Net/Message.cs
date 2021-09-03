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
            if ((startIndex - 4) >= count)//��startIndex-4>=countʱ��˵���������Ѿ��յ����㹻���������������������ݣ���˿�ʼ����
            {
                /*                    string s = Encoding.UTF8.GetString(data, 4, count);
                                    Console.WriteLine("��������һ�����ݣ� " + s);*/
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
                string s = Encoding.UTF8.GetString(data, 8, count - 4);
                processDataCallback(actionCode, s);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);//���ǵ�ճ�����������TCP�Զ�������ճ�������ȡ��һ�����ݺ󣬽��������ݴ�data��
                                                                             //ȥ���������Array.Copy�������Կ����ǽ��ոն�ȡ�����ݴ�data�������Ƴ���Ȼ�󽫺����������ǰ�ƶ�������Ŀ�ͷ
                startIndex -= (count + 4);
            }
            else return;
        }
    }

    /// <summary>
    /// ������������͵�������з�װ
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
            Console.WriteLine("����:��Message�кϲ��ֽ�������ִ���" + e);
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
            Console.WriteLine("����:��Message�кϲ��ֽ�������ִ���" + e);
            return null;
        }


    }

}
