using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// 적 Ai 스크립트로부터 값을 받아 적 이동, 사격 스크립트를 제어하는 스크립트
// 모든 유형의 적들이 사용가능
public class Enemy_Control : MonoBehaviour
{
    [HideInInspector]
    public Transform player; // 플레이어 트랜스폼
    [Header("좌표설정")]    
    public Vector2 FleetSpawnpoint; // 전대 스폰위치
    public Vector2 Escapepoint; // 맵 탈출위치

    [Header("본인 스폰위치(자동설정됨)")]
    public Vector2 SelfSpawnposition; // 참조가능한 스폰위치

    [Header("여러 거리 확인용")]
    public float PlayertoFleetSpawn; // 플레이어와 전대 스폰위치 사이 거리
    public float EnemytoPlayer; // 플레이어와 적 사이 거리
    public float EnemytoSelfSpawn; // 적과 적 자신의 스폰위치 사이의 거리
    public float EnemytoFleetSpawn; // 적과 전대 스폰위치 사이의 거리

    [Header("적 관련 수치 조정")]
    public float smallAgrro; // 작은 어그로 범위
    public float largeAgrro; // 큰 어그로 범위
    public float MaxAtkRange; // 최대 공격 사거리
    public float TimeTillAtk; // 공격형적 추적 타이머

    [Header("기회주의자형 적 어그로 여부")]
    public bool isAggro;


    public enum STATE
    {
        IDLE, // 초기 상태
        PURSUE, // 추적 상태
        WAIT, // 대기 상태
        GOBACK, // 복귀 상태
        RETREAT, // 후퇴 상태
        ESCAPE // 탈출 상태
    };

    public STATE statename; // STATE 변수 (Enemy_Movement 제어)
    public bool isShoot; // 발사를 제어하는 불 변수 (Enemy_Shooter 제어)

    public void OnEnable()
    {
        // 플레이어 오브젝트를 찾아 트랜스폼 할당
        player = GameObject.Find("Player").transform;
        // 자신의 스폰위치를 현재 위치로 설정
        SelfSpawnposition = transform.position;

        // 유일하게 Enemy_Control이 계산가능한 거리값, 나머지는 각 Ai 스크립트로 부터 가져옴
        PlayertoFleetSpawn = Vector2.Distance(player.position, FleetSpawnpoint);
        EnemytoPlayer = Vector2.Distance((Vector2)transform.position, player.position);
        EnemytoFleetSpawn = Vector2.Distance((Vector2)transform.position, FleetSpawnpoint);
        EnemytoSelfSpawn = Vector2.Distance((Vector2)transform.position, SelfSpawnposition);
    }

    
    public void FixedUpdate()
    {
        // PlayertoSpawn을 지속적으로 갱신
        PlayertoFleetSpawn = Vector2.Distance(player.position, FleetSpawnpoint);
        EnemytoPlayer = Vector2.Distance((Vector2)transform.position, player.position);
        EnemytoFleetSpawn = Vector2.Distance((Vector2)transform.position, FleetSpawnpoint);
        EnemytoSelfSpawn = Vector2.Distance((Vector2)transform.position, SelfSpawnposition);
    }
}
