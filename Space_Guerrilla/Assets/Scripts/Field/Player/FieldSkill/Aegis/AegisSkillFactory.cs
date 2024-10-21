using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;

//Aegis ��ų�� �����ϴ� ���丮 Ŭ����
public class AegisSkillFactory
{
    public AegisSkillBase createSkill(AegisSkillName skillName, GameObject player, int level)
    {
        switch (skillName)
        {
            case AegisSkillName.AfterBurner:

                return new AfterBurnerBase(player, level);

            case AegisSkillName.StreamLiner:

                return new StreamLinerBase(player, level);


            case AegisSkillName.LaunchCruiseMissile:

                return null;

            default:
                throw new System.Exception("Err");
        }

    }

    

}
