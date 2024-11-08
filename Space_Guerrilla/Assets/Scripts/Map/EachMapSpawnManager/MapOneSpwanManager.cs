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

        switch (MapManager.instance.shipName)
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
        spawnEnemy(enemyName: EnemyName.EnemyOne, enemySpawnNode: enemyOneSpawnNode);





    }

    public void spawnEnemy(EnemyName enemyName, int enemySpawnNode)
    {
        var enemyNodeList = MapManager.instance.enemyNodeList; //MapManager의 enemyNodeList 가져오기
        var nodes = MapManager.instance.Nodes; //MapManager의 Nodes 가져오기
        var enemyNode = nodes.transform.GetChild(enemySpawnNode).GetComponent<Node>(); //적을 생성하려는 Node 가져오기

        //현재 적을 생성하려는 Node가 MapManager의 enemyNodeList 에 없다면, 추가하기 
        if (!enemyNodeList.Contains(enemyNode))
        {
            enemyNodeList.Add(enemyNode);
            enemyNode.nodeType = NodeType.Enemy; //NodeType 설정
        }

        //enemyCircle 인스턴스 새로 생성, 임시로 좌표는 생성 Node와 동일하게. 이후 수정 필요
        var enemy = Instantiate(enemyCircle[enemyName], enemyNode.transform.position, Quaternion.identity, enemyNode.transform);
        enemyNode.enemyObjects.Add(enemy); //Node의 enemyObjects에 추가
        enemy.GetComponent<Enemy_Circle>().currentNode = enemyNode; //Enemy_Circle의 node 설정

    }
}


