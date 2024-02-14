using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// ��ȸ������ �� Ai�� �ൿ�� �����ϴ� ��ũ��Ʈ
// ��ȸ������ �� State��� Enemy_Control ���̸� ����
public class Opportunistic_AI : MonoBehaviour
{
    Opportunistic_State currentState; // ���� ������Ʈ
    public Transform player; // �÷��̾� Ʈ������
    public Enemy_Control control; // ��Ʈ�� ������Ʈ

    static public bool isShoot; // Enemy_Shooter�� �����ϴµ� ����� �� ����

    public Vector2 SelfSpawnposition; // ���� ������ġ
    public Vector2 FleetSpawnpoint; // ���뽺����ġ

    public float EnemytoPlayer; // ���� �÷��̾� ���� �Ÿ�
    public float EnemytoSelfSpawn; // ���� ���� ������ġ ���� �Ÿ�
    
    // ���� �ʱ�ȭ
    void OnEnable()
    {
        control = GetComponent<Enemy_Control>(); // ��Ʈ�� ������Ʈ�� ������
        player = GameObject.Find("Player").transform; // �÷��̾� ������Ʈ�� ã�� Ʈ������ �Ҵ�
        currentState = new Opportunistic_Idle(gameObject, player, control); // �ʱ� ������Ʈ�� Idle�� ����
        SelfSpawnposition = (Vector2)transform.position; // �ڽ��� ������ġ�� ����

        // ���� �Ÿ��� �ʱⰪ�� ���
        EnemytoPlayer = Vector2.Distance((Vector2)transform.position, player.position);
        EnemytoSelfSpawn = Vector2.Distance((Vector2)transform.position, SelfSpawnposition);

        // Oppotunistic ������ �ʿ��� �ʱⰪ���� ��Ʈ�� ������Ʈ�� ����
        control.EnemytoPlayer = EnemytoPlayer;
        control.EnemytoSelfSpawn = EnemytoSelfSpawn;
    }


    // ������ ���������� ����
    void FixedUpdate()
    {
        // Opportunistic_Ai�� �����ϴ� ������ ���������� ����
        EnemytoPlayer = Vector2.Distance((Vector2)transform.position, player.position);
        EnemytoSelfSpawn = Vector2.Distance((Vector2)transform.position, SelfSpawnposition);
        control.EnemytoPlayer = EnemytoPlayer;
        control.EnemytoSelfSpawn = EnemytoSelfSpawn;

        // ���� State�� ���������� ����
        currentState = currentState.Process();
        // ���� State�� Enmey_Control�� ����
        control.statename = (Enemy_Control.STATE)currentState.name;
        if(currentState.Aggro() == true)
        {
            control.isAggro = true;
        }
        else
        {
            control.isAggro = false;
        }
    }
}
