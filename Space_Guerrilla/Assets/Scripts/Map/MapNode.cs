using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    //�ϴ� ��� ����
    //MapNode Class ����, Node�� ���� Map�� ���� ���� �ʿ��� UI, �Լ����� ����
    public class MapNode : MonoBehaviour
    {
         

        public NodeStates nodeState;
        public Node node; //Node ���� ������ ����

        public MapNode(NodeStates nodeState, Node node)
        {
            this.nodeState = nodeState;
            this.node = node;
        }
        
    }


}
 
