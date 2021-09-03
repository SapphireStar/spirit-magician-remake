using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using Common;
using System;
using System.Text;
using ElfWizard.Manager;

namespace ElfWizard {
    /// <summary>
    /// 用于管理服务器端的Socket连接
    /// </summary>
    public class ClientManager : BaseManagerNonMono
    {
        private const string IP = "127.0.0.1";
        private const int PORT = 1145;
        private Socket clientSocket;
        private Message msg;
        public ClientManager(GameFacade gameFacade) : base(gameFacade) { }
        public override void OnInit()
        {
            base.OnInit();
            msg = new Message();
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Start();
            try
            {
                clientSocket.Connect(IP, PORT);
            }
            catch (System.Exception e)
            {
                Debug.Log("警告:无法连接到服务器端"+e);
            }


        }

        private void Start()
        {
            clientSocket.BeginReceive(msg.Data,msg.StartIndex,msg.RemainSize, SocketFlags.None, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                if (clientSocket == null || clientSocket.Connected == false) return;
                int count = clientSocket.EndReceive(ar);
                msg.ReadMessage(count, processDataCallback);
                Start();

            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        private void processDataCallback(ActionCode actionCode, string data)
        {
            facade.HandleResponse(actionCode, data);
        }

        /// <summary>
        /// 向服务器端发送请求
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="actionCode"></param>
        /// <param name="data"></param>
        public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
        {
            byte[] bytes = Message.PackData(requestCode, actionCode, data);
            clientSocket.Send(bytes);
        }
        /// <summary>
        /// 销毁对象，切断连接
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();
            try
            {
                clientSocket.Close();
            }
            catch (System.Exception e)
            {
                Debug.Log("警告:无法关闭与服务器的连接" + e);
            }
        }

    }
}