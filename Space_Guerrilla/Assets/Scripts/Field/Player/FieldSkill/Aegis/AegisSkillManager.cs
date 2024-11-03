using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSkillName;

public class AegisSkillManager : SkillManager
{


     

    public override void OnEnable()
    {
        this.skillFactory = new AegisSKillFactory(); //우주선에 맞는 전용 SkillFactory 생성 저장
        this.player = this.gameObject;
        playerInput = player.GetComponent<PlayerInput>();
        curStatus = SkillName.None;

        //임시로 OnEnable을 통해서 dict에 스킬 추가하게끔 하고, 추후에는 다른 값을 통해 AddSKill 메서드를 통해 추가하게끔?
        AddSkill(SkillName.AegisAfterBurner, 1);
        AddSkill(SkillName.AegisStreamLiner, 1);
        AddSkill(SkillName.AegisLaunchMissile, 1);


    }

    public override void FixedUpdate()
    {
        if (playerInput.skillQ)
        {
            UseSkill(SkillName.AegisAfterBurner);
        }

        if (playerInput.skillZ)
        {
            UseSkill(SkillName.AegisStreamLiner);
        }

        if (playerInput.skillX)
        {
            UseSkill(SkillName.AegisLaunchMissile);
        }

    }


}
