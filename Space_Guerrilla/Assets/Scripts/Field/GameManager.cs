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


    //�� �Ʒ��� �������� Player ���ּ��� ���� ������ �ʿ�Ǵ� ������
    public PlayerInfo playerInfo;
    public PlayerBulletInfo playerBulletInfo;
    public PlayerMovement playerMovement;
    public Player_Bullet playerBullet;
    public Player_CruiseMissile playerCruiseMissile;
    public ShipEntity shipEntity;
    public ShipEntity bulletShipEntity;
    public Shooter[] shooters;




    //�� �Ʒ��� �������� Map�� ������ �ҷ����ų� Ȱ���ϴµ��� �ʿ��� ������

   


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

        //MapManager�� �ִ� Player �����ͼ� ������Ű��
        player = Instantiate(MapManager.instance.playerShip, new Vector3(0, 0, 0), Quaternion.identity);
        getInfo();

        //�� �Ʒ� ���� ���� ���� �ʿ�/
        //MapManager�� ����Ǿ� �ִ� ShipName�� ���� Player ���ּ��� Prefab�� �ҷ����� switch��

        /*
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
        */

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

        playerInfo = MapManager.instance.playerShipInfo; //MapManger���� PlayerInfo ��������
        playerBulletInfo = MapManager.instance.playerBulletInfo; //MapManager���� PlayerBulletInfo ��������
      
        playerMovement = player.GetComponent<PlayerMovement>(); //Player�� PlayerMovement ��������
        shipEntity = player.GetComponent<ShipEntity>(); //Player�� ShipEntity ��������
        shooters = player.GetComponentsInChildren<Shooter>();

        playerMovement.moveSpeed = playerInfo.moveSpeed; //�̵��ӵ� ����
        playerMovement.rotateSpeed = playerInfo.rotateSpeed; //ȸ���ӵ� ���� 
        shipEntity.getShipEntity(playerInfo); //ShipEntity ����

        //�� ���� ����(���ּ��� ���� ������ ���� ����� ��������)
        for (int i = 0; i < shooters.Length; i++)
        {
            shooters[i].bulletType = playerInfo.bulletType1;
            shooters[i].magCapacity = playerInfo.magCapacity1;
            shooters[i].magAmmo = playerInfo.magAmmo1;
            shooters[i].recoil = playerInfo.recoil1;
            shooters[i].reloadTime = playerInfo.reloadTime1;
            shooters[i].timeBetFire = playerInfo.timeBetFire1;
            shooters[i].projectilesPerFire = playerInfo.projectilesPerFire1;
            shooters[i].timeBetProjectiles = playerInfo.timeBetProjectiles1;
            shooters[i].reloadInterval = playerInfo.reloadInterval1;
        }

        //źȯ ����(źȯ�� ���� ������ ���� ����� GameManager�� �����ϰ�, 
        //źȯ ���� �ø��� GameManager�� �ִ� ������ �ٽ� �ѹ� ���� ����� źȯ�鿡 ����.
        //(�̻��Ͽ� ���� �ڵ�� �߰��� �����ؾ� ��(ShipEntity�� �̻��Ͽ��� ����??)
        bulletShipEntity.getShipEntity(playerBulletInfo);
        playerBullet.getBulletInfo(playerBulletInfo);

    }



}
