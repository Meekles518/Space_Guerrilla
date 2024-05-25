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

        public int turnCount; //���� Turn ���� �����ϴ� ����
        public Turn turn; //���� Turn ���¸� ������ ����
        public Phase phase; //���� Phase�� ������ ����
        public int moveChance; //Player�� ������ ���θ� Ȯ���ϴ� bool ����
        public int defaultMoveChance; // Player�� �⺻ �̵� Ƚ��
        public bool abilityChance; //Player�� ��� ��� ���θ� Ȯ���ϴ� bool ����
        public bool playerDetected; //Player�� Ž���� ���θ� Ȯ���ϴ� bool ����

        public static MapManager instance;

        public List<Node> enemyNodeList; //Enemy�� ��ġ�� Node�� ������ List
        public Node playerNode; //PlayerNode�� ������ ������ ���� 

        [HideInInspector]
        public GameObject Nodes; //Nodes GameObject�� ������ ����
        [HideInInspector]
        public GameObject Map;
        
        public List<GameObject> enemyPrefabs; //Enemy�� Map�� ǥ���ϴ� 

        public GameObject playerInfo; //PlayerInfo�� ������ ����

        [HideInInspector]
        public PlayerSkill pSkill; //PlayerSkill ������ ����

        public Image Img; //Image�� ������ ����
        public TMP_Text ButtonText; //Button�� Text �� ������ ����

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

                Nodes = GameObject.Find("Nodes");

                turn = Turn.Player; //���� �� Player turn���� ����
                phase = Phase.Default; //���� �� Default Phase�� ����



                abilityChance = true; //��� ��� ������ true�� ����.
                playerDetected = false;

                //���ʿ��� ù ��° Node�� Player�� ��ġ�� Node�� ������.
                playerNode = Nodes.transform.GetChild(0).GetComponent<Node>();
                playerNode.nodeType = NodeType.Player;
                playerNode.setColor();

                DontDestroyOnLoad(this); //MapManager�� �� ���濡�� �����ǰ� ��
                Map = GameObject.Find("Map");
                DontDestroyOnLoad(Map); //Map�� �� ���濡�� �����ǰ� ��.



                //�� �Ʒ��� 2�� ������ �ܺο��� ������ ���ּ��� ������ �����;� ��
                defaultMoveChance = 1; //Player�� �⺻ �̵� Ƚ���� 1�� ����. ���Ŀ� ���ּ��� ������ �޾Ƽ� ������ �� �ֵ��� �ؾ� ��
                moveChance = defaultMoveChance; //�̵� ���� Ƚ���� 1�� ����, 


                //�� �Ʒ��� ��� �ڵ����, ����Ǵ� Map�� ���� ���������� ���� switch������ �����ؾ� ��
                //���Ƿ� ���� 11�� ��忡 �ִٰ� ����
                enemyNodeList.Add(Nodes.transform.GetChild(10).GetComponent<Node>()); //11�� Node ����
                var enemy = Instantiate(enemyPrefabs[0], enemyNodeList[0].transform); //11�� Node�� �θ�� �ؼ� ����

                //���� �ʱ� ��ġ�� z��ǥ�� 1f�� ��, Node�� �������� �Ⱥ��̰� ��, Node���� z��ǥ�� 0
                enemy.transform.localPosition = new Vector3(0f, 0f, 1f);
                enemyNodeList[0].enemyObjects.Add(enemy); //Node�� �� List�� enemy �߰� 

                pSkill = playerInfo.GetComponent<PlayerSkill>(); //PlayerInfo���� PlayerSkill ��������


                //���ּ��� ������ �����ͼ�, �� ���ּ��� ��ų�� pSkill�� �����ϴ� �ڵ� �ʿ�.
                //�ӽ÷� ���ּ��� Ship1���� ����
                shipName = ShipName.Ship1;
                pSkill.getSkill(); //Skill ���� �����ͼ� �����ϱ�
                pSkill.SetSkillBtn(); //Skill���� ��ư�� �߰� �� ���� �� ��ġ


                awakeCheck = true;
            }

             
        }


         
        //
        private void OnEnable()
        {
            if (turn == Turn.Engage)
            {
                turn = Turn.Enemy;
                checkTurn();

            }


        }

        //���� �� ����� �Լ�
        private void OnApplicationQuit()
        {

        }



        //�� �Ʒ��� Turn�� ���õ� �Լ��� 

        //���� Turn�� ���¿� ���� �ൿ�� ������ �Լ�
        public void checkTurn()
        {
           

            //���� Turn�� �������� Ȯ��
            switch ( turn)
            {

                //Player Turn�̶��
                case Turn.Player:
                    turnCount++; //�� ī��Ʈ ����

                    //Player�� �� ��ų ��Ÿ���� 1�� ���ҽ�Ű�� �۾� �ʿ�
                    for (int i = 0; i < pSkill.skillCurrentColltime.Count; i++)
                    {
                        pSkill.skillCurrentColltime[i] -= 1.0f;

                    }

                    Img.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);

                   
                    ButtonText.text = ("Turn End");
                  
                    Debug.Log("Player Turn");

                    break;

                //Enemy Turn �̶��
                case Turn.Enemy:
                    turnCount++;
                    Img.color = Color.gray;
                    
                    ButtonText.text = ("Turn End");
                   
                    Debug.Log("Enemy Turn");
                    StartCoroutine(EnemyTurn());

                    break;

                //Engage Turn�̶��
                case Turn.Engage:

                    Img.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                  
                    ButtonText.text = ("Engage!"); //Button�� Text ����
                 

                    break;

            }


        }//checkTurn



        //Scene�� �����ϴ� �Լ��� �ʿ�
        public void changeScene()
        {
            SceneManager.LoadScene("Field");

        }//fieldScene


        //Enemy Turn ���� ������ ������ ���� �ڷ�ƾ
        public IEnumerator EnemyTurn()
        {
            //1�� �Ŀ� �ڵ� �����ϱ�
            yield return new WaitForSecondsRealtime(1f);
            playerDetected = meetEnemy(); //���� Ž�� ���� Ȯ��.

            //���� �ִ� Node�鿡 ����
            foreach(Node node in enemyNodeList)
            {
                //Node�� ���鿡�� ����
                for(int i = 0; i < node.enemyObjects.Count; i++)
                {
                    //�̵���� �ǽ�
                    node.enemyObjects[i].GetComponent<Enemy_Circle>().enemyAi();
                }

            }

            //�� AI ���� ������ ���� �ڵ带 �ۼ��ؾ� ��.

            Debug.Log("Enemy Turn End");
            //1�� �Ŀ� �ڵ� �����ϱ�
            yield return new WaitForSecondsRealtime(1f);


            //Player ��忡 ���� �����Ѵٸ�
            if (meetEnemy())
            {
                //�ٷ� Engage ������ ����
                 turn = Turn.Engage;
                checkTurn();
            }

            else
            {
                //Player ������ ����
                 turn = Turn.Player;
                moveChance = defaultMoveChance;
                checkTurn();
            }

        }//EnemyTurn �ڷ�ƾ


        //�� ���� ��ư�� ������ �� ���� ����Ǵ� ����
        public void turnEnd()
        {
            //Player �� ���̶��
            if ( turn == Turn.Player)
            {
                //Player ��忡 ���� �����Ѵٸ�
                if (meetEnemy())
                {
                    //�ٷ� Engage ������ ����
                    turn = Turn.Engage;
                    checkTurn(); //Scene ����
                }

                else
                {
                    //Enemy�� ������ ����
                    turn = Turn.Enemy;
                    checkTurn();
                }

            }

            //Engage ���̶��
            else if (turn == Turn.Engage)
            {
                changeScene(); //Scene ����
            }


        }//turnEnd

        //�� Node�� �� ���θ� Ȯ���ϴ� �Լ�. ���� ����ϴ� �ڵ�� �Լ��� ����
        public bool meetEnemy()
        {
            if ( playerNode.enemyObjects.Count > 0)
            {
                return true;
            }

            else return false;


        }//checkEnemy


    }


}

