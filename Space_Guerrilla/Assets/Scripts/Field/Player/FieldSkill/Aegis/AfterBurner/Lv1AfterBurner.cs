using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lv1 AfterBurner 스킬을 사용하는 클래스
public class Lv1AfterBurner : ISkillBehavior
{

    private PlayerMovement playerMovement; //플레이어 이동을 담당하는 클래스
    private Shooter[] shooters; //Shooter 배열

    public int afterBurnerCnt { get; set; } //afterBurner 사용 가능 횟수
    public float afterBurnerTime {get; set; } //afterBurner 지속시간
    public float afterBurnerSpeed { get; set; } //afterBurner 속도
    private int magCapacity { get; set; } //탄창 최대 탄환 수

    private AegisSkillManager AegisSkillManager; //AegisSkillManager 클래스

    //지속시간 및 속도는 생성자에서 초기화
    public Lv1AfterBurner(GameObject p, AegisSkillManager skillManager)
    {
        this.playerMovement = p.GetComponent<PlayerMovement>(); //PlayerMovement 클래스를 받아옴
        this.afterBurnerCnt = 2; //사용 가능 횟수 2
        this.afterBurnerTime = 2.5f;   //afterBurner 지속시간 2.5
        this.afterBurnerSpeed = 50f; //afterBurner 속도 50 추가
        this.shooters = p.GetComponentsInChildren<Shooter>();
        this.magCapacity = shooters[0].magCapacity; //Shooters의 최대 탄환 수 저장해놓고
        this.AegisSkillManager = skillManager; //AegisSkillManager 받아옴
    }

    public void UseSkill()
    {
        //스킬 사용 횟수가 남아 있지 않으면 사용 안함
        

        magnumSet(0);
        afterBurnerActive(); //afterBurner 활성화
    }

    public void CancelSkill()
    {

    }


    //실질적으로 afterBurner의 기능(이속 증가)을 구현하는 함수
    private void afterBurnerActive()
    {
        
        playerMovement.moveSpeed += afterBurnerSpeed; //이동속도 증가
    }//afterBurnerActive

    //실질적으로 afterBurner 취소의 기능(이속 감소)을 구현하는 함수
    private void afterBurnerDeactive()
    {
        playerMovement.moveSpeed -= afterBurnerSpeed; //이동속도 감소
    }//afterBurnerDeactive


    //Shooters 배열의 발사대의 최대/현재 탄환을 조절하는 메서드
    private void magnumSet(int targetNum)
    {
        for (int i = 0; i < shooters.Length; i++)
        {
            shooters[i].magCapacity = targetNum;
            shooters[i].magAmmo = targetNum;
        }


    }//magnumSet

     
}
