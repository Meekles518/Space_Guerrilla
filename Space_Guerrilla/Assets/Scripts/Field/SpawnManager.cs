using Map;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // * 이 아래는 구상 중인 내용*
    //현재 MapManager에서 Turn과 관련된 로직을 다루고 있다(차후에 TurnManager로 쪼개야 함)
    //MapManager에 기본적으로 필드 전투 돌입 전 누구의 Turn이였는지를 저장하는 lastTurn 변수가 존재

    //따라서 일반적인 맵에서 필드로의 이동 시에는 MapManager의 lastTurn 변수를 통해 우주선의 생성위치를 결정하고,
    //연속 전투 시에만 isAtk 값을 활용하게끔 하면 어떨까?

    public Vector3 playerSpawn; // 플레이어 스폰 위치
    public Vector3 reinforceSpawn; // 적 증원 스폰위치
    private Vector3 mapCenter = Vector3.zero; // 맵중앙
    // public Vector3 playerPos; // 현재 플레이어 위치의 각도

    public GameObject player;
    public bool isAtk = false; // 플레이어가 공격중인지(연속 전투 구분용으로 활용하면 어떨까?)
    public int atkDirection; // 공격중일때 필드로 들어온 방향
    public float escapeRange = 50; // 탈출범위

    public List<EnemySpawnInfo> enemySpawnInfo; // 필드에 기존에 있던 적 정보


    private void OnEnable()
    {
        // 맵쪽에서 부터 공격여부랑 필드내 적 정보 받아옴
        /*if (MapManager.instance.isAtk == true)
            isAtk = true;
        else
        {
            isAtk=false;
        }*/

        //enemyInfo = MapManager.instance.enemyinfo;

        // 필드내 적의 종류와 위치를 읽어서 생성
        //고유성 유지를 위해 PoolManager가 아닌 독자로 Spawner에서 생성하게 변경?
        //for (int i = 0; i < enemySpawnInfo.Count; i++)
        //{

        //    GameManager.instance.poolManager.Get(enemySpawnInfo[i].EnemyTypes, enemySpawnInfo[i].EnemySpawn);

        //}


        ////수비중일시 맵중앙에 생성
        //if (isAtk == false)
        //{
        //    playerSpawn = mapCenter;
        //    atkDirection = 0;

        //}

        ////공격중일시 들어온 방향 생성(atkDirection 0 = 오른쪽 90 = 아래, 180= 왼쪽)
        //else if(isAtk == true)
        //{
        //    //atkDirection = MapManager.instance.atkDirection;
        //    playerSpawn = new Vector3(Mathf.Sin(atkDirection * Mathf.Deg2Rad) * escapeRange, Mathf.Cos(atkDirection * Mathf.Deg2Rad) * escapeRange, 0);
        //}

        //Instantiate(MapManager.instance.playerShip, playerSpawn, Quaternion.identity);






    }//OnEnable


    public void FixedUpdate()
    {
        player = GameManager.instance.player;
        Vector3 playerDir = player.transform.position - mapCenter;
        reinforceSpawn = escapeRange * playerDir;


    }//FixedUpdate


    //현재 Node의 주변 Node의 위치를 통해 필드에 Gate를 만드는 메서드
    public void createGate()
    {

    }//createGate


    //Player와 현재 Node에 있는 Enemy들을 실질적으로 생성하는 메서드
    public void spawnShip()
    {

    }//spawnShip


    //적 증원 메서드, 일단 보류
    public void Reinforce()
    {


    }//Reinforce

}
