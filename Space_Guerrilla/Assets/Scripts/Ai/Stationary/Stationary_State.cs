using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using static UnityEngine.RuleTile.TilingRuleOutput;

// ������ ���� State���� �θ� Ŭ����
public class Stationary_State
{
    // State�� ������ enum���� ���� (��� ������ ������ ����)
    public enum STATE
    {
        IDLE, // �ʱ� ����
        PURSUE, // ���� ����
        WAIT, // ��� ����
        GOBACK, // ���� ����
        RETREAT // ���� ����
    };

    // �� State�� ����, ������, ���ö� ������ �ż������ enum���� ����
    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    };

    public STATE name; // �ڽ��� ������Ʈ�� ������ ����
    protected EVENT stage; // ���� ����ǰ� �ִ� �̺�Ʈ
    protected GameObject enemy; // �� ������Ʈ
    protected Transform player; // �÷��̾� Ʈ������
    protected Stationary_State nextState; // �������� �Ѿ ������Ʈ ����
    protected Enemy_Control control; // ��Ʈ�� ������Ʈ

    // Offensive_State ���� ����
    // ������ �� Ŭ������ �θ�� �ϴ� �ڽ� Ŭ�������� enemy, player, control, currTime�� ���� ���� �� �ʿ䰡 ����
    // State ���Խ� �ڵ����� ���� �ż��带 Enter�� ����
    public Stationary_State(GameObject _enemy, Transform _player, Enemy_Control _control)
    {
        enemy = _enemy;
        player = _player;
        control = _control;
        stage = EVENT.ENTER;
    }

    // Enter, Update, Exit �ż��� ����
    // ��� �ż������ stage�� ������ �� �ż���� ����
    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void FixedUpdate() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    // ���� �ż��尡 ����� �ñ⿡ Ai ������Ʈ���� ���� �ż��带 ����
    public Stationary_State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) FixedUpdate();
        if (stage == EVENT.EXIT)
        {
            // Exit �ż���� ���� ������Ʈ�� ���� ������Ʈ�� ����
            Exit();
            return nextState;
        }
        return this;
    }

    // ���� ��׷� ���θ� �Ǵ��� �� �ż���
    public bool Aggro()
    {
        float PlayertoSpawn = control.PlayertoFleetSpawn; // �÷��̾�� ���� ������ġ ������ �Ÿ�
        float smallAgrro = control.smallAgrro; // ���� ��׷� ����
        float largeAgrro = control.largeAgrro; // ū ��׷� ����

        // �÷��̾ ū ��׷� ���� �ȿ� ���� �ִٸ�
        if (PlayertoSpawn <= largeAgrro)
        {
            // ���� ��׷� ������ ���Դ��� �Ǵ�, �׷��ٸ� ��׷� ����
            if (PlayertoSpawn <= smallAgrro)
            {
                return true;
            }
            // ���� ��׷� ���� ��, ū ��׷� ���� ���̶��
            else
            {
                // ���ӸŴ������� playerInput�� ������ �÷��̾ �߻縦 �ϴ��� ����, �߻�� ��׷� ����
                if (GameManager.instance.playerInput.fire)
                {
                    return true;
                }
                return false;
            }
        }
        return false;
    }
}