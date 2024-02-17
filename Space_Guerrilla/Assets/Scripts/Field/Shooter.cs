using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 실제로 총알을 생성하고 발사하는 스크립트
// 포대 자체에 컴포넌트로 탑재, 모든 유형의 적이 사용가능
public class Shooter : MonoBehaviour
{
    // 총의 상태를 표현하는데 사용할 타입을 선언한다
    public enum State
    {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }

    public State state { get; private set; } // 현재 총의 상태

    public Transform fireTransform; // 투사체가 발사될 위치
    public Rigidbody2D objectRigidbody; // 발사자의  Rigidbody

    public int magCapacity; // 탄창 용량
    public int magAmmo; // 현재 탄창에 남아있는 탄약
    public float recoil; // 발사시 반동
    public int bulletType; // 발사하는 총알의 타입 예) 플레이어 총알, 적 총알 등

    private float lastFireTime; // 총을 마지막으로 발사한 시점
    public float timeBetFire; // 투사체 발사 간격
    public int projectilesPerFire; // 한번 클릭시 발사하는 투사체 수
    public float timeBetProjectiles; // 한번 클릭시 발사되는 투사체 간의 시간 간격
    public float reloadTime; // 재장전 소요 시간


    private void OnEnable()
    {
        // 현재 탄창을 가득채우기
        magAmmo = magCapacity;
        // 포대의 현재 상태를 투사체를 쏠 준비가 된 상태로 변경
        state = State.Ready;
        // 마지막으로 투사체를 쏜 시점을 초기화
        lastFireTime = 0;
        // 발사기의 리지드바디 컴포넌트를 가져옴
        objectRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // 포대의 현재 위치를 지속적으로 업데이트
        fireTransform = gameObject.transform;
    }

    // 발사 시도
    public void Fire()
    {
        // 현재 상태가 발사 가능한 상태
        // && 마지막 투사체 발사 시점에서 timeBetFire 이상의 시간이 지남
        if (state == State.Ready
            && Time.time >= lastFireTime + timeBetFire)
        {
            // 마지막 총 발사 시점을 갱신
            lastFireTime = Time.time;
            // 실제 발사 처리 실행
            Shot();
          
        }
    }

    // 실제 발사 처리
    private void Shot()
    {      
        // 발사로직 코루틴 실행
        StartCoroutine("ShotLogic");

        // 남은 탄환의 수를 -1
        magAmmo--;
        
        if (magAmmo <= 0)
        {
            // 탄창에 남은 탄약이 없다면, 현재 상태를 Empty으로 갱신
            state = State.Empty;
            
        }
    }

    // 한번 클릭시 투사체 발사 개수와 발사간의 간격을 조절하는 기능
    IEnumerator ShotLogic()
    {        
            // 한번의 클릭에 발사하는 투사체수 만큼 for문 안을 반복
            for (int i = 0; i < projectilesPerFire; i++)
            {               
                // 실제 투사체 발사 메서드 ShootProjectiles가 클릭당 발사 속도마다 작동
                Invoke("ShootProjectiles", timeBetProjectiles * i);
                
            }
            
            // 투사체 발사 간격만큼 대기
            yield return new WaitForSeconds(timeBetFire);         
    }

    // 필요한 투사체를 생성해서 발사
    private void ShootProjectiles()
    {
        // 풀에서 투사체를 불러와 발사기 위치에 생성
        GameManager.instance.poolManager.Get(bulletType, fireTransform);
        //투사체를 발사한 위치 반대 방향으로 플레이어에게 반동을 줌
        objectRigidbody.AddForce(-fireTransform.up.normalized * recoil);
    }


    // 재장전
    public void Reload()
    {
        // 이미 재장전 중이거나 탄창에 탄약이 이미 가득한 경우 재장전 할수 없다
        if (state != State.Reloading && magAmmo < magCapacity)
        {
            // 재장전 처리 시작
            StartCoroutine(ReloadRoutine());
        }
    }

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine()
    {
        // 현재 상태를 재장전 중 상태로 전환
        state = State.Reloading;

        // 재장전 소요 시간 만큼 처리를 쉬기
        yield return new WaitForSeconds(reloadTime);

        //탄창에 탄약을 채운다.
        magAmmo = magCapacity;

        // 현재 상태를 발사 준비된 상태로 변경
        state = State.Ready;
    }
}