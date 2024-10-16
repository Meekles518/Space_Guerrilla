using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Skill;

namespace Map
{
    //�켱 MapManager����, ���ּ��� ���� ���� ������ ������ �����.
    //�� ��, Awake �Լ��� ���� PlayerInfo GameObject�� �� ���ּ��� ���� ������ ��� ������Ʈ�� �߰�.
    //�� �������� �� ���ּ��� ���� �ɷ�ġ �� ��ų�� ���� ������ ��� �ִ�.
    //(�̸� ���ؼ��� �� ���ּ��� �ɷ�ġ �� ��ų ������ �⺻������ ������ �ִ� ��ũ��Ʈ�� �ʿ��ϴ�.)
    //(���Ŀ� �ӹ� ���� �� ���ּ��� �ɷ�ġ ��ȭ�� �����;� �Ѵٴ� ���� ����ϸ�, ��򰡿��� �� �����;� ��)
    // A ShipSkill : ShipSkill �� ���·� �� ���ּ����� ��ų Ŭ������ ���� ����?
    //�� ���ּ��� ��ų Ŭ��������, �� ��ų�� ������ ��Ÿ��, �� ��ų�� Ư¡�� ���ۿ� ���� ����



    public class MapManager : MonoBehaviour
    {
       
        //MapManager Class ����
        public static MapManager instance;

        //TurnManager ��������
        public TurnManager turnManager;

        //Node�鿡 ���Ǵ� ����
        public List<Node> enemyNodeList; //Enemy�� ��ġ�� Node�� ������ List
        public Node playerNode; //PlayerNode�� ������ ������ ���� 


        
        [HideInInspector]
        public GameObject Nodes; //Nodes GameObject�� ������ ����
        [HideInInspector]
        public GameObject Map;
        
        public List<GameObject> enemyPrefabs; //Enemy�� Map�� ǥ���ϴ� 


        //���ּ��� ���Ǵ� ��ġ���� �����ϱ� ���� ������Ʈ���� ������ ������
        public GameObject playerInfo; //PlayerInfo�� ������ ����
   
        public PlayerInfo playerShipInfo; //PlayerInfo�� ������ ���� ������ ������ ����
        public PlayerBulletInfo playerBulletInfo; //PlaterBulletInfo�� ������ ���� ������ ������ ����
        public List<float> skillMaxCooltime = new List<float>(); //��ų���� �ִ� ��Ÿ���� ������ ����
        public List<float> skillCurCooltime = new List<float>(); //��ų���� ���� ��Ÿ�� ���� ���� ����


        //���� ���� ȭ�鿡�� Player�� ���ּ��� ������ ������ ����
        //���߿��� ���� ���� ȭ�鿡�� ���ּ� GameObject�� �����;� �ϸ�, ����� �ӽ÷� insepctor â���� ����
        public GameObject playerShip; 
   
        public ShipName shipName; //ShipName ���� ����

        private bool awakeCheck = false; //Map�� �ʱ� ������ �� ���� �̷�������� �����ϴ� ����

        //�̱��� ���� ����
        //Awake �Լ��� Start �Լ��� ������ ���� ������ �ʿ�?
        //Awake �Լ��� Start �Լ����� ���� ���۵Ǵ� ���� ������, DontDestroyAndLoad �Լ��� ������ ������Ʈ��
        //���� ���� �� �̵� ��, Awake �Լ��� ����ǰ�, Start �Լ��� ������� �ʴ´�.
        private void Awake()
        {
            if (!awakeCheck)
            {

                instance = this;

                //TurnManager ��������
                turnManager = this.gameObject.GetComponent<TurnManager>();

                Nodes = GameObject.Find("Nodes");


                //���ʿ��� ù ��° Node�� Player�� ��ġ�� Node�� ������.
                playerNode = Nodes.transform.GetChild(0).GetComponent<Node>();
                playerNode.nodeType = NodeType.Player;
                playerNode.setColor();

                DontDestroyOnLoad(this); //MapManager�� �� ���濡�� �����ǰ� ��
                Map = GameObject.Find("Map");
                DontDestroyOnLoad(Map); //Map�� �� ���濡�� �����ǰ� ��.


                //�� �Ʒ��� ��� �ڵ����, ����Ǵ� Map�� ���� ���������� ���� switch������ �����ؾ� ��
                //���Ƿ� ���� 11�� ��忡 �ִٰ� ����
                enemyNodeList.Add(Nodes.transform.GetChild(10).GetComponent<Node>()); //11�� Node ����
                var enemy = Instantiate(enemyPrefabs[0], enemyNodeList[0].transform); //11�� Node�� �θ�� �ؼ� ����

                //���� �ʱ� ��ġ�� z��ǥ�� 1f�� ��, Node�� �������� �Ⱥ��̰� ��, Node���� z��ǥ�� 0
                enemy.transform.localPosition = new Vector3(0f, 0f, 1f);
                enemyNodeList[0].enemyObjects.Add(enemy); //Node�� �� List�� enemy �߰� 


                //���ּ��� ������ �����ͼ�, �� ���ּ��� ��ų�� pSkill�� �����ϴ� �ڵ� �ʿ�.
                //�ӽ÷� ���ּ� �̸� ����, ���� ȭ�鿡�� ���ּ� �������� �ڵ�� ��ü�ؾ� ��.
                shipName = ShipName.Aegis;

                //�� ó�� Map �� ������ ��, �ʱ� ���� ���� �ڵ� ����
                //���� ������ �ʿ��� ������Ʈ�� ��������
                
                playerShipInfo = playerInfo.GetComponent<PlayerInfo>();
                playerBulletInfo = playerInfo.GetComponent<PlayerBulletInfo>();


                //���� �ο��� ���⼭ �ʿ��� �����ΰǰ�?
                //������ ���ּ� ������Ʈ�� ���� �ο��ϴ� ���� �ƴ϶�, PlayerInfo �� ��ġ�� ����ΰ�,
                //�нú곪 ���� �ֹ� ���� ȿ���� PlayerInfo ���� ��ġ�� �����ϴ� ������ ����.
                //Field �ҷ��� �� ���ּ��� �����ϰ� �� ���ּ��� PlayerInfo�� ������ ������ ����,
                //���� ��Ƽ�� ��ų ��� �� ���ּ��� ������ ������ �������Ѽ� �ʵ忡���� ����ǰ� ����.


                //Ȥ�� ���� �ϴ� switch ���� ����
                switch (shipName)
                {
                    case ShipName.Aegis:


                        break;

                }

 


                awakeCheck = true;
            }


        }//Awake



        //MapManager OnEnable��, �Ϲ������� �ʵ忡�� ������ �̵� �� ����
        private void OnEnable()
        {
           


        }//OnEnable

        //���� �� ����� �Լ�
        private void OnApplicationQuit()
        {

        }



    }
}

