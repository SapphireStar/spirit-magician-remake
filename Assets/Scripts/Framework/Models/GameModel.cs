using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public interface IGameModel:IModel
    {
        string MapName { get; set; }
    }
    public class GameModel : AbstractModel, IGameModel
    {
        public string MapName { get; set; }
        protected override void OnInit()
        {
            
        }
    }
}