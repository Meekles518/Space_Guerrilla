using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;

public class AfterBurnerBase : AegisSkillBase
{
    //��ų ��뿡 �ʿ��� ������Ʈ�� ����
    private PlayerMovement playerMovement; //PlayerMovement 
    private Shooter[] shooters; //shooter �迭

    //��ų Base �����ڿ��� ��ų ���ۿ� �ʿ��� ������Ʈ���� ����
    //base(level)�� ���� �����ڿ��� SetBehavior �����ϱ�
    public AfterBurnerBase(PlayerMovement pm, Shooter[] sts, int level) : base(level)
    {
        SkillName = AegisSkillName.AfterBurner;
        playerMovement = pm;
        shooters = sts;
        SetBehavior(level);
    }

    //��ų�� ���� �� �ν��Ͻ� �������� �޼���
    protected override void SetBehavior(int level)
    {
        switch (level)
        {
            case 1:
                //Lv1�� AfterBurner �ν��Ͻ� �����ϱ�
                skillBehavior = new Lv1AfterBurner(playerMovement, shooters);
                skillCnt = (skillBehavior as Lv1AfterBurner).afterBurnerCnt;
                skillImg = null; //���� �� ��ų �̹��� ����


                break;

            

        }
    }//SetBehavior


}
