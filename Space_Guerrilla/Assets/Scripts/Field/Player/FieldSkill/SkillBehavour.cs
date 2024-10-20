using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    //스킬을 사용하는 행동을 정의하는 인터페이스
    //모든 스킬들은 이 인터페이스를 상속받아 구현
    public interface ISkillBehavior
    {


        //실제 스킬을 사용하는 메서드
        public abstract void UseSkill();

        //사용중인 스킬을 취소하는 메서드
        public abstract void CancelSkill();

        //이외에도 필요한게 있다면..


    }


