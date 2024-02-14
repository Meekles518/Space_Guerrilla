using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 수비형 적이 작은 어그로 범위 경계에서 어그로가 끌려있는 상태
public class Defensive_Wait : Defensive_State
{

    public Defensive_Wait(GameObject _enemy, Transform _player, Enemy_Control _control)
        : base(_enemy, _player, _control)
    {

        name = STATE.WAIT;
    }

    public override void Enter()
    {
        // 수비형적의 어그로 여부 = 참
        GameManager.instance.isDefensiveEngage = true;
        Debug.Log("Wait");
        base.Enter();
    }

    public override void FixedUpdate()
    {
        // 사격 가능
        control.isShoot = true;

        // 플레이어가 큰 어그로 범위를 벗어난다면
        if (control.PlayertoFleetSpawn > control.largeAgrro && control.EnemytoPlayer > control.MaxAtkRange)
        {
            // 다음 스테이트를 GoBack로 변경
            nextState = new Defensive_GoBack(enemy, player, control);
            stage = EVENT.EXIT;
        }

        // 플레이어가 작은 어그로 범위내로 재진입 한다면
        else if (control.PlayertoFleetSpawn < control.smallAgrro)
        {
            // 다음 스테이트를 Pursue로 변경
            nextState = new Defensive_Pursue(enemy, player, control);
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {


        base.Exit();
    }
}