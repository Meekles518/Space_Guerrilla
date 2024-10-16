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
        public static MapManager instance;

        //TurnManager 가져오기
        public TurnManager turnManager;

        //Node들에 사용되는 변수
        public List<Node> enemyNodeList; //Enemy가 위치한 Node를 저장할 List
        public Node playerNode; //PlayerNode의 정보를 저장할 변수 


        
        [HideInInspector]
        public GameObject Nodes; //Nodes GameObject를 저장할 변수
        [HideInInspector]
        public GameObject Map;
        
        public List<GameObject> enemyPrefabs; //Enemy를 Map에 표시하는 


        //우주선에 사용되는 수치들을 관리하기 위해 컴포넌트들을 저장할 변수들
        public GameObject playerInfo; //PlayerInfo를 저장할 변수
   
        public PlayerInfo playerShipInfo; //PlayerInfo의 값들을 깊은 복사해 저장할 변수
        public PlayerBulletInfo playerBulletInfo; //PlaterBulletInfo의 값들을 깊은 복사해 저장할 변수
        public List<float> skillMaxCooltime = new List<float>(); //스킬들의 최대 쿨타임을 저장할 변수
        public List<float> skillCurCooltime = new List<float>(); //스킬들의 현재 쿨타임 상태 저장 변수


        //게임 시작 화면에서 Player의 우주선을 가져와 저장할 변수
        //나중에는 게임 시작 화면에서 우주선 GameObject를 가져와야 하며, 현재는 임시로 insepctor 창에서 설정
        public GameObject playerShip; 
   
        public ShipName shipName; //ShipName 저장 변수

        private bool awakeCheck = false; //Map의 초기 설정이 한 번만 이루어지도록 관리하는 변수

        //싱글턴 패턴 구현
        //Awake 함수와 Start 함수의 원리에 대한 고찰이 필요?
        //Awake 함수가 Start 함수보다 먼저 시작되는 것은 맞으나, DontDestroyAndLoad 함수로 생성한 오브젝트는
        //씬을 여러 번 이동 시, Awake 함수만 실행되고, Start 함수는 실행되지 않는다.
        private void Awake()
        {
            if (!awakeCheck)
            {

                instance = this;

                //TurnManager 가져오기
                turnManager = this.gameObject.GetComponent<TurnManager>();

                Nodes = GameObject.Find("Nodes");


                //최초에만 첫 번째 Node를 Player이 위치한 Node로 설정함.
                playerNode = Nodes.transform.GetChild(0).GetComponent<Node>();
                playerNode.nodeType = NodeType.Player;
                playerNode.setColor();

                DontDestroyOnLoad(this); //MapManager이 씬 변경에도 유지되게 함
                Map = GameObject.Find("Map");
                DontDestroyOnLoad(Map); //Map이 씬 변경에도 유지되게 함.


                //이 아래의 모든 코드들은, 실행되는 Map이 무슨 종류인지에 따라 switch문으로 구분해야 함
                //임의로 적이 11번 노드에 있다고 가정
                enemyNodeList.Add(Nodes.transform.GetChild(10).GetComponent<Node>()); //11번 Node 저장
                var enemy = Instantiate(enemyPrefabs[0], enemyNodeList[0].transform); //11번 Node를 부모로 해서 생성

                //적의 초기 위치의 z좌표를 1f로 해, Node에 가려져서 안보이게 함, Node들의 z좌표는 0
                enemy.transform.localPosition = new Vector3(0f, 0f, 1f);
                enemyNodeList[0].enemyObjects.Add(enemy); //Node의 적 List에 enemy 추가 


                //우주선의 정보를 가져와서, 그 우주선의 스킬을 pSkill에 저장하는 코드 필요.
                //임시로 우주선 이름 지정, 시작 화면에서 우주선 가져오는 코드로 대체해야 함.
                shipName = ShipName.Aegis;

                //맨 처음 Map 에 들어왔을 때, 초기 스탯 설정 코드 구문
                //스탯 설정에 필요한 컴포넌트들 가져오기
                
                playerShipInfo = playerInfo.GetComponent<PlayerInfo>();
                playerBulletInfo = playerInfo.GetComponent<PlayerBulletInfo>();


                //스탯 부여가 여기서 필요한 과정인건가?
                //스탯을 우주선 오브젝트에 직접 부여하는 것이 아니라, PlayerInfo 상에 수치로 적어두고,
                //패시브나 연구 주문 등의 효과는 PlayerInfo 상의 수치를 개변하는 것으로 구현.
                //Field 불러올 때 우주선을 생성하고 그 우주선에 PlayerInfo의 정보를 저장한 다음,
                //각종 액티브 스킬 사용 시 우주선의 순간적 정보만 개변시켜서 필드에서만 적용되게 하자.


                //혹시 몰라 일단 switch 구문 설정
                switch (shipName)
                {
                    case ShipName.Aegis:


                        break;

                }

 


                awakeCheck = true;
            }


        }//Awake



        //MapManager OnEnable시, 일반적으로 필드에서 맵으로 이동 시 실행
        private void OnEnable()
        {
           


        }//OnEnable

        //종료 시 실행될 함수
        private void OnApplicationQuit()
        {

        }



    }
}

