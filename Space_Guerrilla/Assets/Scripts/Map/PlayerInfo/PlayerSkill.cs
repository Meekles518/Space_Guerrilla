using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Map;


namespace Skill
{

    public class PlayerSkill : MonoBehaviour
    {

        //어딘가에서 Skill의 이름과 쿨타임 정보들을 저장해놓고, MapManager에서 판단한 우주선의 종류에 따라
        //그 우주선의 스킬 정보를 여기에 저장하고, 버튼 생성 후 그 스킬들의 기능을 버튼에 저장 및 위치 세팅

        //2월 20일 생각, 게임오브젝트 구현이 낫나, 버튼이 낫나??


        //skill의 이름들을 담을 리스트
        public List<string> skillNames = new List<string>();

        //skill의 최대 쿨타임을 저장할 리스트
        public List<float> skillMaxCooltime = new List<float>();

        //skill의 현재 쿨타임을 저장할 리스트
        public List<float> skillCurrentCooltime = new List<float>();


        public float skillVerticalDistance; //Skill UI들의 가로 거리 

        public float skillHorizontalDinstance; //Skill UI들의 세로 거리


        //Map 상의 스킬들을 버튼으로 화면에 표시하는 함수
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


        //다른 스크립트에 기록되어 있는 스킬 정보들을 여기로 가져오는 함수
        public void getSkill()
        {
            //MapManager의 shipName에 따라 cas문
            switch (MapManager.instance.shipName)
            {
                //Ship1 이라면
                case ShipName.Ship1:

                    Ship_1 ship_1 = ScriptableObject.CreateInstance<Ship_1>();
                    skillNames = ship_1.skillNames;
                    skillMaxCooltime = ship_1.skillMaxCooltime;
                    skillCurrentCooltime = ship_1.skillMaxCooltime;



                    break;

                //아래에 계속해서 각 우주선 별 스킬을 가져오는 코드를 작성해야 함.

                case ShipName.Aegis:

                    Aegis aegis = ScriptableObject.CreateInstance<Aegis>();
                    skillNames = aegis.skillNames;
                    skillMaxCooltime = aegis.skillMaxCooltime;
                    skillCurrentCooltime = aegis.skillMaxCooltime;


                    break;

            }



        }//getSkill 


    }

}
 
 
