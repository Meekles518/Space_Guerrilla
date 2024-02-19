using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Enemy의 총알 정보를 담은 스크립트
public class EnemyBulletInfo : MonoBehaviour
{

    //Enemy Bullet 스크립트에 넣어야 할 변수값들
    [Header("총알 속성")]
    public float speed; // 총알의 속도
    public float spreadRange; // 탄퍼짐 정도


    //Enemy Bullet의 ShipEntity 스크립트에 넣어야 할 변수값들
    [Header("오브젝트 스탯")]
    public float maxhealth; //우주선의 현재체력
    public float shield; // 우주선의 방어도
    public float damage; //우주선의 공격력(방어력)
    public float defensestat; // 우주선의 방호력(우주선의 damage와 총 health+shield에 영향을줌)
    [Header("충돌 관련 수치")]
    public float collideRate; // 충돌판정을 시행하는 주기
    private bool inCollision; // 현재 충돌 여부를 판단하는 논리 변수
    [Header("현재 체력")]
    public float health;




}
