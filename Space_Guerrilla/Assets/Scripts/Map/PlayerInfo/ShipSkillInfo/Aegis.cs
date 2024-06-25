using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Skill
{
    [CreateAssetMenu]
    public class Aegis : ScriptableObject
    {

        //직접 Skill들의 이름과 쿨타임을 List안에 기입해야 함

        //skill의 이름들을 담을 리스트
        public List<string> skillNames = new List<string>() { "Aegis_fieldRepair", "Aegis_dimensionJump", "Aegis_ward", "Aegis_remoteRepair", "Aegis_missile", "Aegis_afterBurner", "Aegis_streamLiner"};

        //skill의 최대 쿨타임을 저장할 리스트
        //-1은 패시브, 0은 라이트 스킬, 그 이외는 헤비 스킬 의미
        public List<float> skillMaxCooltime = new List<float>() { -1f, 3f, 1f, 4f, 4f, 0f, 0f};



    }

}
 
