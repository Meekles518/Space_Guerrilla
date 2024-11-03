using PlayerSkillName;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterBurnerBase : SkillBase
{
    //
    public AfterBurnerBase(SkillName skillName, int lvl, GameObject player, SkillManager skillManager) : base(skillName, lvl, player, skillManager)
    {
        //각 스킬에 맞는 추가 변수 값 할당 과정을 생성자에서 처리 필요

    }


    public override void UseSkill()
    {
        throw new System.NotImplementedException();
    }

    public void ActiveSkill()
    {
        throw new System.NotImplementedException();
    }

    public void CancelSkill()
    {
        throw new System.NotImplementedException();
    }
}
