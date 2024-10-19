using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//EnemyOne의 기본 정보를 담은 스크립트
public class EnemyOneInfo : ShipInfo
{
    //생성자에 EnemyOne의 기본 정보 부여
    //값은 임의로 설정

    //Enemy Control 스크립트에 들어가야 하는 변수값들
    public float MaxAtkRange { get; set; } // 최대 공격 사거리

    public float smallAgrro { get; set; } // 작은 어그로 범위
    public float largeAgrro { get; set; } // 큰 어그로 범위
    public float TimeTillAtk { get; set; } // 공격형적 추적 타이머

    //Enemy Movement 스크립트에 들어가야 하는 변수값들
    public float OptimalAtkRange { get; set; } // 적정 공격 사거리


    public EnemyOneInfo()
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

        //Shooter 및 Enemy Shooter 스크립트에 들어가야 하는 변수값들
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
}
