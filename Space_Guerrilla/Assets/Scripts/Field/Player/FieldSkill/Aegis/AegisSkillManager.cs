using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;

namespace Aegis
{
    //Aegis가 사용할 스킬들을 enum으로 열거
    public enum AegisSkillName
    {
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
    public AegisSkillFactory skillFactory;

    //Player 우주선과 관련된 컴포넌트 저장 변수
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private Shooter[] shooters; 


    private void OnEnable()
    {
        skillFactory = new AegisSkillFactory(); //스킬 팩토리 인스턴스 생성

        //우주선에서 필요한 컴포넌트들 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        shooters = GetComponentsInChildren<Shooter>(); 
    }

    //스킬을 사용가능 Dict에 추가/교체 하는 메서드
    public void AddSkill(AegisSkillName skillName, int level)
    {
        //스킬 팩토리에서 특정 스킬의 특정 레벨 인스턴스 생성해서 가져오기
        AegisSkillBase skillBase = skillFactory.createSkill(skillName, playerMovement, shooters, level);
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
        switch (skillName)
        {
            case AegisSkillName.AfterBurner:

                //StreamLiner이 작동 중이 아니라면
                if (!checkActive(AegisSkillName.StreamLiner))
                {
                    //스킬 사용
                }

                //StreamLiner 작동 중일 시
                else
                {
                    //StreamLiner 캔슬 및 스킬 사용 로직

                }

                break;

            case AegisSkillName.StreamLiner:

                //AfterBurner이 작동 중이 아니라면
                if (!checkActive(AegisSkillName.AfterBurner))
                {
                    //스킬 사용

                }

                break;

            case AegisSkillName.LaunchCruiseMissile:

                //AfterBurner, StreamLiner이 작동중이 아니라면
                if (!checkActive(AegisSkillName.AfterBurner) && !checkActive(AegisSkillName.StreamLiner))
                {
                    //스킬 사용

                }


                break;
        }



    }//Skill
    
    //다른 스킬이 사용중인지 확인하는 메서드
    private bool checkActive(AegisSkillName skillName)
    {
        if (!activeSkill.ContainsKey(skillName) || !activeSkill[skillName].IsActive)
        {
            return true;
        }
        return false;
    }//checkActive

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
