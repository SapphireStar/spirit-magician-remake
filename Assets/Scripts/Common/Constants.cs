using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Constants 
{
    public const float PlayerSpeed = 3;
    public const float AccelerSpeed = 2;//用来控制动画之间的切换速度
    public const float Gravity = 9.8f;
    public static string SceneToLoad="map01";
    public static Action action;//用于在加载场景之后，进行一些初始化工作
}
