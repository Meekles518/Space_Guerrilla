using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;


public class StreamLinerBase : AegisSkillBase
{

    public StreamLinerBase(GameObject p, int level) : base(level)
    {
        SkillName = AegisSkillName.StreamLiner;
        player = p;
        SetBehavior(level);
    }


    protected override void SetBehavior(int level)
    {
        switch (level)
        {
            case 1:

                skillBehavior = new Lv1StreamLiner(player);
                skillCnt = (skillBehavior as Lv1StreamLiner).streamLinerCnt;
                skillImg = null; //레벨 별 스킬 이미지 설정
                break;


        }

    }//SetBehavior

}
