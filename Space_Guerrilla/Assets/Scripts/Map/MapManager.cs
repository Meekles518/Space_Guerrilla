using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



namespace Map
{
    public class MapManager : MonoBehaviour
    {
        //MapManager Class ����


        public Turn turn; //���� Turn ���¸� ������ ����
        public int moveChance; //Player�� ������ ���θ� Ȯ���ϴ� bool ����
        public int defaultMoveChance; // Player�� �⺻ �̵� Ƚ��

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

            turn = Turn.Player; //���� �� Player turn���� ����
            defaultMoveChance = 1; //Player�� �⺻ �̵� Ƚ���� 1�� ����.
            moveChance = defaultMoveChance; //�̵� ���� Ƚ���� 1�� ����, ���Ŀ� ���ּ��� ������ �޾Ƽ� ������ �� �ֵ��� �ؾ� ��

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

                    Debug.Log("Player Turn");

                    break;

                //Enemy Turn �̶��
                case Turn.Enemy:

                    Debug.Log("Enemy Turn");
                    StartCoroutine(EnemyTurn());

                    break;

                //Engage Turn�̶��
                case Turn.Engage:

                    changeScene();

                    break;

            }


        }//checkTurn



        //Scene�� �����ϴ� �Լ��� �ʿ�
        public void changeScene()
        {

        }//fieldScene


        //Enemy Turn ���� ������ ������ ���� �ڷ�ƾ
        public IEnumerator EnemyTurn()
        {
            //1�� �Ŀ� �ڵ� �����ϱ�
            yield return new WaitForSecondsRealtime(1f);


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

