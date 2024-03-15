using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Player의 총알에 관한 정보를 저장할 스크립트
public class PlayerBulletInfo : MonoBehaviour
{

    //Player Bullet 스크립트에 넣어야 할 변수값들
    [Header("총알 속성, Player Bullet 스크립트로 넘어가야 할 변수들")]
    public float speed; // 총알의 속도, CruiseMissile 스크립트도 speed 변수가 필요함
    public float spreadRange; // 탄퍼짐 정도


    //Player CruiseMissile 스크립트에 넣어야 할 변수값들

    [Header("미사일 속성, Player CruiseMissile 스크립트로 넘어가야 할 변수들")]
    
    public float lifespan;
    public float Scan_range;
    public float rotateSpeed; // 회전 속도





    //Player Bullet의 ShipEntity 스크립트에 넣어야 할 변수값들
    [Header("Player Bullet의 ShipEntity 스크립트로 넘어가야 할 변수들")]
    [Header("오브젝트 스탯")]
    public float maxhealth; //우주선의 현재체력
    public float shield; // 우주선의 방어도
    public float damage; //우주선의 공격력(방어력)
    public float defensestat; // 우주선의 방호력(우주선의 damage와 총 health+shield에 영향을줌)
    [Header("현재 체력")]
    public float health;
    public float rebound;
    [Header("충돌 관련 수치")]
    public float collideRate; // 충돌판정을 시행하는 주기
    


    


}
