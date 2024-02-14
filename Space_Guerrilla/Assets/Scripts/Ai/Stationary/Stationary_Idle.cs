using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ������ ���� �ʱ� ������ġ�� �ִ� ����
public class Stationary_Wait : Stationary_State
{
    public Stationary_Wait(GameObject _enemy, Transform _player, Enemy_Control _control)
        : base(_enemy, _player, _control)
    {
        // ���� State�� �̸��� IDLE�� ����
        name = STATE.WAIT;
    }

    // Idle ���Խ�
    public override void Enter()
    {
        Debug.Log("Idle");
        base.Enter();
    }

    // Idle ������ ��
    public override void FixedUpdate()
    {
        // ��� �Ұ�
        control.isShoot = true;

    }

    public override void Exit()
    {


        base.Exit();
    }
}