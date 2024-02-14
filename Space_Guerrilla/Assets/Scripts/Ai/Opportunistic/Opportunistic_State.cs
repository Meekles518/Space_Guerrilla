using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using static UnityEngine.RuleTile.TilingRuleOutput;

// 기회주의형 적의 State들의 부모 클래스
public class Opportunistic_State
{
    // State의 종류를 enum으로 선언
    public enum STATE
    {
        IDLE, // 초기 상태
        PURSUE, // 추적 상태
        WAIT, // 대기 상태
        GOBACK, // 복귀 상태
        RETREAT // 후퇴 상태
    };

    // 각 State의 진입, 진행중, 나올때 실행할 매서드들을 enum으로 선언
    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    };

    public STATE name; // 자신의 스테이트를 지정할 변수
    protected EVENT stage; // 현재 진행되고 있는 이벤트
    protected GameObject enemy; // 적 오브젝트
    protected Transform player; // 플레이어 트랜스폼
    protected Opportunistic_State nextState; // 다음으로 넘어갈 스테이트 설정
    protected Enemy_Control control; // 컨트롤 컴포넌트

    // Opportunistic_State 형식 선언
    // 앞으로 이 클래스를 부모로 하는 자식 클래스들은 enemy, player, control을 선언 없이 사용가능하게 하는 효과
    // State 진입시 자동으로 현재 매서드를 Enter로 설정
    public Opportunistic_State(GameObject _enemy, Transform _player, Enemy_Control _control)
    {
        enemy = _enemy;
        player = _player;
        control = _control;
        stage = EVENT.ENTER;
    }

    // Enter, Update, Exit 매서드 선언
    // 모든 매서드들은 stage를 다음에 올 매서드로 갱신
    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void FixedUpdate() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    // 다음 매서드가 실행될 시기에 Ai 컴포넌트에서 다음 매서드를 결정
    public Opportunistic_State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) FixedUpdate();
        if (stage == EVENT.EXIT)
        {
            // Exit 매서드는 현재 스테이트를 다음 스테이트로 변경
            Exit();
            return nextState;
        }
        return this;
    }

    // 기회주의형적의 어그로 여부를 판단할 불 매서드
    public bool Aggro()
    {
        float PlayertoSpawn = control.PlayertoFleetSpawn; // 플레이어와 스폰위치 사이의 거리
        float smallAgrro = control.smallAgrro; // 작은 어그로 범위
        float largeAgrro = control.largeAgrro; // 큰 어그로 범위

        // 플레이어가 큰 어그로 범위 안에 들어와 있다면
        if (PlayertoSpawn <= largeAgrro)
        {

            // 작은 어그로 범위에 들어왔는지 판단, 그렇다면 어그로 끌림
            if (PlayertoSpawn <= smallAgrro)
            {
                return true;
            }
            // 작은 어그로 범위 밖, 큰 어그로 범위 안이라면
            else
            {
                // 게임매니저에서 playerInput을 가져와 플레이어가 발사를 하는지 감시, 발사시 어그로 끌림
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