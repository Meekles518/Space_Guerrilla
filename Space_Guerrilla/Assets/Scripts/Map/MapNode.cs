using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    //일단 사용 안함
    //MapNode Class 생성, Node를 실제 Map에 띄우기 위해 필요한 UI, 함수들을 포함
    public class MapNode : MonoBehaviour
    {
         

        public NodeStates nodeState;
        public Node node; //Node 정보 저장할 변수

        public MapNode(NodeStates nodeState, Node node)
        {
            this.nodeState = nodeState;
            this.node = node;
        }
        
    }


}
 
