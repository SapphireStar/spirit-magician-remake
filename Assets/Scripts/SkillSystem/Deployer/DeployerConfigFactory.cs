using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ElfWizard
{

    //�ͷ������ù���
    //�ṩ�����ͷ����ĸ����㷨����Ĺ���
    //������Ĵ�����ʹ�÷��룬���ͷ�����Ҫ�㷨ʱ��ֻ��Ҫ���ù���������������
    public class DeployerConfigFactory
    {
        static Dictionary<string, object> cache = new Dictionary<string, object>();

        static DeployerConfigFactory()
        {
            cache = new Dictionary<string, object>();
        }

        //���䴴�����̣�
        //1:��ȡ����
        //2:��������
        public static IAttackSelector CreateAttackSelector(SkillData data)
        {
            //ѡ������
            //ѡ��������������
            //�����ռ�+ö����+AttackSelecter
            //��������ѡ����SectorAttackSelecter
            string className = string.Format("ElfWizard.{0}AttackSelector", data.selectorType);
            return CreateObject<IAttackSelector>(className);
        }
        public static IImpactEffect[] CreateImpactEffects(SkillData data)
        {
            IImpactEffect[] impactArray=new IImpactEffect[data.impactType.Length];
            //����Ч����
            //�����淶��impactType[]+Impact

            for (int i = 0; i < data.impactType.Length; i++)
            {
                string classNameImpact = string.Format("ElfWizard.{0}Impact", data.impactType[i]);
                impactArray[i] = CreateObject<IImpactEffect>(classNameImpact);

            }
            return impactArray;
        }
        public static IBuff CreateBuff(SkillData data)
        {
            IBuff buff;
            string buffName = string.Format("ElfWizard.{0}BUFF", data.Buff);
            buff = CreateObject<IBuff>(buffName);
            return buff;
        }

        /// <summary>
        /// ����CreateAttackSelector����Ķ��󶼼̳���IAttackSelector������CreateImpactEffects����Ķ��󶼼̳���IImpactEffect�ӿ�
        /// ����CreateBuff����Ķ��󶼼̳���IBuff�࣬��ˣ�ֻҪ����Щ�ӿںͳ������ж���ͳһ�ĳ��󷽷���������ʵ�֡���ôͨ���ù���
        /// ���������Ķ��󶼿��Ա����������Щ������������Щ������Զ���Щ�����в�ͬ��ʵ�ַ�ʽ��ʵ���˶�̬��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="className"></param>
        /// <returns></returns>
        private static T CreateObject<T>(string className) where T : class
        {
            if (!cache.ContainsKey(className))
            {
                Type type = Type.GetType(className);
                object instance = Activator.CreateInstance(type);
                cache.Add(className, instance);

            }
            return cache[className] as T;//����һ�����棬��������û������Ҫ���㷨���Ͷ���ʱ��ʹ�÷��䴴����Ȼ����뵽�ֵ��У����ֵ����и����Ͷ�����ֱ�ӷ��ظö���


        }
    }
}