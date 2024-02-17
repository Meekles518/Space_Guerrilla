using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Map
{
    //MapGenerator Class ����
    //Map�� ���� �� ȭ�鿡 ���µ��� �ʿ��� ��� �Լ� ����
    public class MapGenerator : MonoBehaviour
    {

        public GameObject topParent; //Map ���� �� ��� ���� �θ� �� GameObject ����
        public GameObject mapParent; //Map�� ���� GameObject ����
        public Camera cam; //Camera ����
        public Map map; //Map�� ������ ����
        public List<Node> mapNodes = new List<Node>(); //MapNode�� ������ List

        [Tooltip("Node�� Prefeb")]
        [SerializeField]
        public GameObject nodePrefeb; //Node�� Prefeb�� ������ ����
        [Tooltip("���ἱ�� Prefab")]
        public GameObject linePrefab; //Line�� Prefab�� ������ ����
        [Tooltip("Node visiting color")]
        public Color32 visitingColor = Color.white; //�湮 ���� ����� ��
        [Tooltip("Locked node color")]
        public Color32 lockedColor = Color.gray; //��� ����� ��
        [Tooltip("Unavailable path color")]
        public Color32 lineLockedColor = Color.gray; //��� ������ ��
        [Tooltip("offset From Node to Line")]
        public float offsetFromNodes = 0.5f; //���� ���ἱ ������ �Ÿ�



        //nodeList�� �ִ� Node�� ���� ������ ���� Map ��ü�� �����ִ� �Լ�
        public Map GetMap(NodeList nL)
        {

            int nodeListLength = nL.nodeList.Count; //node�� ��
            int connectionListLength = nL.connectionList.Count; //���� ������ ��
             


            //node�� ���� ���� ������ ���� �ٸ��ٸ�, NodeList�� ������ �ִ� ��
            if (nodeListLength != connectionListLength || nodeListLength == 0)
            {
                Debug.LogWarning("NodeList Error, �˼� ����� ����?");
                return null;
            }

            List<Node> nodes = new List<Node>(); //node���� ���� List

            //node�� �� ��ŭ �ݺ��� ����
            for (int i = 0; i < nodeListLength; i++)
            {

                //connectionList[i]�� ó������ ������ foreach�� ����
                foreach(var connectionNode in nL.connectionList[i])
                {
                    //�� Node���� connected List�� ��� ���� ����
                    nL.nodeList[i].AddConnections(nL.nodeList[connectionNode - 1]);
                }

                nodes.Add(nL.nodeList[i]); //nodes �� Node �߰�

            }

             
            return new Map(nodes); //���� ������ Map ��ü return

        }//GetMap


        public void ShowMap(Map m)
        {

            if (m == null)
            {
                Debug.LogWarning("Map is null, �˼� ����� ����?");
                return;
            }

            map = m;
            
            CreateTopParent();

            CreateNodes(m.nodes);

        }//ShowMap


        //topParent�� �����ϰ�, ���õ� UI�� �����ϴ� �Լ�(����)
        public void CreateTopParent()
        {
            topParent = new GameObject("TopParent");
            mapParent = new GameObject("MapParent");
            mapParent.transform.SetParent(topParent.transform);


        }//CreateMapParent


        //Map�� node�鿡 ���� �����ϴ� �Լ�
        public void CreateNodes(List<Node> nodes)
        {
            foreach (var node in nodes)
            {
                var mapNode = CreateMapNode(node);
                mapNodes.Add(mapNode);
            }



        }//CreateNodes


        //Node�� Instantiate �ϴ� �Լ�
        public Node CreateMapNode(Node nd)
        {
            var mapNodeObject = Instantiate(nodePrefeb, topParent.transform);
            Node mapNode = mapNodeObject.GetComponent<Node>(); //Node ������Ʈ ��������
            mapNode.addInfo(nd); // nd�� ������ mapNode�� �ű��
            mapNode.transform.localPosition = mapNode.position; //mapNode�� ���� ��ġ ����

            mapNode.setUp(); // mapNode�� sprite ���� 

            return mapNode;

        }//CreateMapNode


        //mapNodes�� �� Node�� ������, �� Node�� connected ����Ʈ�� Node�� ���ἱ �׸��� �Լ�
        public void DrawLines()
        {
            //mapNodes�� �� ����, Node�� ���� ����
            foreach (var node in mapNodes)
            {
                //�� node�� ����Ǿ� �ִ� connected�� ����
                foreach (var connection in node.connected)
                {
                    AddLine(node, connection);
                }
            }
        }//DrawLines


        //fromNode�� toNode�� ���ἱ�� �׸��� �Լ�
        public void AddLine(Node fromNode, Node toNode)
        {
            if (linePrefab == null)
            {
                Debug.LogWarning("linePrefab ���� �ȵ�");
                return;
            }

            var lineObject = Instantiate(linePrefab, topParent.transform); //linePrefab ����
             

            var fromPoint = fromNode.transform.position +
                (toNode.transform.position - fromNode.transform.position).normalized * offsetFromNodes;

            var toPoint = toNode.transform.position +
                (fromNode.transform.position - toNode.transform.position).normalized * offsetFromNodes;



        }//AddLine


    }

}
 
