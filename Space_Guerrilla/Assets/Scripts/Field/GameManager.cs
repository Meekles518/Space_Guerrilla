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
    [HideInInspector]
    public GameObject player;
    [Header("기회주의자 적 어그로 확인용 변수")]
    public bool isDefensiveEngage;
    


    void Awake()
    {

        instance = this;
        isDefensiveEngage = false;
        player = GameObject.Find("Player");
        playerInput = player.GetComponent<PlayerInput>();
      


    }
    public void FixedUpdate()
    {

    }
}
