using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;

namespace Aegis
{
    //Aegis가 사용할 스킬들을 enum으로 열거
    public enum AegisSkillName
    {
        None,
        AfterBurner,
        StreamLiner,
        LaunchCruiseMissile,
    }
}


//Aegis 스킬 시전/취소/상호작용 등을 관리하는 클래스
//실 Aegis Prefab에 추가되어야 할 스크립트(예정)
public class AegisSkillManager : MonoBehaviour
{
    //Aegis가 사용 가능한 스킬들을 가지고 있는 Dict
    public Dictionary<AegisSkillName, AegisSkillBase> activeSkill = new Dictionary<AegisSkillName, AegisSkillBase>();
    public SkillFactory<AegisSkillBase, AegisSkillName, AegisSkillManager> skillFactory;

    //Player 우주선과 관련된 컴포넌트 저장 변수
    private GameObject player;
    private PlayerInput playerInput;

    public AegisSkillName curStatus; //현재 사용 중인 스킬의 상태 표시


    public void OnEnable()
    {
        skillFactory = new AegisSkillFactory(); //스킬 팩토리 인스턴스 생성
        player = this.gameObject; //이 컴포넌트를 가지고 있는 GameObject(우주선 자체)
        playerInput = player.GetComponent<PlayerInput>(); //PlayerInput 컴포넌트 가져오기
        curStatus = AegisSkillName.None; //현재 사용 중인 스킬 초기화
    }

    //스킬을 사용가능 Dict에 추가/교체 하는 메서드
    public void AddSkill(AegisSkillName skillName, int level)
    {
        //스킬 팩토리에서 특정 스킬의 특정 레벨 인스턴스 생성해서 가져오기
        AegisSkillBase skillBase = skillFactory.createSkill(skillName, player, level, this);
        activeSkill.Add(skillName, skillBase); //Dict에 추가


    }//AddSkill

    //스킬을 Dict에서 제거하는 메서드
    public void RemoveSkill(AegisSkillName skillName)
    {
        if (activeSkill.ContainsKey(skillName))
        {
            activeSkill.Remove(skillName);
        }
    }//RemoveSkill

    //스킬 사용 버튼을 눌렀을 때에 동작(실행/취소 등)을 실행시키는 메서드
    public void Skill(AegisSkillName skillName)
    {
        if (activeSkill.ContainsKey(skillName))
        {
            activeSkill[skillName].UseSkill();
        }

    }//Skill
    

    private void FixedUpdate()
    {
        if (playerInput.skillQ)
        {
            Skill(AegisSkillName.AfterBurner);
        }

        if (playerInput.skillZ)
        {
            Skill(AegisSkillName.StreamLiner);
        }

        if (playerInput.skillX)
        {
            Skill(AegisSkillName.LaunchCruiseMissile);
        }



    }



}
