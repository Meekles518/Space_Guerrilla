using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Player의 우주선에 관한 변수를 저장할 스크립트
public class PlayerInfo : MonoBehaviour
{
    //Shooter 스크립트에 들어가야 하는 변수값들, 왼쪽 주 무기의 값들
    [Header("총 성능 조정, Player이 가지고 있는 Shooter 스크립트로 넘어가야 할 변수들")]
    public int bulletType1; // 발사하는 총알의 타입 예) 플레이어 총알, 적 총알 등
    public int magCapacity1; // 탄창 용량
    public int magAmmo1; // 현재 탄창에 남아있는 탄약
    public float recoil1; // 발사시 반동
    public float reloadTime1; // 재장전 소요 시간
    public float timeBetFire1; // 투사체 발사 간격
    public int projectilesPerFire1; // 한번 클릭시 발사하는 투사체 수
    public float timeBetProjectiles1; // 한번 클릭시 발사되는 투사체 간의 시간 간격
    public float reloadInterval1;


    //Shooter 스크립트에 들어가야 하는 변수값들, 오른쪽 보조 무기의 값들
    [Header("총 성능 조정, Player이 가지고 있는 Shooter 스크립트로 넘어가야 할 변수들")]
    public int bulletType2; // 발사하는 총알의 타입 예) 플레이어 총알, 적 총알 등
    public int magCapacity2; // 탄창 용량
    public int magAmmo2; // 현재 탄창에 남아있는 탄약
    public float recoil2; // 발사시 반동
    public float reloadTime2; // 재장전 소요 시간
    public float timeBetFire2; // 투사체 발사 간격
    public int projectilesPerFire2; // 한번 클릭시 발사하는 투사체 수
    public float timeBetProjectiles2; // 한번 클릭시 발사되는 투사체 간의 시간 간격
    public float reloadInterval2;



    //ShipEntity 스크립트에 들어가야 하는 변수값들
    [Header("오브젝트 스탯, Player의 ShipEntity 스크립트로 넘어가야 할 변수들")]
    public float maxhealth; //우주선의 현재체력
    public float shield; // 우주선의 방어도
    public float damage; //우주선의 공격력(방어력)
    public float defensestat; // 우주선의 방호력(우주선의 damage와 총 health+shield에 영향을줌)
    [Header("현재 체력")]
    public float health;
    public float rebound;
    [Header("충돌 관련 수치")]
    public float collideRate; // 충돌판정을 시행하는 주기


    //PlayerMovement 스크립트에 들어가야 하는 변수값들
    [Header("이동, 회전 속도, PlayerMovement 스크립르토 넘어가야 할 변수들")]
    public float moveSpeed; // 이동 속도
    public float rotateSpeed; // 회전 속도








}
