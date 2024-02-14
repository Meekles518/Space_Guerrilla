using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ������ ���� �ʱ� ������ġ�� �ִ� ����
public class Defensive_Idle : Defensive_State
{
    // Idle ������Ʈ ����
    public Defensive_Idle(GameObject _enemy, Transform _player, Enemy_Control _control)
        : base(_enemy, _player, _control)
    {

        name = STATE.IDLE;
    }

    // Idle ���Խ�
    public override void Enter()
    {
        // ���������� ��׷� ���� = ����
        GameManager.instance.isDefensiveEngage = false;
        Debug.Log("Idle"); // �α׿� �߰���
        base.Enter();
    }

    // Idle ������ ��
    public override void FixedUpdate()
    {
        // ��� �Ұ�
        control.isShoot = false;

        // ��׷ΰ� ������ ��
        if (Aggro())
        {
            // ���� ������Ʈ�� Pursue�� ����
            nextState = new Defensive_Pursue(enemy, player, control);
            // ���� �̺�Ʈ�� Exit���� ����
            stage = EVENT.EXIT;
        }
        
    }

    public override void Exit()
    {


        base.Exit();
    }
}