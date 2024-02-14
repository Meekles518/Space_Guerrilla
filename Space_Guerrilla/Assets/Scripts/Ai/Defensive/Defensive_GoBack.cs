using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using static UnityEngine.RuleTile.TilingRuleOutput;

// 어그로가 풀린 수비형 적이 본인 스폰위치로 복귀
public class Defensive_GoBack : Defensive_State
{

    public Defensive_GoBack(GameObject _enemy, Transform _player, Enemy_Control _control)
        : base(_enemy, _player, _control)
    {

        name = STATE.GOBACK;
    }

    public override void Enter()
    {
        // 수비형적의 어그로 여부 = 거짓
        GameManager.instance.isDefensiveEngage = false;
        Debug.Log("GoBack");
        base.Enter();
    }

    public override void FixedUpdate()
    {
        // 사격 불가
        control.isShoot = false;

        // 복귀중 어그로가 끌렸을 시
        if (Aggro())
        {
            // 다음 스테이트를 Pursue로 변경
            nextState = new Defensive_Pursue(enemy, player, control);
            stage = EVENT.EXIT;
        }

        // 복귀 위치에 오차를 두지 않으면 왔다갔다 거리면서 다음 스테이트로 넘어가지 않음
        else
        {
            // 본인 스폰위치 근처에 도달시
            if (control.EnemytoSelfSpawn < 0.2f)
            {
                // 다음 스테이트를 Idle로 변경
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
