using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// �� Ai ��ũ��Ʈ�κ��� ���� �޾� �� �̵�, ��� ��ũ��Ʈ�� �����ϴ� ��ũ��Ʈ
// ��� ������ ������ ��밡��
public class Enemy_Control : MonoBehaviour
{
    public Transform player; // �÷��̾� Ʈ������
    public Vector2 SelfSpawnposition; // ���������� ������ġ
    public Vector2 FleetSpawnpoint; // ���� ������ġ
    public Vector2 Escapepoint; // �� Ż����ġ

    public float PlayertoFleetSpawn; // �÷��̾�� ���� ������ġ ���� �Ÿ�
    public float EnemytoPlayer; // �÷��̾�� �� ���� �Ÿ�
    public float EnemytoSelfSpawn; // ���� �� �ڽ��� ������ġ ������ �Ÿ�
    public float EnemytoFleetSpawn; // ���� ���� ������ġ ������ �Ÿ�

    public float smallAgrro; // ���� ��׷� ����
    public float largeAgrro; // ū ��׷� ����
    public float MaxAtkRange; // �ִ� ���� ��Ÿ�
    public float timer; // Ÿ�̸� ����
    public int projectilesPerFire; // �ѹ� Ŭ���� �߻��ϴ� ����ü ��
    public bool isAggro;

    public enum STATE
    {
        IDLE, // �ʱ� ����
        PURSUE, // ���� ����
        WAIT, // ��� ����
        GOBACK, // ���� ����
        RETREAT, // ���� ����
        ESCAPE // Ż�� ����
    };

    public STATE statename; // STATE ���� (Enemy_Movement ����)
    public bool isShoot; // �߻縦 �����ϴ� �� ���� (Enemy_Shooter ����)

    public void OnEnable()
    {
        // �÷��̾� ������Ʈ�� ã�� Ʈ������ �Ҵ�
        player = GameObject.Find("Player").transform;
        // �ڽ��� ������ġ�� ���� ��ġ�� ����
        SelfSpawnposition = transform.position;
        // ��׷� ���� ���� (�̰Ÿ� ����� ����Ƽ �ν����Ϳ��� �ǽð����� ���� ���뺰 ��������)
        smallAgrro = 15f;
        largeAgrro = 25f;
        // ������ �����ʰ� �ʱⰪ�� ����
        //statename = STATE.IDLE;
        isShoot = false;
        isAggro = false;
        Escapepoint = new Vector2(80, 0);

        // �����ϰ� Enemy_Control�� ��갡���� �Ÿ���, �������� �� Ai ��ũ��Ʈ�� ���� ������
        PlayertoFleetSpawn = Vector2.Distance(player.position, FleetSpawnpoint);
        EnemytoPlayer = Vector2.Distance((Vector2)transform.position, player.position);
        EnemytoFleetSpawn = Vector2.Distance((Vector2)transform.position, FleetSpawnpoint);
        EnemytoSelfSpawn = Vector2.Distance((Vector2)transform.position, SelfSpawnposition);
    }

    
    public void FixedUpdate()
    {
        // PlayertoSpawn�� ���������� ����
        PlayertoFleetSpawn = Vector2.Distance(player.position, FleetSpawnpoint);
        EnemytoPlayer = Vector2.Distance((Vector2)transform.position, player.position);
        EnemytoFleetSpawn = Vector2.Distance((Vector2)transform.position, FleetSpawnpoint);
        EnemytoSelfSpawn = Vector2.Distance((Vector2)transform.position, SelfSpawnposition);
    }
}
