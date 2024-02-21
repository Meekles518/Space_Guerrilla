using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

//���� Map�� ��Ÿ���� ���� ����ϴ� Circle�� ��� ������ ���� Ŭ����,
//��� Ai ��ũ��Ʈ�� Enemy_Circle Ŭ������ ��ӹ޴´�??
public class Enemy_Circle : MonoBehaviour
{

    //���� �ൿ���� enum, ������ ���� ������ �ƴ϶� ������ ������, ��� ����� ���� ������ ǥ��
    public enum Status
    {
       RoamingHuntRun,
       HouseHuntRun,
       RomingRun,
       HuntRun,

    }


    public Status status; //���� �ൿ������ ������ ����
    public bool telescopeHunt; //������ ���� ��带 ������ bool ����
    public Node targetNode; //��ǥ Node�� ������ ����
    public EnemyInfo enemyInfo;

    public Node currentNode; //���� ��ġ�� Node�� ������ ����

    //virtual, ���� �Լ��� ���� Ai ������ ����. �� Class�� ��ӹ޴� ���� �ٸ� ������ ����
    //�Լ��� ���� �ϼ���Ű��.
    public virtual void enemyAi()
    {
        enemyInfo = GetComponent<EnemyInfo>(); //EnemyInfo ������Ʈ ��������
        currentNode = GetComponentInParent<Node>(); //�θ��� Node ���� ��������

        //���� Phase�� ��
        if (MapManager.instance.phase == Phase.Hunt)
        {


        }

        //���� Phase �� ��
        else if (MapManager.instance.phase == Phase.Assemble)
        {

        }

        else
        {
            switch (status)
            {
                case Status.RoamingHuntRun:
                    Run();   
                    Hunt();
                    Roaming();
                    break;

                case Status.HouseHuntRun:
                    Run();
                    Hunt();
                    House();
                    break;

                case Status.RomingRun:
                    Run();
                    Roaming();
                    break;

                case Status.HuntRun:

                    Run();
                    Hunt();
                    break;



                default:
                    break;
            }

        }
         

        

    }//movement

    public void Run()
    {
        //ü���� 30% ������ ���
        if (enemyInfo.health / enemyInfo.maxhealth <= 0.3f)
        {
            //���� Node�� Player�� ��ġ�� Node�� ���
            if (currentNode == MapManager.instance.playerNode)
            {
                //�α��� ������ Node�� �̵�
                int ran = Random.Range(0, currentNode.connected.Count);
                movement(currentNode.connected[ran]);
            }
        }

    }//Run

    public void Hunt()
    {
        //���� Player�� Ž���� ���¶��
        if (MapManager.instance.playerDetected)
        {

        }


    }//Hunt

    public void Roaming()
    {


    }//Roaming


    public void House()
    {

    }//House

    //��ǥ node���� �ִܰŸ��� �̵��ϴ� 
    public void BFS(Node node)
    {


    }//BFS

    //������ ���� �̵���Ű�� �Լ�
    public void movement(Node node)
    {
        
        currentNode.enemyObjects.Remove(gameObject); //Node���� �� ���� ����
        //���� �� Node�� ���� �� �̻� ���ٸ�
        if (currentNode.enemyObjects.Count == 0)
        {
            //MapManager�� �� Node ����Ʈ���� ����
            MapManager.instance.enemyNodeList.Remove(currentNode);
        }

        //���� �� Node ����Ʈ�� �̵��Ϸ��� node�� �������� �ʴ´ٸ�
        if (!MapManager.instance.enemyNodeList.Contains(node))
        {
            //�̵��Ϸ��� Node �߰�
            MapManager.instance.enemyNodeList.Add(node);
        }
        node.enemyObjects.Add(gameObject); //�̵��Ϸ��� Node�� �� List�� �߰�

        gameObject.transform.parent = node.gameObject.transform; //�θ� ����




    }//movement


     


}

