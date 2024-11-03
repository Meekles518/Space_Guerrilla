using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// 공격형 적 Ai의 행동을 제어하는 스크립트
// 공격형 적 State들과 Enemy_Control 사이를 연결
public class Opportunistic_AI : MonoBehaviour
{
    [Header("현재 스테이트")]
    Ai_State currentState; // 현재 스테이트
    [HideInInspector]
    public Transform player; // 플레이어 트랜스폼
    [HideInInspector]
    public Enemy_Control control; // Enemy_Control 컴포넌트
    [HideInInspector]
    public float currTime; // 타이머에 사용할 현재시간

    // 값들 초기화
    void OnEnable()
    {
        control = GetComponent<Enemy_Control>(); // 컨트롤 컴포넌트를 가져옴
        currentState = new Opportunistic_Idle(gameObject, player, control, currTime); // 초기 스테이트를 Idle로 설정
    }

    // 값들을 지속적으로 갱신
    void FixedUpdate()
    {
        // 현재 State를 지속적으로 갱신
        currentState = currentState.Process();
        // 현재 State를 Enmey_Control로 전달
        control.statename = (Enemy_Control.STATE)currentState.name;

        if (currentState.Aggro() == true)
        {
            control.isAggro = true;
        }
        else
        {
            control.isAggro = false;
        }

    }
}
    

