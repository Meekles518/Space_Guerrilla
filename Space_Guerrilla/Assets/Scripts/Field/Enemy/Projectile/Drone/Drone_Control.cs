using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// 
public class Drone_Control : MonoBehaviour
{
 
    public Transform player; // 플레이어 트랜스폼
    public Transform selfposition; // 드론 트랜스폼
    //public float smallAgrro; // 작은 어그로 범위
    //public float largeAgrro; // 큰 어그로 범위
    public float timer; // 타이머 변수
    public PlayerInput playerInput; // PlayerInput을 가져옴
    public bool isSpread; //산개 여부를 확인하는 bool 변수
    public bool isAuto; //Auto 여부를 확인하는 bool 변수

    //이 아래의 변수들은 Drone의 Target 설정에 사용되는 변수
    public Transform droneTarget; //  드론의 Target의 Transform을 저장할 변수
    public Vector2 TargetPosition; //  Target의 Position을 나타낼 vector2 변수
    public RaycastHit2D[] Targets;  // CirclecastAll로 가져오는 모든 오브젝트들을 저장할 배열
    public LayerMask Target_layer;  // 검색을 시행할 layer(Enemy, 기체에만 해당)
    public Transform Nearest_enemy; // 캐스트한 오브젝트 중 가장 가까운 Enemy 오브젝트
    public float Scan_range;    //검색 범위를 저장할 float 변수


    //이 아래의 변수들은 Follow State 관리에 사용되는 변수
    public float Player_drone_Distance; //Player와 Drone의 현재 거리를 저장할 변수
    public float P_d_maxDistance;   // Drone이 Player로부터 멀어질 수 있는 최대 거리
    public Vector2 Last_player; //Follow State 진입 시의 Player Vector2
    public Vector2 Last_drone;  //Follow State 진입 시의 Drone Vector2
    public Vector2 relativePosition;    // Follow State 진입 시의 Player와 Drone의 상대 위치
    public Vector2 FollowPosition;  //Follow State시 Drone이 이동해야 할 목표 위치


    public Vector2 mousePosition;   //월드맵 상에서의 현재 마우스 위치 
    public Vector2 Player_vec;  //Player의 Vector2
    public Vector2 Drone_vec;   //Drone의 Vector2

    public enum STATE
    {
        IDLE, // 초기 상태
        SPREAD, //산개 상태
        ENGAGE,  //교전 상태
        FOLLOW  //플레이어 추적 상태
    };

    public STATE statename; // STATE 변수 (Drone_Movement 제어)

    public void OnEnable()
    {
    
        // 플레이어 오브젝트를 찾아 트랜스폼 할당
        player = GameObject.Find("Player").transform;
        // PlayerInput을 가져옴
        //playerInput = GetComponent<PlayerInput>();
        playerInput = player.GetComponent<PlayerInput>();
        // 자신의 Transform 컴포넌트 가져옴
        selfposition = GetComponent<Transform>();
        //스캔 범위 설정
        Scan_range = 5f;

        // 에러가 나지않게 초기값들 설정
        statename = STATE.IDLE;
        timer = 100;
        isAuto = playerInput.isAuto;
        droneTarget = null;
        Nearest_enemy = null;
        isAuto = playerInput.isAuto;
        Debug.Log(playerInput.isAuto);
        Player_drone_Distance = 0f;
        P_d_maxDistance = 20f;  //임의로 최대 거리 20f로 설정
 
    }//OnEnable

    
    public void FixedUpdate()
    {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 자기 위치 갱신
        selfposition = GetComponent<Transform>();
        Drone_vec = selfposition.position;
        Player_vec = player.position;
        //Player와 Drone의 거리 계산
        Player_drone_Distance = Vector2.Distance(Drone_vec, Player_vec);

        //Special Input, 산개 모드일 경우
        if (playerInput.special)
        {
            isSpread = true;
        }
        else
        {
            isSpread = false;
        }

        //Auto Input, Auto 모드 버튼을 누르면 현재 Auto 상태를 뒤집음. 기본 상태는 false
        isAuto = playerInput.isAuto;

        //드론의 Target이 설정되있지 않다면
        if (droneTarget == null)
        {
            Find(); //드론 주변의 적을 탐색하는 함수
        }

        //드론의 Target이 정해졌다면
        else
        {
            TargetPosition = droneTarget.position;
        }


    }//FixedUpdate



    //드론 주변의 적을 탐색하는 함수
    public void Find()
    {
        // 검색 레이어를 Enemy로 설정
        Target_layer = LayerMask.GetMask("Enemy");
        //근처의 모든 Enemy 오브젝트를 검색
        Targets = Physics2D.CircleCastAll(transform.position, Scan_range, Vector2.zero, 0, Target_layer);
        //가장 가까운 Enemy 오브젝트를 Target으로 설정
        Nearest_enemy = Nearest();
        if (Nearest_enemy != null)
        {
            droneTarget = Nearest_enemy;
        }//if

    }//Find


    //가장 가까운 오브젝트의 Transform를 return 해줄 함수
    public Transform Nearest()
    {
        //가장 가까운 Target을 돌려줄 변수
        Transform Result = null;

        //각 Target들 중 최소 거리를 저장할 변수
        float distance = Scan_range * 2;

        //Targets 배열에 들어있는 모든 원소에 foreach로 접근
        foreach(RaycastHit2D Target in Targets)
        {
            //드론의 좌표와, Target의 좌표 가져오기
            Vector2 Drone_pos = transform.position;
            Vector2 Target_pos = Target.transform.position;

            //Distance 함수를 통해 드론과 Target의 거리 계산
            float Cur_distance = Vector2.Distance(Drone_pos, Target_pos);

            //현재 foreach문의 Target과의 거리(Cur_distance)가, 저장되어 있는 distance보다 짧으면
            //distance와 Result의 값을 새로 초기화
            if (Cur_distance < distance)
            {
                distance = Cur_distance;
                Result = Target.transform;
            }

           

        }//foreach

        

        return Result;

    }//Nearest

    //마우스 - 드론 - Player 의 세 점을 연결할 때 나오는 각도를 구해서, 90도가 넘는지 확인하는 함수
    public bool Over90or270()
    {
        Vector2 P_D_vector = Player_vec - Drone_vec;    //Drone 벡터에서 Player 벡터를 뺀 값
        Vector2 M_D_vector = mousePosition - Drone_vec; //Mouse 벡터에서 Drone 벡터를 뺀 값

        //마우스 - 드론 - Player의 세 점을 연결할 때 나오는 각도값
        float Rotate = Quaternion.FromToRotation(P_D_vector, M_D_vector).eulerAngles.z;
        
        //각도가 90도를 넘고 270보다 작으면
        if (Rotate > 90 && Rotate < 270)
        {
            return true;   //true 

        }

        //각도가 90도를 넘지 않는다면
        return false;   //false
       
    }//Over90


}