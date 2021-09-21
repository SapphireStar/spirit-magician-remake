using System.Collections;
using System.Collections.Generic;
using ElfWizard;
using Framework;
using PbBattle;
using UnityEngine;

namespace ElfWizard.Model
{
    public interface IBattleModel : IModel
    {
        public NewPlayerController player { get; }
        public NewPlayerController enemy { get; }
        public NewPlayerController currentTurn { get; }
        public BindableProperty<int> currentTurnElfs { get; }
        public BattleRoundInfo curRoundInfo { get; }
        public BattleRoundInfo nextRoundInfo { get; }
        public PlayerBattleInfo playerBattleInfo { get; }
        public PlayerBattleInfo enemyBattleInfo { get; }

        public BattleInfo battleInfo { get; }
        public GameObject GameStartUI { get; }

        public int roundIndex { get; }
        public int activeUID { get; }
        public List<DiceInfo> diceInfo { get; }
        public DiceFormation diceFormation { get; }
        public List<int> specialEffects { get; }
        public List<BattleUnitCarriedSkill> buCarriedSkill { get; }
        public int countDown { get; }
        public int RemainTime { get; }

        public string MapName { get; }
    }
    public class BattleModel:AbstractModel,IBattleModel
    {

        public NewPlayerController player { get; }
        public NewPlayerController enemy { get; }
        public NewPlayerController currentTurn { get; }
        public BindableProperty<int> currentTurnElfs { get; } = new BindableProperty<int>
        {
            Value = 0
        };
        public BattleRoundInfo curRoundInfo { get; }
        public BattleRoundInfo nextRoundInfo { get; }
        public PlayerBattleInfo playerBattleInfo { get; }
        public PlayerBattleInfo enemyBattleInfo { get; }

        public BattleInfo battleInfo { get; }
        public GameObject GameStartUI { get; }

        public int roundIndex { get; }
        public int activeUID { get; }
        public List<DiceInfo> diceInfo { get; }
        public DiceFormation diceFormation { get; }
        public List<int> specialEffects { get; }
        public List<BattleUnitCarriedSkill> buCarriedSkill { get; }
        public int countDown { get; set; }
        public int RemainTime { get; set; }

        public string MapName { get; set; }
        protected override void OnInit()
        {
            
        }
    }
}