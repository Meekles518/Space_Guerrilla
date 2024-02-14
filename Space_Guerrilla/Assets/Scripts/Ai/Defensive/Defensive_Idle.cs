using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 수비형 적이 초기 스폰위치에 있는 상태
public class Defensive_Idle : Defensive_State
{
    // Idle 스테이트 선언
    public Defensive_Idle(GameObject _enemy, Transform _player, Enemy_Control _control)
        : base(_enemy, _player, _control)
    {

        name = STATE.IDLE;
    }

    // Idle 진입시
    public override void Enter()
    {
        // 수비형적의 어그로 여부 = 거짓
        GameManager.instance.isDefensiveEngage = false;
        Debug.Log("Idle"); // 로그에 뜨게함
        base.Enter();
    }

    // Idle 상태일 때
    public override void FixedUpdate()
    {
        // 사격 불가
        control.isShoot = false;

        // 어그로가 끌렸을 시
        if (Aggro())
        {
            // 다음 스테이트를 Pursue로 설정
            nextState = new Defensive_Pursue(enemy, player, control);
            // 다음 이벤트를 Exit으로 설정
            stage = EVENT.EXIT;
        }
        
    }

    public override void Exit()
    {


        base.Exit();
    }
}