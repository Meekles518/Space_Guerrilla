using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ������ ���� �ʱ� ������ġ�� �ִ� ����
public class Offensive_Idle : Offensive_State
{
    public Offensive_Idle(GameObject _enemy, Transform _player, Enemy_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        // ���� State�� �̸��� IDLE�� ����
        name = STATE.IDLE;
    }

    // Idle ���Խ� 
    public override void Enter()
    {
        currTime = 0f; // ����ð� �ʱ�ȭ
        Debug.Log("Idle");
        base.Enter();
    }

    // Idle ������ ��
    public override void FixedUpdate()
    {
        // ��� �Ұ�
        control.isShoot = false;
        // ���� �ð��� ��� ����ȭ
        currTime += Time.fixedDeltaTime;

        // �ð��� ������ �ʱ�ȭ�κ��� 3�� �����ٸ�
        if (currTime > control.timer)
        {
            // ���� ������Ʈ�� Pursue�� ����
            nextState = new Offensive_Pursue(enemy, player, control, currTime);
            // ���� �̺�Ʈ�� Exit���� ����
            stage = EVENT.EXIT;
        }
        // ��׷ΰ� ������ ��
        else if (Aggro())
        {
            // ���� ������Ʈ�� Pursue�� ����
            nextState = new Offensive_Pursue(enemy, player, control, currTime);
            // ���� �̺�Ʈ�� Exit���� ����
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {


        base.Exit();
    }
}
