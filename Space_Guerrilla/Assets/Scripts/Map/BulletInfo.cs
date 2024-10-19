using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//총알에 관한 기본 정보를 저장할 스크립트
//추가로 관리해야 할 데이터가 있다면, 이 클래스를 상속받은 후에 추가로 변수를 선언하고, 값을 부여하면 됨
public class BulletInfo : MonoBehaviour
{

    //Player Bullet 스크립트에 넣어야 할 변수값들
    public float speed { get; set; } // 총알의 속도, CruiseMissile 스크립트도 speed 변수가 필요함
    public float spreadRange { get; set; } // 탄퍼짐 정도


    //Player CruiseMissile 스크립트에 넣어야 할 변수값들   
    public float lifespan { get; set; } //미사일의 생존주기
    public float Scan_range { get; set; } //미사일의 스캔 범위
    public float rotateSpeed { get; set; } // 회전 속도

    //Player Bullet의 ShipEntity 스크립트에 넣어야 할 변수값들
    public float maxhealth { get; set; } //총알의 최대체력
    public float shield { get; set; } // 총알의 방어도
    public float damage { get; set; } //총알의 공격력(방어력)
    public float defensestat { get; set; } // 총알의 방호력(우주선의 damage와 총 health+shield에 영향을줌)

    public float health { get; set; } // 총알의 현재체력
    public float rebound { get; set; } // 총알의 반동

    public float collideRate { get; set; } // 충돌판정을 시행하는 주기






}
