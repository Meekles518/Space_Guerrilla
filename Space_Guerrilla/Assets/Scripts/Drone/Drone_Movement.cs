using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State에 따른 적의 움직임(이동, 회전)을 담당하는 스크립트
public class Drone_Movement : MonoBehaviour
{
    public GameObject player;    // 플레이어
    private Rigidbody2D droneRigidbody; // 드론의 리지드바디
    public Drone_Control control; // 적 제어 스크립트

    private Vector2 Playerposition; // 현재 플레이어의 위치
    public Vector2 Targetposition; // 드론이 이동, 바라볼 목표 위치

    public float moveSpeed; // 이동 속도
    public float rotateSpeed; // 회전 속도

    public Vector2 moveDirection; // 우주선이 이동해야하는 방향
    private Vector2 rotateDirection; // 우주선이 회전해야하는 방향
    private float actualRotate; // 현재 각도에서 rotateDirection을 만족하기 위해 회전해야하는 각도(도)
    private float x_Random; // x좌표 랜덤변수
    private float y_Random; // y좌표 랜덤변수
    private float xrandomRange = 2f; // x좌표 랜덤변수 범위
    private float yrandomRange = 2f; // y좌표 랜덤변수 범위
    private float currTime; // 현재시간
    

    // 오브젝트 활성화 시 실행
    private void OnEnable()
    {
        // 초기값들 할당
        droneRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed = 4f;
        rotateSpeed = 100f;
        player = GameObject.Find("Player");
        control = GetComponent<Drone_Control>();
        Targetposition = transform.position;

    }


    private void FixedUpdate()
    {
        // 플레이어의 위치를 지속적으로 갱신
        Playerposition = player.transform.position;
         

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
        if (control.statename == Drone_Control.STATE.IDLE && control.isSpread == false && control.isAuto == false)
        {
            //마우스 커서의 위치로 향하게 함
            Targetposition = control.mousePosition;
        }

        // AUTO State일 때 목표위치를 
        else if (control.statename == Drone_Control.STATE.IDLE && control.isSpread == false && control.isAuto == true)
        {
            //Player의 위치에서 일정 범위 내의 무작위 위치로 향하게 함
            Targetposition = Playerposition + new Vector2(x_Random, y_Random);
        }

        // SPREAD State일 때 목표위치를 
        else if (control.statename == Drone_Control.STATE.SPREAD)
        {
            //마우스 커서의 반대 위치로 가게 함
            //Targetposition = new Vector2(control.selfposition.position.x, control.selfposition.position.y) + new Vector2(control.selfposition.position.x - Input.mousePosition.x, control.selfposition.position.y - Input.mousePosition.y);
            Targetposition = ((Vector2)transform.position - control.mousePosition) +(Vector2)transform.position;
        }

        // Engage State일 때 목표위치를 
        else if (control.statename == Drone_Control.STATE.ENGAGE)
        {
            // Targetposition = 적위치, 스캐너 써서 인접 거리 내의 적 중 가장 가까운 적 추적
            //Drone_Control에서 찾은 Target을 향해 이동
            Targetposition = control.TargetPosition;
           
        }

        // Follow State일 때 목표 위치를
        else if (control.statename == Drone_Control.STATE.FOLLOW)
        {
            //Drone_Control에 저장되어 있는 FollowPosition으로 이동
            Targetposition = control.FollowPosition;
        }

        // 움직임 실행
        Move();
        // 회전 실행
        Rotate();


    }//FIxedUpdate


    // 드론의 이동을 시키는 매서드
    //각 State에 따른 이동을 if문으로 구현해야 함
    private void Move()
    {
            // 이동방향 = 목표위치 - 현재위치 + 랜덤위치
            moveDirection = Targetposition - (Vector2)transform.position;
            // 이동방향으로 AddForce를 해줌 (관성 부여)
            droneRigidbody.AddForce(moveDirection.normalized * moveSpeed);
            // 만약 드론의 속도가 목표 속도에 도달했다면 속도를 고정시킴
            if (droneRigidbody.velocity.sqrMagnitude > moveSpeed)
            {
                droneRigidbody.velocity = moveDirection.normalized * moveSpeed;
            }

         

    }//Move

    // 드론 오브젝트의 회전을 시키는 매서드
    private void Rotate()
    {

        // 회전 방향 = 목표위치 - 현재위치
        rotateDirection = Targetposition - (Vector2)transform.position;
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


    }//Rotate
}