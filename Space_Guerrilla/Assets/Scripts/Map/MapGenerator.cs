using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Map
{
    //MapGenerator Class 정의
    //Map을 생성 및 화면에 띄우는데에 필요한 모든 함수 포함
    public class MapGenerator : MonoBehaviour
    {

        public GameObject topParent; //Map 생성 시 모든 것의 부모가 될 GameObject 변수
        public GameObject mapParent; //Map을 담을 GameObject 변수
        public Camera cam; //Camera 변수
        public Map map; //Map을 저장할 변수
        public List<Node> mapNodes = new List<Node>(); //MapNode를 저장할 List

        [Tooltip("Node의 Prefeb")]
        [SerializeField]
        public GameObject nodePrefeb; //Node의 Prefeb을 저장할 변수
        [Tooltip("연결선의 Prefab")]
        public GameObject linePrefab; //Line의 Prefab을 저장할 변수
        [Tooltip("Node visiting color")]
        public Color32 visitingColor = Color.white; //방문 중인 노드의 색
        [Tooltip("Locked node color")]
        public Color32 lockedColor = Color.gray; //잠긴 노드의 색
        [Tooltip("Unavailable path color")]
        public Color32 lineLockedColor = Color.gray; //잠긴 간선의 색
        [Tooltip("offset From Node to Line")]
        public float offsetFromNodes = 0.5f; //노드와 연결선 사이의 거리



        //nodeList에 있는 Node와 간선 정보를 통해 Map 객체를 돌려주는 함수
        public Map GetMap(NodeList nL)
        {

            int nodeListLength = nL.nodeList.Count; //node의 수
            int connectionListLength = nL.connectionList.Count; //간선 정보의 수
             


            //node의 수와 간선 정보의 수가 다르다면, NodeList에 문제가 있는 것
            if (nodeListLength != connectionListLength || nodeListLength == 0)
            {
                Debug.LogWarning("NodeList Error, 검수 제대로 안해?");
                return null;
            }

            List<Node> nodes = new List<Node>(); //node들을 담을 List

            //node의 수 만큼 반복문 실행
            for (int i = 0; i < nodeListLength; i++)
            {

                //connectionList[i]의 처음부터 끝까지 foreach로 접근
                foreach(var connectionNode in nL.connectionList[i])
                {
                    //각 Node들의 connected List에 노드 정보 저장
                    nL.nodeList[i].AddConnections(nL.nodeList[connectionNode - 1]);
                }

                nodes.Add(nL.nodeList[i]); //nodes 에 Node 추가

            }

             
            return new Map(nodes); //새로 생성한 Map 객체 return

        }//GetMap


        public void ShowMap(Map m)
        {

            if (m == null)
            {
                Debug.LogWarning("Map is null, 검수 제대로 안해?");
                return;
            }

            map = m;
            
            CreateTopParent();

            CreateNodes(m.nodes);

        }//ShowMap


        //topParent를 생성하고, 관련된 UI를 설정하는 함수(예정)
        public void CreateTopParent()
        {
            topParent = new GameObject("TopParent");
            mapParent = new GameObject("MapParent");
            mapParent.transform.SetParent(topParent.transform);


        }//CreateMapParent


        //Map의 node들에 대해 접근하는 함수
        public void CreateNodes(List<Node> nodes)
        {
            foreach (var node in nodes)
            {
                var mapNode = CreateMapNode(node);
                mapNodes.Add(mapNode);
            }



        }//CreateNodes


        //Node를 Instantiate 하는 함수
        public Node CreateMapNode(Node nd)
        {
            var mapNodeObject = Instantiate(nodePrefeb, topParent.transform);
            Node mapNode = mapNodeObject.GetComponent<Node>(); //Node 컴포넌트 가져오기
            mapNode.addInfo(nd); // nd의 정보를 mapNode에 옮기기
            mapNode.transform.localPosition = mapNode.position; //mapNode의 실제 위치 조정

            mapNode.setUp(); // mapNode의 sprite 설정 

            return mapNode;

        }//CreateMapNode


        //mapNodes의 각 Node에 접근해, 각 Node의 connected 리스트의 Node와 연결선 그리는 함수
        public void DrawLines()
        {
            //mapNodes의 각 원소, Node에 대해 접근
            foreach (var node in mapNodes)
            {
                //각 node에 저장되어 있는 connected에 접근
                foreach (var connection in node.connected)
                {
                    AddLine(node, connection);
                }
            }
        }//DrawLines


        //fromNode와 toNode를 연결선을 그리는 함수
        public void AddLine(Node fromNode, Node toNode)
        {
            if (linePrefab == null)
            {
                Debug.LogWarning("linePrefab 설정 안됨");
                return;
            }

            var lineObject = Instantiate(linePrefab, topParent.transform); //linePrefab 생성
             

            var fromPoint = fromNode.transform.position +
                (toNode.transform.position - fromNode.transform.position).normalized * offsetFromNodes;

            var toPoint = toNode.transform.position +
                (fromNode.transform.position - toNode.transform.position).normalized * offsetFromNodes;



        }//AddLine


    }

}
 
