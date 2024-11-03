using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSkillName;


public abstract class SkillFactory 
{
    public abstract SkillBase CreateSkill(SkillName skillName, int lvl, GameObject player, SkillManager skillManager);

}
