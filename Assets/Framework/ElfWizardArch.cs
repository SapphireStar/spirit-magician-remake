using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfWizard.Util;
using Framework;
using ElfWizard;

namespace Framework
{
    public class ElfWizardArch : Architecture<ElfWizardArch>
    {
        protected override void Init()
        {
            RegisterModel<IUserModel>(new UserModel());
            RegisterModel<IBattleModel>(new BattleModel());
            RegisterModel<IGameModel>(new GameModel());

            RegisterSystem<IUISystem>(new UIManager());
            RegisterSystem<IAudioSystem>(new AudioManager());
            RegisterSystem<ISpawnSystem>(new SpawnSystem());
            RegisterSystem<IBattleSystem>(new BattleSystem());

            RegisterUtility<IResourceUtility>(new ResourceManager());


        }
    }
}