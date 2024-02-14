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
        RETREAT // ���� ����
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
        statename = STATE.IDLE;
        isShoot = false;
        isAggro = false;
        timer = 100;
        // �����ϰ� Enemy_Control�� ��갡���� �Ÿ���, �������� �� Ai ��ũ��Ʈ�� ���� ������
        PlayertoFleetSpawn = Vector2.Distance(player.position, FleetSpawnpoint);
        EnemytoPlayer = 1000;
        EnemytoSelfSpawn = 1000;
        EnemytoFleetSpawn = 0;
    }

    
    public void FixedUpdate()
    {
        // PlayertoSpawn�� ���������� ����
        PlayertoFleetSpawn = Vector2.Distance(player.position, FleetSpawnpoint);
    }
}
