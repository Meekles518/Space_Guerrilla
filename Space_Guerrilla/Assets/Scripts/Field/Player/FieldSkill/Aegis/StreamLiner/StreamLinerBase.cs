using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;


public class StreamLinerBase : AegisSkillBase
{
    private Shooter[] shooters; 


    public StreamLinerBase(Shooter[] sts, int level) : base(level)
    {
        SkillName = AegisSkillName.StreamLiner;
        shooters = sts;
        SetBehavior(level);
    }


    protected override void SetBehavior(int level)
    {
        switch (level)
        {
            case 1:

                skillBehavior = new Lv1StreamLiner(shooters);
                break;


        }

    }//SetBehavior

}
