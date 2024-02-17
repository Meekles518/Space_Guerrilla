using System.Collections;
using System.Collections.Generic;
using UnityEditor;
 
using UnityEngine;
using Map;


/* 대략적 Map 그림
         * 
                            2번(1, 1) -- 3번(2, 1) -- 4번(3, 1)
                            |            |              |
                            |            |              |
            1번(0, 0) --    5번(1, 0) -- 6번(2, 0) -- 7번(3, 0) -- 11번(4, 0)
                            |            |              |
                            |            |              |
                            8번(1, -1) -- 9번(2, -1) -- 10번(3, -1)
          
         
        */

//임시로 직접 Node들과 경로들을 선언하고, 구현.
//만약 랜덤으로 맵을 구현하거나, 더 효율적인 방법을 도입할 시 수정이 필요함


//1번 부터 11번 Node까지 정의, 기능 구현 테스트를 위해 하드코딩 
//NodeType과 Point 정보만을 담고 있음 


     
    //NodeList Class 정의, Node들을 랜덤 배치가 아닌 수작업으로 진행 
    //여러 Map들의 NodeList들을 ScriptableObject로 구현해, 돌려 쓰기 가능?
    public class NodeList_1 : NodeList
    {

        //NodeType과 Point 정보만을 담고 있음
        public new List<Node> nodeList = new List<Node>
    {
            new Node(NodeType.Empty, new Point(0, 0), new List<Node>{}), //1번 Node
            new Node(NodeType.Empty, new Point(1, 1), new List<Node>{}), //2번 Node
            new Node(NodeType.Empty, new Point(2, 1), new List<Node>{}), //3번 Node
            new Node(NodeType.Empty, new Point(3, 1), new List<Node>{}), //4번 Node
            new Node(NodeType.Empty, new Point(1, 0), new List<Node>{}), //5번 Node
            new Node(NodeType.Empty, new Point(2, 0), new List < Node > {}), //6번 Node
            new Node(NodeType.Empty, new Point(3, 0), new List < Node > {}), //7번 Node
            new Node(NodeType.Empty, new Point(1, -1), new List<Node>{}), //8번 Node
            new Node(NodeType.Empty, new Point(2, -1), new List<Node>{}), //9번 Node
            new Node(NodeType.Empty, new Point(3, -1), new List < Node > {}), //10 Node
            new Node(NodeType.Empty, new Point(4, 0), new List < Node > {}), //11 Node

    };



        //각 Node들과 연결된 다른 노드들을 저장할 List
        public new List<List<int>> connectionList = new List<List<int>>
    {
            new List<int> {5}, //1번 Node와 연결된 Node들의 번호
            new List<int> {3, 5}, //2번 Node와 연결된 Node들의 번호
            new List<int> {2, 4, 6}, //3번 Node와 연결된 Node들의 번호
            new List<int> {3, 7}, //4번 Node와 연결된 Node들의 번호
            new List<int> {1, 2, 6, 8}, //5번 Node와 연결된 Node들의 번호
            new List<int> {3, 5, 7, 9}, //6번 Node와 연결된 Node들의 번호
            new List<int> {4, 6, 10, 11}, //7번 Node와 연결된 Node들의 번호
            new List<int> {5, 9}, //8번 Node와 연결된 Node들의 번호
            new List<int> {6, 8, 10}, //9번 Node와 연결된 Node들의 번호
            new List<int> {7, 9}, //10번 Node와 연결된 Node들의 번호
            new List<int> {6}, //11번 Node와 연결된 Node들의 번호

    };

      
    }
        


    



 
