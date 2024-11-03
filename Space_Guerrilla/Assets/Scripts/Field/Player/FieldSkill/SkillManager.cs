using PlayerSkillName;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSkillName
{
    //우주선 이름 + 스킬 이름 의 형태로 스킬 이름 열거
    public enum SkillName
    {
        None,
        AegisAfterBurner,
        AegisStreamLiner,
        AegisLaunchMissile,

    }
}


//모든 우주선의 스킬 사용을 관리할 SkillManager 추상 클래스
//이 클래스를 상속받는 각 우주선의 SkillManager 스크립트가 우주선 prefab에 추가되어야 함
public abstract class SkillManager : MonoBehaviour
{
    //사용 가능한 스킬을 담을 딕셔너리
    public Dictionary<SkillName, SkillBase> activeSkill = new Dictionary<SkillName, SkillBase>();


    public SkillName curStatus; //현재 시전 중인 스킬의 이름 저장 변수
    protected SkillFactory skillFactory; //스킬 팩토리
    protected GameObject player; //플레이어
    protected PlayerInput playerInput; //플레이어 입력

    public abstract void OnEnable(); //OnEnable 메서드 하위 클래스에서 구현 의무
    public abstract void FixedUpdate(); //FixedUpdate 메서드 하위 클래스에서 구현 의무

    //activeSkill에 스킬 추가하는 메서드
    public void AddSkill(SkillName skillName, int lvl)
    {
        var skill = skillFactory.CreateSkill(skillName, lvl, player, this);
        activeSkill.Add(skillName, skill);
    }

    //activeSkill에서 스킬 제거하는 메서드
    public void RemoveSkill(SkillName skillName)
    {
        if (activeSkill.ContainsKey(skillName))
        {
            activeSkill.Remove(skillName);
        }
    }

    //스킬 버튼을 눌렀을 때 동작 수행을 시도하는 메서드
    public void UseSkill(SkillName skillName)
    {
        if (activeSkill.ContainsKey(skillName))
        {
            activeSkill[skillName].UseSkill();
        }
    }

}
