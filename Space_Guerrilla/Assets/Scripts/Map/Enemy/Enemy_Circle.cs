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
    public Node currentNode; //���� ��ġ�� Node�� ������ ����

    //virtual, ���� �Լ��� ���� Ai ������ ����. �� Class�� ��ӹ޴� ���� �ٸ� ������ ����
    //�Լ��� ���� �ϼ���Ű��.
    public virtual void enemyAi()
    {
       

    }//movement

    public void Run()
    {
              

    }//Run

    public void Hunt()
    {



    }//Hunt

    public void Roaming()
    {


    }//Roaming


    public void House()
    {

    }//House

    //��ǥ node���� �ִܰŸ��� �̵��ϴ� �Լ�
    //������, ��ǥ Node���� ���� ���� ��ġ�� Node�� ���� �ִ� ��θ� ����,
    //connected ����Ʈ�� ���� ���� ��ġ�� Node�� ã�Ƴ��ٸ�, ���������� ����
    public Node BFS(Node node)
    {
        //�������� ���� Node�� ���
        if (node == currentNode)
        {
            return node;
        }


        List<Node> visitedNodes = new List<Node>(); //�湮�ߴ� Node��
        List<Node> queue = new List<Node>(); //queue ��� ���� List

        visitedNodes.Add(node); //�湮�ߴ� Node�� ��ǥ Node �߰�
        //��ǥ Node�� ����� Node�鿡 ����
        foreach(Node nd in node.connected)
        {
            //��ǥ Node�� ���� Node�� ����Ǿ� �ִٸ�
            if (nd == currentNode)
            {
                return node; //�ٷ� ��ǥ Node return
            }

            visitedNodes.Add(nd); //����� Node�鵵 �̸� �湮 ó��
            queue.Add(nd); //�ƴ϶��, queue�� ����� Node�� �߰�
        }

        //queue�� ���̰� 0���� Ŭ ���
        while (true)
        {
            List<Node> subList = new List<Node>(); 

            while(queue.Count > 0)
            {
                Node nextNode = queue[0]; //queue�� ù ��° ���� ��������
                queue.Remove(queue[0]); //queue�� ù ��° ���Ҹ� ����Ʈ���� ����
                

                foreach(Node nd in nextNode.connected)
                {
                    //nextNode�� ����� Node �߿� ���� ���� ��ġ�� Node�� �����Ѵٸ�
                    if (nd == currentNode)
                    {
                        //nextNode�� return�ϱ�
                        return nextNode;
                    }

                    //nextNode�� ����� nd�� �� �湮�� �� ���� Node���� �ִٸ�
                    if (!visitedNodes.Contains(nd))
                    {
                        visitedNodes.Add(nd); //nextNode�� ����� Node�鵵 �̸� �湮ó��
                        subList.Add(nd); //subList�� nd �߰�
                    }

                }//foreach


            }//while queue.Count > 0
              

            foreach (Node nd in subList)
            {
                queue.Add(nd);
            }

            //���� queue�� ���̰� 0�̶��, Node ���Ḯ��Ʈ�� ������ �ִٴ� ��.
            if (queue.Count == 0)
            {
                Debug.LogWarning("Node ���� ����Ʈ�� ���� ����");
                break;
            }


        }//while true

        return null; //null�� ��ȯ�Ǹ� �Լ��� Node ���Ḯ��Ʈ�� ������ �ִٴ� �ǹ�

    }//BFS

    //������ ���� �̵���Ű�� �Լ�
    public void movement(Node node)
    {
        //�������� ���� Node�� ���
        if (node == currentNode)
        {
            return;
        }
        
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

