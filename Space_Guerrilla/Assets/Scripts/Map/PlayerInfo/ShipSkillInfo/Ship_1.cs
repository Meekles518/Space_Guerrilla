using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    [CreateAssetMenu]
    public class Ship_1 : ScriptableObject
    {
       



        //직접 Skill들의 이름과 쿨타임을 List안에 기입해야 함

        //skill의 이름들을 담을 리스트
        public List<string> skillNames = new List<string>() {"Skill1", "Skill2", "Skill3"};

        //skill의 최대 쿨타임을 저장할 리스트
        public List<float> skillMaxCooltime = new List<float>() {3.0f, 5.0f, 6.0f };


        //각 Skill들의 이름을 함수명으로 만들어 구현해야 함.
        //모든 Skill들은 사용 시 MapManager에 접근해 현재 쿨타임의 상태를 확인해야 한다.
        //각 스킬을 의미하는 함수 구현 시 그 스킬의 특징들과 쿨타임 설정들을 같이 기입.

        //Skill1 함수
        public void Skill1()
        {


        }//Skill1


        //SKill2 함수
        public void Skill2()
        {


        }//Skill2

        //Skill3 함수
        public void Skill3()
        {


        }//Skill3



    }



}
 
