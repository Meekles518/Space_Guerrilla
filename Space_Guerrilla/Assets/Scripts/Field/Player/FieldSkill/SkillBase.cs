using PlayerSkillName;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillBase : MonoBehaviour
{
    public SkillName skillName; //스킬의 이름
    public int Level { get; protected set; } //스킬의 레벨\
    public int skillCnt { get; protected set; } //스킬의 사용 가능 횟수
    public Image skillImg { get; set; } //스킬 아이콘 이미지(임시 변수)
    private SkillManager skillManager { get; set; } //스킬 매니저
    private GameObject player { get; set; } //플레이어


    public SkillBase(SkillName skillName, int level, GameObject player, SkillManager skillManager)
    {
        this.skillName = skillName;
        this.Level = level;
        this.player = player;
        this.skillManager = skillManager;
    }

    public abstract void UseSkill(); //스킬 버튼 눌렀을 때 실행될 메서드, 현 상태에 따라 다른 동작 수행
   
    //현재 우선 스킬에는 종류에 따라 시전/취소 의 기능이 있음. 필요한 것은 추후에 추가 가능.





}
