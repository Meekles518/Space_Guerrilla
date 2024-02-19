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

     

    //Turn 상태를 열거 enum
    public enum Turn
    {
        Player,
        Enemy,
        Engage,    
    }




}



