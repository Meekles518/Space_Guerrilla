using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public class AegisSkill : FieldSkill
    {

        private PlayerInput playerInput; // PlayerInput을 불러옴
        private PlayerMovement playerMovement;
        private Shooter[] shooters; //PlayerShooter들을 저장할 배열


        //skill의 이름들을 담을 리스트
        public List<string> skillNames = new List<string>() { 
            "Aegis_fieldRepair", 
            "Aegis_dimensionJump", 
            "Aegis_ward",
            "Aegis_remoteRepair", 
            "Aegis_missile", 
            "Aegis_afterBurner", 
            "Aegis_streamLiner" };
        //

        //skill의 최대 쿨타임을 저장할 리스트
        //-1은 패시브, 0은 라이트 스킬, 그 이외는 헤비 스킬 의미
        public List<float> skillMaxCooltime = new List<float>() { 
            -1f,
            3f, 
            1f, 
            4f, 
            4f, 
            0f, 
            0f };

        //원격 수리 회복량
        public float remoteRepairVal = 120;

        //각각 스킬 가능 횟수, 스킬 사용한 횟수, 스킬 지속 시간, 이동속도 증가 수치
        public int afterBurnerCnt = 2;
        public int afterBurnerUse;
        public float afterBurnerTime = 2.5f;
        public float afterBurnerSpeed = 50f;
        public bool afterBurnerUsing = false;
        public int magCapacity;

        //각각 스킬 가능 횟수, 스킬 사용한 횟수, 스킬 지속 시간, 주 무기 과열 시간 수치
        public float streamLinerCnt = 1;
        public int streamLinerUse;
        public float streamLinerTime = 3f;
        public float streamLinerCool = 2f;

        public override void SetSkillBtn()
        {
            
        }


        private void OnEnable()
        {
            //사용 횟수 초기화
            afterBurnerUse = 0;
            streamLinerUse = 0;

            //player의 component들 가져오기
            playerInput = GetComponent<PlayerInput>();
            playerMovement = GetComponent<PlayerMovement>();
            shooters = GetComponentsInChildren<Shooter>();
            magCapacity = shooters[0].magCapacity;
        }


        // Update is called once per frame
        private void FixedUpdate()
        {

        }

        //이아래에 스킬 함수들 구현 시작


        IEnumerator Aegis_afterBurner(float time)
        {
            //사용 횟수가 남아있다면
            if (afterBurnerCnt > afterBurnerUse)
            {
                
                //우선 스킬 사용중인지를 체크
                if (!afterBurnerUsing)
                {
                    
                    afterBurnerUsing = true; //사용중 표시
                    playerMovement.moveSpeed += afterBurnerSpeed; //이동속도 증가

                    //모든 shooter의 최대 탄환과 현재 탄환 수를 0으로 만들기
                    for (int i = 0; i < shooters.Length(); i++)
                    {
                        shooters[i].magCapacity = 0;
                        shooters[i].magAmmo = 0;
                    }

                    yield return new WaitForSeconds(time);

                    //스킬의 효과가 지속되고 있을 경우에만 아래 코드 실행
                    if (afterBurnerUsing)
                    {
                        playerMovement.moveSpeed -= afterBurnerSpeed;

                        //모든 shooter의 최대 탄환과 현재 탄환 수를 최대로 만들기
                        for (int i = 0; i < shooters.Length(); i++)
                        {
                            shooters[i].magCapacity = magCapacity;
                            shooters[i].magAmmo = magCapacity;
                        }

                        afterBurnerUse = false; //사용 안함 표시
                        afterBurnerUse++; //사용한 횟수 증가
                    }

                     

                }

                //스킬 효과 시전 중 다시 스킬 버튼을 눌러 중도에 취소 할 경우에
                else
                {
                    //스킬의 효과가 지속되고 있을 경우에만 아래 코드 실행
                    if (afterBurnerUsing)
                    {
                        playerMovement.moveSpeed -= afterBurnerSpeed;

                        //모든 shooter의 최대 탄환과 현재 탄환 수를 최대로 만들기
                        for (int i = 0; i < shooters.Length(); i++)
                        {
                            shooters[i].magCapacity = magCapacity;
                            shooters[i].magAmmo = magCapacity;
                        }

                        afterBurnerUse = false; //사용 안함 표시
                        afterBurnerUse++; //사용한 횟수 증가
                    }
                }

            }


        }

         
        public void Aegis_streamLiner()
        {

        }

    }
}
 
