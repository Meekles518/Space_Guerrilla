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
        // 공격형 적은 Pursue 상태에 진입하면 끝까지 추적을 개시하므로 Exit조건이 없음
    }

    public override void Exit()
    {
        base.Exit();
    }
}
