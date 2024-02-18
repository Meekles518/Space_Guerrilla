using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// ������ �� Ai�� �ൿ�� �����ϴ� ��ũ��Ʈ
// ������ �� State��� Enemy_Control ���̸� ����
public class Offensive_AI : MonoBehaviour
{
    [Header("���� ������Ʈ")]
    Ai_State currentState; // ���� ������Ʈ
    [HideInInspector]
    public Transform player; // �÷��̾� Ʈ������
    [HideInInspector]
    public Enemy_Control control; // Enemy_Control ������Ʈ
    [HideInInspector]
    public float currTime; // Ÿ�̸ӿ� ����� ����ð�

    
    // ���� �ʱ�ȭ
    void OnEnable()
    {
        control = GetComponent<Enemy_Control>(); // ��Ʈ�� ������Ʈ�� ������
        currentState = new Offensive_Idle(gameObject, player, control, currTime); // �ʱ� ������Ʈ�� Idle�� ����
        currTime = 0f;
    }


    // ������ ���������� ����
    void FixedUpdate()
    {
        // ���� State�� ���������� ����
        currentState = currentState.Process();
        // ���� State�� Enmey_Control�� ����
        control.statename = (Enemy_Control.STATE)currentState.name;
    }
}
