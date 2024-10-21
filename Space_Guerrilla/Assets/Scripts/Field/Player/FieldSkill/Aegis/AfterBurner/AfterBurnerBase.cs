using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;

public class AfterBurnerBase : AegisSkillBase
{


    //스킬 Base 생성자에서 스킬 동작에 필요한 컴포넌트들을 저장
    //base(level)를 통해 생성자에서 SetBehavior 실행하기
    public AfterBurnerBase(GameObject p, int level) : base(level)
    {
        SkillName = AegisSkillName.AfterBurner;
        player = p;
        SetBehavior(level);
    }

    //스킬의 레벨 별 인스턴스 가져오는 메서드
    protected override void SetBehavior(int level)
    {
        switch (level)
        {
            case 1:
                //Lv1의 AfterBurner 인스턴스 생성하기
                skillBehavior = new Lv1AfterBurner(player);
                skillCnt = (skillBehavior as Lv1AfterBurner).afterBurnerCnt;
                skillImg = null; //레벨 별 스킬 이미지 설정


                break;

            

        }
    }//SetBehavior


}
