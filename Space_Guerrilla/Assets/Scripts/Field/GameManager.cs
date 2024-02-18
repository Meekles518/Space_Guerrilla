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
    [HideInInspector]
    public GameObject player;
    [Header("��ȸ������ �� ��׷� Ȯ�ο� ����")]
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
