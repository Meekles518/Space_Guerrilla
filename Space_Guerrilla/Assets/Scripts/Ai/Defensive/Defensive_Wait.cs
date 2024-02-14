using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ������ ���� ���� ��׷� ���� ��迡�� ��׷ΰ� �����ִ� ����
public class Defensive_Wait : Defensive_State
{

    public Defensive_Wait(GameObject _enemy, Transform _player, Enemy_Control _control)
        : base(_enemy, _player, _control)
    {

        name = STATE.WAIT;
    }

    public override void Enter()
    {
        // ���������� ��׷� ���� = ��
        GameManager.instance.isDefensiveEngage = true;
        Debug.Log("Wait");
        base.Enter();
    }

    public override void FixedUpdate()
    {
        // ��� ����
        control.isShoot = true;

        // �÷��̾ ū ��׷� ������ ����ٸ�
        if (control.PlayertoFleetSpawn > control.largeAgrro && control.EnemytoPlayer > control.MaxAtkRange)
        {
            // ���� ������Ʈ�� GoBack�� ����
            nextState = new Defensive_GoBack(enemy, player, control);
            stage = EVENT.EXIT;
        }

        // �÷��̾ ���� ��׷� �������� ������ �Ѵٸ�
        else if (control.PlayertoFleetSpawn < control.smallAgrro)
        {
            // ���� ������Ʈ�� Pursue�� ����
            nextState = new Defensive_Pursue(enemy, player, control);
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {


        base.Exit();
    }
}