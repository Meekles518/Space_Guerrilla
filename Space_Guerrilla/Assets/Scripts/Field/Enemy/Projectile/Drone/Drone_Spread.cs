using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Spread : Drone_State
{
    //����� Spread ���¸� ������ Script
    public Drone_Spread(GameObject _enemy, Transform _player, Drone_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        name = STATE.SPREAD;

    }//Drone_Spread

    //Spread State ���� ��
    public override void Enter()
    {
        //Debug.Log("Spread Enter");
        base.Enter();

    }//Enter

    //Spread State �� ��
    public override void FixedUpdate()
    {   
        // �갳 ���°� �ƴ� ���
        if(!control.isSpread)
        {
            //Debug.Log("Idle");
            //���� State�� Idle�� ����
            nextState = new Drone_Idle(enemy, player, control, currTime);
            stage = EVENT.EXIT;
        }

        //�갳 ���¿���, Player�κ��� ���� �Ÿ� �̻� �־�����,
        //���콺 - ��� - Player �� ���� ������ ������ 90�� ���϶�, �갳 ������ Player�κ��� �־����ٸ�
        else if (control.Player_drone_Distance > control.P_d_maxDistance && !control.Over90or270())
        {
            // Follow State�� ����
            //Debug.Log("Follow");
            nextState = new Drone_Follow(enemy, player, control, currTime);
            stage = EVENT.EXIT;

        }
       

    }//FixedUpdate

    //State Ż��
    public override void Exit()
    {
        //Debug.Log("Spread Exit");
        base.Exit();
    }//Exit





}
