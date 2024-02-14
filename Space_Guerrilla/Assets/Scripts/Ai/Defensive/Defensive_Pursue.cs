using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 수비형적이 작은 어그로 범위내에서 플레이어를 추적하는 형태
public class Defensive_Pursue : Defensive_State
{
    // Pursue 선언
    public Defensive_Pursue(GameObject _enemy, Transform _player, Enemy_Control _control)
        : base(_enemy, _player, _control)
    {

        name = STATE.PURSUE;
    }

    public override void Enter()
    {
        // 수비형적의 어그로 여부 = 참
        GameManager.instance.isDefensiveEngage = true;
        Debug.Log("Pursue");
        base.Enter();
    }

    public override void FixedUpdate()
    {
        // 사격 가능
        control.isShoot = true;

        // 플레이어가 큰 어그로 범위를 벗어나면
        if (control.PlayertoFleetSpawn > control.largeAgrro)
        {
            // 다음 스테이트를 Pursue로 변경
            nextState = new Defensive_GoBack(enemy, player, control);
            stage = EVENT.EXIT;
        }
        // 적 본인이 작은 어그로 범위를 벗어나려고 하면
        if (control.EnemytoFleetSpawn >= control.smallAgrro)
        {
            // 다음 스테이트를 Wait로 변경
            nextState = new Defensive_Wait(enemy, player, control);
            // 다음 이벤트를 Exit으로 변경
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {


        base.Exit();
    }
}