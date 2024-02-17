using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



namespace Map
{
    public class MapManager : MonoBehaviour
    {
        //MapManager Class ����






        public static MapManager instance;
        public List<Node> enemyNodeList; //Enemy�� ��ġ�� Node�� ������ List
        public Node playerNode; //PlayerNode�� ������ ������ ���� 

        public GameObject Nodes; //Nodes GameObject�� ������ ����

        [Tooltip("Enemy circle Prefabs, will appear at Map")]
        public List<GameObject> enemyPrefabs; //Enemy�� Map�� ǥ���ϴ� 




        //�̱��� ���� ����
        //Awakw �Լ��� ù ���� �� �� �ѹ��� �����, �� �̵��� �ص� �ݺ�������� ����
        private void Awake()
        {
            instance = this;

            Nodes = GameObject.Find("Nodes");

            //���ʿ��� ù ��° Node�� Player�� ��ġ�� Node�� ������.
            playerNode = Nodes.transform.GetChild(0).GetComponent<Node>();
            playerNode.nodeType = NodeType.Player;
            playerNode.setColor();
            playerNode.attainableState();
            DontDestroyOnLoad(this); //MapManager�� �� ���濡�� �����ǰ� ��
            DontDestroyOnLoad(GameObject.Find("Map")); //Map�� �� ���濡�� �����ǰ� ��.



            //���Ƿ� ���� 11�� ��忡 �ִٰ� ����
            enemyNodeList.Add(Nodes.transform.GetChild(10).GetComponent<Node>()); //11�� Node ����
            var enemy = Instantiate(enemyPrefabs[0], enemyNodeList[0].transform); //11�� Node�� �θ�� �ؼ� ����
            //���� �ʱ� ��ġ�� z��ǥ�� 1f�� ��, Node�� �������� �Ⱥ��̰� ��, Node���� z��ǥ�� 0
            enemy.transform.localPosition = new Vector3(0f, 0f, 1f);
            enemyNodeList[0].enemyObjects.Add(enemy); //Node�� �� List�� enemy �߰� 

        }


        //���� �� ����� �Լ�, ù ��° ��带 PlayerNode�� �����ؾ� ��.
        //Start �Լ��� �� �̵� �� �ݺ� ����Ǵ� �Լ�.
        private void Start()
        {



        }

        //���� �� ����� �Լ�
        private void OnApplicationQuit()
        {

        }


    }


}

