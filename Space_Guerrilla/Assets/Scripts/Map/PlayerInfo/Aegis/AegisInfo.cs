using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AegisInfo : ShipInfo
{

    //초기값 초기화
    public override void init()
    {
        //Shooter 스크립트에 들어가야 하는 변수값들, 주 무기의 값들
        bulletType1 = 0; // 발사하는 총알의 타입 예) 플레이어 총알, 적 총알 등
        magCapacity1 = 10; // 탄창 용량
        magAmmo1 = 10; // 현재 탄창에 남아있는 탄약
        recoil1 = 0.1f; // 발사시 반동
        reloadTime1 = 3f; // 재장전 소요 시간
        timeBetFire1 = 1f; // 투사체 발사 간격
        projectilesPerFire1 = 1; // 한번 클릭시 발사하는 투사체 수
        timeBetProjectiles1 = 0.2f; // 한번 클릭시 발사되는 투사체 간의 시간 간격
        reloadInterval1 = 0.1f;


        //ShipEntity 스크립트에 들어가야 하는 변수값들
        maxhealth = 100f; //우주선의 최대체력
        shield = 10f; // 우주선의 방어도
        damage = 10f; //우주선의 공격력(방어력)
        defensestat = 3f; // 우주선의 방호력(우주선의 damage와 총 health+shield에 영향을줌)
        health = 100f; // 우주선의 현재체력
        rebound = 0.1f; // 우주선의 반동
        collideRate = 0.1f; // 충돌판정을 시행하는 주기

        //PlayerMovement 스크립트에 들어가야 하는 변수값들
        moveSpeed = 10f; // 이동 속도
        rotateSpeed = 10f; // 회전 속도

    }

    public override void lvlUp()
    {
        throw new System.NotImplementedException();
    }

    //Map 에 있는 Player의 ShipInfo에서 데이터를 가져와 우주선의 ShipInfo에 저장하는 메서드
    public override void loadDataFromMap(ShipInfo mapShipInfo)
    {
        // mapShipInfo가 AegisInfo 타입인지 확인
        if (mapShipInfo is not AegisInfo)
        {
            Debug.LogError("mapShipInfo is not of type AegisInfo");
            return;
        }

        // mapShipInfo를 AegisInfo로 캐스팅
        AegisInfo aegisInfo = (AegisInfo)mapShipInfo;

        base.loadPublicData(aegisInfo); // 부모 클래스의 loadDataFromMap 호출

        //PlayerMovement 스크립트에 들어가야 하는 변수값들
        moveSpeed = aegisInfo.moveSpeed; // 이동 속도
        rotateSpeed = aegisInfo.rotateSpeed; // 회전 속도

        var playerMovement = GetComponent<PlayerMovement>(); //PlayerMovement 가져오기
        playerMovement.moveSpeed = moveSpeed; //PlayerMovement에 moveSpeed 할당
        playerMovement.rotateSpeed = rotateSpeed; //PlayerMovement에 rotateSpeed 할당



    } //loadDataFromMap


}
