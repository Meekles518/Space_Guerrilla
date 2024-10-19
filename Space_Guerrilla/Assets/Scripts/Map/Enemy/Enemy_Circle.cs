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
    public Node currentNode; //현재 위치한 Node를 저장할 변수

    //virtual, 가상 함수로 적의 Ai 로직을 구현. 이 Class를 상속받는 서로 다른 종류의 적이
    //함수를 마저 완성시키기.
    public virtual void enemyAi()
    {
       

    }//movement

    public void Run()
    {
              

    }//Run

    public void Hunt()
    {



    }//Hunt

    public void Roaming()
    {


    }//Roaming


    public void House()
    {

    }//House

    //목표 node까지 최단거리로 이동하는 함수
    //역으로, 목표 Node에서 현재 적이 위치한 Node로 가는 최단 경로를 얻어내고,
    //connected 리스트에 현재 적이 위치한 Node를 찾아낸다면, 역추적으로 구성
    public Node BFS(Node node)
    {
        //목적지가 현재 Node일 경우
        if (node == currentNode)
        {
            return node;
        }


        List<Node> visitedNodes = new List<Node>(); //방문했던 Node들
        List<Node> queue = new List<Node>(); //queue 사용 위한 List

        visitedNodes.Add(node); //방문했던 Node에 목표 Node 추가
        //목표 Node와 연결된 Node들에 대해
        foreach(Node nd in node.connected)
        {
            //목표 Node가 현재 Node와 연결되어 있다면
            if (nd == currentNode)
            {
                return node; //바로 목표 Node return
            }

            visitedNodes.Add(nd); //연결된 Node들도 미리 방문 처리
            queue.Add(nd); //아니라면, queue에 연결된 Node들 추가
        }

        //queue의 길이가 0보다 클 경우
        while (true)
        {
            List<Node> subList = new List<Node>(); 

            while(queue.Count > 0)
            {
                Node nextNode = queue[0]; //queue의 첫 번째 원소 가져오기
                queue.Remove(queue[0]); //queue의 첫 번째 원소를 리스트에서 제거
                

                foreach(Node nd in nextNode.connected)
                {
                    //nextNode와 연결된 Node 중에 현재 적이 위치한 Node가 존재한다면
                    if (nd == currentNode)
                    {
                        //nextNode를 return하기
                        return nextNode;
                    }

                    //nextNode와 연결된 nd들 중 방문한 적 없는 Node들이 있다면
                    if (!visitedNodes.Contains(nd))
                    {
                        visitedNodes.Add(nd); //nextNode와 연결된 Node들도 미리 방문처리
                        subList.Add(nd); //subList에 nd 추가
                    }

                }//foreach


            }//while queue.Count > 0
              

            foreach (Node nd in subList)
            {
                queue.Add(nd);
            }

            //만약 queue의 길이가 0이라면, Node 연결리스트에 문제가 있다는 것.
            if (queue.Count == 0)
            {
                Debug.LogWarning("Node 연결 리스트에 오류 있음");
                break;
            }


        }//while true

        return null; //null이 반환되면 함수나 Node 연결리스트에 문제가 있다는 의미

    }//BFS

    //실제로 적을 이동시키는 함수
    public void movement(Node node)
    {
        //목적지가 현재 Node일 경우
        if (node == currentNode)
        {
            return;
        }
        
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

