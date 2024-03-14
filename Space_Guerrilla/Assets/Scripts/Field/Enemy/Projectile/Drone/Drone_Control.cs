using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// 
public class Drone_Control : MonoBehaviour
{
 
    public Transform player; // �÷��̾� Ʈ������
    public Transform selfposition; // ��� Ʈ������
    //public float smallAgrro; // ���� ��׷� ����
    //public float largeAgrro; // ū ��׷� ����
    public float timer; // Ÿ�̸� ����
    public PlayerInput playerInput; // PlayerInput�� ������
    public bool isSpread; //�갳 ���θ� Ȯ���ϴ� bool ����
    public bool isAuto; //Auto ���θ� Ȯ���ϴ� bool ����

    //�� �Ʒ��� �������� Drone�� Target ������ ���Ǵ� ����
    public Transform droneTarget; //  ����� Target�� Transform�� ������ ����
    public Vector2 TargetPosition; //  Target�� Position�� ��Ÿ�� vector2 ����
    public RaycastHit2D[] Targets;  // CirclecastAll�� �������� ��� ������Ʈ���� ������ �迭
    public LayerMask Target_layer;  // �˻��� ������ layer(Enemy, ��ü���� �ش�)
    public Transform Nearest_enemy; // ĳ��Ʈ�� ������Ʈ �� ���� ����� Enemy ������Ʈ
    public float Scan_range;    //�˻� ������ ������ float ����


    //�� �Ʒ��� �������� Follow State ������ ���Ǵ� ����
    public float Player_drone_Distance; //Player�� Drone�� ���� �Ÿ��� ������ ����
    public float P_d_maxDistance;   // Drone�� Player�κ��� �־��� �� �ִ� �ִ� �Ÿ�
    public Vector2 Last_player; //Follow State ���� ���� Player Vector2
    public Vector2 Last_drone;  //Follow State ���� ���� Drone Vector2
    public Vector2 relativePosition;    // Follow State ���� ���� Player�� Drone�� ��� ��ġ
    public Vector2 FollowPosition;  //Follow State�� Drone�� �̵��ؾ� �� ��ǥ ��ġ


    public Vector2 mousePosition;   //����� �󿡼��� ���� ���콺 ��ġ 
    public Vector2 Player_vec;  //Player�� Vector2
    public Vector2 Drone_vec;   //Drone�� Vector2

    public enum STATE
    {
        IDLE, // �ʱ� ����
        SPREAD, //�갳 ����
        ENGAGE,  //���� ����
        FOLLOW  //�÷��̾� ���� ����
    };

    public STATE statename; // STATE ���� (Drone_Movement ����)

    public void OnEnable()
    {
    
        // �÷��̾� ������Ʈ�� ã�� Ʈ������ �Ҵ�
        player = GameObject.Find("Player").transform;
        // PlayerInput�� ������
        //playerInput = GetComponent<PlayerInput>();
        playerInput = player.GetComponent<PlayerInput>();
        // �ڽ��� Transform ������Ʈ ������
        selfposition = GetComponent<Transform>();
        //��ĵ ���� ����
        Scan_range = 5f;

        // ������ �����ʰ� �ʱⰪ�� ����
        statename = STATE.IDLE;
        timer = 100;
        isAuto = playerInput.isAuto;
        droneTarget = null;
        Nearest_enemy = null;
        isAuto = playerInput.isAuto;
        Debug.Log(playerInput.isAuto);
        Player_drone_Distance = 0f;
        P_d_maxDistance = 20f;  //���Ƿ� �ִ� �Ÿ� 20f�� ����
 
    }//OnEnable

    
    public void FixedUpdate()
    {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // �ڱ� ��ġ ����
        selfposition = GetComponent<Transform>();
        Drone_vec = selfposition.position;
        Player_vec = player.position;
        //Player�� Drone�� �Ÿ� ���
        Player_drone_Distance = Vector2.Distance(Drone_vec, Player_vec);

        //Special Input, �갳 ����� ���
        if (playerInput.special)
        {
            isSpread = true;
        }
        else
        {
            isSpread = false;
        }

        //Auto Input, Auto ��� ��ư�� ������ ���� Auto ���¸� ������. �⺻ ���´� false
        isAuto = playerInput.isAuto;

        //����� Target�� ���������� �ʴٸ�
        if (droneTarget == null)
        {
            Find(); //��� �ֺ��� ���� Ž���ϴ� �Լ�
        }

        //����� Target�� �������ٸ�
        else
        {
            TargetPosition = droneTarget.position;
        }


    }//FixedUpdate



    //��� �ֺ��� ���� Ž���ϴ� �Լ�
    public void Find()
    {
        // �˻� ���̾ Enemy�� ����
        Target_layer = LayerMask.GetMask("Enemy");
        //��ó�� ��� Enemy ������Ʈ�� �˻�
        Targets = Physics2D.CircleCastAll(transform.position, Scan_range, Vector2.zero, 0, Target_layer);
        //���� ����� Enemy ������Ʈ�� Target���� ����
        Nearest_enemy = Nearest();
        if (Nearest_enemy != null)
        {
            droneTarget = Nearest_enemy;
        }//if

    }//Find


    //���� ����� ������Ʈ�� Transform�� return ���� �Լ�
    public Transform Nearest()
    {
        //���� ����� Target�� ������ ����
        Transform Result = null;

        //�� Target�� �� �ּ� �Ÿ��� ������ ����
        float distance = Scan_range * 2;

        //Targets �迭�� ����ִ� ��� ���ҿ� foreach�� ����
        foreach(RaycastHit2D Target in Targets)
        {
            //����� ��ǥ��, Target�� ��ǥ ��������
            Vector2 Drone_pos = transform.position;
            Vector2 Target_pos = Target.transform.position;

            //Distance �Լ��� ���� ��а� Target�� �Ÿ� ���
            float Cur_distance = Vector2.Distance(Drone_pos, Target_pos);

            //���� foreach���� Target���� �Ÿ�(Cur_distance)��, ����Ǿ� �ִ� distance���� ª����
            //distance�� Result�� ���� ���� �ʱ�ȭ
            if (Cur_distance < distance)
            {
                distance = Cur_distance;
                Result = Target.transform;
            }

           

        }//foreach

        

        return Result;

    }//Nearest

    //���콺 - ��� - Player �� �� ���� ������ �� ������ ������ ���ؼ�, 90���� �Ѵ��� Ȯ���ϴ� �Լ�
    public bool Over90or270()
    {
        Vector2 P_D_vector = Player_vec - Drone_vec;    //Drone ���Ϳ��� Player ���͸� �� ��
        Vector2 M_D_vector = mousePosition - Drone_vec; //Mouse ���Ϳ��� Drone ���͸� �� ��

        //���콺 - ��� - Player�� �� ���� ������ �� ������ ������
        float Rotate = Quaternion.FromToRotation(P_D_vector, M_D_vector).eulerAngles.z;
        
        //������ 90���� �Ѱ� 270���� ������
        if (Rotate > 90 && Rotate < 270)
        {
            return true;   //true 

        }

        //������ 90���� ���� �ʴ´ٸ�
        return false;   //false
       
    }//Over90


}