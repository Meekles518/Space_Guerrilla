using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ��ȸ���������� �÷��̾ �����ϴ� ����
public class Opportunistic_Pursue : Ai_State
{
    public Opportunistic_Pursue(GameObject _enemy, Transform _player, Enemy_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {

        name = STATE.PURSUE;
    }

    public override void Enter()
    {

        base.Enter();
    }

    public override void FixedUpdate()
    {
        // ��� ����
        control.isShoot = true;

        //if (this.control == GameManager.OppControl)

        // ������ ��׷ΰ� �������� �ʰ� ���������� ��׷ΰ� �������� ���� ��
        if (( GameManager.OppControl == null && !GameManager.instance.isDefensiveEngage)
           || (GameManager.OppControl != null && GameManager.OppControl.PlayertoFleetSpawn > GameManager.OppControl.largeAgrro))
        {

                // ���� ������Ʈ�� Retreat�� ����
                nextState = new Opportunistic_Retreat(enemy, player, control, currTime);
                stage = EVENT.EXIT;

        }
    }
    public override void Exit()
    {


        base.Exit();
    }
}
