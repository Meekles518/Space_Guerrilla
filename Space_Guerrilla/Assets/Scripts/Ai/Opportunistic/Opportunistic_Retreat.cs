using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using static UnityEngine.RuleTile.TilingRuleOutput;

// ��׷ΰ� Ǯ�� ��ȸ������ ���� �÷��̾ ����ϸ� ������ġ�� ����
public class Opportunistic_Retreat : Opportunistic_State
{

    public Opportunistic_Retreat(GameObject _enemy, Transform _player, Enemy_Control _control)
        : base(_enemy, _player, _control)
    {

        name = STATE.RETREAT;
    }

    public override void Enter()
    {
        if (GameManager.OppControl == control)
        {
            GameManager.OppControl = null;
        }
        Debug.Log("Idle");
        Debug.Log("Retreat");
        base.Enter();
    }

    public override void FixedUpdate()
    {
        // ��� ����
        control.isShoot = true;
        // ���� ���� ��׷ΰ� �����ų� ���������� ��׷ΰ� ������ ��
        if (GameManager.instance.isDefensiveEngage || Aggro())
        {
            if(Aggro() && GameManager.OppControl == null)
            {
                GameManager.OppControl = control;
            }
            // ���� ������Ʈ�� Pursue�� ����
            nextState = new Opportunistic_Pursue(enemy, player, control);
            stage = EVENT.EXIT;
        }

        // ���� ��ġ�� ������ ���� ������ �Դٰ��� �Ÿ��鼭 ���� ������Ʈ�� �Ѿ�� ����
        else
        {
            // ���� ������ġ ��ó�� ���޽�
            if (control.EnemytoSelfSpawn < 0.2f)
            {
                // ���� ������Ʈ�� Idle�� ����
                nextState = new Opportunistic_Idle(enemy, player, control);
                stage = EVENT.EXIT;
            }
        }

    }

    public override void Exit()
    {


        base.Exit();
    }
}