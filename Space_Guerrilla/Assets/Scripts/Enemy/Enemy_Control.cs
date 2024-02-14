using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// 적 Ai 스크립트로부터 값을 받아 적 이동, 사격 스크립트를 제어하는 스크립트
// 모든 유형의 적들이 사용가능
public class Enemy_Control : MonoBehaviour
{
    public Transform player; // 플레이어 트랜스폼
    public Vector2 SelfSpawnposition; // 참조가능한 스폰위치
    public Vector2 FleetSpawnpoint; // 전대 스폰위치

    public float PlayertoFleetSpawn; // 플레이어와 전대 스폰위치 사이 거리
    public float EnemytoPlayer; // 플레이어와 적 사이 거리
    public float EnemytoSelfSpawn; // 적과 적 자신의 스폰위치 사이의 거리
    public float EnemytoFleetSpawn; // 적과 전대 스폰위치 사이의 거리

    public float smallAgrro; // 작은 어그로 범위
    public float largeAgrro; // 큰 어그로 범위
    public float MaxAtkRange; // 최대 공격 사거리
    public float timer; // 타이머 변수
    public int projectilesPerFire; // 한번 클릭시 발사하는 투사체 수
    public bool isAggro;

    public enum STATE
    {
        IDLE, // 초기 상태
        PURSUE, // 추적 상태
        WAIT, // 대기 상태
        GOBACK, // 복귀 상태
        RETREAT // 후퇴 상태
    };

    public STATE statename; // STATE 변수 (Enemy_Movement 제어)
    public bool isShoot; // 발사를 제어하는 불 변수 (Enemy_Shooter 제어)

    public void OnEnable()
    {
        // 플레이어 오브젝트를 찾아 트랜스폼 할당
        player = GameObject.Find("Player").transform;
        // 자신의 스폰위치를 현재 위치로 설정
        SelfSpawnposition = transform.position;
        // 어그로 범위 설정 (이거를 지우면 유니티 인스펙터에서 실시간으로 개별 전대별 설정가능)
        smallAgrro = 15f;
        largeAgrro = 25f;
        // 에러가 나지않게 초기값들 설정
        statename = STATE.IDLE;
        isShoot = false;
        isAggro = false;
        timer = 100;
        // 유일하게 Enemy_Control이 계산가능한 거리값, 나머지는 각 Ai 스크립트로 부터 가져옴
        PlayertoFleetSpawn = Vector2.Distance(player.position, FleetSpawnpoint);
        EnemytoPlayer = 1000;
        EnemytoSelfSpawn = 1000;
        EnemytoFleetSpawn = 0;
    }

    
    public void FixedUpdate()
    {
        // PlayertoSpawn을 지속적으로 갱신
        PlayertoFleetSpawn = Vector2.Distance(player.position, FleetSpawnpoint);
    }
}
