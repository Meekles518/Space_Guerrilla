using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;

public class AfterBurnerBase : AegisSkillBase
{


    //��ų Base �����ڿ��� ��ų ���ۿ� �ʿ��� ������Ʈ���� ����
    //base(level)�� ���� �����ڿ��� SetBehavior �����ϱ�
    public AfterBurnerBase(GameObject p, int level) : base(level)
    {
        SkillName = AegisSkillName.AfterBurner;
        player = p;
        SetBehavior(level);
    }

    //��ų�� ���� �� �ν��Ͻ� �������� �޼���
    protected override void SetBehavior(int level)
    {
        switch (level)
        {
            case 1:
                //Lv1�� AfterBurner �ν��Ͻ� �����ϱ�
                skillBehavior = new Lv1AfterBurner(player);
                skillCnt = (skillBehavior as Lv1AfterBurner).afterBurnerCnt;
                skillImg = null; //���� �� ��ų �̹��� ����


                break;

            

        }
    }//SetBehavior


}
