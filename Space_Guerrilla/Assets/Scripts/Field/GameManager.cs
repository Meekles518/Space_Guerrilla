using JetBrains.Annotations;
using Map;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 현재로서는 일부 스크립트를 인스턴스 참조로 쉽게 찾는 역할만 함
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
    [Header("기회주의자 적 어그로 확인용 변수")]
    public bool isDefensiveEngage;

    public bool errorCheck = false;


    //이 아래의 변수들은 Player 우주선의 스탯 조정에 필요되는 변수들
    public PlayerInfo playerInfo;
    public PlayerBulletInfo playerBulletInfo;
    public PlayerMovement playerMovement;
    public Player_Bullet playerBullet;
    public Player_CruiseMissile playerCruiseMissile;
    public ShipEntity shipEntity;
    public ShipEntity bulletShipEntity;
    public Shooter[] shooters;




    //이 아래의 변수들은 Map의 정보를 불러오거나 활용하는데에 필요한 변수들

   


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
         
        isDefensiveEngage = false;


        //이 아래 수정하고, Map에서 가져온 게임오브젝트들 전부 Disable 하는 과정 거쳐야 함.
        //Map에서 가져온 PlayerInfo에서, PlayerBulletInfo는 PlayerBullet이 생성될 때 값을 부여하고,
        //Player의 우주선에 관련된 정보는 최초 우주선 Instantiate 시에 값을 부여하는 과정 구현

        //MapManager에 있는 Player 가져와서 생성시키기
        player = Instantiate(MapManager.instance.playerShip, new Vector3(0, 0, 0), Quaternion.identity);
        getInfo();

        //이 아래 전부 수정 개선 필요/
        //MapManager에 저장되어 있는 ShipName을 통해 Player 우주선의 Prefab을 불러오는 switch문

        /*
        switch (MapManager.instance.shipName)
        {
            //Ship1일 때,
            case ShipName.Ship1:

                //여기 부분이 제대로 작동하나?
                

                //playerPrefab에 저장되어 있는 Ship1 생성 후 player 변수에 저장, (0, 0, 0) 위치에 생성
                player = Instantiate(playerPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
                getInfo();

                break;

            case ShipName.Aegis:

                //playerPrefab에 저장되어 있는 Aegis 생성 후 player 변수에 저장, (0, 0, 0) 위치에 생성
                player = Instantiate(playerPrefabs[1], new Vector3(0, 0, 0), Quaternion.identity);
                getInfo();


                break;


        }
        */

        playerInput = player.GetComponent<PlayerInput>();


        //Enemy를 생성하는 코드 부분



        MapManager.instance.Map.SetActive(false); //Map 비활성화 해서 실제 화면에 안보이게 하기

        errorCheck = true;

    }

    
    public void FixedUpdate()
    {

    }

    public void getInfo()
    {
        //PlayerInfo에서 값들을 가져와 Player의 현재 우주선에 저장하는 코드들

        playerInfo = MapManager.instance.playerShipInfo; //MapManger에서 PlayerInfo 가져오기
        playerBulletInfo = MapManager.instance.playerBulletInfo; //MapManager에서 PlayerBulletInfo 가져오기
      
        playerMovement = player.GetComponent<PlayerMovement>(); //Player의 PlayerMovement 가져오기
        shipEntity = player.GetComponent<ShipEntity>(); //Player의 ShipEntity 가져오기
        shooters = player.GetComponentsInChildren<Shooter>();

        playerMovement.moveSpeed = playerInfo.moveSpeed; //이동속도 설정
        playerMovement.rotateSpeed = playerInfo.rotateSpeed; //회전속도 설정 
        shipEntity.getShipEntity(playerInfo); //ShipEntity 설정

        //주 무기 설정(우주선에 직접 값들을 깊은 복사로 가져오기)
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

        //탄환 설정(탄환에 대한 값들을 깊은 복사로 GameManager에 저장하고, 
        //탄환 생성 시마다 GameManager에 있는 값들을 다시 한번 깊은 복사로 탄환들에 저장.
        //(미사일에 대한 코드는 추가로 설정해야 함(ShipEntity가 미사일에만 없다??)
        bulletShipEntity.getShipEntity(playerBulletInfo);
        playerBullet.getBulletInfo(playerBulletInfo);

    }



}
