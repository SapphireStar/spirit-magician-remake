using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ElfWizard
{

    //释放器配置工厂
    //提供创建释放器的各种算法对象的功能
    //将对象的创建和使用分离，当释放器需要算法时，只需要调用工厂方法创建即可
    public class DeployerConfigFactory
    {
        static Dictionary<string, object> cache = new Dictionary<string, object>();

        static DeployerConfigFactory()
        {
            cache = new Dictionary<string, object>();
        }

        //反射创建过程：
        //1:获取类型
        //2:创建对象
        public static IAttackSelector CreateAttackSelector(SkillData data)
        {
            //选区类型
            //选区对象命名规则：
            //命名空间+枚举名+AttackSelecter
            //例如扇形选区：SectorAttackSelecter
            string className = string.Format("ElfWizard.{0}AttackSelector", data.selectorType);
            return CreateObject<IAttackSelector>(className);
        }
        public static IImpactEffect[] CreateImpactEffects(SkillData data)
        {
            IImpactEffect[] impactArray=new IImpactEffect[data.impactType.Length];
            //技能效果：
            //命名规范：impactType[]+Impact

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
        /// 所有CreateAttackSelector创造的对象都继承自IAttackSelector，所有CreateImpactEffects创造的对象都继承自IImpactEffect接口
        /// 所有CreateBuff创造的对象都继承自IBuff类，因此，只要在这些接口和抽象类中定义统一的抽象方法，让子类实现。那么通过该工厂
        /// 创建出来的对象都可以被代码调用这些方法，并且这些子类可以对这些方法有不同的实现方式，实现了多态。
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
            return cache[className] as T;//创建一个缓存，当缓存中没有所需要的算法类型对象时，使用反射创建，然后加入到字典中，若字典中有该类型对象，则直接返回该对象


        }
    }
}