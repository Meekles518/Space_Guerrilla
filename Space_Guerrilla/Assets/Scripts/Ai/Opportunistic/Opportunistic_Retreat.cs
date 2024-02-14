using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using static UnityEngine.RuleTile.TilingRuleOutput;

// 어그로가 풀린 기회주의형 적이 플레이어를 경계하며 스폰위치로 복귀
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
        // 사격 가능
        control.isShoot = true;
        // 후퇴 도중 어그로가 끌리거나 수비형적의 어그로가 끌렸을 시
        if (GameManager.instance.isDefensiveEngage || Aggro())
        {
            if(Aggro() && GameManager.OppControl == null)
            {
                GameManager.OppControl = control;
            }
            // 다음 스테이트를 Pursue로 변경
            nextState = new Opportunistic_Pursue(enemy, player, control);
            stage = EVENT.EXIT;
        }

        // 복귀 위치에 오차를 두지 않으면 왔다갔다 거리면서 다음 스테이트로 넘어가지 않음
        else
        {
            // 본인 스폰위치 근처에 도달시
            if (control.EnemytoSelfSpawn < 0.2f)
            {
                // 다음 스테이트를 Idle로 변경
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