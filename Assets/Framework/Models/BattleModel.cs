using System.Collections;
using System.Collections.Generic;
using ElfWizard;
using PbBattle;
using UnityEngine;

namespace Framework
{
    public interface IBattleModel : IModel
    {

        BindableProperty<int> RemainAttackElfs { get; }

        NewPlayerController player { get; set; }
        NewPlayerController enemy { get; set; }
        NewPlayerController currentTurn { get; set; }

        #region BattleInfo
        BattleRoundInfo curRoundInfo { get; set; }
        BattleRoundInfo nextRoundInfo { get; set; }
        PlayerBattleInfo playerBattleInfo { get; set; }


        PlayerBattleInfo enemyBattleInfo { get; set; }
        BindableProperty<int> PlayerHP { get; }
        BindableProperty<int> EnemyHP { get; }
        Vector3[] PlayerSpawnPoints { get; }
        Vector3[] EnemySpawnPoints { get; }
        BattleInfo battleInfo { get; set; }

        int roundIndex { get; }
        BindableProperty<int> activeUID { get; }
        List<DiceInfo> diceInfo { get; }
        DiceFormation diceFormation { get; set; }
        List<int> specialEffects { get; }
        BattleUnitCarriedSkill playerCarriedSkill { get; }
        BattleUnitCarriedSkill enemyCarriedSkill { get; }
        #endregion



        int countDown { get; }
        int RemainTime { get; }

        string MapName { get; }
        BattleState Turn { get; }

        SkillEffect GetUidCarriedSkill();
    }
    public class BattleModel:AbstractModel,IBattleModel
    {

        public NewPlayerController player { get; set; }//由BattleSceneController管理
        public NewPlayerController enemy { get; set; }
        public NewPlayerController currentTurn { get; set; } = new NewPlayerController();
        public BindableProperty<int> RemainAttackElfs { get; } = new BindableProperty<int>
        {
            Value = -1
        };
        public BattleRoundInfo curRoundInfo { get; set; }
        public BattleRoundInfo nextRoundInfo { get; set; }
        public PlayerBattleInfo playerBattleInfo { get; set; } = new PlayerBattleInfo();

        public PlayerBattleInfo enemyBattleInfo { get; set; } = new PlayerBattleInfo();
        public BindableProperty<int> PlayerHP { get; } = new BindableProperty<int>();

        public BindableProperty<int> EnemyHP { get; } = new BindableProperty<int>();
        public Vector3[] PlayerSpawnPoints { get; } = new Vector3[] {new Vector3(-2.5f,1, 0f),new Vector3(0,1,0f),new Vector3(2.5f,1, 0f) };
        public Vector3[] EnemySpawnPoints { get; } = new Vector3[] { new Vector3(2.5f, 1, 11f), new Vector3(0, 1, 11f), new Vector3( - 2.5f, 1, 11f) };
        public BattleInfo battleInfo { get; set; }
        public GameObject GameStartUI { get; }

        public int roundIndex { get; }
        public BindableProperty<int> activeUID { get; set; } = new BindableProperty<int>() 
        { 
            
        };

        public List<DiceInfo> diceInfo { get; }
        public DiceFormation diceFormation { get; set; }
        public List<int> specialEffects { get; }
        public BattleUnitCarriedSkill playerCarriedSkill { get; } = new BattleUnitCarriedSkill();
        public BattleUnitCarriedSkill enemyCarriedSkill { get; } = new BattleUnitCarriedSkill();
        public int countDown { get; set; }
        public int RemainTime { get; set; }

        public string MapName { get; set; } = "TestFramework";

        public BattleState Turn { get; set; }



        protected override void OnInit()
        {
            activeUID.OnValueChanged += (value) =>//当activeUID改变时，更改当前生效的PlayerController
            {
                if (value == player.UID)
                    currentTurn = player;
                else
                    currentTurn = enemy;
            };

            playerCarriedSkill.Uid = 2;
            playerCarriedSkill.CarriedSkills.Add(new Google.Protobuf.Collections.RepeatedField<SkillEffect>() 
            {
                new SkillEffect(){SkillID = "ElfFire01"},
                new SkillEffect(){SkillID = "ElfIce01"},
                new SkillEffect(){SkillID = "ElfEvil01"},
                new SkillEffect(){SkillID = "ElfHoly01"},
                new SkillEffect(){SkillID = "ElfNature01"},
            });

            enemyCarriedSkill.Uid = -1;
            enemyCarriedSkill.CarriedSkills.Add(new Google.Protobuf.Collections.RepeatedField<SkillEffect>()
            {
                new SkillEffect(){SkillID = "ElfFire01"},
                new SkillEffect(){SkillID = "ElfIce01"},
                new SkillEffect(){SkillID = "ElfEvil01"},
                new SkillEffect(){SkillID = "ElfHoly01"},
                new SkillEffect(){SkillID = "ElfNature01"},
            });

        }
        public SkillEffect GetUidCarriedSkill()
        {
            if (playerCarriedSkill.Uid == activeUID.Value)
                return playerCarriedSkill.CarriedSkills[Random.Range(0, playerCarriedSkill.CarriedSkills.Count)];
            else return enemyCarriedSkill.CarriedSkills[Random.Range(0, enemyCarriedSkill.CarriedSkills.Count)];

        }
    }
}