using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TurnManger�� ���
// 1. ���� �÷��̾� ������ �� �������� ���� -> bool isTurn�� ����
// isTurn true -> ������ư Ŭ�� -> isTurn false
// isTurn false -> ������ �ൿ�� �� ���´ٴ� ��ȣ ����(������� list�� �޾� ����Ʈ�� ���� �ൿ���Ḧ for������ ���� Ȯ��) -> isTurn true
// 2. �� �ൿ����� ���� �߻��� ���� scene���� ��ȯ -> �ٷ� ����ó�� list�� ���� ������� ���� �������θ� Ȯ���ϸ� �ɵ�


public class TurnManager : MonoBehaviour
{
    public bool isTurn;

    // Start is called before the first frame update
    void Start()
    {
        isTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
