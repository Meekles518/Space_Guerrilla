using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;


public class StreamLinerBase : AegisSkillBase
{

    public StreamLinerBase(GameObject p, int level, AegisSkillManager skillManager) : base(level, skillManager)
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

            skillBehavior = new Lv1StreamLiner(player, skillManager);
                skillCnt = (skillBehavior as Lv1StreamLiner).streamLinerCnt;
                skillImg = null; //���� �� ��ų �̹��� ����
                break;


        }

    }//SetBehavior

}
