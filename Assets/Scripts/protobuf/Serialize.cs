using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using PbSpirit;
using System.IO;
using Google.Protobuf;

namespace ElfWizard
{
    public class Serialize : MonoBehaviour
    {
        public Elf data;
        SpiritSkill elfskill;

        // Start is called before the first frame update
        void Start()
        {
/*            playerSppack = new SpiritPackage();
            enemySppack = new SpiritPackage();
            playerSppack.SpiritPackage_.Add(new List<Spirit> { new Spirit {Id="1",Name="ElfFire01",Specialist=SpecialistType.StFire,Rarity=1,Level=1,SkillDescription="test1",Selected=true },
                                                         new Spirit {Id="2",Name="ElfIce01",Specialist=SpecialistType.StIce,Rarity=1,Level=1,SkillDescription="test2",Selected=true }});
            enemySppack.SpiritPackage_.Add(new List<Spirit> { new Spirit {Id="3",Name="EnemySlimeFire01",Specialist=SpecialistType.StFire,Rarity=1,Level=1,SkillDescription="test3",Selected=true },
                                                                            });*/
            /*            elfskill = new PbSpirit.SpiritSkill();
            elfskill.Id = 3;
            elfskill.Name = "fire01";
            elfskill.Description = "this is a test";*/
            #region(obselte)
            /*            data = new Elf
                        {

                            elfType = ElementType.Fire,
                            ElfName = "Fire01",
                            ElfID = 1001,
                            Description = "a basic fire elf",
                            health = 5,
                            Damage = 0.5f,
                            FireDamage = 0.5f,
                            IceDamage = 0,
                            DarkDamage = 0,
                            HolyDamage = 0,
                            NatureDamage = 0,
                            Defence = 0


                        };*/
            #endregion
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnGUI()
        {
/*            if (GUI.Button(new Rect(10, 10, 100, 150), "serialize"))
            {
                //proto�ļ�����֮��Ὣ��д����Ϣ���Զ�ת�����࣬�������ֱ�ӽ���ת���ɶ������ļ�����ͨ��Deserialize�����л�������ֱ�ӽ��������ļ�ת���ɶ���õ���
                using (var file = File.Create("Assets/Scripts/protobuf/src/testSpPack.bin"))
                {
                    //playerSppack.WriteTo(file);

                }
                using (var file = File.Create("Assets/Scripts/protobuf/src/enemytestSpPack.bin"))
                {
                    //enemySppack.WriteTo(file);

                }

            }*/
        }
    }
}
