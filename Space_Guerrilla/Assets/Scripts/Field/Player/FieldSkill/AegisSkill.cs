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
        public int afterBurnerUse = 0;
        public float afterBurnerTime = 2.5f;
        public float afterBurnerSpeed = 50f;
        public bool afterBurnerUsing = false; // 스킬 효과(속도 증가) 작동중 여부
        public bool afterBurnerAvailable = true; // 스킬 사용 가능 여부
        public int magCapacity;

        //각각 스킬 가능 횟수, 스킬 사용한 횟수, 스킬 지속 시간, 주 무기 과열 시간 수치
        public float streamLinerCnt = 1;
        public int streamLinerUse = 0;
        public float streamLinerTime = 3f;
        public float streamLinerCool = 2f;
        public bool streamLinerUsing = false; // 무한 탄환 작동중 여부
        public bool streamLinerAvailable = false; // 스킬 사용 가능 여부
        public int streamLinerMax = 999;
        public float streamLinerRotateTime = 1.5f;


        //미사일 발사 스킬에 관한 변수들
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
        //1. 애프터버너 구동/취소 구현
        //2. 스트림라이너 구동/취소 구현
        //3. 미사일 구동 구현
        //4. 각 스킬간의 연관 관계 구현(애프터버너 사용시 다른 스킬 강제캔슬, 사용 불가)

        public IEnumerator Aegis_afterBurner()
        {
            //사용 횟수가 남아있다면
            if (afterBurnerCnt > afterBurnerUse)
            {
                
                //우선 스킬 사용중인지를 체크
                if (!afterBurnerUsing)
                {

                    afterBurnerActive();

                    yield return new WaitForSeconds(afterBurnerTime);

                    afterBurnerCancel();

                }

                //스킬 효과 시전 중 다시 스킬 버튼을 눌러 중도에 취소 할 경우에
                else
                {
                    afterBurnerCancel();
                }

            }


        }

        private void afterBurnerActive()
        {
            if (!afterBurnerUsing)
            {
                afterBurnerUsing = true; //사용중 표시
                streamLinerAvailable = false; // 스트림라이너 사용 불가 표시
                streamLinerCancel(); // 스트림라이너 스킬 기능 중단(기능 작동중일 때에만 실행되는 함수)
                playerMovement.moveSpeed += afterBurnerSpeed; //이동속도 증가

                //모든 shooter의 최대 탄환과 현재 탄환 수를 0으로 만들기
                magnumZero();

                //이후에 스킬 아이콘을 캔슬 이미지로 변경하는 코드 필요

            }
        }

        private void afterBurnerCancel()
        {
            //스킬의 효과가 지속되고 있을 경우에만 아래 코드 실행
            if (afterBurnerUsing)
            {
                afterBurnerUsing = false; //사용 안함 표시
                playerMovement.moveSpeed -= afterBurnerSpeed;

                //모든 shooter의 최대 탄환과 현재 탄환 수를 최대로 만들기
                magnumSet();

                
                afterBurnerUse++; //사용한 횟수 증가

                //이후에 스킬 아이콘을 원래의 이미지로 변경하는 코드 필요
            }
        }

        private void magnumZero()
        {
            //모든 shooter의 최대 탄환과 현재 탄환 수를 0으로 만들기
            for (int i = 0; i < shooters.Length; i++)
            {
                shooters[i].magCapacity = 0;
                shooters[i].magAmmo = 0;
            }
        }
        private void magnumSet()
        {
            //애프터버너나 스트림라이너 스킬이 아직 작동중이라면 return을 통해 탄환 복구문을 스킵
            if (afterBurnerUsing || streamLinerUsing)
            {
                return;
            }

            //모든 shooter의 현재 탄환과 최대 탄환 수를 최대로 변경하기
            for (int i = 0; i < shooters.Length; i++)
            {
                shooters[i].magCapacity = magCapacity;
                shooters[i].magAmmo = magCapacity;
            }
        }

        
        public IEnumerator Aegis_streamLiner()
        {
            //스킬 사용 횟수가 남아있고, 스킬 사용중이 아니며 사용 가능하다면
            if (streamLinerCnt > streamLinerUse && !streamLinerUsing && streamLinerAvailable)
            {

                streamLinerActive(); // 스킬 실행

                // 회전 시간 + 사용 가능 시간이 지난 후에,
                yield return new WaitForSeconds(streamLinerRotateTime + streamLinerTime);

                streamLinerCancel(); // 스킬 기능 취소, 이전 상태로 복구 

            }
        }
        
        private void streamLinerActive()
        {
            //스킬 사용중이 아니고, 사용 가능하다면
            if (!streamLinerUsing && streamLinerAvailable)
            {
                streamLinerUsing = true; //스킬 사용중 표시

                magnumZero(); // 회전 하는 동안 발사 못하게 탄환 제거

                //shooter 회전시키기
                StartCoroutine(rotateShooter(true));
 

            }
        }

        //shooter 회전 및 총알 수를 제어하는 Coroutine
        private IEnumerator rotateShooter(bool active)
        {

            Quaternion rightShooter = shooters[0].transform.rotation;
            Quaternion leftShooter = shooters[1].transform.rotation;
            float elapsedTime = 0f;

            //active가 true라면, 포신을 직선 방향으로 회전
            if (active)
            {
                //streamLinerRotateTime 동안 shooter 들 회전시키기
                while (elapsedTime < streamLinerRotateTime)
                {
                    shooters[0].transform.rotation = Quaternion.Lerp(rightShooter, Quaternion.Euler(0, 0, 0), elapsedTime / streamLinerRotateTime);
                    shooters[1].transform.rotation = Quaternion.Lerp(leftShooter, Quaternion.Euler(0, 0, 0), elapsedTime / streamLinerRotateTime);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                //회전 후 오차값 방지 위해 목표 각도로 재 조정
                shooters[0].transform.rotation = Quaternion.Euler(0, 0, 0);
                shooters[1].transform.rotation = Quaternion.Euler(0, 0, 0);

                //모든 shooter에 무한탄환 표시
                for (int i = 0; i < shooters.Length; i++)
                {
                    shooters[i].magCapacity = streamLinerMax;
                    shooters[i].magAmmo = streamLinerMax;

                }
            }

            //active가 false라면, 포신을 원래의 방향으로 되돌리는 회전
            else
            {
                //streamLinerRotateTime 동안 shooter 들 회전시키기
                while (elapsedTime < streamLinerRotateTime)
                {
                    shooters[0].transform.rotation = Quaternion.Lerp(rightShooter, Quaternion.Euler(0, 0, -30f), elapsedTime / streamLinerRotateTime);
                    shooters[1].transform.rotation = Quaternion.Lerp(leftShooter, Quaternion.Euler(0, 0, 30f), elapsedTime / streamLinerRotateTime);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                //회전 후 오차값 방지 위해 목표 각도로 재 조정
                shooters[0].transform.rotation = Quaternion.Euler(0, 0, -30f);
                shooters[1].transform.rotation = Quaternion.Euler(0, 0, 30f);

                // 과부하 설정 시간 - 회전 소요 시간, 즉 남은 과부하 시간동안 대기
                yield return new WaitForSeconds(streamLinerCool - streamLinerRotateTime);

                //애프터버너 사용 중이 아니라면, 탄환 수를 정상으로 북구
                magnumSet();
                streamLinerAvailable = true; // 스킬 사용 가능 상태로 표시 
            }

        }

        private void streamLinerCancel()
        {
            //스킬 사용중이라면
            if (streamLinerUsing)
            {
                streamLinerUse++; // 사용한 횟수 추가
                streamLinerUsing = false; //사용 중 아님 표시
                streamLinerAvailable = false; // 과부하로 사용 불가능함 표시
                magnumZero(); // 탄환 0으로 만들기
                StartCoroutine(rotateShooter(false)); // shooter 원래 방향으로 회전
            }
        }

    }
}
 
