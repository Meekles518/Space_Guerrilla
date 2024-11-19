using Map;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Vector3 playerSpawn; // 플레이어 스폰 위치
    public Vector3 reinforceSpawn; // 적 증원 스폰위치
    public Vector3 gateSpawn; //게이트 스폰 위치
    private Vector3 mapCenter = Vector3.zero; // 맵중앙

    public GameObject player; // 플레이어 오브젝트
    public GameObject[] Enemies; // 적 오브젝트 프리팹들을 보관할 변수
    public GameObject Gate; // 게이트 프리팹을 보관할 변수
    public bool isAtk; // 플레이어가 공격중인지
    public int atkGateNumber; // 공격중일때 필드로 들어온 방향
    public float escapeRange = 50; // 탈출범위
    public float gateDist; // 맵 중앙으로부터 게이트까지의 거리
    public float gateAngle; // 맵 중앙을 기준으로 게이트의 각도

    public List<EnemySpawnInfo> enemySpawnInfo; // 필드에 기존에 있던 적들의 정보
    public List<GateSpawnInfo> gateSpawnInfo; // 필드에서 게이트가 생성될 위치정보
    public List<ReinforceInfo> reinforceInfo; // 필드로 추가되는 지원군 적의 정보
    public List<Vector3> gateSpawnPos = new List<Vector3>(); // 스폰된 게이트들의 위치 정보

    private void OnEnable()
    {
        // 맵쪽에서 부터 공격여부랑 필드내 적 정보 받아옴
        /*if (MapManager.instance.isAtk == true){
            isAtk = true;
            atkGateNumber = MapManager.instance.playerNode.atkGateNumber;
        }
        else
        {
            isAtk=false;
        }*/

        //enemySpawnInfo = MapManager.instance.playerNode.enemySpawninfo;
        //gateSpawnInfo = MapManager.instance.playerNode.gateSpawnInfo;
        //reinforceInfo = MpaManager.instance.playerNode.reinforceInfo;
        player = GameManager.instance.player; //플레이어 가져오기
        SpawnPlayer(); //플레이어 생성
        SpawnEnemy(); //필드 내 적 스폰
        SpawnGate(); //필드 내 게이트 생성
    }

    public void FixedUpdate()
    {
        Vector3 playerDir = player.transform.position - mapCenter; //필드중심으로 부터 플레이어의 위치를 지속적으로 계산
        Reinforce(); // 지원군 생성
    }

    public void SpawnEnemy() // 필드 내에 기존에 있던 적들을 생성
    {
        // 플레이어가 현재 있는 노드로부터 enemySpawnInfo를 가져옴
        // 리스트로부터 스폰될 플레이어의 종류와 스폰좌표를 받아와서 스폰시킴
        for (int i = 0; i < enemySpawnInfo.Count; i++)
        {
            Instantiate(Enemies[enemySpawnInfo[i].EnemyTypes], enemySpawnInfo[i].EnemySpawn.position, Quaternion.Euler(0, 0, 0));
        }
    }

    public void SpawnGate() // 필드에 필요한 위치에 게이트를 생성
    {
        // 플레이어가 현재 있는 노드로부터 gateSpawnInfo를 가져옴
        // 리스트로부터 스폰될 게이트의 각도와 필드 중심으로 부터의 거리를 받아옴
        for (int i = 0; i < gateSpawnInfo.Count; i++)
        {
            gateDist = gateSpawnInfo[i].GateDist; // 필드 중심으로부터의 거리
            gateAngle = gateSpawnInfo[i].GateAngle; // 게이트가 생성될 각도(0 = 오른쪽, 90 = 위, 180 = 왼쪽)
            gateSpawn = new Vector3(Mathf.Sin(gateAngle * Mathf.Deg2Rad) * gateDist, Mathf.Cos(gateDist * Mathf.Deg2Rad) * gateAngle, 0); // 주어진 각도와 거리로 게이트의 좌표를 계산
            gateSpawnPos.Add(gateSpawn); // 지원군 스폰을 위해 게이트 위치를 리스트에 넣어서 기억
            GameObject currGate = Instantiate(Gate, gateSpawn, Quaternion.Euler(0, 0, 0)); // 좌표에 게이트를 생성
            currGate.GetComponent<Gate>().gateNum = i; // 게이트 오브젝트의 번호를 설정
        }
    }

    public void SpawnPlayer() // 필드 내에 정확한 위치에 게이트를 생성
    {
        //수비중일시 플레이어 스폰위치를 맵중앙으로 설정
        if (isAtk == false)
        {
            playerSpawn = mapCenter;
        }
        //공격중일시 플레이어 스폰위치를 들어온 게이트 위치로 설정
        else if (isAtk == true)
        {
            playerSpawn = gateSpawnPos[atkGateNumber];
        }
        Instantiate(MapManager.instance.playerShip, playerSpawn, Quaternion.Euler(0, 0, 0)); // 플레이어를 플레이어 스폰위치에 생성
    }

    public void Reinforce() // 다른 노드에서 부터의 지원군 적들을 생성
    {
        // 플레이어가 현재 있는 노드로부터 reinforceInfo를 가져옴
        // 리스트로부터 스폰될 적의 종류, 적이 도달하는 시간, 적이 도달하는 게이트의 번호를 받아옴
        // 리스트에서 항목이 제외되어 리스트의 크기가 줄어서 생기는 오류를 막기 위해 역순으로 for문을 실행
        for(int i = reinforceInfo.Count - 1; i >= 0; i--)
        {
            // 게임시간이 적 지원군이 도착하는 시간을 넘었다면 실행
            if (reinforceInfo[i].ReinforceTime < GameManager.instance.gameTime)
            {
                Instantiate(Enemies[reinforceInfo[i].EnemyTypes], gateSpawnPos[reinforceInfo[i].GateNumber], Quaternion.Euler(0, 0, 0)); // 적 지원군을 생성
                reinforceInfo.Remove(reinforceInfo[i]); // 생성된 적 지원군을 리스트에서 제거
            }
        }
    }
}
