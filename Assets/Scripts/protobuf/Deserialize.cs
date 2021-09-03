using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System.IO;
using PbSpirit;
using Google.Protobuf;

namespace ElfWizard
{
    
    public class Deserialize : MonoBehaviour
    {
        Elf data;
        SpiritSkill elfskill;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnGUI()
        {
/*            if(GUI.Button(new Rect(10, 130, 100, 150), "deserialze"))
            {
                //这里直接通过反序列化，将二进制文件转化成类
                using (var input = File.OpenRead("Assets/Scripts/protobuf/src/testElfSO.bin"))
                {
                    elfskill = SpiritSkill.Parser.ParseFrom(input);
                    Debug.Log(elfskill.Id + " " + elfskill.Name + " " + elfskill.Description);
                }
            }*/
        }
    }
}
