using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using ProtoBuf;


namespace ElfWizard {
    public static class SkillConfigReader
    {
        
        static XmlDocument xml = new XmlDocument();
        private static string path;

        public static SkillData GetSkillData(int ID)
        {
            SkillData data = new SkillData();
            xml.Load("Assets/Resources/SkillData/SkillConfig.xml");
            XmlNodeList skillID = xml.GetElementsByTagName("SkillID");
            XmlNodeList nodes = skillID[ID].ChildNodes;
            data.skillID = ID;
            data.name = nodes[0].InnerText;
            string[] impactTypes = nodes[1].InnerText.Split(',');//将效果的字符串拆分成多个效果
            data.impactType = impactTypes;

            data.coolTime = int.Parse(nodes[2].InnerText);

            string[] atkTags = nodes[3].InnerText.Split(',');
            data.attackTargetTags = atkTags;

            data.attackDistance =int.Parse(nodes[4].InnerText);

            data.attackAngle = int.Parse(nodes[5].InnerText);


            data.atkRatio =nodes[6].InnerText.Split(',');
          

            data.durationTime = float.Parse(nodes[7].InnerText);

            data.atkInterval = float.Parse(nodes[8].InnerText);

            data.animationName = nodes[9].InnerText;

            data.attackType = (nodes[10].InnerText=="Single")? SkillAttackType.Single:SkillAttackType.Group;

            data.selectorType = (nodes[11].InnerText == "Sector") ? SelectorType.Sector : SelectorType.Rectangle;

            data.prefabName = nodes[12].InnerText;

            if(nodes[13]!=null)
            data.Buff = nodes[13].InnerText;

            return data;
        }


    }
}
