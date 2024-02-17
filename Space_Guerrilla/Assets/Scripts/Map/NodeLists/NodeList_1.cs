using System.Collections;
using System.Collections.Generic;
using UnityEditor;
 
using UnityEngine;
using Map;


/* �뷫�� Map �׸�
         * 
                            2��(1, 1) -- 3��(2, 1) -- 4��(3, 1)
                            |            |              |
                            |            |              |
            1��(0, 0) --    5��(1, 0) -- 6��(2, 0) -- 7��(3, 0) -- 11��(4, 0)
                            |            |              |
                            |            |              |
                            8��(1, -1) -- 9��(2, -1) -- 10��(3, -1)
          
         
        */

//�ӽ÷� ���� Node��� ��ε��� �����ϰ�, ����.
//���� �������� ���� �����ϰų�, �� ȿ������ ����� ������ �� ������ �ʿ���


//1�� ���� 11�� Node���� ����, ��� ���� �׽�Ʈ�� ���� �ϵ��ڵ� 
//NodeType�� Point �������� ��� ���� 


     
    //NodeList Class ����, Node���� ���� ��ġ�� �ƴ� ���۾����� ���� 
    //���� Map���� NodeList���� ScriptableObject�� ������, ���� ���� ����?
    public class NodeList_1 : NodeList
    {

        //NodeType�� Point �������� ��� ����
        public new List<Node> nodeList = new List<Node>
    {
            new Node(NodeType.Empty, new Point(0, 0), new List<Node>{}), //1�� Node
            new Node(NodeType.Empty, new Point(1, 1), new List<Node>{}), //2�� Node
            new Node(NodeType.Empty, new Point(2, 1), new List<Node>{}), //3�� Node
            new Node(NodeType.Empty, new Point(3, 1), new List<Node>{}), //4�� Node
            new Node(NodeType.Empty, new Point(1, 0), new List<Node>{}), //5�� Node
            new Node(NodeType.Empty, new Point(2, 0), new List < Node > {}), //6�� Node
            new Node(NodeType.Empty, new Point(3, 0), new List < Node > {}), //7�� Node
            new Node(NodeType.Empty, new Point(1, -1), new List<Node>{}), //8�� Node
            new Node(NodeType.Empty, new Point(2, -1), new List<Node>{}), //9�� Node
            new Node(NodeType.Empty, new Point(3, -1), new List < Node > {}), //10 Node
            new Node(NodeType.Empty, new Point(4, 0), new List < Node > {}), //11 Node

    };



        //�� Node��� ����� �ٸ� ������ ������ List
        public new List<List<int>> connectionList = new List<List<int>>
    {
            new List<int> {5}, //1�� Node�� ����� Node���� ��ȣ
            new List<int> {3, 5}, //2�� Node�� ����� Node���� ��ȣ
            new List<int> {2, 4, 6}, //3�� Node�� ����� Node���� ��ȣ
            new List<int> {3, 7}, //4�� Node�� ����� Node���� ��ȣ
            new List<int> {1, 2, 6, 8}, //5�� Node�� ����� Node���� ��ȣ
            new List<int> {3, 5, 7, 9}, //6�� Node�� ����� Node���� ��ȣ
            new List<int> {4, 6, 10, 11}, //7�� Node�� ����� Node���� ��ȣ
            new List<int> {5, 9}, //8�� Node�� ����� Node���� ��ȣ
            new List<int> {6, 8, 10}, //9�� Node�� ����� Node���� ��ȣ
            new List<int> {7, 9}, //10�� Node�� ����� Node���� ��ȣ
            new List<int> {6}, //11�� Node�� ����� Node���� ��ȣ

    };

      
    }
        


    



 
