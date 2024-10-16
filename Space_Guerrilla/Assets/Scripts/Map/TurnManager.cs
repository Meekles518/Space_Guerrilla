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

        public int turnCount; //현재 Turn 수를 저장하는 변수
        public Turn turn; //현재 Turn 상태를 저장할 변수
        public Turn lastTurn; //마지막 Turn 상태를 저장할 변수
        public Phase phase; //현재 Phase를 저장할 변수
        public int moveChance; //Player의 움직임 여부를 확인하는 bool 변수
        public int defaultMoveChance; // Player의 기본 이동 횟수
        public bool abilityChance; //Player의 기술 사용 여부를 확인하는 bool 변수
        public bool playerDetected; //Player의 탐지됨 여부를 확인하는 bool 변수


        //UI 및 이미지에 필요한 변수들
        public Image Img; //Image를 저장할 변수
        public TMP_Text ButtonText; //Button의 Text 를 저장할 변수


        public ShipName shipName; //ShipName 저장 변수

        private bool awakeCheck = false; //Map의 초기 설정이 한 번만 이루어지도록 관리하는 변수

        private void Awake()
        {

            if (!awakeCheck)
            {
                turn = Turn.Player; //시작 시 Player turn으로 설정
                phase = Phase.Default; //시작 시 Default Phase로 설정


                abilityChance = true; //기술 사용 가능을 true로 설정.
                playerDetected = false;


                //이 아래의 2개 값들은 외부에서 선택한 우주선의 정보를 가져와야 함
                defaultMoveChance = 1; //Player의 기본 이동 횟수를 1로 설정. 이후에 우주선의 정보를 받아서 적용할 수 있도록 해야 함
                moveChance = defaultMoveChance; //이동 가능 횟수를 1로 설정,

              


            }

             


        }//Awake

        //일반적으로 필드에서 맵으로 이동할 때 실행됨
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

        //Turn을 변경하는 메서드
        public void changeTurn(Turn nextTurn)
        {
            switch (nextTurn)
            {
                //Player Turn으로 Turn 변경
                case Turn.Player:

                    //Player
                    turn = Turn.Player; //Turn 변수 초기화
                    lastTurn = Turn.Player; //lastTurn 변수 초기화
                    repairAll(); //전체 수리
                    moveChance = defaultMoveChance; //이동 횟수 초기화
                    //이 아래에 추가로 Player Turn이 시작될 때 초기화될 것들 추가?

                    //

                    break;

                //Enemy Turn으로 Turn 변경
                case Turn.Enemy:

                    //
                    turn = Turn.Enemy;  //turn 변수 초기화
                    lastTurn = Turn.Enemy; //lastTurn 변수 초기화
                    repairAll(); //전체 수리

                    break;

                //
                case Turn.Engage:

                    turn = Turn.Engage;//turn 변수만 초기화

                    break;


            }
        }//changeTurn


        //현재 Turn의 상태에 따른 행동을 서술한 함수
        public void checkTurn()
        {


            //현재 Turn이 무엇인지 확인
            switch (turn)
            {

                //Player Turn이라면
                case Turn.Player:
                    turnCount++; //턴 카운트 증가



                    Img.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);


                    ButtonText.text = ("Turn End");

                    Debug.Log("Player Turn");

                    break;

                //Enemy Turn 이라면
                case Turn.Enemy:
                    turnCount++;
                    Img.color = Color.gray;

                    ButtonText.text = ("Turn End");

                    Debug.Log("Enemy Turn");
                    StartCoroutine(EnemyTurn());

                    break;

                //Engage Turn이라면
                case Turn.Engage:

                    Img.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);

                    ButtonText.text = ("Engage!"); //Button의 Text 변경


                    break;

            }


        }//checkTurn

        //Scene을 변경하는 함수가 필요
        public void changeScene()
        {
            SceneManager.LoadScene("Field");

        }//fieldScene


        //Enemy Turn 동안 실행할 로직을 담은 코루틴
        public IEnumerator EnemyTurn()
        {
            //1초 후에 코드 실행하기
            yield return new WaitForSecondsRealtime(1f);
            playerDetected = meetEnemy(); //적의 탐지 여부 확인.

            //적이 있는 Node들에 접근
            foreach (Node node in MapManager.instance.enemyNodeList)
            {
                //Node의 적들에게 접근
                for (int i = 0; i < node.enemyObjects.Count; i++)
                {
                    //이동명령 실시
                    node.enemyObjects[i].GetComponent<Enemy_Circle>().enemyAi();
                }

            }

            //적 AI 로직 실행을 위한 코드를 작성해야 함.

            Debug.Log("Enemy Turn End");
            //1초 후에 코드 실행하기
            yield return new WaitForSecondsRealtime(1f);


            //Player 노드에 적이 존재한다면
            if (meetEnemy())
            {
                //바로 Engage 턴으로 변경
                changeTurn(Turn.Engage);
            }

            else
            {
                //Player 턴으로 변경
                changeTurn(Turn.Player);
            }

            checkTurn(); //Turn 확인

        }//EnemyTurn 코루틴

        //턴 종료 버튼을 눌렀을 때 턴이 변경되는 설정
        public void turnEnd()
        {
            switch (turn)
            {
                //Player Turn일 때에 Turn End 버튼 누르면
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

                //Engage Turn일 때에 Turn End 버튼 누르면
                case Turn.Engage:

                    changeScene();

                    break;


            }

        }//turnEnd



        //모든 우주선 체력 최대로 회복시키는 메서드?
        public void repairAll()
        {

        }//repairAll


        //현 Node에 적 여부를 확인하는 함수. 자주 사용하는 코드라 함수로 만듦
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

 
