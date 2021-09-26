using System.Collections;
using System.Collections.Generic;
using Google.Protobuf;
using UnityEngine;

namespace Framework
{
    public interface IUserModel : IModel
    {
        IMessage UserData { get; set; }
    }
    public class UserModel : AbstractModel, IUserModel
    {
        public IMessage UserData { get; set; }
        protected override void OnInit()
        {
            
        }
    }
}