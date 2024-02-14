using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Engage : Drone_State
{
    //드론의 Engage 상태를 정의할 Script
    public Drone_Engage(GameObject _enemy, Transform _player, Drone_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        name = STATE.ENGAGE;
    }

    //Engage State 진입 시
    public override void Enter()
    {
        base.Enter();

    }//Enter

    //Engage State인 동안
    public override void FixedUpdate()
    {
        //산개 상태라면
        if (control.isSpread)
        {
            //다음 State를 Spread로 변경
            nextState = new Drone_Spread(enemy, player, control, currTime);
            stage = EVENT.EXIT;

        }

        //Auto 모드가 아니거나, Drone 주변에 Target이 존재하지 않는다면
        else if (!control.playerInput.auto || control.droneTarget == null)
        {
            //다음 State를 Idle로 변경
            nextState = new Drone_Idle(enemy, player, control, currTime);
            stage = EVENT.EXIT;

        }

     

    }//FixedUpdate

    //State 탈출
    public override void Exit()
    {
        base.Exit();
    }//Exit

}
