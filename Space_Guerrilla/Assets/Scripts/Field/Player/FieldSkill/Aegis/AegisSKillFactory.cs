using PlayerSkillName;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AegisSKillFactory : SkillFactory
{
    public override SkillBase CreateSkill(SkillName skillName, int lvl, GameObject player, SkillManager skillManager)
    {
        switch (skillName)
        {
            case SkillName.AegisAfterBurner:
                
                return new AfterBurnerBase(skillName, lvl, player, skillManager);
               

            case SkillName.AegisStreamLiner:

                return new StreamLinerBase(skillName, lvl, player, skillManager);

            case SkillName.AegisLaunchMissile:


                return new LaunchMissileBase(skillName, lvl, player, skillManager);
        }

        return null;
    }


}
