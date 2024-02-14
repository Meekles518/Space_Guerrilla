using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// ������ �� Ai�� �ൿ�� ����
// ������ �� State��� Enemy_Control ���̸� ����
public class Defensive_AI : MonoBehaviour
{
    Defensive_State currentState; // ���� ������Ʈ
    public Transform player; // �÷��̾� Ʈ������
    public Enemy_Control control; // Enemy_Control ������Ʈ

    static public bool isShoot; // Enemy_Shooter�� �����ϴµ� ����� �� ����

    public Vector2 SelfSpawnposition; // ���� ������ġ
    public Vector2 FleetSpawnpoint; // ���� ������ġ

    public float EnemytoPlayer; // ���� �÷��̾� ���� �Ÿ�
    public float EnemytoFleetSpawn; // ���� ���� ������ġ ���� �Ÿ�
    public float EnemytoSelfSpawn; // ���� ���� ������ġ ���� �Ÿ�



    // ���� �ʱ�ȭ
    void OnEnable()
    {
        control = GetComponent<Enemy_Control>(); // ��Ʈ�� ������Ʈ�� ������
        player = GameObject.Find("Player").transform; // �÷��̾� ������Ʈ�� ã�� Ʈ������ �Ҵ�
        currentState = new Defensive_Idle(gameObject, player, control); // ���� ������Ʈ�� Idle�� ����
        SelfSpawnposition = (Vector2) transform.position; // ���� ������ġ

        // ���� �Ÿ��� �ʱⰪ�� ���
        EnemytoPlayer = Vector2.Distance((Vector2)transform.position, player.position);
        EnemytoFleetSpawn = Vector2.Distance((Vector2)transform.position, FleetSpawnpoint);
        EnemytoSelfSpawn = Vector2.Distance((Vector2)transform.position, SelfSpawnposition);

        // Opportunistic ������ �ʿ��� �ʱⰪ���� ��Ʈ�� ������Ʈ�� ����
        control.EnemytoPlayer = EnemytoPlayer;
        control.EnemytoFleetSpawn = EnemytoFleetSpawn;
        control.EnemytoSelfSpawn = EnemytoSelfSpawn;
    }


    // ������ ���������� ����
    void FixedUpdate()
    {
        // Opportunistic_Ai�� �����ϴ� ������ ���������� ����
        EnemytoPlayer = Vector2.Distance((Vector2)transform.position, player.position);
        EnemytoFleetSpawn = Vector2.Distance((Vector2)transform.position, FleetSpawnpoint);
        EnemytoSelfSpawn = Vector2.Distance((Vector2)transform.position, SelfSpawnposition);
        control.EnemytoPlayer = EnemytoPlayer;
        control.EnemytoFleetSpawn = EnemytoFleetSpawn;
        control.EnemytoSelfSpawn = EnemytoSelfSpawn;

        // ���� State�� ���������� ����
        currentState = currentState.Process();
        // ���� State�� Enmey_Control�� ����
        control.statename = (Enemy_Control.STATE) currentState.name;
    }
}