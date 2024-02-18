using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ���������� ���� ��׷� ���������� �÷��̾ �����ϴ� ����
public class Defensive_Pursue : Ai_State
{
    // Pursue ����
    public Defensive_Pursue(GameObject _enemy, Transform _player, Enemy_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {

        name = STATE.PURSUE;
    }

    public override void Enter()
    {
        // ���������� ��׷� ���� = ��
        GameManager.instance.isDefensiveEngage = true;
        base.Enter();
    }

    public override void FixedUpdate()
    {
        // ��� ����
        control.isShoot = true;

        // �÷��̾ ū ��׷� ������ �����
        if (control.PlayertoFleetSpawn > control.largeAgrro)
        {
            // ���� ������Ʈ�� Pursue�� ����
            nextState = new Defensive_GoBack(enemy, player, control, currTime);
            stage = EVENT.EXIT;
        }
        // �� ������ ���� ��׷� ������ ������� �ϸ�
        if (control.EnemytoFleetSpawn >= control.smallAgrro)
        {
            // ���� ������Ʈ�� Wait�� ����
            nextState = new Defensive_Wait(enemy, player, control, currTime);
            // ���� �̺�Ʈ�� Exit���� ����
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {


        base.Exit();
    }
}