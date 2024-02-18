using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// ������ �� Ai�� �ൿ�� �����ϴ� ��ũ��Ʈ
// ������ �� State��� Enemy_Control ���̸� ����
public class Offensive_AI : MonoBehaviour
{
    Ai_State currentState; // ���� ������Ʈ
    public Transform player; // �÷��̾� Ʈ������
    public Enemy_Control control; // Enemy_Control ������Ʈ
   
    static public bool isShoot; // Enemy_Shooter�� �����ϴµ� ����� �� ����

    public Vector2 SelfSpawnposition; // ���� ������ġ
    public Vector2 FleetSpawnpoint; // ���� ������ġ

    public float EnemytoPlayer; // ���� �÷��̾� ���� �Ÿ�

    public float currTime; // Ÿ�̸ӿ� ����� ����ð�
    public float timer; // Ÿ�̸ӿ� ����� Ÿ�̸Ӱ� ������ �ð�
    
    // ���� �ʱ�ȭ
    void OnEnable()
    {
        control = GetComponent<Enemy_Control>(); // ��Ʈ�� ������Ʈ�� ������
        //player = GameObject.Find("Player").transform; // �÷��̾� ������Ʈ�� ã�� Ʈ������ �Ҵ�
        currentState = new Offensive_Idle(gameObject, player, control, currTime); // �ʱ� ������Ʈ�� Idle�� ����
        //SelfSpawnposition= (Vector2)transform.position; // �ڽ��� ������ġ�� ����
    }


    // ������ ���������� ����
    void FixedUpdate()
    {
        // ���� State�� ���������� ����
        currentState = currentState.Process();
        // ���� State�� Enmey_Control�� ����
        control.statename = (Enemy_Control.STATE)currentState.name;
    }
}
