using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    //직관성을 위해 실제 Field의 스크립트에서 사용하는 변수명들을 그대로 채용.


    //Shooter 스크립트에 들어가야 하는 변수값들
    //reloadInterval 변수는 Enemy Shooter에도 적용해야 함.
    [Header("총 성능 조정")]
    public int bulletType; // 발사하는 총알의 타입 예) 플레이어 총알, 적 총알 등
    public int magCapacity; // 탄창 용량
    public int magAmmo; // 현재 탄창에 남아있는 탄약
    public float recoil; // 발사시 반동
    public float reloadTime; // 재장전 소요 시간
    public float timeBetFire; // 투사체 발사 간격
    public int projectilesPerFire; // 한번 클릭시 발사하는 투사체 수
    public float timeBetProjectiles; // 한번 클릭시 발사되는 투사체 간의 시간 간격
    public float reloadInterval; 


    //Enemy Shooter 스크립트에 들어가야 하는 변수들,
    //MaxAtkRange 변수는 Enemy Control 스크립트에도 적용해야 함.
    [Header("적 발사 수치")]     
    public float MaxAtkRange; // 최대 공격 사거리
    //reloadInterval 변수 값도 Enemy Shooter 스크립트에 넣어야 함


    //Enemy Movement 스크립트에 들어가야 하는 변수들
    [Header("적 이동 수치")]
    public float moveSpeed; // 이동 속도
    public float rotateSpeed; // 회전 속도
    public float OptimalAtkRange; // 적정 공격 사거리


    //Enemy Control 스크립트에 들어가야 하는 변수들
    [Header("적 관련 수치 조정")]
    public float smallAgrro; // 작은 어그로 범위
    public float largeAgrro; // 큰 어그로 범위
    public float TimeTillAtk; // 공격형적 추적 타이머
    //MaxAtkRange 변수 값도 Enemy Control 스크립트에 넣어야 함

    //ShipEntity 스크립트에 들어가야 하는 변수값들
    [Header("오브젝트 스탯, Player의 ShipEntity 스크립트로 넘어가야 할 변수들")]
    public float maxhealth; //우주선의 현재체력
    public float shield; // 우주선의 방어도
    public float damage; //우주선의 공격력(방어력)
    public float defensestat; // 우주선의 방호력(우주선의 damage와 총 health+shield에 영향을줌)
    [Header("현재 체력")]
    public float health;



}
