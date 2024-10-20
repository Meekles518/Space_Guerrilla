using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;

//Aegis 스킬을 생성하는 팩토리 클래스
public class AegisSkillFactory
{
    public AegisSkillBase createSkill(AegisSkillName skillName, PlayerMovement pm, Shooter[] sts, int level)
    {
        switch (skillName)
        {
            case AegisSkillName.AfterBurner:

                return new AfterBurnerBase(pm, sts, level);

            case AegisSkillName.StreamLiner:

                return new StreamLinerBase(sts, level);


            case AegisSkillName.LaunchCruiseMissile:

                return null;

            default:
                throw new System.Exception("Err");
        }

    }

    

}
