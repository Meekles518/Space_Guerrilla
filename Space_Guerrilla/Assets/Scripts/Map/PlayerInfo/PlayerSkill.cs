using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Skill
{

    public class PlayerSkill : MonoBehaviour
    {

        //어딘가에서 Skill의 이름과 쿨타임 정보들을 저장해놓고, MapManager에서 판단한 우주선의 종류에 따라
        //그 우주선의 스킬 정보를 여기에 저장하고, 버튼 생성 후 그 스킬들의 기능을 버튼에 저장 및 위치 세팅



        //skill의 이름들을 담을 리스트
        public List<string> skillNames = new List<string>();

        //skill의 최대 쿨타임을 저장할 리스트
        public List<float> skillMaxCooltime = new List<float>();

        //skill의 현재 쿨타임을 저장할 리스트
        public List<float> skillCurrentColltime = new List<float>();


        public float skillVerticalDistance; //Skill UI들의 가로 거리 

        public float skillHorizontalDinstance; //Skill UI들의 세로 거리


        public void SetSkillBtn()
        {
            //Skill의 개수가 홀수면
            if (skillNames.Count % 2 == 1)
            {
                //홀수 개의 스킬 버튼을 균형적으로 생성 및 OnClick 함수에 스킬 기능 추가.



            }

            //Skill의 개수가 짝수면
            else if (skillNames.Count % 2 == 0)
            {


            }



        }//SetSkillBtn

    }

}
 
 
