using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����μ��� �Ϻ� ��ũ��Ʈ�� �ν��Ͻ� ������ ���� ã�� ���Ҹ� ��
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager poolManager;
    public GameObject player;
    public PlayerInput playerInput;
    public bool isDefensiveEngage;
    public static Enemy_Control OppControl;


    void Awake()
    {

        instance = this;
        isDefensiveEngage = false;
        OppControl = null;
      


    }
    public void FixedUpdate()
    {
       // Debug.Log(OppControl);
        //Debug.Log(GameManager.OppControl.isAggro);
    }
}
