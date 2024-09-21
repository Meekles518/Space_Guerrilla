using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 생성된 플레이어의 총알의 행동을 제어하는 스크립트
public class Player_Bullet : MonoBehaviour
{
    public Rigidbody2D rb2; // 총알의 리지드바디
    //[HideInInspector]
    public GameObject player; // 이 총알을 발사하는 오브젝트
    public ShipEntity shipEntity;


    private bool dead; // 이 총알의 활성화 여부를 확인해줄 변수
    private Vector3 moveDirection3; // 총알의 이동방향 (Vector3)
    private Vector2 moveDirection2; // 총알의 이동방향 (Vector2)
    private float spread; // 최종 탄퍼짐

    [Header("총알 속성")]
    public float speed; // 총알의 속도
    public float spreadRange; // 탄퍼짐 정도


    //Player_Bullet 스크립트의 Awake, OnEnable 함수를 재조정해야 한다.
    private void Awake()
    {
       /* //Map에서 가져온 PlayerBulletInfo 의 값을 부여하기
        PlayerBulletInfo playerBulletInfo = MapManager.instance.playerInfo.GetComponent<PlayerBulletInfo>();

        
        this.speed = playerBulletInfo.speed; //Speed 저장
        this.spreadRange = playerBulletInfo.spreadRange; //spreadRange 저장

        var shipEntity = GetComponent<ShipEntity>();
        shipEntity.getShipEntity(playerBulletInfo); //ShipEntity에 필요한 값들 저장


        // 현재 오브젝트의 리지드바디를 가져옴
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        // 최종 탄퍼짐을 탄퍼짐 정도 사이에서 랜덤하게 결정
        spread = Random.Range(-spreadRange, spreadRange);*/
    }

    // 풀매니저에서 비활성화된 총알이 활성화 될때 마다 작동할 매서드
    private void OnEnable()
    {
        //GameManager에 저장되어 있는 총알에 대한 정보들 깊은 복사로 가져오기
        getBulletInfo();



        // 현재 오브젝트의 리지드바디를 가져옴
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        // 최종 탄퍼짐을 탄퍼짐 정도 사이에서 랜덤하게 결정
        spread = Random.Range(-spreadRange, spreadRange);




        // 총알의 비활성화 여부를 거짓으로 바꿈
        dead = false;
        // 총알을 발사 할 주체인 플레이어를 찾음
        player = GameObject.FindGameObjectWithTag("Player");
        // 총알의 이동방향을 플레이어가 향하는 방향으로 설정
        moveDirection3 = Quaternion.AngleAxis(spread, new Vector3(0, 0, 1)) * player.transform.up;
        // 계산에 필요한 Vector2 값으로 위의 방향을 변경해줌
        moveDirection2 = (Vector2)moveDirection3;
        // 총알이 계속 남아있지 않도록 하는 코루틴 Disable을 실행
        StartCoroutine(Disable());
    }

    // 총알에 velocity를 부여해줌
    private void FixedUpdate()
    {
        // 총알의 속도를 원하는 값으로 유지
        rb2.velocity = moveDirection2.normalized * speed;       
    }

    // 총알이 일정 조건을 만족하면 비활성화 시키는 코루틴
    // 추후에 개선 필요 (예를 들어 주변 적대 오브젝트를 검색해 없으면 비활성화 하는 식으로)
    private IEnumerator Disable()
    {
        // 죽은 상태가 아니라면
        while (!dead)
        {
            // 플레이어와 총알 오브젝트 사이의 거리를 계산
            float distance = Vector2.Distance(player.transform.position, gameObject.transform.position);

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

    //PlayerBulletInfo 에 저장되어 있는 탄환의 정보를 가져오는 코드. 오직 GameManager에서만 사용하는 버전
    public void getBulletInfo(PlayerBulletInfo playerBulletInfo)
    {
        this.speed = playerBulletInfo.speed;
        this.spreadRange = playerBulletInfo.spreadRange;


    }

    //GameManager에 저장되어 있는 탄환 정보를 가져오는 코드. 매 탄환 OnEnable시 마다 사용함
    public void getBulletInfo()
    {
        var playerBullet = GameManager.instance.playerBullet;
        var bulletShipEntity = GameManager.instance.bulletShipEntity;
        shipEntity = GetComponent<ShipEntity>();

        this.speed = playerBullet.speed;
        this.spreadRange = playerBullet.spreadRange;
        shipEntity.getShipEntity(bulletShipEntity);


    }



}
