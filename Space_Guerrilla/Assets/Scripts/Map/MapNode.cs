using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Map Node�� ���
// 1. ���� �ȵ�, ���õ�, ���� ���ɿ� ���� �̹��� ���� -> state�� EventSystems�� ���� �÷��̾� Ŭ�� �Ǻ��� ���� �ذ�, Ŭ���� MapPlayerTracker���� Ŭ����� ���� (StS ��ũ��Ʈ�� MapNode�� �ش�)
// 2. �� ���� ���� �߿����(player���� ��ġ, Exit Node, Event Node ��)�� �Ÿ��� ��� -> Map Node�� �� �߿����� �Ÿ� ������ �Ҵ�, �ִܰŸ��� ã���� ���� ������ �Ÿ������� ��ȸ, �ִ� �Ÿ��� ã�� (Sts ��ũ��Ʈ�� Node�� ���)
// �� 2���� Player ������ġ�� ������ �ִܰŸ��� Turn End�ø��� �����ؾ���, �̰� ��ĳ��?


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