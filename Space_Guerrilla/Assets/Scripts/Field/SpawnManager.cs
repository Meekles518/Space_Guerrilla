using Map;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{


    public Vector3 playerSpawn; // 플레이어 스폰 위치
    public Vector3 reinforceSpawn; // 적 증원 스폰위치
    private Vector3 mapCenter = Vector3.zero; // 맵중앙
    // public Vector3 playerPos; // 현재 플레이어 위치의 각도

    public GameObject player;
    public bool isAtk; // 플레이어가 공격중인지
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
        for (int i = 0; i < enemySpawnInfo.Count; i++)
        {

            GameManager.instance.poolManager.Get(enemySpawnInfo[i].EnemyTypes, enemySpawnInfo[i].EnemySpawn);

        }
        

        //수비중일시 맵중앙에 생성
        if (isAtk == false)
        {
            playerSpawn = mapCenter;
            atkDirection = 0;
            
        }

        //공격중일시 들어온 방향 생성(atkDirection 0 = 오른쪽 90 = 아래, 180= 왼쪽)
        else if(isAtk == true)
        {
            //atkDirection = MapManager.instance.atkDirection;
            playerSpawn = new Vector3(Mathf.Sin(atkDirection * Mathf.Deg2Rad) * escapeRange, Mathf.Cos(atkDirection * Mathf.Deg2Rad) * escapeRange, 0);
        }

        Instantiate(MapManager.instance.playerShip, playerSpawn, Quaternion.identity);

    }


    public void FixedUpdate()
    {
        player = GameManager.instance.player;
        Vector3 playerDir = player.transform.position - mapCenter;
        reinforceSpawn = escapeRange * playerDir;


    }


    public void Reinforce()
    {

        
    }

}
