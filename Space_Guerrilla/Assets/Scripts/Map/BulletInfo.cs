using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//총알에 관한 기본 정보를 저장할 스크립트
//추가로 관리해야 할 데이터가 있다면, 이 클래스를 상속받은 후에 추가로 변수를 선언하고, 값을 부여하면 됨
public abstract class BulletInfo : MonoBehaviour
{

    public int level = 1; // 총알의 레벨


    //Player Bullet 스크립트에 넣어야 할 변수값들
    public float speed; // 총알의 속도, CruiseMissile 스크립트도 speed 변수가 필요함
    public float spreadRange; // 탄퍼짐 정도


    //Player CruiseMissile 스크립트에 넣어야 할 변수값들   
    public float lifespan; //미사일의 생존주기
    public float Scan_range; //미사일의 스캔 범위
    public float rotateSpeed; // 회전 속도

    //Player Bullet의 ShipEntity 스크립트에 넣어야 할 변수값들
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


}
