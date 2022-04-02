using System.Collections;
using System.Collections.Generic;
using Base;
using Google.Protobuf;
using UnityEngine;

namespace Framework
{
    public interface IUserModel : IModel
    {
        IMessage UserData { get; set; }
        UserBaseInfo userBaseInfo { get; set; }
    }
    public class UserModel : AbstractModel, IUserModel
    {
        public IMessage UserData { get; set; }
        public UserBaseInfo userBaseInfo { get; set; }

        protected override void OnInit()
        {
            
        }
    }
}