using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//드론 State들의 부모 클래스
public class Drone_State 
{
    // State의 종류를 enum으로 선언(모든 드론이 공유)
    public enum STATE
    {
        IDLE,   //초기 상태
        SPREAD, //산개 상태
        ENGAGE, //교전 상태
        FOLLOW, //플레이어 추적 상태

    };

    // 각 State의 진입, 진행중, 나올 때 실행할 매서드르 enum으로 선언
    public enum EVENT
    {
        ENTER,  //State 진입
        UPDATE, //State 유지
        EXIT    //State 탈출(변경)
    };

    public STATE name;  // 자신의 현재 STATE를 지정할 변수
    public EVENT stage;  // 현재 진행되고 있는 이벤트
    protected GameObject enemy; // 적 오브젝트
    protected Transform player; // 플레이어의 Transform
    public Drone_State nextState; // 다음으로 넘어갈 State 설정
    protected Drone_Control control;  //Control 컴포넌트
    public float currTime;  // 타이머, 필요 없을 수도 있음

    //  Drone_State 의 형식 선언하기
    //  앞으로 이 클래스를 부모로 하는 자식 클래스들은 enemy, player, control, currTime을 재 선언할 필요 없음
    //  State 진입 시 자동으로 현재 매서드를 Enter로 결정
    public Drone_State(GameObject _enemy, Transform _player, Drone_Control _control, float _currTime)
    {
        enemy = _enemy;
        player = _player;
        control = _control;
        currTime = _currTime;
        stage = EVENT.ENTER;


    }

    // Enter, update, Exit 매서드 선언
    //모든 매서드들은 Stage를 다음에 올 매서드로 갱신해준다.
    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void FixedUpdate() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    // 다음 매서드가 실행될 시기에, Ai 컴포넌트에서 다음 매서드를 결정하게 하는 함수
    public Drone_State Process()
    {
        //Debug.Log("Process");
        //Debug.Log(stage);
        if (stage == EVENT.ENTER) Enter();  //ENTER State를 UPDATE로 변경
        if (stage == EVENT.UPDATE) FixedUpdate();   //UPDATE State를 유지
        if (stage == EVENT.EXIT)
        {
            //Exit 매서드는 현재 State 를 다음 State로 변경시켜준다
            Exit();
            return nextState;
        }

        return this;

    }

   



}
