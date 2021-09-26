using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard.Util;
using Framework;

public class GameModel
{
    public BindableProperty<int> Enemykilled = new BindableProperty<int>()
    {
        Value = 0
    };

    public BindableProperty<int> Gold = new BindableProperty<int>()
    {
        Value = 0
    };

    public BindableProperty<int> Score = new BindableProperty<int>()
    {
        Value = 0
    };

    public BindableProperty<int> BestScore = new BindableProperty<int>()
    {
        Value = 0
    };
}
