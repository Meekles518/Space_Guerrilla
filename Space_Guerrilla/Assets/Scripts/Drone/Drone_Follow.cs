using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Follow : Drone_State
{
    public Drone_Follow(GameObject _enemy, Transform _player, Drone_Control _control, float _currTime)
        :base(_enemy, _player, _control, _currTime)
    {
        name = STATE.FOLLOW;
    }//Drone_Follow

    //Follow State 진입 시
    public override void Enter()
    {
        //Follow State 진입 시의 Player와 Drone의 position 정보 저장하기
        control.Last_player = control.player.position;
        control.Last_drone = control.selfposition.position;

        //Follow State 진입 시의 Player와 Drone의 상대 위치 저장
        control.relativePosition = control.Last_drone - control.Last_player;
        control.FollowPosition = control.relativePosition + (Vector2)control.player.position;


        base.Enter();
    }//Enter

    //Follow State 일 때
    public override void FixedUpdate()
    {
        //Player의 이동을 반영해 FollowPosition 을 저장
        control.FollowPosition = control.relativePosition + (Vector2)control.player.position;

        //산개 상태가 아닐 경우
        if(!control.isSpread)
        {
            //다음 State를 Idle로 변경
            nextState = new Drone_Idle(enemy, player, control, currTime);
            stage = EVENT.EXIT;
        }    

        //산개 상태에서, 마우스의 위치가 변경되어서 마우스-드론-Player의 세 점을 연결할 때의 각도가
        //90도를 넘어서, 드론이 Player 쪽으로 이동해야 한다면
        else if (control.Over90or270())
        {
            //다음 State를 Spread로 변경
            nextState = new Drone_Spread(enemy, player, control, currTime);
            stage = EVENT.EXIT;
        }


        //Drone이 다시 Player와 일정 거리 이내로 가까워지면
        else if (control.Player_drone_Distance < control.P_d_maxDistance)
        {
            //다음 State를 Spread로 변경
            nextState = new Drone_Spread(enemy, player, control, currTime);
            stage = EVENT.EXIT;
        }

      


    }//FixedUpdate

    //Follo State Exit
    public override void Exit()
    {
        base.Exit();
    }//Exit


}
