using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



namespace Map
{
    //Node Class�� ����
    public class Node : MonoBehaviour
    {

        public List<Node> connected; //�� Node�� ����� �ٸ� Node�� ��ǥ�� ������ List
        public NodeType nodeType; //NodeType�� ������ ����
          
        public float mouseOnScale = 1.3f; //���콺�� �ö��� �� ��尡 Ŀ�� ������ ������ ����
        public SpriteRenderer sR;
        public List<GameObject> enemyObjects; //Enemy Object�� ������ ������ List


        public float verticalDistance; //Map Node�� ���� ǥ���ϴ� ���� ���� �Ÿ�    
        public float horizontalDistance; //���� ���� �� ��忡 ������ ��, ���� ǥ���ϴ� �� ������ 


        private void Awake()
        {
            mouseOnScale = 1.3f; //�ʱⰪ ����
            verticalDistance = -1f;  //�ʱ� ���� �Ÿ� ����
            horizontalDistance = 0.5f; //�ʱ� ���� �Ÿ� ����

        }


        //������� ������ ����ϴ� �Լ���

        //�ӽ÷� ����� ������ ����� �����ϱ� ����, �� NodeType�� ���� Node�� ������ ���� �� �־�� ��.
        public void setColor()
        {
            sR = GetComponent<SpriteRenderer>(); //SpriteRenderer ������Ʈ ��������

            switch (this.nodeType)
            {
                //NodeType�� Player�� ���
                case NodeType.Player:

                    //������ �ʷϻ����� ����?
                    sR.color = Color.green;

                    break;


                //NodeType�� Special �� ���
                case NodeType.Special:

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
            //�̵� ���� ���� �����Ѵٸ�, �׸��� Player ���̶��
            if (MapManager.instance.turn == Turn.Player && MapManager.instance.moveChance != 0)
            {
                //playerNode�� connected ����Ʈ��, Ŭ���� Node�� �����Ѵٸ�
                if (MapManager.instance.playerNode.connected.Contains(this))
                {

                    playerMovement(this);

                    //�� �� 1���� ������ �� �ִٰ� �����ϰ� �ڵ� �ۼ�, ���Ŀ� ���ּ��� ������
                    //���� �� ���� �޾Ƽ� ������ �� �ֵ��� �ڵ带 �����ؾ� ��.
                    MapManager.instance.moveChance -= 1; //�̵� Ƚ���� 1 ����


                    //���� �̵� �� �� Node�� ���� �ִٸ�, �̵� ��ȸ�� 0���� ���� ����
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


        //Enemy�� ������ ǥ���� �Լ� �ʿ�.
        public void checkEnemy()
        {
            drawEnemy(this);

            //������ Node�� ���� �ִٸ� ���� ȭ�鿡 ��Ÿ����
            foreach (var node in connected)
            {
                node.drawEnemy(node);
            }


        }//checkEnemy

        //Enemy�� ȭ�鿡 ��Ÿ���� �ϴ� �Լ�, 
        public void drawEnemy(Node node)
        {
            int enemyCount = node.enemyObjects.Count;

            //node�� �����ϴ� ���� �ִٸ�, �׸��� �� ���� Ȧ�����
            if (enemyCount != 0 && enemyCount % 2 == 1)
            {
                int middle = enemyCount / 2;


                for (int i = 0; i < enemyCount; i++)
                {
                    //Ȧ�� ���� ���� ���������� ��ġ��Ű��
                    node.enemyObjects[i].transform.localPosition
                        = new Vector3((i - middle) * horizontalDistance, verticalDistance, 1f);
                }

            }

            //node�� ���� �����ϰ�, �� ���� ¦�����
            else if (enemyCount != 0 && enemyCount % 2 == 0)
            {
                float middle = enemyCount / 2 - 0.5f;

                for (float i = 0; i < enemyCount; i++)
                {
                    //¦�� ���� ���� ���������� ��ġ��Ű��
                    node.enemyObjects[(int)i].transform.localPosition
                         = new Vector3((i - middle) * horizontalDistance, verticalDistance, 1f);

                }

            }


        }//drawEnemy


        //Enemy�� �ٽ� Node�� ������ �ؼ� �Ⱥ��̰� �ϴ� �Լ�
        public void cloakEnemy()
        {
            //���� ����� �� �Ⱥ��̰� �ϱ�
            foreach (var obj in this.enemyObjects)
            {
                obj.transform.localPosition = new Vector3(0f, 0f, 1f);
            }

            //�ֺ� ����� �� �Ⱥ��̰� �ϱ�
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
            MapManager.instance.playerNode.nodeType = NodeType.Empty; //���� �ִ� Node�� Empty�� ����

            MapManager.instance.playerNode.setColor(); //�� ����

            node.nodeType = NodeType.Player; //Ŭ���� Node�� Type�� Player�� ����

            setColor(); //�� ����

            MapManager.instance.playerNode.cloakEnemy(); //���� Node�� �ֺ� Node�� �� �����
            node.checkEnemy(); //�̵��� Node�� �ֺ� Node�� �� ǥ���ϱ�

            MapManager.instance.playerNode = node;

        }//playerMovement




    }//Node Class

}


