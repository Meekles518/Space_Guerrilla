using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

//적을 Map에 나타내는 데에 사용하는 Circle이 모두 가지고 있을 클래스,
//모든 Ai 스크립트는 Enemy_Circle 클래스를 상속받는다??
public class Enemy_Circle : MonoBehaviour
{

    //적의 행동성향 enum
    public enum Behavior
    {
        Defensive,
        Offensive,
        Opportunistic,
        Stationary,

    }


    public Behavior behavior; //적의 행동성향을 저장할 변수


    //virtual, 가상 함수로 적의 Ai 로직을 구현. 이 Class를 상속받는 서로 다른 종류의 적이
    //함수를 마저 완성시키기.
    public virtual void movement()
    {

    }





}

