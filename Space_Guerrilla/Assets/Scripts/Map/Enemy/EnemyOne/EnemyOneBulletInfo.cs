using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneBulletInfo : BulletInfo
{
    //초기값 초기화
    public override void init()
    {
        //Enemy Bullet 스크립트에 넣어야 할 변수값들
        speed = 10f; // 총알의 속도
        spreadRange = 0.3f; // 탄퍼짐 정도

        //Enemy Bullet의 ShipEntity 스크립트에 넣어야 할 변수값들
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

    public override void loadDataFromMap(BulletInfo mapBulletInfo)
    {
        // mapShipInfo가 EnemyOneBulletInfo 타입인지 확인
        if (mapBulletInfo is not EnemyOneBulletInfo)
        {
            Debug.LogError("mapBulletInfo is not of type EnemyOneBulletInfo");
            return;
        }

        // mapBulletInfo를 EnemyOneBulletInfo로 캐스팅
        EnemyOneBulletInfo enemyOneBulletInfo = (EnemyOneBulletInfo)mapBulletInfo;

        base.loadPublicData(enemyOneBulletInfo); // 부모 클래스의 loadDataFromMap 호출

    }

}
