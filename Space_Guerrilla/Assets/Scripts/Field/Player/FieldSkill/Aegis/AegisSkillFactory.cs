using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;

//Aegis ��ų�� �����ϴ� ���丮 Ŭ����
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
