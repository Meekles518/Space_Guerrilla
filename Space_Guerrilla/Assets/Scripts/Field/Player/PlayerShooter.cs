using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 원하는 Player 오브젝트의 발사를 제어하는 스크립트
public class PlayerShooter : MonoBehaviour
{
    private PlayerInput playerInput; // PlayerInput을 불러옴

    [Header("Shooter 수동할당 필요")]
    public Shooter shooter; // 필요한 Player 오브젝트의 Shooter (수동으로 프리팹에서 할당해 놓음)
    private float reloadInterval; // 재장전 시기간의 시간 간격
    private float lastReloadTime; // 마지막 장전 시점

    private void Awake()
    {
        // 사용할 컴포넌트들을 가져오기
        playerInput = GetComponent<PlayerInput>();
        // 재장전 시기간의 시간 간격 초기화
        reloadInterval = shooter.reloadInterval;
        // 마지막 장전 시점 초기화
        lastReloadTime = 0;
        shooter.objectRigidbody = GetComponent<Rigidbody2D>();
    }

    // 입력을 감지하고 총 발사하거나 재장전
    private void FixedUpdate()
    {
        // 플레이어의 입력이 fire이고, Shooter의 State가 Ready(발사 가능한 상태)라면
        if (playerInput.fire && shooter.state == Shooter.State.Ready)
        {
            // 발사를하는 Fire 매서드 실행
            shooter.Fire();
           
        }

        // 마지막 장전 시간으로 부터의 시간이 장전주기보다 길고,  
        //탄창의 탄수가 최대 탄수보다 적을 때 혹은 탄창이 비어있다면
        else if (Time.time - lastReloadTime >= reloadInterval && !(shooter.magAmmo == shooter.magCapacity))
        {
                // 장전을 하는 Reload 매서드 실행
                shooter.Reload();
                // 마지막 장전 시간을 현재로 설정
                lastReloadTime = Time.time;           
        }
       
    }

}