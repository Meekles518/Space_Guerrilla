using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



namespace Map
{
    //Node Class를 정의
    public class Node : MonoBehaviour
    {
        public Point point; //Node의 좌표값을 저장할 Point 형 변수
        public List<Node> connected; //이 Node와 연결된 다른 Node의 좌표를 저장할 List
        public NodeType nodeType; //NodeType을 저장할 변수
        public Vector2 position; //실제 위치를 표현할 Vector2 변수
        public NodeStates state = NodeStates.Locked; //NodeStates를 저장할 변수, 초기는 Locked로 고정.
        public SpriteRenderer sR; //SpriteRender 저장 변수
        public Image image; //Image 저장 변수
        public float mouseOnScale = 1.3f; //마우스가 올라갔을 때 노드가 커질 비율을 저장할 변수


        private void Awake()
        {
            mouseOnScale = 1.3f; //초기값 설정

        }



        //Node Class의 값 넣기?
        public Node(NodeType nodeType, Point point)
        {
            this.nodeType = nodeType;
            this.point = point;
            position = new Vector2(this.point.x, this.point.y);
       }



        //Node Class의 값 넣기?
        public Node(NodeType nodeType, Point point, List<Node> connected)
        {
            this.nodeType = nodeType;
            this.point = point;
            this.connected = connected;
            position = new Vector2(this.point.x, this.point.y);
        }


        public void addInfo(Node n)
        {
            point = n.point;
            connected = n.connected;
            nodeType = n.nodeType;
            position = n.position;
            state = n.state;

        }//addInfo



        //Node 연결 정보를 추가하는 함수 
        public void AddConnections(Node n)
        {
            if (connected.Any(e => e.point.Equals(n.point)))
                return;

            connected.Add(n);

        }//AddConnections


        //Node의 연결 정보를 제거하는 함수
        public void RemoveConnections(Node n)
        {

            connected.RemoveAll(e => e.point.Equals(n.point));

        }//RemoveConnections


        //Node의 이미지 변경? 하는 함수
        public void setUp()
        {
            sR.sprite = MapManager.instance.nodeSprites.FirstOrDefault(nS => nS.nodeType == this.nodeType).sprite;
            image.sprite = MapManager.instance.nodeSprites.FirstOrDefault(nS => nS.nodeType == this.nodeType).sprite;

        }//setUp


        //임시로 노드의 구별을 색깔로 진행하기 위해, 현 NodeType에 따라서 Node의 구분을 지을 수 있어야 함.
        public void setColor()
        {
            sR = GetComponent<SpriteRenderer>(); //SpriteRenderer 컴포넌트 가져오기

            switch(this.nodeType)
            {
                //NodeType이 Player일 경우
                case NodeType.Player:

                    //색깔을 초록색으로 변경?
                    sR.color = Color.green; 

                    break;


                //NodeType이 Special 일 경우
                case NodeType.Special:

                    break;


                //NodeType이 Enemy일 경우
                case NodeType.Enemy:

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


        //이 Node 주변 Node의 State를 Attainable로 변경하는 함수
        public void attainableState()
        {
            //foreach문으로 connected 리스트에 접근
            foreach (var node in connected)
            {
                node.state = NodeStates.Attainable; //접근 가능 State로 변경

                //노드의 크기 변환? 코드

            }


        }//attainableState

        //이 Node 주변 Node의 State를 Locked로 변경하는 함수
        public void lockedState()
        {
            //foreach문으로 connected 리스트에 접근
            foreach (var node in connected)
            {
                node.state = NodeStates.Locked;

            }


        }//lockedState



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

            //playerNode의 connected 리스트에, 클릭한 Node가 존재한다면
            if(MapManager.instance.playerNode.connected.Contains(this))
            {
                MapManager.instance.playerNode.nodeType = NodeType.Empty; //원래 있던 Node를 Empty로 변경
                MapManager.instance.playerNode.lockedState(); //원래 있던 Node의 주변 Node의 State를 locked로
                MapManager.instance.playerNode.setColor(); //색 변경

                nodeType = NodeType.Player; //클릭한 Node의 Type을 Player로 변경
                attainableState(); //클릭한 Node의 주변 Node의 state를 attainable로
                setColor(); //색 변경

                MapManager.instance.playerNode = this;
            }

            else
            {
                Debug.LogWarning("U cant move to that node");
            }

        }//OnMouseUpAsButton



         

    }//Node Class

}

 
