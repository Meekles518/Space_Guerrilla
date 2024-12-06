using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//총알에 관한 기본 정보를 저장할 스크립트
//추가로 관리해야 할 데이터가 있다면, 이 클래스를 상속받은 후에 추가로 변수를 선언하고, 값을 부여하면 됨
public abstract class BulletInfo : MonoBehaviour
{

    public int level = 1; // 총알의 레벨


    //Player Or Enemy Bullet 스크립트에 넣어야 할 변수값들
    public float speed; // 총알의 속도
    public float spreadRange; // 탄퍼짐 정도


    //Player CruiseMissile 스크립트에 넣어야 할 변수값들   
    public float missileSpeed; //미사일의 속도
    public float lifespan; //미사일의 생존주기
    public float Scan_range; //미사일의 스캔 범위
    public float rotateSpeed; // 회전 속도

    //Bullet의 ShipEntity 스크립트에 넣어야 할 변수값들
    public float maxhealth; //총알의 최대체력
    public float shield; // 총알의 방어도
    public float damage; //총알의 공격력(방어력)
    public float defensestat; // 총알의 방호력(우주선의 damage와 총 health+shield에 영향을줌)
    public float health; // 총알의 현재체력
    public float rebound; // 총알의 반동
    public float collideRate; // 충돌판정을 시행하는 주기

    public abstract void init();

    //레벨 별 오를 능력치 및 수치를 하위 클래스 구현 시 설정
    public abstract void lvlUp();

    public void setLevel(int lvl)
    {
        level = lvl;
    }

    //Bullet 들의 ShipEntity의 값들만 미리 BulletInfo에 넣어두는 메서드
    //CrieseMissile 은 모든 우주선이 가지는 것이 아니기에, 각 하위 클래스에서 따로 처리하게 함
    public void loadPublicData(BulletInfo mapBulletInfo)
    {
        //
        speed = mapBulletInfo.speed; // 총알의 속도, CruiseMissile 스크립트도 speed 변수가 필요함
        spreadRange = mapBulletInfo.spreadRange; // 탄퍼짐 정도

        //Bullet의 ShipEntity 스크립트에 넣어야 할 변수값들
        maxhealth = mapBulletInfo.maxhealth; //총알의 최대체력
        shield = mapBulletInfo.shield; // 총알의 방어도
        damage = mapBulletInfo.damage; //총알의 공격력(방어력)
        defensestat = mapBulletInfo.defensestat; // 총알의 방호력(우주선의 damage와 총 health+shield에 영향을줌)
        health = mapBulletInfo.health; // 총알의 현재체력
        rebound = mapBulletInfo.rebound; // 총알의 반동
        collideRate = mapBulletInfo.collideRate; // 충돌판정을 시행하는 주기
    }

    //ShipEntity 스크립트에 데이터를 부여하는 메서드
    public void insertDataToShipEntity(ShipEntity shipEntity)
    {
        shipEntity.maxhealth = maxhealth; //총알의 현재체력
        shipEntity.shield = shield; // 총알의 방어도
        shipEntity.damage = damage; //총알의 공격력(방어력)
        shipEntity.defensestat = defensestat; // 총알의 방호력(우주선의 damage와 총 health+shield에 영향을줌)
        shipEntity.health = health;
        shipEntity.rebound = rebound;
        shipEntity.collideRate = collideRate;
    }

    //각 우주선의 총알 정보 별로 따로 설정이 필요한 값들을 하위 클래스에서 처리하는 메서드
    public abstract void loadDataFromMap(BulletInfo mapBulletInfo);

}
