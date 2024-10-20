using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;
using UnityEngine.UI;
//


public abstract class AegisSkillBase
{
    
    public AegisSkillName SkillName { get; set; } //스킬 이름

    public int Level { get; private set; } //스킬 레벨

    public bool IsActive { get; private set; } //스킬 활성화 여부

    public int skillCnt { get;  set; } //스킬 사용 가능 횟수

    public Image skillImg { get;  set; } // 스킬의 image

    protected ISkillBehavior skillBehavior; //스킬 행동 인터페이스


    //AegisSkillBase 생성자
    public AegisSkillBase(int level)
    {
        this.Level = level; //스킬의 레벨 설정
    }//AegisSkillBase 생성자 끝


    protected abstract void SetBehavior(int level); //레벨에 따른 스킬 행동을 설정하는 메서드

    //ISkillBehavior 인터페이스를 상속받은 스킬 행동 클래스를 받아오는 메서드
    public void UseSkill()
    {
        IsActive = true; //스킬 사용 중 표시
        skillBehavior.UseSkill(); //스킬 사용 메서드 호출
    }

    //ISkillBehavior 인터페이스를 상속받은 스킬 행동 클래스를 받아오는 메서드
    public void CancelSkill()
    {
        IsActive = false;   //스킬 미사용 중 표시
        skillBehavior.CancelSkill(); //스킬 취소 메서드 호출
    }




}
