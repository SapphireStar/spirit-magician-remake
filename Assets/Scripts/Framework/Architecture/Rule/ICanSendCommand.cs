using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public interface ICanSendCommand : IBelongToArchitecture
    {

    }

    public static class CanSendCommandExtension
    {
        public static void SendCommand<T>(this ICanSendCommand self) where T : ICommand, new()
        {
            self.getArchitecture().SendCommand<T>();
        }
        public static void SendCommand<T>(this ICanSendCommand self,T command) where T : ICommand, new()
        {
            self.getArchitecture().SendCommand<T>(command);
        }
    }
}