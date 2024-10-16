using Map;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // * �� �Ʒ��� ���� ���� ����*
    //���� MapManager���� Turn�� ���õ� ������ �ٷ�� �ִ�(���Ŀ� TurnManager�� �ɰ��� ��)
    //MapManager�� �⺻������ �ʵ� ���� ���� �� ������ Turn�̿������� �����ϴ� lastTurn ������ ����

    //���� �Ϲ����� �ʿ��� �ʵ���� �̵� �ÿ��� MapManager�� lastTurn ������ ���� ���ּ��� ������ġ�� �����ϰ�,
    //���� ���� �ÿ��� isAtk ���� Ȱ���ϰԲ� �ϸ� ���?
    //���� �̵� ���� Node�� �����ϴ� ������ MapManager�� �������� ����. TurnManager�� �ɰ� �� ����ؾ� �ҵ�


    public Vector3 playerSpawn; // �÷��̾� ���� ��ġ
    public Vector3 reinforceSpawn; // �� ���� ������ġ
    private Vector3 mapCenter = Vector3.zero; // ���߾�
    // public Vector3 playerPos; // ���� �÷��̾� ��ġ�� ����

    public GameObject player;
    public bool isAtk = false; // �÷��̾ ����������(���� ���� ���п����� Ȱ���ϸ� ���?)
    public int atkDirection; // �������϶� �ʵ�� ���� ����
    public float escapeRange = 50; // Ż�����

    public List<EnemySpawnInfo> enemySpawnInfo; // �ʵ忡 ������ �ִ� �� ����


    private void OnEnable()
    {
        // ���ʿ��� ���� ���ݿ��ζ� �ʵ峻 �� ���� �޾ƿ�
        /*if (MapManager.instance.isAtk == true)
            isAtk = true;
        else
        {
            isAtk=false;
        }*/

        //enemyInfo = MapManager.instance.enemyinfo;

        // �ʵ峻 ���� ������ ��ġ�� �о ����
        //������ ������ ���� PoolManager�� �ƴ� ���ڷ� Spawner���� �����ϰ� ����?
        //for (int i = 0; i < enemySpawnInfo.Count; i++)
        //{

        //    GameManager.instance.poolManager.Get(enemySpawnInfo[i].EnemyTypes, enemySpawnInfo[i].EnemySpawn);

        //}


        ////�������Ͻ� ���߾ӿ� ����
        //if (isAtk == false)
        //{
        //    playerSpawn = mapCenter;
        //    atkDirection = 0;

        //}

        ////�������Ͻ� ���� ���� ����(atkDirection 0 = ������ 90 = �Ʒ�, 180= ����)
        //else if(isAtk == true)
        //{
        //    //atkDirection = MapManager.instance.atkDirection;
        //    playerSpawn = new Vector3(Mathf.Sin(atkDirection * Mathf.Deg2Rad) * escapeRange, Mathf.Cos(atkDirection * Mathf.Deg2Rad) * escapeRange, 0);
        //}

        //Instantiate(MapManager.instance.playerShip, playerSpawn, Quaternion.identity);

        createGate(); //Gate ����

        spawnShip(); //���ּ� ����



    }//OnEnable


    public void FixedUpdate()
    {
        player = GameManager.instance.player;
        Vector3 playerDir = player.transform.position - mapCenter;
        reinforceSpawn = escapeRange * playerDir;


    }//FixedUpdate


    //���� Node�� �ֺ� Node�� ��ġ�� ���� �ʵ忡 Gate�� ����� �޼���
    public void createGate()
    {
        //���� Player ��ġ�� Node�� ����� Node���� ����� �迭 ��������
        //(�� connected �迭�� Map ���� �� �ӽ÷� inspector â���� ������ ������, ���� �ʻ���)
        var connectedNodes = MapManager.instance.playerNode.connected; 

        //�� �Ʒ��� connectedNodes �迭�� ���� ����� Node���� ������ ���� �ʵ忡 Gate ���� �ڵ� �ۼ�



    }//createGate


    //Player�� ���� Node�� �ִ� Enemy���� ���������� �����ϴ� �޼���
    public void spawnShip()
    {
        //���� ���� ������ ���̿���, ��� ��忡�� �����ؿԳ��� ���� ���� ��ġ �����ؾ� ��
        //MapManager.instance.turnManager.lastTurn�� ���ؼ� ���� ������ ���̿����� Ȯ�� ����
        //�켱 ���� ������ �ְ�, Player�� �̵����θ� ������ �߻��ϴ� �͸� �ڵ�� ���� �غ��� �ҵ�
        //Player �� ������ �̵� ���� ��ġ�ߴ� ��带 ���� �� �����ϴ� ������ TurnManager���� �߰� �ʿ�



    }//spawnShip


    //�� ���� �޼���, �ϴ� ����
    public void Reinforce()
    {


    }//Reinforce

}
