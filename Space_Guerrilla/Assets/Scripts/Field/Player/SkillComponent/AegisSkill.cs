using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public class AegisSkill : MonoBehaviour
    {

        private PlayerInput playerInput; // PlayerInput을 불러옴

        //skill의 이름들을 담을 리스트
        public List<string> skillNames = new List<string>() { 
            "Aegis_fieldRepair", 
            "Aegis_dimensionJump", 
            "Aegis_ward",
            "Aegis_remoteRepair", 
            "Aegis_missile", 
            "Aegis_afterBurner", 
            "Aegis_streamLiner" };

        //원격 수리 회복량
        public float remoteRepairVal = 120;

        //각각 스킬 가능 횟수, 스킬 지속 시간, 이동속도 증가 수치
        public int afterBurnerCnt = 2;
        public float afterBurnerTime = 2.5f;
        public float afterBurnerSpeed = 50f;

        //각각 스킬 가능 횟수, 스킬 지속 시간, 주 무기 과열 시간 수치
        public float streamLinerCnt = 1;
        public float streamLinerTime = 3f;
        public float streamLinerCool = 2f;


        private void OnEnable()
        {
            
        }


        // Update is called once per frame
        private void FixedUpdate()
        {

        }



    }
}
 
