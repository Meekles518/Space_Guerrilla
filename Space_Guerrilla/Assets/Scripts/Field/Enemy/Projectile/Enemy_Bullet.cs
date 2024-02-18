using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 생성된 적의 총알의 행동을 제어하는 스크립트
public class Enemy_Bullet : MonoBehaviour
{
    private Rigidbody2D rb2; // 총알의 리지드바디
    public GameObject enemy; // 이 총알을 발사하는 오브젝트
    private bool dead; // 이 총알의 활성화 여부를 확인해줄 불 변수
    public float speed; // 총알의 속도

    private Vector3 moveDirection3; // 총알의 이동방향 (Vector3)
    private Vector2 moveDirection2; // 총알의 이동방향 (Vector2)
    private Vector2 bulletPosition; // 현재 총알의 위치
    public float spreadRange; // 탄퍼짐 정도
    private float spread; // 최종 탄퍼짐

    // 총알을 발사한 enemy를 특정하는데 사용할 값들
    // enemy는 여러개 있을 수 있기 때문에 플레이어 총알처럼 GetObject를 사용할 수 없을듯
    public RaycastHit2D[] Targets; // CircleCastAll로 가져오는 오브젝트들
    public LayerMask Target_layer; // 검색을 시행할 레이어(Enemy 레이어)
    public Transform Nearest_enemy; // 검색된 오브젝트중 가장 가까운 Enemy 오브젝트

    private void Awake()
    {
        // 현재 오브젝트의 리지드바디를 가져옴
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        // 속도 선언
        speed = 10f;
        // 탄퍼짐 정도 선언
        spreadRange = 5f;
        // 최종 탄퍼짐을 탄퍼짐 정도 사이에서 랜덤하게 결정
        spread = Random.Range(-spreadRange, spreadRange);
    }

    // 풀매니저에서 비활성화된 총알이 활성화 될때 마다 작동할 매서드
    private void OnEnable()
    {
        // 검색 레이어를 Enemy로 설정
        Target_layer = LayerMask.GetMask("Enemy");
        // 근처 모든 Enemy 오브젝트를 검색
        Targets = Physics2D.CircleCastAll(transform.position, 3, Vector2.zero, 0, Target_layer);
        // 가장 가까운 Enmey 오브젝트를 자신을 발사한 적으로 간주
        Nearest_enemy = Nearest();
        // 총알의 비활성화 여부를 거짓으로 바꿈
        dead = false;
        // 총알의 이동방향을 플레이어가 향하는 방향으로 설정
        moveDirection3 = Quaternion.AngleAxis(spread, new Vector3(0, 0, 1)) * Nearest_enemy.transform.up;
        // 계산에 필요한 Vector2 값으로 위의 방향을 변경해줌
        moveDirection2 = (Vector2)moveDirection3;
        // 총알의 현재 위치를 계산
        bulletPosition = new Vector2(transform.position.x, transform.position.y);       
        // 총알이 계속 남아있지 않도록 하는 코루틴 Disable을 실행
        StartCoroutine(Disable());
    }

    Transform Nearest() {

        // 가장 가까운 Target을 돌려줄 변수
        Transform Result = null;
        // Player와 가장 가까운 Target의 거리를 저장할 변수, 초기값은 임의로 매우 큰 수인 100으로 설정
        float Difference = 100f;

        // Targets에 들어있는 모든 원소에 foreach로 접근
        foreach (RaycastHit2D Target in Targets) {

            // 총알의 좌표와, Target의 좌표를 가져오기
            Vector3 My_pos = transform.position;
            Vector3 Target_pos = Target.transform.position;
            // Distance를 통해 총알과 Target의 거리를 가져오기
            float Current_difference = Vector3.Distance(My_pos, Target_pos);

            // 현재 foreach문의 Target과의 거리가, 저장되어 있는 Distance보다 짧으면
            // Difference와 Result를 새로 초기화
            if (Current_difference < Difference) {
                //더 가까운 거리, Target으로 교체
                Difference = Current_difference;
                Result = Target.transform;
            }
        }
        // 값을 반환
        return Result;
    }

    // 총알에 velocity를 부여해줌
    private void FixedUpdate()
    {
        // 총알의 속도를 원하는 값으로 유지
        rb2.velocity = moveDirection2.normalized * speed;
    }

    // 총알이 일정 조건을 만족하면 비활성화 시키는 코루틴
    private IEnumerator Disable()
    {
        // 죽은 상태가 아니라면
        while (!dead)
        {
            // 플레이어와 총알 오브젝트 사이의 거리를 계산
            float distance = Vector2.Distance(Nearest_enemy.transform.position, gameObject.transform.position);

            // 거리가 100f 이내라면 1초후 코루틴 재실행
            if (distance <= 100f)
            {
                yield return new WaitForSeconds(1f);
            }
            // 거리가 100f 초과라면 총알을 비활성화, 상태를 죽음으로 바꿈
            else
            {
                gameObject.SetActive(false);
                dead = true;
            }
        }
    }
}