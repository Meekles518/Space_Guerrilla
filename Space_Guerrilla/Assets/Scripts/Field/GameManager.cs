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
    public PoolManager poolManager;
    [HideInInspector]
    public PlayerInput playerInput;
    [HideInInspector]
    public static Enemy_Control OppControl;
    //[HideInInspector]
    public GameObject player;
    [Header("��ȸ������ �� ��׷� Ȯ�ο� ����")]
    public bool isDefensiveEngage;

    public bool errorCheck = false;

    //�� �Ʒ��� �������� Map�� ������ �ҷ����ų� Ȱ���ϴµ��� �ʿ��� ������

    public List<GameObject> playerPrefabs; //Player ���ּ� Prefab���� ������ ����.

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
         
        isDefensiveEngage = false;


        //�� �Ʒ� �����ϰ�, Map���� ������ ���ӿ�����Ʈ�� ���� Disable �ϴ� ���� ���ľ� ��.
        //Map���� ������ PlayerInfo����, PlayerBulletInfo�� PlayerBullet�� ������ �� ���� �ο��ϰ�,
        //Player�� ���ּ��� ���õ� ������ ���� ���ּ� Instantiate �ÿ� ���� �ο��ϴ� ���� ����
        
        //MapManager�� ����Ǿ� �ִ� ShipName�� ���� Player ���ּ��� Prefab�� �ҷ����� switch��
        switch (MapManager.instance.shipName)
        {
            //Ship1�� ��,
            case ShipName.Ship1:

                //���� �κ��� ����� �۵��ϳ�?
                

                //playerPrefab�� ����Ǿ� �ִ� Ship1 ���� �� player ������ ����, (0, 0, 0) ��ġ�� ����
                player = Instantiate(playerPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
                getInfo();

                break;

            case ShipName.Aegis:

                //playerPrefab�� ����Ǿ� �ִ� Aegis ���� �� player ������ ����, (0, 0, 0) ��ġ�� ����
                player = Instantiate(playerPrefabs[1], new Vector3(0, 0, 0), Quaternion.identity);
                getInfo();


                break;


        }

        playerInput = player.GetComponent<PlayerInput>();


        //Enemy�� �����ϴ� �ڵ� �κ�



        MapManager.instance.Map.SetActive(false); //Map ��Ȱ��ȭ �ؼ� ���� ȭ�鿡 �Ⱥ��̰� �ϱ�

        errorCheck = true;

    }

    
    public void FixedUpdate()
    {

    }

    public void getInfo()
    {
        //PlayerInfo���� ������ ������ Player�� ���� ���ּ��� �����ϴ� �ڵ��

        var playerInfo = MapManager.instance.playerInfo.GetComponent<PlayerInfo>(); //MapManger���� PlayerInfo ��������
        var playerMovement = player.GetComponent<PlayerMovement>(); //Player�� PlayerMovement ��������
        var playerShipEntity = player.GetComponent<ShipEntity>(); //Player�� ShipEntity ��������
        var playerShooters = player.GetComponentsInChildren<Shooter>();

        playerMovement.moveSpeed = playerInfo.moveSpeed; //�̵��ӵ� ����
        playerMovement.rotateSpeed = playerInfo.rotateSpeed; //ȸ���ӵ� ���� 
        playerShipEntity.getShipEntity(playerInfo); //ShipEntity ����

        //�� ���� ����
        for (int i = 0; i < playerShooters.Length; i++)
        {
            playerShooters[i].bulletType = playerInfo.bulletType1;
            playerShooters[i].magCapacity = playerInfo.magCapacity1;
            playerShooters[i].magAmmo = playerInfo.magAmmo1;
            playerShooters[i].recoil = playerInfo.recoil1;
            playerShooters[i].reloadTime = playerInfo.reloadTime1;
            playerShooters[i].timeBetFire = playerInfo.timeBetFire1;
            playerShooters[i].projectilesPerFire = playerInfo.projectilesPerFire1;
            playerShooters[i].timeBetProjectiles = playerInfo.timeBetProjectiles1;
            playerShooters[i].reloadInterval = playerInfo.reloadInterval1;
        }


    }



}
