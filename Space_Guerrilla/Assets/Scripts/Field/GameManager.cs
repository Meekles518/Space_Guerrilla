using JetBrains.Annotations;
using Map;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����μ��� �Ϻ� ��ũ��Ʈ�� �ν��Ͻ� ������ ���� ã�� ���Ҹ� ��
public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager instance;
    [HideInInspector]
    public PoolManager poolManager; //PoolManager ����
    [HideInInspector]
    public SpawnManager spawnManager; //SpawnManager ����
    [HideInInspector]
    public PlayerInput playerInput; //PlayerInput ����
    //[HideInInspector]
    public static Enemy_Control OppControl; //�� ĳ���� ����
    //[HideInInspector]
    public GameObject player;

    [Header("��ȸ������ �� ��׷� Ȯ�ο� ����")]
    public bool isDefensiveEngage;

    public ShipInfo playerShipInfo; //Map���� ������ PlayerInfo�� ������ ����
    public BulletInfo playerBulletInfo; //Map���� ������ PlayerBulletInfo�� ������ ����                                


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
         
        isDefensiveEngage = false;


        //�� �Ʒ� �����ϰ�, Map���� ������ ���ӿ�����Ʈ�� ���� Disable �ϴ� ���� ���ľ� ��.
        //Map���� ������ PlayerInfo����, PlayerBulletInfo�� PlayerBullet�� ������ �� ���� �ο��ϰ�,

        playerShipInfo = MapManager.instance.playerShipInfo; //Map���� ������ PlayerInfo�� ����
        playerBulletInfo = MapManager.instance.playerBulletInfo; //Map���� ������ PlayerBulletInfo�� ����


        MapManager.instance.Map.SetActive(false); //Map ��Ȱ��ȭ �ؼ� ���� ȭ�鿡 �Ⱥ��̰� �ϱ�



    }

    
    public void FixedUpdate()
    {

    }



}
