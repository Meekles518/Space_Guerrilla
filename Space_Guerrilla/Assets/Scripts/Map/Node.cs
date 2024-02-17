using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



namespace Map
{
    //Node Class�� ����
    public class Node : MonoBehaviour
    {
        public Point point; //Node�� ��ǥ���� ������ Point �� ����
        public List<Node> connected; //�� Node�� ����� �ٸ� Node�� ��ǥ�� ������ List
        public NodeType nodeType; //NodeType�� ������ ����
        public Vector2 position; //���� ��ġ�� ǥ���� Vector2 ����
        public NodeStates state = NodeStates.Locked; //NodeStates�� ������ ����, �ʱ�� Locked�� ����.
        public SpriteRenderer sR; //SpriteRender ���� ����
        public Image image; //Image ���� ����
        public float mouseOnScale = 1.3f; //���콺�� �ö��� �� ��尡 Ŀ�� ������ ������ ����


        private void Awake()
        {
            mouseOnScale = 1.3f; //�ʱⰪ ����

        }



        //Node Class�� �� �ֱ�?
        public Node(NodeType nodeType, Point point)
        {
            this.nodeType = nodeType;
            this.point = point;
            position = new Vector2(this.point.x, this.point.y);
       }



        //Node Class�� �� �ֱ�?
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



        //Node ���� ������ �߰��ϴ� �Լ� 
        public void AddConnections(Node n)
        {
            if (connected.Any(e => e.point.Equals(n.point)))
                return;

            connected.Add(n);

        }//AddConnections


        //Node�� ���� ������ �����ϴ� �Լ�
        public void RemoveConnections(Node n)
        {

            connected.RemoveAll(e => e.point.Equals(n.point));

        }//RemoveConnections


        //Node�� �̹��� ����? �ϴ� �Լ�
        public void setUp()
        {
            sR.sprite = MapManager.instance.nodeSprites.FirstOrDefault(nS => nS.nodeType == this.nodeType).sprite;
            image.sprite = MapManager.instance.nodeSprites.FirstOrDefault(nS => nS.nodeType == this.nodeType).sprite;

        }//setUp


        //�ӽ÷� ����� ������ ����� �����ϱ� ����, �� NodeType�� ���� Node�� ������ ���� �� �־�� ��.
        public void setColor()
        {
            sR = GetComponent<SpriteRenderer>(); //SpriteRenderer ������Ʈ ��������

            switch(this.nodeType)
            {
                //NodeType�� Player�� ���
                case NodeType.Player:

                    //������ �ʷϻ����� ����?
                    sR.color = Color.green; 

                    break;


                //NodeType�� Special �� ���
                case NodeType.Special:

                    break;


                //NodeType�� Enemy�� ���
                case NodeType.Enemy:

                    break;


                //NodeType�� Empty�� ���
                case NodeType.Empty:

                    //���� �Ͼ������ ����
                    sR.color = Color.white;

                    break;


                default:
                    break;


            }


        }//setColor


        //�� Node �ֺ� Node�� State�� Attainable�� �����ϴ� �Լ�
        public void attainableState()
        {
            //foreach������ connected ����Ʈ�� ����
            foreach (var node in connected)
            {
                node.state = NodeStates.Attainable; //���� ���� State�� ����

                //����� ũ�� ��ȯ? �ڵ�

            }


        }//attainableState

        //�� Node �ֺ� Node�� State�� Locked�� �����ϴ� �Լ�
        public void lockedState()
        {
            //foreach������ connected ����Ʈ�� ����
            foreach (var node in connected)
            {
                node.state = NodeStates.Locked;

            }


        }//lockedState



        public void OnMouseEnter() //���콺�� �ö��� �� 
        {
            transform.localScale = new Vector3(mouseOnScale, mouseOnScale, 1f); //Scale 1.3�� �ϱ�

        }//OnMouseEnter


        public void OnMouseExit()
        {
            transform.localScale = new Vector3(1f, 1f, 1f);//Scale 1��� �ǵ�����
        }//OnMouseExit

        
        


        public void OnMouseUpAsButton() //�� ������Ʈ ������ ���콺 �ٿ�� ���� �Ͼ�� ��
        {

            //playerNode�� connected ����Ʈ��, Ŭ���� Node�� �����Ѵٸ�
            if(MapManager.instance.playerNode.connected.Contains(this))
            {
                MapManager.instance.playerNode.nodeType = NodeType.Empty; //���� �ִ� Node�� Empty�� ����
                MapManager.instance.playerNode.lockedState(); //���� �ִ� Node�� �ֺ� Node�� State�� locked��
                MapManager.instance.playerNode.setColor(); //�� ����

                nodeType = NodeType.Player; //Ŭ���� Node�� Type�� Player�� ����
                attainableState(); //Ŭ���� Node�� �ֺ� Node�� state�� attainable��
                setColor(); //�� ����

                MapManager.instance.playerNode = this;
            }

            else
            {
                Debug.LogWarning("U cant move to that node");
            }

        }//OnMouseUpAsButton



         

    }//Node Class

}

 
