using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



namespace Map
{
    public class MapManager : MonoBehaviour
    {
        //MapManager Class 생성

        public Map CurMap; //현재 Map을 저장할 변수

      

        [SerializeField]
        public List<NodeSprite> nodeSprites; //NodeSprite 들을 저장할 List 
        public static MapManager instance;
        public List<Node> nodeList;
        public Node playerNode; //PlayerNode의 정보를 저장할 변수 


        //싱글턴 패턴 구현
        private void Awake()
        {
            instance = this;

        }

        //시작 시 실행될 함수, 첫 번째 노드를 PlayerNode로 설정해야 함.
        private void Start()
        {

            //첫 번째 Node를 PlayerNode로 설정, 첫 번째 Node의 Node 컴포넌트 저장하기
            playerNode = GameObject.Find("Nodes").transform.GetChild(0).GetComponent<Node>();
            playerNode.nodeType = NodeType.Player; 
            playerNode.setColor();
            playerNode.attainableState();

        }

        //종료 시 실행될 함수
        private void OnApplicationQuit()
        {
            
        }


    }


}

