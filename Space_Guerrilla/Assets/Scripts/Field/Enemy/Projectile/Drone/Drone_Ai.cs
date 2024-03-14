using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// 공격형 적 Ai의 행동을 제어하는 스크립트
// 공격형 적 State들과 Enemy_Control 사이를 연결
public class Drone_AI : MonoBehaviour
{
    public Drone_State currentState; // 현재 스테이트
    public Transform player; // 플레이어 트랜스폼
    public Drone_Control control; // Enemy_Control 컴포넌트

    public float currTime; // 타이머에 사용할 현재시간
    public float timer; // 타이머에 사용할 타이머가 끝나는 시간

    // 값들 초기화
    void OnEnable()
    {
        control = GetComponent<Drone_Control>(); // 컨트롤 컴포넌트를 가져옴
        player = GameObject.Find("Player").transform; // 플레이어 오브젝트를 찾아 트랜스폼 할당
        currentState = new Drone_Idle(gameObject, player, control, currTime); // 초기 스테이트를 Idle로 설정

        control.timer = timer;

    }//OnEnable


    // 값들을 지속적으로 갱신
    void FixedUpdate()
    {
        control.timer = timer;

        // 현재 State를 지속적으로 갱신
        currentState = currentState.Process();
        // 현재 State를 Drone_Control에 전달
        control.statename = (Drone_Control.STATE)currentState.name;

    }//FixedUpdate
}