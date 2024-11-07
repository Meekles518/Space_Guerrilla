using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;


[System.Serializable]
public class PlayerShipPair
{
    public ShipName shipName;
    public GameObject playerGameObject;
}

[System.Serializable]
public class EnemyCirclePair
{
    public EnemyName enemyName;
    public GameObject enemyCircle;
}


public abstract class MapSpawnManager : MonoBehaviour
{
    //Inspector 창에서 Player, EnemyCircle를 설정할 수 있도록 List로 설정
    public List<PlayerShipPair> playerShipList;
    public List<EnemyCirclePair> enemyCircleList;

    //Player의 우주선 GameObject들을 담을 Dict
    public Dictionary<ShipName, GameObject> playerShip = new Dictionary<ShipName, GameObject>();

    //적을 표시하는 EnemyCircle을 담을 Dict
    public Dictionary<EnemyName, GameObject> enemyCircle = new Dictionary<EnemyName, GameObject>();

    //Player와 Enemy를 가져올 Dict 값 설정
    public void setDict()
    {
        foreach (PlayerShipPair playerShipPair in playerShipList)
        {
            playerShip.Add(playerShipPair.shipName, playerShipPair.playerGameObject);
        }

        foreach (EnemyCirclePair enemyCirclePair in enemyCircleList)
        {
            enemyCircle.Add(enemyCirclePair.enemyName, enemyCirclePair.enemyCircle);
        }

    }


    //맵 첫 생성 시에 적 및 플레이어를 생성하는 메서드
    public abstract void init();

    //일정 시기마다 적 전체를 레벨업 시키는 메서드
    public abstract void enemyLvlUp();

    //일정 시기마다 적을 추가로 생성하는 메서드
    public abstract void enemyReinforcement();

}
