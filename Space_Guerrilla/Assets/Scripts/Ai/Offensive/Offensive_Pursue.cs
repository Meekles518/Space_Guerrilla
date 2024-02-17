using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 공격형적이 플레이어를 추적하는 상태
public class Offensive_Pursue : Offensive_State
{
    public Offensive_Pursue(GameObject _enemy, Transform _player, Enemy_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        currTime = 0f; // 현재시간 초기화
        // 현재 State의 이름을 PURSUE로 변경
        name = STATE.PURSUE;
    }

    public override void Enter()
    {
        Debug.Log("Pursue");
        base.Enter();
    }

    public override void FixedUpdate()
    {
        // 사격을 함
        control.isShoot = true;
        // 현재 시간을 계속 동기화
        currTime += Time.fixedDeltaTime;
        if (currTime > 5f)
        {
            // 다음 스테이트를 Pursue로 설정
            nextState = new Offensive_Escape(enemy, player, control, currTime);
            // 다음 이벤트를 Exit으로 설정
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
