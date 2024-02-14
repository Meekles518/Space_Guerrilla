using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// 플레이어 우주선의 이동 및 회전 등 움직임을 제어
public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput; // PlayerInput을 가져옴
    private Rigidbody2D playerRigidbody; // 플레이어의 리지드바디

    public float moveSpeed; // 이동 속도
    public float rotateSpeed; // 회전 속도

    private Vector2 moveDirection; // 우주선이 이동해야하는 방향
    private Vector2 rotateDirection; // 우주선이 회전해야하는 방향
    private Vector2 mousePosition; // 월드맵 상에서의 현재 마우스 위치
    private float actualRotate; // 현재 각도에서 rotateDirection을 만족하기 위해 회전해야하는 각도(도)

    private void Awake()
    {
        // 사용할 컴포넌트들을 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        //playerAnimator = GetComponent<Animator>();

        // 이동속도 설정
        moveSpeed = 5f;
        // 회전속도 설정
        rotateSpeed = 100f;
    }

    // 특정 물리주기에 맞춰 Rotate와 Move 실행
    private void FixedUpdate()
    {
        // 움직임 실행
        Move();
        // 회전 실행
        Rotate();
    }

    // 입력값에 따라 우주선을 움직임
    private void Move()
    {
        // 가로축, 세로축 입력값을 통해 moveDirection 구함
        moveDirection = new Vector2(playerInput.moveHorizontal, playerInput.moveVertical);
        // 그 방향의 단위벡터 * 이동속도만큼의 addForce를 해줌 (관성 의도)
        playerRigidbody.AddForce(moveDirection.normalized * moveSpeed);
        // 만약 플레이어의 속도가 목표 속도에 도달했다면 속도를 고정시킴
        if(playerRigidbody.velocity.sqrMagnitude > moveSpeed) 
        {
            playerRigidbody.velocity = moveDirection.normalized * moveSpeed;
        }
    }

    // 마우스 방향으로 우주선을 회전
    private void Rotate()
    {
        // mousePosition에 관한 입력 감지
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 마우스 좌표에서 자신의 좌표 빼서 벡터값 구하기
        rotateDirection = mousePosition - (Vector2) transform.position;

        // 회전해야 하는 각도를 단위 '도'로 구함
        actualRotate = Quaternion.FromToRotation((Vector2) transform.up, rotateDirection).eulerAngles.z;


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
