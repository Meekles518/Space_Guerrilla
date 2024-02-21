using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

//적을 Map에 나타내는 데에 사용하는 Circle이 모두 가지고 있을 클래스,
//모든 Ai 스크립트는 Enemy_Circle 클래스를 상속받는다??
public class Enemy_Circle : MonoBehaviour
{

    //적의 행동성향 enum, 조합의 합이 랜덤이 아니라 정해져 있으니, 모든 경우의 수를 열거형 표현
    public enum Status
    {
       RoamingHuntRun,
       HouseHuntRun,
       RomingRun,
       HuntRun,

    }


    public Status status; //적의 행동성향을 저장할 변수
    public bool telescopeHunt; //망원경 추적 모드를 구별할 bool 변수
    public Node targetNode; //목표 Node를 저장할 변수
    public EnemyInfo enemyInfo;

    public Node currentNode; //현재 위치한 Node를 저장할 변수

    //virtual, 가상 함수로 적의 Ai 로직을 구현. 이 Class를 상속받는 서로 다른 종류의 적이
    //함수를 마저 완성시키기.
    public virtual void enemyAi()
    {
        enemyInfo = GetComponent<EnemyInfo>(); //EnemyInfo 컴포넌트 가져오기
        currentNode = GetComponentInParent<Node>(); //부모의 Node 정보 가져오기

        //추적 Phase일 때
        if (MapManager.instance.phase == Phase.Hunt)
        {


        }

        //집결 Phase 일 때
        else if (MapManager.instance.phase == Phase.Assemble)
        {

        }

        else
        {
            switch (status)
            {
                case Status.RoamingHuntRun:
                    Run();   
                    Hunt();
                    Roaming();
                    break;

                case Status.HouseHuntRun:
                    Run();
                    Hunt();
                    House();
                    break;

                case Status.RomingRun:
                    Run();
                    Roaming();
                    break;

                case Status.HuntRun:

                    Run();
                    Hunt();
                    break;



                default:
                    break;
            }

        }
         

        

    }//movement

    public void Run()
    {
        //체력이 30% 이하일 경우
        if (enemyInfo.health / enemyInfo.maxhealth <= 0.3f)
        {
            //현재 Node가 Player이 위치한 Node일 경우
            if (currentNode == MapManager.instance.playerNode)
            {
                //인근의 무작위 Node로 이동
                int ran = Random.Range(0, currentNode.connected.Count);
                movement(currentNode.connected[ran]);
            }
        }

    }//Run

    public void Hunt()
    {
        //만약 Player이 탐지된 상태라면
        if (MapManager.instance.playerDetected)
        {

        }


    }//Hunt

    public void Roaming()
    {


    }//Roaming


    public void House()
    {

    }//House

    //목표 node까지 최단거리로 이동하는 
    public void BFS(Node node)
    {


    }//BFS

    //실제로 적을 이동시키는 함수
    public void movement(Node node)
    {
        
        currentNode.enemyObjects.Remove(gameObject); //Node에서 적 정보 제거
        //만약 이 Node에 적이 더 이상 없다면
        if (currentNode.enemyObjects.Count == 0)
        {
            //MapManager의 적 Node 리스트에서 제거
            MapManager.instance.enemyNodeList.Remove(currentNode);
        }

        //현재 적 Node 리스트에 이동하려는 node가 존재하지 않는다면
        if (!MapManager.instance.enemyNodeList.Contains(node))
        {
            //이동하려는 Node 추가
            MapManager.instance.enemyNodeList.Add(node);
        }
        node.enemyObjects.Add(gameObject); //이동하려는 Node의 적 List에 추가

        gameObject.transform.parent = node.gameObject.transform; //부모 변경




    }//movement


     


}

