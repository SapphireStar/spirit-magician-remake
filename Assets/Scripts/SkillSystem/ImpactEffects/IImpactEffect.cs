using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    //����ѡ���Ľӿ�
    public interface IImpactEffect
    {
        //Ӱ��Ч��
        //data:��������
        //skillTrans�������������transform
        void Excecute(SkillDeployer deployer,int targetIndex);
        void InitEffect(float level1, float level2, float level3);
    }
}