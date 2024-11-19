using JetBrains.Annotations;
using Map;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public List<ReinforceInfo> reinforceInfo;
    public float gameTime;
    public float lastReinforce;
    public bool remainEnemies;
    public bool canEscape;
    public int gateNum;
                                       


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        isDefensiveEngage = false;
        gameTime = 0f;
        canEscape = false;
        gateNum = 0;
        //reinforceInfo = MapManager.instance.playerNode.reinforceInfo;
        lastReinforce = reinforceInfo[reinforceInfo.Count - 1].ReinforceTime;
        //이 아래 수정하고, Map에서 가져온 게임오브젝트들 전부 Disable 하는 과정 거쳐야 함.
        //Map에서 가져온 PlayerInfo에서, PlayerBulletInfo는 PlayerBullet이 생성될 때 값을 부여하고,
        playerShipInfo = MapManager.instance.playerShip.GetComponent<ShipInfo>(); //Map에서 가져온 PlayerInfo를 저장
        playerBulletInfo = MapManager.instance.playerShip.GetComponent<BulletInfo>(); //Map에서 가져온 PlayerBulletInfo를 저장

        MapManager.instance.Map.SetActive(false); //Map 비활성화 해서 실제 화면에 안보이게 하기
    }

    public void FixedUpdate()
    {
        gameTime = Time.fixedDeltaTime;
        CheckEnemy();
        if(playerInput.skillQ == true)
        {
            Escape();
        }
    }

    public void CheckEnemy()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(Enemies.Length == 0)
        {
            remainEnemies = false;
            canEscape = true;
        }
        else if(Enemies.Length > 1)
        {
            remainEnemies = true;
        }
    }

    public void Escape(){
        if (canEscape == true)
        {
            if (true)//탈출버튼을 꾹누르면
            {
                MapManager.instance.Map.SetActive(true);
                SceneManager.UnloadSceneAsync("Field");
            }
        }
        else if(canEscape == false)
        {
            // UI에 탈출 비활성화라고 표시
        }
    }
}
