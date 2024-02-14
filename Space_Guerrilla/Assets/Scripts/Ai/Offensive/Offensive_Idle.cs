using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 공격형 적이 초기 스폰위치에 있는 상태
public class Offensive_Idle : Offensive_State
{
    public Offensive_Idle(GameObject _enemy, Transform _player, Enemy_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        // 현재 State의 이름을 IDLE로 변경
        name = STATE.IDLE;
    }

    // Idle 진입시 
    public override void Enter()
    {
        currTime = 0f; // 현재시간 초기화
        Debug.Log("Idle");
        base.Enter();
    }

    // Idle 상태일 때
    public override void FixedUpdate()
    {
        // 사격 불가
        control.isShoot = false;
        // 현재 시간을 계속 동기화
        currTime += Time.fixedDeltaTime;

        // 시간이 마지막 초기화로부터 3초 지났다면
        if (currTime > control.timer)
        {
            // 다음 스테이트를 Pursue로 설정
            nextState = new Offensive_Pursue(enemy, player, control, currTime);
            // 다음 이벤트를 Exit으로 설정
            stage = EVENT.EXIT;
        }
        // 어그로가 끌렸을 시
        else if (Aggro())
        {
            // 다음 스테이트를 Pursue로 설정
            nextState = new Offensive_Pursue(enemy, player, control, currTime);
            // 다음 이벤트를 Exit으로 설정
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {


        base.Exit();
    }
}
