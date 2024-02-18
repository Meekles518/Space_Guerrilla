using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ���������� �÷��̾ �����ϴ� ����
public class Offensive_Pursue : Ai_State
{
    public Offensive_Pursue(GameObject _enemy, Transform _player, Enemy_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        currTime = 0f; // ����ð� �ʱ�ȭ
        // ���� State�� �̸��� PURSUE�� ����
        name = STATE.PURSUE;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void FixedUpdate()
    {
        // ����� ��
        control.isShoot = true;
        // ���� �ð��� ��� ����ȭ
        currTime += Time.fixedDeltaTime;
        if (currTime > 100f)
        {
            // ���� ������Ʈ�� Pursue�� ����
            nextState = new Offensive_Escape(enemy, player, control, currTime);
            // ���� �̺�Ʈ�� Exit���� ����
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}