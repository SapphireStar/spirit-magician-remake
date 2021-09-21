using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard.Events
{
    public class InitPlayerElfPackageEvent
    {
        public List<PbSpirit.Spirit> playerElfs = new List<PbSpirit.Spirit>();
        public List<PbSpirit.Spirit> enemyElfs = new List<PbSpirit.Spirit>();
    }
}