using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ������ ���� �ʱ� ������ġ�� �ִ� ����
public class Offensive_Escape : Offensive_State
{
    public Offensive_Escape(GameObject _enemy, Transform _player, Enemy_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        // ���� State�� �̸��� IDLE�� ����
        name = STATE.ESCAPE;
    }

    // Idle ���Խ� 
    public override void Enter()
    {

        base.Enter();
    }

    // Idle ������ ��
    public override void FixedUpdate()
    {
        // ��� �Ұ�
        control.isShoot = false;


    }

    public override void Exit()
    {


        base.Exit();
    }
}