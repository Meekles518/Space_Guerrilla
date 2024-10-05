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
        public Turn turn; //���� Turn ���¸� ������ ����, ���� ���� ���� ���� �� GameManager���� �� ���� DimensionJump�� ����?
        public Turn lastTurn; //���� Turn ����(Player, enemy)�� ������ ����
        public Phase phase; //���� Phase�� ������ ����
        public int moveChance; //Player�� ������ ���θ� Ȯ���ϴ� bool ����
        public int defaultMoveChance; // Player�� �⺻ �̵� Ƚ��
        public bool abilityChance; //Player�� ��� ��� ���θ� Ȯ���ϴ� bool ����
        public bool playerDetected; //Player�� Ž���� ���θ� Ȯ���ϴ� bool ����
        

        public static MapManager instance;

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
      


        //UI �� �̹����� �ʿ��� ������
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
                lastTurn = Turn.Player; //���� �� ���� ���� Player�� ����
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
         
        //MapManager OnEnable��, ��κ��� ��Ȳ������ Field���� Map���� �Ѿ�� �� Ȱ��
        private void OnEnable()
        {
            //
            switch(turn)
            {
                //���� ���� ���� ��
                case Turn.Engage:

                    //���� ���� Player���ٸ�, �̹� ���� Enemy�� ������ ����
                    if (lastTurn == Turn.Player)
                    {
                        changeTurn(Turn.Enemy);
                    }

                    //���� ���� Enemy ���ٸ�, �̹� ���� Player ������ ����
                    else if (lastTurn == Turn.Enemy)
                    {
                        changeTurn(Turn.Player);
                    }
                    
                    checkTurn(); //�� �˻�

                    break;


                case Turn.DimensionJump:




                    break;


            }


        }//OnEnable

        //���� �� ����� �Լ�
        private void OnApplicationQuit()
        {

        }

        //�� �Ʒ��� Turn�� ���õ� �Լ��� 

        //���� Turn�� ���¿� ���� UI ���� �޼���
        public void checkTurn()
        {
            //���� Turn�� �������� Ȯ��
            switch (turn)
            {
                //Player Turn�̶��
                case Turn.Player:
                    turnCount++; //�� ī��Ʈ ����

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

                //���� ���� Turn�̶��
                case Turn.DimensionJump:


                    break;

            }

        }//checkTurn

        
        //���������� turn ������ �� ������ ���� turn�� �ٲٰ� �ʿ��� ������ �缳���ϴ� �޼���
        public void changeTurn(Turn nextTurn)
        {
            switch(nextTurn)
            {
                //Player ������ ����
                case Turn.Player:

                    //Player ������ ����
                    turn = Turn.Player;
                    lastTurn = Turn.Player; //���� ���� Player�� ����
                    repairAll(); //Player�� ��� Enemy ü�� ����
                    moveChance = defaultMoveChance; //�ӽ÷� �̵� ���� Ƚ�� �ʱ�ȭ

                    //�� �Ʒ��� Player�� ���� ����, �̵� ���� ���� ���� ������ �߰��ؾ� ��

                    break;

                //Enemy ������ ����
                case Turn.Enemy:

                    //Enemy�� ������ ����
                    turn = Turn.Enemy;
                    lastTurn = Turn.Enemy; //���� ���� Enemy�� ����
                    repairAll(); //Player�� ��� Enemy ü�� ����

                    break;

                //Engage ������ ����
                case Turn.Engage:

                    turn = Turn.Engage;

                    break;

                //���� ���� ������ ����
                case Turn.DimensionJump:

                    turn = Turn.DimensionJump;

                    //������ �̵��ϰԲ� ���� �� ���� �Ұ� ���� ����?

                    break;
            }
        }//changeTurn



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
                changeTurn(Turn.Engage);
            }

            else
            {
                //Player ������ ����
                changeTurn(Turn.Player);
            }

            checkTurn(); //�� �˻�

        }//EnemyTurn �ڷ�ƾ


        //�� ���� ��ư�� ������ �� ���� ����Ǵ� ����
        public void turnEnd()
        {

            switch(turn)
            {
                //Player Turn���� �� ���� ��ư ���� ��
                case Turn.Player:

                    //Player ��忡 ���� �����Ѵٸ�
                    if (meetEnemy())
                    {
                        //�ٷ� Engage ������ ����
                        changeTurn(Turn.Engage);
                    }

                    else
                    {
                        //Enemy ������ ����
                        changeTurn(Turn.Enemy);
                    }

                    checkTurn(); //�� �˻�

                    break;

                //Engage Turn���� �� ���� ��ư ���� ��
                case Turn.Engage:

                    changeScene(); //Scene ����

                    break;

                //���� ���� Turn���� �� ���� ��ư ���� ��
                case Turn.DimensionJump:

                    //���� ���� �Ŀ��� ������ �̵��ؾ� �ϹǷ�, �̵��ߴ��� ���θ� ������ ���� �ʿ�


                    //�� �Ʒ� �ڵ���� �̵� �Ŀ� ��ư�� ������ ���� ���� ����

                    //Player ��忡 ���� �����Ѵٸ�
                    if (meetEnemy())
                    {
                        //�ٷ� Engage ������ ����
                        changeTurn(Turn.Engage);
                    }

                    else
                    {
                        //���� ���� Player���ٸ�, �̹� ���� Enemy�� ������ ����
                        if (lastTurn == Turn.Player)
                        {
                            changeTurn(Turn.Enemy);
                        }

                        //���� ���� Enemy ���ٸ�, �̹� ���� Player ������ ����
                        else if (lastTurn == Turn.Enemy)
                        {
                            changeTurn(Turn.Player);
                        }

                       
                    }

                    checkTurn(); //�� �˻�

                    break;


            }



        }//turnEnd

        //�� Node�� �� ���θ� Ȯ���ϴ� �Լ�. ���� ����ϴ� �ڵ�� �Լ��� ����
        public bool meetEnemy()
        {
            if ( playerNode.enemyObjects.Count > 0)
            {
                return true;
            }

            return false;


        }//checkEnemy


        //Player�� ��� Enemy�� ü���� ������Ű�� �޼���
        public void repairAll()
        {

        }


    }


}

