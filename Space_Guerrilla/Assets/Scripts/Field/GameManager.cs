using JetBrains.Annotations;
using Map;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//GameManager.instance.player
// 현재로서는 일부 스크립트를 인스턴스 참조로 쉽게 찾는 역할만 함
public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager instance;
    //[HideInInspector]
    public PoolManager poolManager; //PoolManager 변수
    //[HideInInspector]
    public SpawnManager spawnManager; //SpawnManager 변수
    [HideInInspector]
    public PlayerInput playerInput; //PlayerInput 변수
    //[HideInInspector]
    public static Enemy_Control OppControl; //적 캐릭터 변수
    //[HideInInspector]
    public GameObject player;

    [Header("기회주의자 적 어그로 확인용 변수")]
    public bool isDefensiveEngage;

    public List<ReinforceInfo> reinforceInfo;
    public float gameTime;
    public float lastReinforce;
    public bool remainEnemies;
    public bool canEscape;
    public Node gateNode; //게이트를 구별하기 위해 번호 대신 Node를 채용
                                       

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        isDefensiveEngage = false;
        gameTime = 0f;
        canEscape = false;
        gateNode = null;
        //reinforceInfo = MapManager.instance.playerNode.reinforceInfo;
        lastReinforce = reinforceInfo[reinforceInfo.Count - 1].ReinforceTime;
        //이 아래 수정하고, Map에서 가져온 게임오브젝트들 전부 Disable 하는 과정 거쳐야 함.

        MapManager.instance.Map.SetActive(false); //Map 비활성화 해서 실제 화면에 안보이게 하기

        //다른 스크립트들이 GameManager.instance.player로 플레이어를 참조하게 기존 코드가 작성되어 있으나, 
        //12/06 기준 player 변수에 실 플레이어 우주선이 할당되는 코드가 존재하지 않음. 
        //또한 PlayerInput 및 PoolManager 스크립트도 GameManager에 할당되어 있지 않음.
        //우선 HideInInspector 을 없애서 인스펙터 창에서 수동으로 추가해놓음(필드는 모든 전투에서 동일하기에)

        player = spawnManager.PlayerInstantiate(); //플레이어 생성
        playerInput = player.GetComponent<PlayerInput>(); //플레이어의 PlayerInput 컴포넌트 가져오기
        spawnManager.setObjects(); //필드에 필요한 Gate, Enemy, Player 생성 및 위치 조정

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
