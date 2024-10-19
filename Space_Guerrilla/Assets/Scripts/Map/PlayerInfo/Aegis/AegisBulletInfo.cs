using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AegisBulletInfo : BulletInfo
{
    //생성자에 AegisBulletInfo의 기본 데이터를 설정. 
    //모든 값들은 임의로 막 설정함.
    public AegisBulletInfo()
    {
        //Player Bullet 스크립트에 넣어야 할 변수값들
        speed = 10f; // 총알의 속도, CruiseMissile 스크립트도 speed 변수가 필요함
        spreadRange = 0.3f; // 탄퍼짐 정도

        //Player CruiseMissile 스크립트에 넣어야 할 변수값들
        lifespan = 10f; //미사일의 생존주기
        Scan_range = 10f; //미사일의 스캔 범위
        rotateSpeed = 10f; // 회전 속도

        //Player Bullet의 ShipEntity 스크립트에 넣어야 할 변수값들
        maxhealth = 30f; //총알의 최대체력
        shield = 0f; // 총알의 방어도
        damage = 10f; //총알의 공격력(방어력)
        defensestat = 0f; // 총알의 방호력(우주선의 damage와 총 health+shield에 영향을줌)
        health = 30f; // 총알의 현재체력
        rebound = 0f; // 총알의 반동
        collideRate = 0.1f; // 충돌판정을 시행하는 주기


    }


}
