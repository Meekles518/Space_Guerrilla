using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Map
{
    public class Map : MonoBehaviour
    {
        //Map Class 생성

        public List<Node> nodes; //Map의 모든 Node들을 저장할 List

        //Map의 변수에 값 저장
        public Map(List<Node> nodes)
        {
            this.nodes = nodes;
        }





    }


}

 
