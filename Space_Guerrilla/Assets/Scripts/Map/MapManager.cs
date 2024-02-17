using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



namespace Map
{
    public class MapManager : MonoBehaviour
    {
        //MapManager Class ����

        public Map CurMap; //���� Map�� ������ ����

      

        [SerializeField]
        public List<NodeSprite> nodeSprites; //NodeSprite ���� ������ List 
        public static MapManager instance;
        public List<Node> nodeList;
        public Node playerNode; //PlayerNode�� ������ ������ ���� 


        //�̱��� ���� ����
        private void Awake()
        {
            instance = this;

        }

        //���� �� ����� �Լ�, ù ��° ��带 PlayerNode�� �����ؾ� ��.
        private void Start()
        {

            //ù ��° Node�� PlayerNode�� ����, ù ��° Node�� Node ������Ʈ �����ϱ�
            playerNode = GameObject.Find("Nodes").transform.GetChild(0).GetComponent<Node>();
            playerNode.nodeType = NodeType.Player; 
            playerNode.setColor();
            playerNode.attainableState();

        }

        //���� �� ����� �Լ�
        private void OnApplicationQuit()
        {
            
        }


    }


}

