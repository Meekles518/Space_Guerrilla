using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


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
        //MapSpawnManager 가져오기
        public MapSpawnManager mapSpawnManager;

        //Node들에 사용되는 변수
        public List<Node> enemyNodeList; //Enemy가 위치한 Node를 저장할 List
        public Node playerNode; //PlayerNode의 정보를 저장할 변수 


        [HideInInspector]
        public GameObject Nodes; //Nodes GameObject를 저장할 변수
        [HideInInspector]
        public GameObject Map;

        public List<GameObject> enemyPrefabs; //Enemy를 Map에 표시하는 



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

                instance = this; //싱글턴 패턴 구현

                //TurnManager 가져오기
                turnManager = this.gameObject.GetComponent<TurnManager>();

                //MapSpawnManager 가져오기
                mapSpawnManager = this.gameObject.GetComponent<MapSpawnManager>();

                Nodes = GameObject.Find("Nodes"); //Nodes GameObject를 찾아서 저장

                DontDestroyOnLoad(this); //MapManager이 씬 변경에도 유지되게 함
                Map = GameObject.Find("Map");
                DontDestroyOnLoad(Map); //Map이 씬 변경에도 유지되게 함.
          
                shipName = ShipName.Aegis;

                mapSpawnManager.init(); //MapSpawnManager의 초기 설정 실행




                awakeCheck = true;
            }//if


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

