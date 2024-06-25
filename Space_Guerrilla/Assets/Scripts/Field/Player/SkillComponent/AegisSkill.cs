using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public class AegisSkill : MonoBehaviour
    {

        private PlayerInput playerInput; // PlayerInput�� �ҷ���

        //skill�� �̸����� ���� ����Ʈ
        public List<string> skillNames = new List<string>() { 
            "Aegis_fieldRepair", 
            "Aegis_dimensionJump", 
            "Aegis_ward",
            "Aegis_remoteRepair", 
            "Aegis_missile", 
            "Aegis_afterBurner", 
            "Aegis_streamLiner" };

        //���� ���� ȸ����
        public float remoteRepairVal = 120;

        //���� ��ų ���� Ƚ��, ��ų ���� �ð�, �̵��ӵ� ���� ��ġ
        public int afterBurnerCnt = 2;
        public float afterBurnerTime = 2.5f;
        public float afterBurnerSpeed = 50f;

        //���� ��ų ���� Ƚ��, ��ų ���� �ð�, �� ���� ���� �ð� ��ġ
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
 
