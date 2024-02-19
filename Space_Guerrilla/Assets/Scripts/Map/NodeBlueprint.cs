using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Map
{
    //NodeType�� ������ enum 
    public enum NodeType
    {
        Empty, //��� �ִ� Node
        Player, //Player�� ��ġ�� Node
        Enemy, //Enemy �� ��ġ�� Node
        Special, //Ư���� Node? (����, Ż�� ���� ��)�� ǥ���ϴ� Node, �ӽ� ����

    }

     

    //Turn ���¸� ���� enum
    public enum Turn
    {
        Player,
        Enemy,
        Engage,    
    }




}



