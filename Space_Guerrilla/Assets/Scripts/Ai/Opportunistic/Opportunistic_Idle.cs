using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 기회주의형 적이 초기 스폰위치에 있는 상태
public class Opportunistic_Idle : Opportunistic_State
{
    public Opportunistic_Idle(GameObject _enemy, Transform _player, Enemy_Control _control)
        : base(_enemy, _player, _control)
    {
        // 현재 State의 이름을 IDLE로 변경
        name = STATE.IDLE;
    }

    // Idle 진입시
    public override void Enter()
    {

        base.Enter();
    }

    // Idle 상태일 때
    public override void FixedUpdate()
    {
        // 사격 불가
        control.isShoot = false;
        Debug.Log(Aggro());

        // 수비형적의 어그로가 끌리거나 본인의 어그로가 끌렸다면
        if (GameManager.instance.isDefensiveEngage || Aggro() || GameManager.OppControl.isAggro == true)
        {
            if (Aggro())
            {
                if(GameManager.OppControl == null)
                {
                    GameManager.OppControl = control;
                    Debug.Log(GameManager.OppControl);
                }
            }
            // 다음 스테이트를 Pursue로 설정
            nextState = new Opportunistic_Pursue(enemy, player, control);
            // 다음 이벤트를 Exit으로 설정
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {


        base.Exit();
    }
}
