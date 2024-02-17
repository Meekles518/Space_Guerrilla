using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



namespace Map
{
    public class MapManager : MonoBehaviour
    {
        //MapManager Class 생성






        public static MapManager instance;
        public List<Node> enemyNodeList; //Enemy가 위치한 Node를 저장할 List
        public Node playerNode; //PlayerNode의 정보를 저장할 변수 

        public GameObject Nodes; //Nodes GameObject를 저장할 변수

        [Tooltip("Enemy circle Prefabs, will appear at Map")]
        public List<GameObject> enemyPrefabs; //Enemy를 Map에 표시하는 




        //싱글턴 패턴 구현
        //Awakw 함수는 첫 생성 시 단 한번만 실행됨, 씬 이동을 해도 반복실행되지 않음
        private void Awake()
        {
            instance = this;

            Nodes = GameObject.Find("Nodes");

            //최초에만 첫 번째 Node를 Player이 위치한 Node로 설정함.
            playerNode = Nodes.transform.GetChild(0).GetComponent<Node>();
            playerNode.nodeType = NodeType.Player;
            playerNode.setColor();
            playerNode.attainableState();
            DontDestroyOnLoad(this); //MapManager이 씬 변경에도 유지되게 함
            DontDestroyOnLoad(GameObject.Find("Map")); //Map이 씬 변경에도 유지되게 함.



            //임의로 적이 11번 노드에 있다고 가정
            enemyNodeList.Add(Nodes.transform.GetChild(10).GetComponent<Node>()); //11번 Node 저장
            var enemy = Instantiate(enemyPrefabs[0], enemyNodeList[0].transform); //11번 Node를 부모로 해서 생성
            //적의 초기 위치의 z좌표를 1f로 해, Node에 가려져서 안보이게 함, Node들의 z좌표는 0
            enemy.transform.localPosition = new Vector3(0f, 0f, 1f);
            enemyNodeList[0].enemyObjects.Add(enemy); //Node의 적 List에 enemy 추가 

        }


        //시작 시 실행될 함수, 첫 번째 노드를 PlayerNode로 설정해야 함.
        //Start 함수는 씬 이동 시 반복 실행되는 함수.
        private void Start()
        {



        }

        //종료 시 실행될 함수
        private void OnApplicationQuit()
        {

        }


    }


}

