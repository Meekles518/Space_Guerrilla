using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;

//Aegis ��ų�� �����ϴ� ���丮 Ŭ����
public class AegisSkillFactory : SkillFactory<AegisSkillBase, AegisSkillName, AegisSkillManager>
{
    public AegisSkillBase createSkill(AegisSkillName skillName, GameObject player, int level, AegisSkillManager skillManager)
    {
        switch (skillName)
        {
            case AegisSkillName.AfterBurner:

                return new AfterBurnerBase(player, level, skillManager);

            case AegisSkillName.StreamLiner:

                return new StreamLinerBase(player, level, skillManager);


            case AegisSkillName.LaunchCruiseMissile:

                return null;

            default:
                throw new System.Exception("Err");
        }

    }

    

}
