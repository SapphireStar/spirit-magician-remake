using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{
    public class testScript : MonoBehaviour
    {
        CharacterSkillSystem skillsystem;
        // Start is called before the first frame update
        void Start()
        {
            skillsystem = GetComponent<CharacterSkillSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                skillsystem.UseSkill(2);
            }
        }
    }
}
