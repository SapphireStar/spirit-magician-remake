using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void GetHit(float health);
    public Vector3 GetBeAttackPlace();//获取该对象被攻击的位置
}
