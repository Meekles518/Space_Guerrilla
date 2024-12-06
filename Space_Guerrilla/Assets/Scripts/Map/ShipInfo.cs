using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//우주선의 기본 데이터를 저장할 스크립트
//추가로 관리해야 할 데이터가 있다면, 이 클래스를 상속받은 후에 추가로 변수를 선언하고, 값을 부여하면 됨
public abstract class ShipInfo : MonoBehaviour
{

    public int level = 1; // 우주선의 레벨

    //Shooter 스크립트에 들어가야 하는 변수값들, 주 무기의 값들
    public int bulletType1; // 발사하는 총알의 타입 예) 플레이어 총알, 적 총알 등
    public int magCapacity1; // 탄창 용량
    public int magAmmo1; // 현재 탄창에 남아있는 탄약
    public float recoil1; // 발사시 반동
    public float reloadTime1; // 재장전 소요 시간
    public float timeBetFire1; // 투사체 발사 간격
    public int projectilesPerFire1;  // 한번 클릭시 발사하는 투사체 수
    public float timeBetProjectiles1; // 한번 클릭시 발사되는 투사체 간의 시간 간격
    public float reloadInterval1;


    //보조 무기는 매 우주선마다 차이가 있을 수 있으니, 기본 데이터에서 관리하지 않음



    //ShipEntity 스크립트에 들어가야 하는 변수값들
    public float maxhealth; //우주선의 최대체력
    public float shield; // 우주선의 방어도
    public float damage; //우주선의 공격력(방어력)
    public float defensestat; // 우주선의 방호력(우주선의 damage와 총 health+shield에 영향을줌)
    public float health; // 우주선의 현재체력
    public float rebound; // 우주선의 반동
    public float collideRate; // 충돌판정을 시행하는 주기


    //PlayerMovement 스크립트에 들어가야 하는 변수값들
    public float moveSpeed; // 이동 속도
    public float rotateSpeed; // 회전 속도


    public abstract void init();

    //레벨 별 오를 능력치 및 수치를 하위 클래스 구현 시 설정
    public abstract void lvlUp();

    public void setLevel(int lvl)
    {
        level = lvl;
    }

    public void loadPublicData(ShipInfo mapShipInfo)
    {
        //Shooter 스크립트에 들어가야 하는 변수값들, 주 무기의 값들
        bulletType1 = mapShipInfo.bulletType1; // 발사하는 총알의 타입 예) 플레이어 총알, 적 총알 등
        magCapacity1 = mapShipInfo.magCapacity1; // 탄창 용량
        magAmmo1 = mapShipInfo.magAmmo1; // 현재 탄창에 남아있는 탄약
        recoil1 = mapShipInfo.recoil1; // 발사시 반동
        reloadTime1 = mapShipInfo.reloadTime1; // 재장전 소요 시간
        timeBetFire1 = mapShipInfo.timeBetFire1; // 투사체 발사 간격
        projectilesPerFire1 = mapShipInfo.projectilesPerFire1; // 한번 클릭시 발사하는 투사체 수
        timeBetProjectiles1 = mapShipInfo.timeBetProjectiles1; // 한번 클릭시 발사되는 투사체 간의 시간 간격
        reloadInterval1 = mapShipInfo.reloadInterval1;


        //ShipEntity 스크립트에 들어가야 하는 변수값들
        maxhealth = mapShipInfo.maxhealth; //우주선의 최대체력
        shield = mapShipInfo.shield; // 우주선의 방어도
        damage = mapShipInfo.damage; //우주선의 공격력(방어력)
        defensestat = mapShipInfo.defensestat; // 우주선의 방호력(우주선의 damage와 총 health+shield에 영향을줌)
        health = mapShipInfo.health; // 우주선의 현재체력
        rebound = mapShipInfo.rebound; // 우주선의 반동
        collideRate = mapShipInfo.collideRate; // 충돌판정을 시행하는 주기


        var shooters = GetComponentsInChildren<Shooter>(); //shooter들 자식에서 가져오기
        var shipEntity = GetComponent<ShipEntity>(); //ShipEntity 가져오기

        //우주선 Prefab의 컴포넌트에 값을 부여
        for (int i = 0; i < shooters.Length; i++)
        {
            shooters[i].bulletType = bulletType1;
            shooters[i].magCapacity = magCapacity1;
            shooters[i].magAmmo = magAmmo1;
            shooters[i].recoil = recoil1;
            shooters[i].reloadTime = reloadTime1;
            shooters[i].timeBetFire = timeBetFire1;
            shooters[i].projectilesPerFire = projectilesPerFire1;
            shooters[i].timeBetProjectiles = timeBetProjectiles1;
            shooters[i].reloadInterval = reloadInterval1;
        }

        //ShipEntity 스크립트에 값을 부여
        shipEntity.maxhealth = maxhealth;
        shipEntity.shield = shield;
        shipEntity.damage = damage;
        shipEntity.defensestat = defensestat;
        shipEntity.health = health;
        shipEntity.rebound = rebound;
        shipEntity.collideRate = collideRate;

    }

    public abstract void loadDataFromMap(ShipInfo mapShipInfo);


}
