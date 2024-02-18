using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//��� State���� �θ� Ŭ����
public class Drone_State 
{
    // State�� ������ enum���� ����(��� ����� ����)
    public enum STATE
    {
        IDLE,   //�ʱ� ����
        SPREAD, //�갳 ����
        ENGAGE, //���� ����
        FOLLOW, //�÷��̾� ���� ����

    };

    // �� State�� ����, ������, ���� �� ������ �ż��帣 enum���� ����
    public enum EVENT
    {
        ENTER,  //State ����
        UPDATE, //State ����
        EXIT    //State Ż��(����)
    };

    public STATE name;  // �ڽ��� ���� STATE�� ������ ����
    public EVENT stage;  // ���� ����ǰ� �ִ� �̺�Ʈ
    protected GameObject enemy; // �� ������Ʈ
    protected Transform player; // �÷��̾��� Transform
    public Drone_State nextState; // �������� �Ѿ State ����
    protected Drone_Control control;  //Control ������Ʈ
    public float currTime;  // Ÿ�̸�, �ʿ� ���� ���� ����

    //  Drone_State �� ���� �����ϱ�
    //  ������ �� Ŭ������ �θ�� �ϴ� �ڽ� Ŭ�������� enemy, player, control, currTime�� �� ������ �ʿ� ����
    //  State ���� �� �ڵ����� ���� �ż��带 Enter�� ����
    public Drone_State(GameObject _enemy, Transform _player, Drone_Control _control, float _currTime)
    {
        enemy = _enemy;
        player = _player;
        control = _control;
        currTime = _currTime;
        stage = EVENT.ENTER;


    }

    // Enter, update, Exit �ż��� ����
    //��� �ż������ Stage�� ������ �� �ż���� �������ش�.
    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void FixedUpdate() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    // ���� �ż��尡 ����� �ñ⿡, Ai ������Ʈ���� ���� �ż��带 �����ϰ� �ϴ� �Լ�
    public Drone_State Process()
    {
        //Debug.Log("Process");
        //Debug.Log(stage);
        if (stage == EVENT.ENTER) Enter();  //ENTER State�� UPDATE�� ����
        if (stage == EVENT.UPDATE) FixedUpdate();   //UPDATE State�� ����
        if (stage == EVENT.EXIT)
        {
            //Exit �ż���� ���� State �� ���� State�� ��������ش�
            Exit();
            return nextState;
        }

        return this;

    }

   



}
