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
    //우선 MapManager에서, 우주선의 종류 값을 저장할 변수를 만든다.
    //그 후, Awake 함수를 통해 PlayerInfo GameObject에 그 우주선에 대한 정보가 담긴 컴포넌트를 추가.
    //이 정보에는 그 우주선의 각종 능력치 및 스킬에 대한 정보가 담겨 있다.
    //(이를 위해서는 각 우주선의 능력치 및 스킬 정보를 기본적으로 가지고 있는 스크립트가 필요하다.)
    //(이후에 임무 시작 전 우주선의 능력치 변화를 가져와야 한다는 것을 고려하면, 어딘가에서 또 가져와야 함)
    // A ShipSkill : ShipSkill 의 형태로 각 우주선마다 스킬 클래스를 만들어서 관리?
    //각 우주선의 스킬 클래스에는, 맵 스킬의 개수와 쿨타임, 각 스킬의 특징과 동작에 대해 서술



    public class MapManager : MonoBehaviour
    {

         
        //MapManager Class 생성

        public int turnCount; //현재 Turn 수를 저장하는 변수
        public Turn turn; //현재 Turn 상태를 저장할 변수
        public int moveChance; //Player의 움직임 여부를 확인하는 bool 변수
        public int defaultMoveChance; // Player의 기본 이동 횟수
        public bool abilityChance; //Player의 기술 사용 여부를 확인하는 bool 변수

        public static MapManager instance;
        public List<Node> enemyNodeList; //Enemy가 위치한 Node를 저장할 List
        public Node playerNode; //PlayerNode의 정보를 저장할 변수 

        [HideInInspector]
        public GameObject Nodes; //Nodes GameObject를 저장할 변수

        
        public List<GameObject> enemyPrefabs; //Enemy를 Map에 표시하는 

        public GameObject playerInfo; //PlayerInfo를 저장할 변수

        [HideInInspector]
        public PlayerSkill pSkill; //PlayerSkill 저장할 변수

        public Image Img; //Image를 저장할 변수
        public TMP_Text ButtonText; //Button의 Text 를 저장할 변수

        

        //싱글턴 패턴 구현
        //Awakw 함수는 첫 생성 시 단 한번만 실행됨, 씬 이동을 해도 반복실행되지 않음
        private void Awake()
        {
            

            instance = this;

            Nodes = GameObject.Find("Nodes");

            turn = Turn.Player; //시작 시 Player turn으로 설정
            defaultMoveChance = 1; //Player의 기본 이동 횟수를 1로 설정. 이후에 우주선의 정보를 받아서 적용할 수 있도록 해야 함
            moveChance = defaultMoveChance; //이동 가능 횟수를 1로 설정,  
            abilityChance = true; //기술 사용 가능을 true로 설정.

            //최초에만 첫 번째 Node를 Player이 위치한 Node로 설정함.
            playerNode = Nodes.transform.GetChild(0).GetComponent<Node>();
            playerNode.nodeType = NodeType.Player;
            playerNode.setColor();
             
            DontDestroyOnLoad(this); //MapManager이 씬 변경에도 유지되게 함
            DontDestroyOnLoad(GameObject.Find("Map")); //Map이 씬 변경에도 유지되게 함.



            //임의로 적이 11번 노드에 있다고 가정
            enemyNodeList.Add(Nodes.transform.GetChild(10).GetComponent<Node>()); //11번 Node 저장
            var enemy = Instantiate(enemyPrefabs[0], enemyNodeList[0].transform); //11번 Node를 부모로 해서 생성

            //적의 초기 위치의 z좌표를 1f로 해, Node에 가려져서 안보이게 함, Node들의 z좌표는 0
            enemy.transform.localPosition = new Vector3(0f, 0f, 1f);
            enemyNodeList[0].enemyObjects.Add(enemy); //Node의 적 List에 enemy 추가 

            pSkill = playerInfo.GetComponent<PlayerSkill>(); //PlayerInfo에서 PlayerSkill 가져오기


            //우주선의 정보를 가져와서, 그 우주선의 스킬을 pSkill에 저장하는 코드 필요.


        }


         
        //Start 함수는 씬 이동 시 반복 실행되는 함수.
        private void Start()
        {
            if (turn == Turn.Engage)
            {
                turn = Turn.Enemy;
                checkTurn();

            }


        }

        //종료 시 실행될 함수
        private void OnApplicationQuit()
        {

        }



        //이 아래는 Turn과 관련된 함수들 

        //현재 Turn의 상태에 따른 행동을 서술한 함수
        public void checkTurn()
        {
           

            //현재 Turn이 무엇인지 확인
            switch ( turn)
            {

                //Player Turn이라면
                case Turn.Player:
                    turnCount++;
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


            //적 AI 로직 실행을 위한 코드를 작성해야 함.

            Debug.Log("Enemy Turn End");
            //1초 후에 코드 실행하기
            yield return new WaitForSecondsRealtime(1f);


            //Player 노드에 적이 존재한다면
            if (meetEnemy())
            {
                //바로 Engage 턴으로 변경
                 turn = Turn.Engage;
                checkTurn();
            }

            else
            {
                //Player 턴으로 변경
                 turn = Turn.Player;
                moveChance = defaultMoveChance;
                checkTurn();
            }

        }//EnemyTurn 코루틴


        //턴 종료 버튼을 눌렀을 때 턴이 변경되는 설정
        public void turnEnd()
        {
            //Player 의 턴이라면
            if ( turn == Turn.Player)
            {
                //Player 노드에 적이 존재한다면
                if (meetEnemy())
                {
                    //바로 Engage 턴으로 변경
                    turn = Turn.Engage;
                    checkTurn(); //Scene 변경
                }

                else
                {
                    //Enemy의 턴으로 변경
                    turn = Turn.Enemy;
                    checkTurn();
                }

            }

            //Engage 턴이라면
            else if (turn == Turn.Engage)
            {
                changeScene(); //Scene 변경
            }


        }//turnEnd

        //현 Node에 적 여부를 확인하는 함수. 자주 사용하는 코드라 함수로 만듦
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

