using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// ������ �� Ai�� �ൿ�� �����ϴ� ��ũ��Ʈ
// ������ �� State��� Enemy_Control ���̸� ����
public class Drone_AI : MonoBehaviour
{
    public Drone_State currentState; // ���� ������Ʈ
    public Transform player; // �÷��̾� Ʈ������
    public Drone_Control control; // Enemy_Control ������Ʈ

    public float currTime; // Ÿ�̸ӿ� ����� ����ð�

    // ���� �ʱ�ȭ
    void OnEnable()
    {
        control = GetComponent<Drone_Control>(); // ��Ʈ�� ������Ʈ�� ������
        player = GameObject.Find("Player").transform; // �÷��̾� ������Ʈ�� ã�� Ʈ������ �Ҵ�
        currentState = new Drone_Idle(gameObject, player, control, currTime); // �ʱ� ������Ʈ�� Idle�� ����



    }//OnEnable


    // ������ ���������� ����
    void FixedUpdate()
    {

        // ���� State�� ���������� ����
        currentState = currentState.Process();
        // ���� State�� Drone_Control�� ����
        control.statename = (Drone_Control.STATE)currentState.name;

    }//FixedUpdate
}