using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State에 따른 적의 움직임(이동, 회전)을 담당하는 스크립트
public class Enemy_Movement : MonoBehaviour
{
    public GameObject player;    // 플레이어
    private Rigidbody2D enemyRigidbody; // 적의 리지드바디
    public Enemy_Control control; // 적 제어 스크립트

    public Vector2 Spawnposition; // Enemy 오브젝트의 스폰위치
    private Vector2 Playerposition; // 현재 플레이어의 위치
    public Vector2 Targetposition; // 이 Enemy 오브젝트가 이동, 바라볼 목표 위치
    public float EnemytoPlayer; // 플레이어와 이 적 사이의 거리

    public float moveSpeed; // 이동 속도
    public float rotateSpeed; // 회전 속도
    public float OptimalAtkRange; // 적정 공격 사거리

    public Vector2 moveDirection; // 우주선이 이동해야하는 방향
    private Vector2 rotateDirection; // 우주선이 회전해야하는 방향
    private float actualRotate; // 현재 각도에서 rotateDirection을 만족하기 위해 회전해야하는 각도(도)
    private float x_Random; // x좌표 랜덤변수
    private float y_Random; // y좌표 랜덤변수
    private float xrandomRange = 1f; // x좌표 랜덤변수 범위
    private float yrandomRange = 1f; // y좌표 랜덤변수 범위
    private float currTime; // 현재시간

    // 오브젝트 활성화 시 실행
    private void OnEnable()
    {
        // 초기값들 할당
        enemyRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed = 4f;
        rotateSpeed = 100f;
        OptimalAtkRange = 5f;
        player = GameObject.Find("Player");
        Spawnposition = transform.position; // 내 스폰위치
        Targetposition = Spawnposition; // 타겟위치를 스폰포인트(현재위치)로 초기화
        control = GetComponent<Enemy_Control>();

    }


    private void FixedUpdate()
    {
        // 플레이어의 위치를 지속적으로 갱신
        Playerposition = player.transform.position;
        // 플레이어와 나 사이의 거리를 지속적으로 갱신
        EnemytoPlayer = control.EnemytoPlayer;

        // 움직임 실행
        Move();
        // 회전 실행
        Rotate();

        // 이동시 적들이 뭉치는 현상을 방지하기 위해 플레이어 주위의 랜덤한 지점으로 이동
        // 코루틴을 사용하지 않는 이유는 Random.Range가 코루틴에서 지속적으로 갱신되지 않음, x_Random과 y_Random이 하나의 값으로 고정 (코루틴으로 하는 방법이 있으면 수정요함)
        // 현재 시간을 계속 동기화
        currTime += Time.deltaTime;
        // 시간이 마지막 초기화로부터 3초 지났다면
        if (currTime > 3)
        {
            // x_Random과 y_Random을 무작위로 설정
            x_Random = Random.Range(-xrandomRange, xrandomRange);
            y_Random = Random.Range(-yrandomRange, yrandomRange);
            // 현재 시간 초기화
            currTime = 0;
        }
        // Idle State일 때 목표위치를 현재위치(자신의 스폰위치) + y축방향으로 설정
        if (control.statename == Enemy_Control.STATE.IDLE)
        {
            Targetposition = new Vector2(transform.position.x, transform.position.y) + new Vector2(0, 1);
        }
        // Pursue State일 때 목표위치를 플레이어 위치로 설정
        else if (control.statename == Enemy_Control.STATE.PURSUE)
        {
            Targetposition = Playerposition;
        }
        // Wait State일 때 목표위치를 플레이어 위치로 설정
        else if (control.statename == Enemy_Control.STATE.WAIT)
        {
            Targetposition = Playerposition;
        }
        // GoBack State일 때 목표위치를 스폰위치로 설정
        else if (control.statename == Enemy_Control.STATE.GOBACK)
        {
            Targetposition = Spawnposition;
        }
        // Retreat State일 때 목표위치를 플레이어 위치로 설정
        else if (control.statename == Enemy_Control.STATE.RETREAT)
        {
            Targetposition = Playerposition;
        }
    }


    // 적 이동을 시키는 매서드
    private void Move()
    {
        // Pursue 상태에서 적이 적정사거리로 밖에 있을 때
        if (EnemytoPlayer > OptimalAtkRange && control.statename == Enemy_Control.STATE.PURSUE)
        {                
             // 이동방향 = 목표위치 - 현재위치 + 랜덤위치
             moveDirection = Targetposition - new Vector2(transform.position.x, transform.position.y) + new Vector2(x_Random, y_Random);
             // 이동방향으로 AddForce를 해줌 (관성 부여)
             enemyRigidbody.AddForce(moveDirection.normalized * moveSpeed);
             // 만약 적의 속도가 목표 속도에 도달했다면 속도를 고정시킴
             if(enemyRigidbody.velocity.sqrMagnitude > moveSpeed)
             {
                enemyRigidbody.velocity = moveDirection.normalized * moveSpeed;
             }
        }
        // GoBack 상태일때
        else if(control.statename == Enemy_Control.STATE.GOBACK)
        {
            // 이동방향 = 목표위치 - 현재위치
            moveDirection = Targetposition - new Vector2(transform.position.x, transform.position.y);
            // 이동방향으로 AddForce를 해줌 (관성 부여)
            enemyRigidbody.AddForce(moveDirection.normalized * moveSpeed);
            // 만약 적의 속도가 목표 속도에 도달했다면 속도를 고정시킴
            if (enemyRigidbody.velocity.sqrMagnitude > moveSpeed)
            {
                enemyRigidbody.velocity = moveDirection.normalized * moveSpeed;
            }
        }
        // 후퇴 상태일때
        // 이동방향은 스폰위치, 회전방향은 플레이어 위치이므로 목표위치를 스폰위치로 강제고정
        else if (control.statename == Enemy_Control.STATE.RETREAT)
        {
            // 이동방향 = 스폰위치 - 현재위치
            moveDirection = Spawnposition - new Vector2(transform.position.x, transform.position.y);
            // 이동방향을 normalize해서 속도를 곱해줌
            // 이동방향으로 AddForce를 해줌 (관성 부여)
            enemyRigidbody.AddForce(moveDirection.normalized * moveSpeed);
            // 만약 적의 속도가 목표 속도에 도달했다면 속도를 고정시킴
            if (enemyRigidbody.velocity.sqrMagnitude > moveSpeed)
            {
                enemyRigidbody.velocity = moveDirection.normalized * moveSpeed;
            }
        }
        else
        {
            // 적 오브젝트의 속도를 0으로 변경
            enemyRigidbody.velocity = new Vector2(0, 0);
        }
        
    }

    // 적 오브젝트의 회전을 시키는 매서드
    private void Rotate()
    {
        
         // 회전 방향 = 목표위치 - 현재위치
         rotateDirection = Targetposition - new Vector2(transform.position.x, transform.position.y);
         // 회전해야 하는 각도를 '도'로 구현
         actualRotate = Quaternion.FromToRotation((Vector2)transform.up, rotateDirection).eulerAngles.z;

         // 회전해야 하는 각도의 값에 따라 회전 방향을 결정해줌.
         if (actualRotate < 1 || actualRotate > 359)
         {
             // 떨림을 방지하기 위해 오차를 1도 넣음
             return;
         }

         // 시계방향으로 회전이 더 가까울 때 시계방향으로 회전
         else if (actualRotate > 180)
         {
             transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed, Space.Self);
         }

         // 반시계방향으로 회전이 더 가까울 때 반시계방향으로 회전
         else if (actualRotate < 180)
         {
             transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);
         }
        

    }



}