using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Map Node의 기능
// 1. 선택 안됨, 선택됨, 선택 가능에 따라 이미지 변경 -> state와 EventSystems를 통한 플레이어 클릭 판별을 통해 해결, 클릭시 MapPlayerTracker에게 클릭사실 전달 (StS 스크립트의 MapNode에 해당)
// 2. 이 노드로 부터 중요노드들(player현재 위치, Exit Node, Event Node 등)의 거리를 계산 -> Map Node에 각 중요노드의 거리 변수를 할당, 최단거리를 찾을때 인접 노드들의 거리변수를 조회, 최단 거리를 찾음 (Sts 스크립트이 Node와 비슷)
// 단 2에서 Player 현재위치로 부터의 최단거리는 Turn End시마다 갱신해야함, 이거 어캐함?


namespace Map
{
    public enum NodeStates
    {
        Neutral,
        Current,
        Attainable
    }
}

namespace Map
{


    public class MapNode : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerEnter(PointerEventData data){

        }

        public void OnPointerExit(PointerEventData data)
        {

        }

        public void OnPointerUp(PointerEventData data)
        {

        }

        public void OnPointerDown(PointerEventData data)
        {

        }
    }
}