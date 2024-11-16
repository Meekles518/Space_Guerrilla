using Map;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{


    public Vector3 playerSpawn; // 플레이어 스폰 위치
    public Vector3 reinforceSpawn; // 적 증원 스폰위치
    public Vector3 gateSpawn; //게이트 스폰 위치
    private Vector3 mapCenter = Vector3.zero; // 맵중앙
    // public Vector3 playerPos; // 현재 플레이어 위치의 각도

    public GameObject player; // 플레이어 오브젝트
    public GameObject[] Enemies; // 적 오브젝트 프리팹을 보관할 변수
    public GameObject Gate;
    public bool isAtk; // 플레이어가 공격중인지
    public int atkDirection; // 공격중일때 필드로 들어온 방향
    public float escapeRange = 50; // 탈출범위
    public float gateDist;
    public float gateAngle;


    public List<EnemySpawnInfo> enemySpawnInfo; // 필드에 기존에 있던 적 정보
    public List<GateSpawnInfo> gateSpawninfo; // 필드에서 게이트가 생성될 위치
    


    private void OnEnable()
    {
        // 맵쪽에서 부터 공격여부랑 필드내 적 정보 받아옴
        /*if (MapManager.instance.isAtk == true)
            isAtk = true;
        else
        {
            isAtk=false;
        }*/

        //enemySpawnInfo = MapManager.instance.playerNode.enemySpawninfo;
        //gateSpawnInfo = MapManager.instance.playerNode.gateSpawninfo;

        // 필드내 적의 종류와 위치를 읽어서 생성
        for (int i = 0; i < enemySpawnInfo.Count; i++)
        {
            Instantiate(Enemies[enemySpawnInfo[i].EnemyTypes], enemySpawnInfo[i].EnemySpawn.position, Quaternion.Euler(0,0,0));
        }


        for (int i = 0; i < gateSpawninfo.Count; i++)
        {
            gateDist = gateSpawninfo[i].GateDist;
            gateAngle = gateSpawninfo[i].GateAngle;
            gateSpawn = new Vector3(Mathf.Sin(gateAngle * Mathf.Deg2Rad) * gateDist, Mathf.Cos(gateDist * Mathf.Deg2Rad) * gateAngle, 0);
            Instantiate(Gate, gateSpawn, Quaternion.Euler(0, 0, 0));
        }



            //수비중일시 플레이어를 맵중앙에 생성
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

        Instantiate(MapManager.instance.playerShip, playerSpawn, Quaternion.Euler(0, 0, 0));

    }


    public void FixedUpdate()
    {
        player = GameManager.instance.player;
        Vector3 playerDir = player.transform.position - mapCenter;



    }


    public void Reinforce()
    {

        
    }

}
