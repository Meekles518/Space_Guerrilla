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
    public PoolManager poolManager; //PoolManager 변수
    [HideInInspector]
    public SpawnManager spawnManager; //SpawnManager 변수
    [HideInInspector]
    public PlayerInput playerInput; //PlayerInput 변수
    //[HideInInspector]
    public static Enemy_Control OppControl; //적 캐릭터 변수
    //[HideInInspector]
    public GameObject player;

    [Header("기회주의자 적 어그로 확인용 변수")]
    public bool isDefensiveEngage;

    public ShipInfo playerShipInfo; //Map에서 가져온 PlayerInfo를 저장할 변수
    public BulletInfo playerBulletInfo; //Map에서 가져온 PlayerBulletInfo를 저장할 변수                                


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
         
        isDefensiveEngage = false;


        //이 아래 수정하고, Map에서 가져온 게임오브젝트들 전부 Disable 하는 과정 거쳐야 함.
        //Map에서 가져온 PlayerInfo에서, PlayerBulletInfo는 PlayerBullet이 생성될 때 값을 부여하고,

        playerShipInfo = MapManager.instance.playerShipInfo; //Map에서 가져온 PlayerInfo를 저장
        playerBulletInfo = MapManager.instance.playerBulletInfo; //Map에서 가져온 PlayerBulletInfo를 저장


        MapManager.instance.Map.SetActive(false); //Map 비활성화 해서 실제 화면에 안보이게 하기



    }

    
    public void FixedUpdate()
    {

    }



}
