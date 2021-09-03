using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public interface Spawnable
    {
        public void AddElf(string elf,int level, int uid);
        public void UpdateElfPos();
        public void RemoveLastElf();
    }
}