using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public class AegisSkill : FieldSkill
    {

        private PlayerInput playerInput; // PlayerInput을 불러옴
        private PlayerMovement playerMovement;
        private PlayerShooter[] shooters; //PlayerShooter들을 저장할 배열


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

        }


        // Update is called once per frame
        private void FixedUpdate()
        {

        }



    }
}
 
