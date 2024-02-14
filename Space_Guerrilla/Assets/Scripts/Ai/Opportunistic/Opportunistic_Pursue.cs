using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 기회주의형적이 플레이어를 추적하는 형태
public class Opportunistic_Pursue : Opportunistic_State
{
    public Opportunistic_Pursue(GameObject _enemy, Transform _player, Enemy_Control _control)
        : base(_enemy, _player, _control)
    {

        name = STATE.PURSUE;
    }

    public override void Enter()
    {
        Debug.Log("Pursue");
        base.Enter();
    }

    public override void FixedUpdate()
    {
        // 사격 가능
        control.isShoot = true;
        Debug.Log(Aggro());
        //if (this.control == GameManager.OppControl)

        // 본인의 어그로가 끌려있지 않고 수비형적의 어그로가 끌려있지 않을 때
        if (( GameManager.OppControl == null && !GameManager.instance.isDefensiveEngage)
           || (GameManager.OppControl != null && GameManager.OppControl.PlayertoFleetSpawn > GameManager.OppControl.largeAgrro))
        {

                // 다음 스테이트를 Retreat로 변경
                nextState = new Opportunistic_Retreat(enemy, player, control);
                stage = EVENT.EXIT;

        }
    }
    public override void Exit()
    {


        base.Exit();
    }
}
