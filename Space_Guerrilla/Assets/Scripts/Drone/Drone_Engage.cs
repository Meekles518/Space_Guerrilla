using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Engage : Drone_State
{
    //����� Engage ���¸� ������ Script
    public Drone_Engage(GameObject _enemy, Transform _player, Drone_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        name = STATE.ENGAGE;
    }

    //Engage State ���� ��
    public override void Enter()
    {
        base.Enter();

    }//Enter

    //Engage State�� ����
    public override void FixedUpdate()
    {
        //�갳 ���¶��
        if (control.isSpread)
        {
            //���� State�� Spread�� ����
            nextState = new Drone_Spread(enemy, player, control, currTime);
            stage = EVENT.EXIT;

        }

        //Auto ��尡 �ƴϰų�, Drone �ֺ��� Target�� �������� �ʴ´ٸ�
        else if (!control.playerInput.auto || control.droneTarget == null)
        {
            //���� State�� Idle�� ����
            nextState = new Drone_Idle(enemy, player, control, currTime);
            stage = EVENT.EXIT;

        }

     

    }//FixedUpdate

    //State Ż��
    public override void Exit()
    {
        base.Exit();
    }//Exit

}
