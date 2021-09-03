using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System;
using ElfWizard.Manager;

namespace ElfWizard
{

    [Serializable]
    [ProtoContract]
    public class Elf
    {
        [ProtoMember(1)]
        public ElementType elfType { get; set; }
        [ProtoMember(2)]
        public string ElfName { get; set; }
        [ProtoMember(3)]
        public int ElfID { get; set; }

        public Sprite Elf_Icon { get; set; }
        [TextArea]
        [ProtoMember(4)]
        public string Description = "";
        public GameObject ElfPrefab;
        //[Header("Elf Properties")]
        [ProtoMember(5)]
        public float health { get; set; }
        [ProtoMember(6)]
        public float Damage { get; set; }
        [ProtoMember(7)]
        public float FireDamage { get; set; }
        [ProtoMember(8)]
        public float IceDamage { get; set; }
        [ProtoMember(9)]
        public float DarkDamage { get; set; }
        [ProtoMember(10)]
        public float HolyDamage { get; set; }
        [ProtoMember(11)]
        public float NatureDamage { get; set; }
        [ProtoMember(12)]
        public float Defence { get; set; }


    }



}

