using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SkillFactory<TSkillBase, TSkillName, TSkillManager>
{
    public TSkillBase createSkill(TSkillName skillName, GameObject player, int level, TSkillManager skillManager);

}
