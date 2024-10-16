using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Map
{
    public class TurnManager : MonoBehaviour
    {

        public int turnCount; //���� Turn ���� �����ϴ� ����
        public Turn turn; //���� Turn ���¸� ������ ����
        public Turn lastTurn; //������ Turn ���¸� ������ ����
        public Phase phase; //���� Phase�� ������ ����
        public int moveChance; //Player�� ������ ���θ� Ȯ���ϴ� bool ����
        public int defaultMoveChance; // Player�� �⺻ �̵� Ƚ��
        public bool abilityChance; //Player�� ��� ��� ���θ� Ȯ���ϴ� bool ����
        public bool playerDetected; //Player�� Ž���� ���θ� Ȯ���ϴ� bool ����


        //UI �� �̹����� �ʿ��� ������
        public Image Img; //Image�� ������ ����
        public TMP_Text ButtonText; //Button�� Text �� ������ ����


        public ShipName shipName; //ShipName ���� ����

        private bool awakeCheck = false; //Map�� �ʱ� ������ �� ���� �̷�������� �����ϴ� ����

        private void Awake()
        {

            if (!awakeCheck)
            {
                turn = Turn.Player; //���� �� Player turn���� ����
                phase = Phase.Default; //���� �� Default Phase�� ����


                abilityChance = true; //��� ��� ������ true�� ����.
                playerDetected = false;


                //�� �Ʒ��� 2�� ������ �ܺο��� ������ ���ּ��� ������ �����;� ��
                defaultMoveChance = 1; //Player�� �⺻ �̵� Ƚ���� 1�� ����. ���Ŀ� ���ּ��� ������ �޾Ƽ� ������ �� �ֵ��� �ؾ� ��
                moveChance = defaultMoveChance; //�̵� ���� Ƚ���� 1�� ����,

              


            }

             


        }//Awake

        //�Ϲ������� �ʵ忡�� ������ �̵��� �� �����
        private void OnEnable()
        {
            switch (turn)
            {
                case Turn.Engage:

                    if (lastTurn == Turn.Player)
                    {
                        changeTurn(Turn.Enemy);
                    }

                    else if (lastTurn == Turn.Enemy)
                    {
                        changeTurn(Turn.Player);
                    }


                    break;



            }


        }//OnEnable

        //Turn�� �����ϴ� �޼���
        public void changeTurn(Turn nextTurn)
        {
            switch (nextTurn)
            {
                //Player Turn���� Turn ����
                case Turn.Player:

                    //Player
                    turn = Turn.Player; //Turn ���� �ʱ�ȭ
                    lastTurn = Turn.Player; //lastTurn ���� �ʱ�ȭ
                    repairAll(); //��ü ����
                    moveChance = defaultMoveChance; //�̵� Ƚ�� �ʱ�ȭ
                    //�� �Ʒ��� �߰��� Player Turn�� ���۵� �� �ʱ�ȭ�� �͵� �߰�?

                    //

                    break;

                //Enemy Turn���� Turn ����
                case Turn.Enemy:

                    //
                    turn = Turn.Enemy;  //turn ���� �ʱ�ȭ
                    lastTurn = Turn.Enemy; //lastTurn ���� �ʱ�ȭ
                    repairAll(); //��ü ����

                    break;

                //
                case Turn.Engage:

                    turn = Turn.Engage;//turn ������ �ʱ�ȭ

                    break;


            }
        }//changeTurn


        //���� Turn�� ���¿� ���� �ൿ�� ������ �Լ�
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
            foreach (Node node in MapManager.instance.enemyNodeList)
            {
                //Node�� ���鿡�� ����
                for (int i = 0; i < node.enemyObjects.Count; i++)
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

            checkTurn(); //Turn Ȯ��

        }//EnemyTurn �ڷ�ƾ

        //�� ���� ��ư�� ������ �� ���� ����Ǵ� ����
        public void turnEnd()
        {
            switch (turn)
            {
                //Player Turn�� ���� Turn End ��ư ������
                case Turn.Player:

                    if (meetEnemy())
                    {
                        changeTurn(Turn.Engage);
                    }

                    else
                    {
                        changeTurn(Turn.Enemy);
                    }

                    checkTurn();
                    break;

                //Engage Turn�� ���� Turn End ��ư ������
                case Turn.Engage:

                    changeScene();

                    break;


            }

        }//turnEnd



        //��� ���ּ� ü�� �ִ�� ȸ����Ű�� �޼���?
        public void repairAll()
        {

        }//repairAll


        //�� Node�� �� ���θ� Ȯ���ϴ� �Լ�. ���� ����ϴ� �ڵ�� �Լ��� ����
        public bool meetEnemy()
        {
            if (MapManager.instance.playerNode.enemyObjects.Count > 0)
            {
                return true;
            }

            else return false;


        }//checkEnemy




    }//class


}

 
