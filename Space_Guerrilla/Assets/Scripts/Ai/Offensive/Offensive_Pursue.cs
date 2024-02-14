using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ���������� �÷��̾ �����ϴ� ����
public class Offensive_Pursue : Offensive_State
{
    public Offensive_Pursue(GameObject _enemy, Transform _player, Enemy_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        // ���� State�� �̸��� PURSUE�� ����
        name = STATE.PURSUE;
    }

    public override void Enter()
    {
        Debug.Log("Pursue");
        base.Enter();
    }

    public override void FixedUpdate()
    {
        // ����� ��
        control.isShoot = true;
        // ������ ���� Pursue ���¿� �����ϸ� ������ ������ �����ϹǷ� Exit������ ����
    }

    public override void Exit()
    {
        base.Exit();
    }
}
