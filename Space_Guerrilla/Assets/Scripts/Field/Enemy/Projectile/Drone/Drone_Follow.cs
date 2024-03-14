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

    //Follow State ���� ��
    public override void Enter()
    {
        //Follow State ���� ���� Player�� Drone�� position ���� �����ϱ�
        control.Last_player = control.player.position;
        control.Last_drone = control.selfposition.position;

        //Follow State ���� ���� Player�� Drone�� ��� ��ġ ����
        control.relativePosition = control.Last_drone - control.Last_player;
        control.FollowPosition = control.relativePosition + (Vector2)control.player.position;


        base.Enter();
    }//Enter

    //Follow State �� ��
    public override void FixedUpdate()
    {
        //Player�� �̵��� �ݿ��� FollowPosition �� ����
        control.FollowPosition = control.relativePosition + (Vector2)control.player.position;

        //�갳 ���°� �ƴ� ���
        if(!control.isSpread)
        {
            //���� State�� Idle�� ����
            nextState = new Drone_Idle(enemy, player, control, currTime);
            stage = EVENT.EXIT;
        }    

        //�갳 ���¿���, ���콺�� ��ġ�� ����Ǿ ���콺-���-Player�� �� ���� ������ ���� ������
        //90���� �Ѿ, ����� Player ������ �̵��ؾ� �Ѵٸ�
        else if (control.Over90or270())
        {
            //���� State�� Spread�� ����
            nextState = new Drone_Spread(enemy, player, control, currTime);
            stage = EVENT.EXIT;
        }


        //Drone�� �ٽ� Player�� ���� �Ÿ� �̳��� ���������
        else if (control.Player_drone_Distance < control.P_d_maxDistance)
        {
            //���� State�� Spread�� ����
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
