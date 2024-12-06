using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//EnemyOne의 기본 정보를 담은 스크립트
public class EnemyOneInfo : ShipInfo
{
    //생성자에 EnemyOne의 기본 정보 부여
    //값은 임의로 설정

    //Enemy Control 스크립트에 들어가야 하는 변수값들
    public float MaxAtkRange; // 최대 공격 사거리

    public float smallAgrro; // 작은 어그로 범위
    public float largeAgrro; // 큰 어그로 범위
    public float TimeTillAtk; // 공격형적 추적 타이머

    //Enemy Movement 스크립트에 들어가야 하는 변수값들
    public float OptimalAtkRange; // 적정 공격 사거리


    //초기값 초기화
    public override void init()
    {
        //Shooter 스크립트에 들어가야 하는 변수값들
        bulletType1 = 1; // 발사하는 총알의 타입 예) 플레이어 총알, 적 총알 등
        magCapacity1 = 10; // 탄창 용량
        magAmmo1 = 10; // 현재 탄창에 남아있는 탄약
        recoil1 = 0.1f; // 발사시 반동
        reloadTime1 = 3f; // 재장전 소요 시간
        timeBetFire1 = 1f; // 투사체 발사 간격
        projectilesPerFire1 = 1; // 한번 클릭시 발사하는 투사체 수
        timeBetProjectiles1 = 0.2f; // 한번 클릭시 발사되는 투사체 간의 시간 간격
        reloadInterval1 = 0.1f;

        //Enemy Movement 스크립트에 들어가야 하는 변수값들
        moveSpeed = 10f; // 이동 속도
        rotateSpeed = 10f; // 회전 속도
        OptimalAtkRange = 15f; // 적정 공격 사거리

        //Enemy Control 스크립트에 들어가야 하는 변수값들
        MaxAtkRange = 20f; // 최대 공격 사거리
        smallAgrro = 25f; // 작은 어그로 범위
        largeAgrro = 50f; // 큰 어그로 범위
        TimeTillAtk = 3f; // 공격형적 추적 타이머

        //ShipEntity 스크립트에 들어가야 하는 변수값들
        maxhealth = 30f; //우주선의 현재체력
        shield = 0f; // 우주선의 방어도
        damage = 10f; //우주선의 공격력(방어력)
        defensestat = 0f; // 우주선의 방호력(우주선의 damage와 총 health+shield에 영향을줌)
        health = 30f;
        rebound = 0f;
        collideRate = 0.1f;
    }


    public override void lvlUp()
    {
        throw new System.NotImplementedException();
    }

    //Enemy_Circle에 있는 ShipInfo에서 데이터를 가져와 우주선의 ShipInfo에 저장하는 메서드
    public override void loadDataFromMap(ShipInfo mapShipInfo)
    {
        // mapShipInfo가 AegisInfo 타입인지 확인
        if (mapShipInfo is not EnemyOneInfo)
        {
            Debug.LogError("mapShipInfo is not of type EnemyOneInfo");
            return;
        }

        // mapShipInfo를 AegisInfo로 캐스팅
        EnemyOneInfo enemyOneInfo = (EnemyOneInfo)mapShipInfo;

        base.loadPublicData(enemyOneInfo);

        this.OptimalAtkRange = enemyOneInfo.OptimalAtkRange;


        this.MaxAtkRange = enemyOneInfo.MaxAtkRange;
        this.smallAgrro = enemyOneInfo.smallAgrro;
        this.largeAgrro = enemyOneInfo.largeAgrro;
        this.TimeTillAtk = enemyOneInfo.TimeTillAtk;

        var enemyMovement = GetComponent<Enemy_Movement>(); //EnemyMovement 가져오기
        var enemyControl = GetComponent<Enemy_Control>(); //EnemyControl 가져오기

        enemyMovement.moveSpeed = moveSpeed; //EnemyMovement에 moveSpeed 할당
        enemyMovement.rotateSpeed = rotateSpeed; //EnemyMovement에 rotateSpeed 할당
        enemyMovement.OptimalAtkRange = OptimalAtkRange; //EnemyMovement에 OptimalAtkRange 할당

        enemyControl.MaxAtkRange = MaxAtkRange; //EnemyControl에 MaxAtkRange 할당
        enemyControl.smallAgrro = smallAgrro; //EnemyControl에 smallAgrro 할당
        enemyControl.largeAgrro = largeAgrro; //EnemyControl에 largeAgrro 할당
        enemyControl.TimeTillAtk = TimeTillAtk; //EnemyControl에 TimeTillAtk 할당



    }//loadDataFromMap

}
