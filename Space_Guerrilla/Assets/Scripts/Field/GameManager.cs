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

                break;


        }

        //PlayerInfo���� ������ ������ Player�� ���� ���ּ��� �����ϴ� �ڵ��

        var playerInfo = MapManager.instance.playerInfo.GetComponent<PlayerInfo>(); //MapManger���� PlayerInfo ��������
        var playerMovement = player.GetComponent<PlayerMovement>(); //Player�� PlayerMovement ��������
        var playerShipEntity = player.GetComponent<ShipEntity>(); //Player�� ShipEntity ��������
        var playerShooters = player.GetComponentsInChildren<Shooter>();
        
        playerMovement.moveSpeed = playerInfo.moveSpeed; //�̵��ӵ� ����
        playerMovement.rotateSpeed = playerInfo.rotateSpeed; //ȸ���ӵ� ���� 
        playerShipEntity.getShipEntity(playerInfo); //ShipEntity ����

        //Player�� Shooter ��ũ��Ʈ�� ���� �ο��ϴ� �ڵ�. ����� �ϵ��ڵ����� ������ �� ����, �������� �������⿡ �� �ο�
        //���� �ڵ� ������ ���� Ȯ�强 ���� �ʿ�
        
        //������ �� ���� ���� ����
        playerShooters[0].bulletType = playerInfo.bulletType1;
        playerShooters[0].magCapacity = playerInfo.magCapacity1;
        playerShooters[0].magAmmo = playerInfo.magAmmo1;
        playerShooters[0].recoil = playerInfo.recoil1;
        playerShooters[0].reloadTime = playerInfo.reloadTime1;
        playerShooters[0].timeBetFire = playerInfo.timeBetFire1;
        playerShooters[0].projectilesPerFire = playerInfo.projectilesPerFire1; 
        playerShooters[0].timeBetProjectiles = playerInfo.timeBetProjectiles1; 
        playerShooters[0].reloadInterval = playerInfo.reloadInterval1;

        //�������� ���� ���� ���� ����
        playerShooters[1].bulletType = playerInfo.bulletType2;
        playerShooters[1].magCapacity = playerInfo.magCapacity2;
        playerShooters[1].magAmmo = playerInfo.magAmmo2;
        playerShooters[1].recoil = playerInfo.recoil2;
        playerShooters[1].reloadTime = playerInfo.reloadTime2;
        playerShooters[1].timeBetFire = playerInfo.timeBetFire2;
        playerShooters[1].projectilesPerFire = playerInfo.projectilesPerFire2;
        playerShooters[1].timeBetProjectiles = playerInfo.timeBetProjectiles2;
        playerShooters[1].reloadInterval = playerInfo.reloadInterval2;

        playerInput = player.GetComponent<PlayerInput>();


        //Enemy�� �����ϴ� �ڵ� �κ�



        MapManager.instance.Map.SetActive(false); //Map ��Ȱ��ȭ �ؼ� ���� ȭ�鿡 �Ⱥ��̰� �ϱ�

        errorCheck = true;

    }

    
    public void FixedUpdate()
    {

    }





}
