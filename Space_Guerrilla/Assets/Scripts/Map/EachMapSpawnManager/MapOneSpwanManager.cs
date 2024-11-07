using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOneSpwanManager : MapSpawnManager
{
    public override void enemyLvlUp()
    {
        throw new System.NotImplementedException();
    }

    public override void enemyReinforcement()
    {
        throw new System.NotImplementedException();
    }

    public override void init()
    {
        setDict(); //Dict 설정하기

        int playerSpawnNode = 0;
        int enemyOneSpawnNode = 11;

        switch(MapManager.instance.shipName)
        {
            case ShipName.Aegis:
                MapManager.instance.playerShip = Instantiate(playerShip[ShipName.Aegis]);
                break;
        }

        //PlayerNode 설정
        MapManager.instance.playerNode = MapManager.instance.Nodes.transform.GetChild(playerSpawnNode).GetComponent<Node>();
        MapManager.instance.playerNode.nodeType = NodeType.Player;
        MapManager.instance.playerNode.setColor();


        //EnemyOne의 Node 설정
        MapManager.instance.enemyNodeList.Add(MapManager.instance.Nodes.transform.GetChild(enemyOneSpawnNode).GetComponent<Node>());
        MapManager.instance.enemyNodeList[0].nodeType = NodeType.Enemy;
        var enemyOne = Instantiate(enemyCircle[EnemyName.EnemyOne], MapManager.instance.enemyNodeList[0].transform.position, Quaternion.identity);
        MapManager.instance.enemyNodeList[0].enemyObjects.Add(enemyOne);





    }
}


