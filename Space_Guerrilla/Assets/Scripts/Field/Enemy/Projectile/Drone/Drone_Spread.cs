using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Spread : Drone_State
{
    //드론의 Spread 상태를 정의할 Script
    public Drone_Spread(GameObject _enemy, Transform _player, Drone_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        name = STATE.SPREAD;

    }//Drone_Spread

    //Spread State 진입 시
    public override void Enter()
    {
        //Debug.Log("Spread Enter");
        base.Enter();

    }//Enter

    //Spread State 일 때
    public override void FixedUpdate()
    {   
        // 산개 상태가 아닐 경우
        if(!control.isSpread)
        {
            //Debug.Log("Idle");
            //다음 State를 Idle로 변경
            nextState = new Drone_Idle(enemy, player, control, currTime);
            stage = EVENT.EXIT;
        }

        //산개 상태에서, Player로부터 일정 거리 이상 멀어지고,
        //마우스 - 드론 - Player 세 점을 연결한 각도가 90도 이하라, 산개 방향이 Player로부터 멀어진다면
        else if (control.Player_drone_Distance > control.P_d_maxDistance && !control.Over90or270())
        {
            // Follow State로 변경
            //Debug.Log("Follow");
            nextState = new Drone_Follow(enemy, player, control, currTime);
            stage = EVENT.EXIT;

        }
       

    }//FixedUpdate

    //State 탈출
    public override void Exit()
    {
        //Debug.Log("Spread Exit");
        base.Exit();
    }//Exit





}
