using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 공격형 적이 초기 스폰위치에 있는 상태
public class Offensive_Escape : Offensive_State
{
    public Offensive_Escape(GameObject _enemy, Transform _player, Enemy_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        // 현재 State의 이름을 IDLE로 변경
        name = STATE.ESCAPE;
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


    }

    public override void Exit()
    {


        base.Exit();
    }
}