using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 생성된 플레이어의 총알의 행동을 제어하는 스크립트
public class Enemy_CruiseMissile : MonoBehaviour
{
    private Rigidbody2D rb2; // 미사일의 리지드바디
    public Transform enemy; // 이 미사일을 발사하는 오브젝트
    private bool dead; // 이 미사일의 활성화 여부를 확인해줄 변수
    public float speed; // 미사일의 속도

    private Vector3 moveDirection3; // 총알의 이동방향 (Vector3)
    private Vector2 moveDirection2; // 총알의 이동방향 (Vector2)
    private Vector2 bulletPosition; // 현재 총알의 위치
    public float lifespan;
    private float currtime;
    private Transform missileTarget;

    private Vector2 Targetposition;
    public RaycastHit2D[] Targets; // CircleCastAll로 가져오는 오브젝트들
    public LayerMask Target_layer; // 검색을 시행할 레이어(Enemy 레이어)
    public Transform Nearest_enemy; // 검색된 오브젝트중 가장 가까운 Enemy 오브젝트
    public float Scan_range;
    private Vector2 rotateDirection; // 우주선이 회전해야하는 방향
    private float actualRotate;
    public float rotateSpeed; // 회전 속도

    public RaycastHit2D[] Targets1; // CircleCastAll로 가져오는 오브젝트들
    public LayerMask Target_layer1; // 검색을 시행할 레이어(Enemy 레이어)
    public Transform Nearest_enemy1; // 검색된 오브젝트중 가장 가까운 Enemy 오브젝트

    private void Awake()
    {
        // 현재 오브젝트의 리지드바디를 가져옴
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        // 속도 선언
        //speed = 20f;
    }

    // 풀매니저에서 비활성화된 총알이 활성화 될때 마다 작동할 매서드
    private void OnEnable()
    {
        // 총알의 비활성화 여부를 거짓으로 바꿈
        dead = false;
        Target_layer = LayerMask.GetMask("Enemy");
        // 근처 모든 Enemy 오브젝트를 검색
        Targets = Physics2D.CircleCastAll(transform.position, 3, Vector2.zero, 0, Target_layer);
        // 가장 가까운 Enmey 오브젝트를 자신을 발사한 적으로 간주
        enemy = Nearest();
        // 총알의 이동방향을 플레이어가 향하는 방향으로 설정
        moveDirection3 = enemy.up;
        // 계산에 필요한 Vector2 값으로 위의 방향을 변경해줌
        moveDirection2 = (Vector2)moveDirection3;
        // 총알의 현재 위치를 계산
        bulletPosition = new Vector2(transform.position.x, transform.position.y);
        // 총알이 계속 남아있지 않도록 하는 코루틴 Disable을 실행
        currtime = 0f;
        missileTarget = null;
    }

    // 총알에 velocity를 부여해줌
    private void FixedUpdate()
    {
        // 총알의 속도를 원하는 값으로 유지
        rb2.velocity = (Vector2)transform.up.normalized * speed;

        currtime += Time.fixedDeltaTime;
        if (currtime >= lifespan)
        {
            gameObject.SetActive(false);
            dead = true;
        }

        if (missileTarget == null)
        {
            Targetposition = transform.position + transform.up.normalized;
            Find();
        }
        else
        {
            Targetposition = missileTarget.position;
            Rotate();
            if (Vector2.Distance(missileTarget.position, transform.position) >= Scan_range)
            {
                missileTarget = null;
            }

        }
    }
    private void Find()
    {
        // 검색 레이어를 Enemy로 설정
        Target_layer = LayerMask.GetMask("Player");
        // 근처 모든 Enemy 오브젝트를 검색
        Targets = Physics2D.CircleCastAll(transform.position, Scan_range, Vector2.zero, 0, Target_layer);
        // 가장 가까운 Enmey 오브젝트를 자신을 발사한 적으로 간주
        Nearest_enemy = Nearest();
        if (Nearest_enemy != null)
        {
            missileTarget = Nearest_enemy;
        }
    }

    private Transform Nearest()
    {

        // 가장 가까운 Target을 돌려줄 변수
        Transform Result = null;
        // Player와 가장 가까운 Target의 거리를 저장할 변수, 초기값은 임의로 매우 큰 수인 100으로 설정
        float Difference = Scan_range * 2;

        // Targets에 들어있는 모든 원소에 foreach로 접근
        foreach (RaycastHit2D Target in Targets)
        {

            // 총알의 좌표와, Target의 좌표를 가져오기
            Vector2 My_pos = transform.position;
            Vector2 Target_pos = Target.transform.position;
            // Distance를 통해 총알과 Target의 거리를 가져오기
            float Current_difference = Vector2.Distance(My_pos, Target_pos);

            // 현재 foreach문의 Target과의 거리가, 저장되어 있는 Distance보다 짧으면
            // Difference와 Result를 새로 초기화
            if (Current_difference < Difference)
            {
                //더 가까운 거리, Target으로 교체
                Difference = Current_difference;
                Result = Target.transform;
            }
        }
        // 값을 반환
        return Result;
    }

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
