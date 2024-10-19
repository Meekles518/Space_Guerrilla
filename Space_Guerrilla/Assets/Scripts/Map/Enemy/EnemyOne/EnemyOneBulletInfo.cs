using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneBulletInfo : BulletInfo
{


    //생성자에 EnemyOne의 기본 데이터를 설정
    //모든 값들은 임의 부여
    public EnemyOneBulletInfo()
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

    }//생성자 끝

}
