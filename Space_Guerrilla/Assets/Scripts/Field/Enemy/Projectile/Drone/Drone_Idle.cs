using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Idle : Drone_State
{
    //드론의 초기 기본 Idle 상태를 정의할 Script
    public Drone_Idle(GameObject _enemy, Transform _player, Drone_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        name = STATE.IDLE;
    }


    //Idle State 진입 시
    public override void Enter()
    {
        currTime = 0f;
        //Debug.Log("Idle Enter");
        base.Enter();
    }//Enter

    // Idle 상태일 때
    public override void FixedUpdate()
    {



        //산개 상태라면
        if (control.isSpread)
        {
            //Debug.Log("Spread");
            
            //다음 State를 Spread로 변경
            nextState = new Drone_Spread(enemy, player, control, currTime);
            stage = EVENT.EXIT;

        }


        //산개 상태가 아니고, 자동 모드이고, 드론의 Target이 설정되어 있다면
        else if (control.playerInput.isAuto && control.TargetPosition != null)
        {
            //다음 State를 Engage로 변경
            nextState = new Drone_Engage(enemy, player, control, currTime);
            stage = EVENT.EXIT;

        }


        
    }//FixedUpdate


    //State 변경 시 
    public override void Exit()
    {
        //Debug.Log("Spread Exit");
        base.Exit();
    }//Exit

}
