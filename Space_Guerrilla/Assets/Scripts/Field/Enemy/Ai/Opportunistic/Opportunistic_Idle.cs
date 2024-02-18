using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ��ȸ������ ���� �ʱ� ������ġ�� �ִ� ����
public class Opportunistic_Idle : Ai_State
{
    public Opportunistic_Idle(GameObject _enemy, Transform _player, Enemy_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        // ���� State�� �̸��� IDLE�� ����
        name = STATE.IDLE;
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

        // ���������� ��׷ΰ� �����ų� ������ ��׷ΰ� ���ȴٸ�
        if (GameManager.instance.isDefensiveEngage || Aggro() || GameManager.OppControl.isAggro == true)
        {
            if (Aggro())
            {
                if(GameManager.OppControl == null)
                {
                    GameManager.OppControl = control;
                    Debug.Log(GameManager.OppControl);
                }
            }
            // ���� ������Ʈ�� Pursue�� ����
            nextState = new Opportunistic_Pursue(enemy, player, control, currTime);
            // ���� �̺�Ʈ�� Exit���� ����
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {


        base.Exit();
    }
}
