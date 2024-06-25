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

    //이 아래의 변수들은 Map의 정보를 불러오거나 활용하는데에 필요한 변수들

    public List<GameObject> playerPrefabs; //Player 우주선 Prefab들을 저장할 변수.

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
        
        //MapManager에 저장되어 있는 ShipName을 통해 Player 우주선의 Prefab을 불러오는 switch문
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

        var playerInfo = MapManager.instance.playerInfo.GetComponent<PlayerInfo>(); //MapManger에서 PlayerInfo 가져오기
        var playerMovement = player.GetComponent<PlayerMovement>(); //Player의 PlayerMovement 가져오기
        var playerShipEntity = player.GetComponent<ShipEntity>(); //Player의 ShipEntity 가져오기
        var playerShooters = player.GetComponentsInChildren<Shooter>();

        playerMovement.moveSpeed = playerInfo.moveSpeed; //이동속도 설정
        playerMovement.rotateSpeed = playerInfo.rotateSpeed; //회전속도 설정 
        playerShipEntity.getShipEntity(playerInfo); //ShipEntity 설정

        //주 무기 설정
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
