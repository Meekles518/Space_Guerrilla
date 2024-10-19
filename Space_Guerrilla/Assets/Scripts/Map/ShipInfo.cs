using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//우주선의 기본 데이터를 저장할 스크립트
//추가로 관리해야 할 데이터가 있다면, 이 클래스를 상속받은 후에 추가로 변수를 선언하고, 값을 부여하면 됨
public class ShipInfo : MonoBehaviour
{
    //Shooter 스크립트에 들어가야 하는 변수값들, 주 무기의 값들
    public int bulletType1 { get; set; } // 발사하는 총알의 타입 예) 플레이어 총알, 적 총알 등
    public int magCapacity1 { get; set; } // 탄창 용량
    public int magAmmo1 { get; set; } // 현재 탄창에 남아있는 탄약
    public float recoil1 { get; set; } // 발사시 반동
    public float reloadTime1 { get; set; } // 재장전 소요 시간
    public float timeBetFire1 { get; set; } // 투사체 발사 간격
    public int projectilesPerFire1 { get; set; } // 한번 클릭시 발사하는 투사체 수
    public float timeBetProjectiles1 { get; set; } // 한번 클릭시 발사되는 투사체 간의 시간 간격
    public float reloadInterval1 { get; set; }


    //보조 무기는 매 우주선마다 차이가 있을 수 있으니, 기본 데이터에서 관리하지 않음



    //ShipEntity 스크립트에 들어가야 하는 변수값들
    public float maxhealth { get; set; } //우주선의 최대체력
    public float shield { get; set; } // 우주선의 방어도
    public float damage { get; set; } //우주선의 공격력(방어력)
    public float defensestat { get; set; } // 우주선의 방호력(우주선의 damage와 총 health+shield에 영향을줌)

    public float health { get; set; } // 우주선의 현재체력
    public float rebound { get; set; } // 우주선의 반동

    public float collideRate { get; set; } // 충돌판정을 시행하는 주기


    //PlayerMovement 스크립트에 들어가야 하는 변수값들
    public float moveSpeed { get; set; } // 이동 속도
    public float rotateSpeed { get; set; } // 회전 속도








}
