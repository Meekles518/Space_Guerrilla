using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



namespace Map
{
    //Node Class를 정의
    public class Node : MonoBehaviour
    {

        public List<Node> connected; //이 Node와 연결된 다른 Node의 좌표를 저장할 List
        public NodeType nodeType; //NodeType을 저장할 변수
          
        public float mouseOnScale = 1.3f; //마우스가 올라갔을 때 노드가 커질 비율을 저장할 변수
        public SpriteRenderer sR;
        public List<GameObject> enemyObjects; //Enemy Object의 정보를 저장할 List


        public float verticalDistance; //Map Node와 적을 표시하는 원의 세로 거리    
        public float horizontalDistance; //여러 적이 한 노드에 존재할 때, 적을 표시하는 원 끼리의 


        private void Awake()
        {
            mouseOnScale = 1.3f; //초기값 설정
            verticalDistance = -1f;  //초기 세로 거리 설정
            horizontalDistance = 0.5f; //초기 가로 거리 설정

        }


        //여기부터 실제로 사용하는 함수들

        //임시로 노드의 구별을 색깔로 진행하기 위해, 현 NodeType에 따라서 Node의 구분을 지을 수 있어야 함.
        public void setColor()
        {
            sR = GetComponent<SpriteRenderer>(); //SpriteRenderer 컴포넌트 가져오기

            switch (this.nodeType)
            {
                //NodeType이 Player일 경우
                case NodeType.Player:

                    //색깔을 초록색으로 변경?
                    sR.color = Color.green;

                    break;


                //NodeType이 Special 일 경우
                case NodeType.Special:

                    break;


                //NodeType이 Empty일 경우
                case NodeType.Empty:

                    //색을 하얀색으로 변경
                    sR.color = Color.white;

                    break;


                default:
                    break;


            }


        }//setColor

 


        public void OnMouseEnter() //마우스가 올라갔을 때 
        {
            transform.localScale = new Vector3(mouseOnScale, mouseOnScale, 1f); //Scale 1.3배 하기

        }//OnMouseEnter


        public void OnMouseExit()
        {
            transform.localScale = new Vector3(1f, 1f, 1f);//Scale 1배로 되돌리기
        }//OnMouseExit


        public void OnMouseUpAsButton() //한 오브젝트 위에서 마우스 다운과 업이 일어났을 때
        {
            //이동 가능 수가 존재한다면, 그리고 Player 턴이라면
            if (MapManager.instance.turn == Turn.Player && MapManager.instance.moveChance != 0)
            {
                //playerNode의 connected 리스트에, 클릭한 Node가 존재한다면
                if (MapManager.instance.playerNode.connected.Contains(this))
                {

                    playerMovement(this);

                    //턴 당 1번만 움직일 수 있다고 가정하고 코드 작성, 이후에 우주선의 설정에
                    //따라 그 값을 받아서 적용할 수 있도록 코드를 수정해야 함.
                    MapManager.instance.moveChance -= 1; //이동 횟수를 1 감소


                    //만약 이동 후 그 Node에 적이 있다면, 이동 기회를 0으로 강제 설정
                    if (MapManager.instance.meetEnemy())
                    {
                        MapManager.instance.moveChance = 0;
                        MapManager.instance.abilityChance = false;
                    }

                }

                else
                {
                    Debug.LogWarning("U cant move to that node");
                }
            }

            else
            {
                Debug.LogWarning("U cant move to that node");
            }

        }//OnMouseUpAsButton


        //Enemy의 유무를 표시할 함수 필요.
        public void checkEnemy()
        {
            drawEnemy(this);

            //인접한 Node에 적이 있다면 적을 화면에 나타내기
            foreach (var node in connected)
            {
                node.drawEnemy(node);
            }


        }//checkEnemy

        //Enemy를 화면에 나타나게 하는 함수, 
        public void drawEnemy(Node node)
        {
            int enemyCount = node.enemyObjects.Count;

            //node에 존재하는 적이 있다면, 그리고 그 수가 홀수라면
            if (enemyCount != 0 && enemyCount % 2 == 1)
            {
                int middle = enemyCount / 2;


                for (int i = 0; i < enemyCount; i++)
                {
                    //홀수 개의 원을 균형적으로 위치시키기
                    node.enemyObjects[i].transform.localPosition
                        = new Vector3((i - middle) * horizontalDistance, verticalDistance, 1f);
                }

            }

            //node에 적이 존재하고, 그 수가 짝수라면
            else if (enemyCount != 0 && enemyCount % 2 == 0)
            {
                float middle = enemyCount / 2 - 0.5f;

                for (float i = 0; i < enemyCount; i++)
                {
                    //짝수 개의 원을 균형적으로 위치시키기
                    node.enemyObjects[(int)i].transform.localPosition
                         = new Vector3((i - middle) * horizontalDistance, verticalDistance, 1f);

                }

            }


        }//drawEnemy


        //Enemy를 다시 Node에 가리게 해서 안보이게 하는 함수
        public void cloakEnemy()
        {
            //현재 노드의 적 안보이게 하기
            foreach (var obj in this.enemyObjects)
            {
                obj.transform.localPosition = new Vector3(0f, 0f, 1f);
            }

            //주변 노드의 적 안보이게 하기
            foreach (var node in connected)
            {
                foreach (var obj in node.enemyObjects)
                {
                    obj.transform.localPosition = new Vector3(0f, 0f, 1f);
                }
            }

        }//cloakEnemy


        public void playerMovement(Node node)
        {
            MapManager.instance.playerNode.nodeType = NodeType.Empty; //원래 있던 Node를 Empty로 변경

            MapManager.instance.playerNode.setColor(); //색 변경

            node.nodeType = NodeType.Player; //클릭한 Node의 Type을 Player로 변경

            setColor(); //색 변경

            MapManager.instance.playerNode.cloakEnemy(); //현재 Node와 주변 Node의 적 지우기
            node.checkEnemy(); //이동할 Node와 주변 Node의 적 표시하기

            MapManager.instance.playerNode = node;

        }//playerMovement




    }//Node Class

}


