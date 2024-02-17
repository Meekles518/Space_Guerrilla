using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Map
{
    //NodeType을 열거형 enum 
    public enum NodeType
    {
        Empty, //비어 있는 Node
        Player, //Player이 위치한 Node
        Enemy, //Enemy 가 위치한 Node
        Special, //특수한 Node? (거점, 탈출 지점 등)을 표현하는 Node, 임시 설정

    }

    //NodeState를 열거형 enum
    public enum NodeStates
    {
        Locked, //잠긴, 즉 갈 수 없는 Node
        Attainable, //이동할 수 있는 Node

    }

    [CreateAssetMenu]
    //Key로 NodeType을, value로 Sprite를 넣는 형태로 이미지 자료를 저장해서 활용하는 방법 구현
    public class NodeSprite : ScriptableObject
    {
        public NodeType nodeType;
        public Sprite sprite;


    }//NodeSprite

    public class NodeList
    {
        public List<Node> nodeList;
        public List<List<int>> connectionList;


    }




}


