using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    //攻击选区的接口
    public interface IImpactEffect
    {
        //影响效果
        //data:技能数据
        //skillTrans技能所在物体的transform
        void Excecute(SkillDeployer deployer,int targetIndex);
        void InitEffect(float level1, float level2, float level3);
    }
}