using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using static UnityEngine.RuleTile.TilingRuleOutput;

// ��׷ΰ� Ǯ�� ������ ���� ���� ������ġ�� ����
public class Defensive_GoBack : Defensive_State
{

    public Defensive_GoBack(GameObject _enemy, Transform _player, Enemy_Control _control)
        : base(_enemy, _player, _control)
    {

        name = STATE.GOBACK;
    }

    public override void Enter()
    {
        // ���������� ��׷� ���� = ����
        GameManager.instance.isDefensiveEngage = false;
        Debug.Log("GoBack");
        base.Enter();
    }

    public override void FixedUpdate()
    {
        // ��� �Ұ�
        control.isShoot = false;

        // ������ ��׷ΰ� ������ ��
        if (Aggro())
        {
            // ���� ������Ʈ�� Pursue�� ����
            nextState = new Defensive_Pursue(enemy, player, control);
            stage = EVENT.EXIT;
        }

        // ���� ��ġ�� ������ ���� ������ �Դٰ��� �Ÿ��鼭 ���� ������Ʈ�� �Ѿ�� ����
        else
        {
            // ���� ������ġ ��ó�� ���޽�
            if (control.EnemytoSelfSpawn < 0.2f)
            {
                // ���� ������Ʈ�� Idle�� ����
                nextState = new Defensive_Idle(enemy, player, control);
                stage = EVENT.EXIT;
            }
        }

    }

    public override void Exit()
    {


        base.Exit();
    }
}
