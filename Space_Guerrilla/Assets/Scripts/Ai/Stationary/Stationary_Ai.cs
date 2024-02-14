using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// 공격형 적 Ai의 행동을 제어하는 스크립트
// 공격형 적 State들과 Enemy_Control 사이를 연결
public class Stationary_AI : MonoBehaviour
{
    Stationary_State currentState; // 현재 스테이트
    public Transform player; // 플레이어 트랜스폼
    public Enemy_Control control; // Enemy_Control 컴포넌트

    static public bool isShoot; // Enemy_Shooter를 제어하는데 사용할 불 변수

    public Vector2 SelfSpawnposition; // 본인 스폰위치
    public Vector2 FleetSpawnpoint; // 전대 스폰위치

    public float EnemytoPlayer; // 적과 플레이어 사이 거리

    public float currTime; // 타이머에 사용할 현재시간
    public float timer; // 타이머에 사용할 타이머가 끝나는 시간

    // 값들 초기화
    void OnEnable()
    {
        control = GetComponent<Enemy_Control>(); // 컨트롤 컴포넌트를 가져옴
        player = GameObject.Find("Player").transform; // 플레이어 오브젝트를 찾아 트랜스폼 할당
        currentState = new Stationary_Wait(gameObject, player, control); // 초기 스테이트를 Idle로 설정
        SelfSpawnposition = (Vector2)transform.position; // 자신의 스폰위치를 지정

        // 각종 거리의 초기값을 계산
        EnemytoPlayer = Vector2.Distance((Vector2)transform.position, player.position);

        // Offensive 적에게 필요한 초기값들을 컨트롤 컴포넌트에 전달
        control.EnemytoPlayer = EnemytoPlayer;
        control.timer = timer;
    }


    // 값들을 지속적으로 갱신
    void FixedUpdate()
    {
        // Offensive_Ai가 제공하는 값들을 지속적으로 갱신
        EnemytoPlayer = Vector2.Distance((Vector2)transform.position, player.position);
        control.EnemytoPlayer = EnemytoPlayer;
        control.timer = timer;

        // 현재 State를 지속적으로 갱신
        currentState = currentState.Process();
        // 현재 State를 Enmey_Control로 전달
        control.statename = (Enemy_Control.STATE)currentState.name;
    }
}