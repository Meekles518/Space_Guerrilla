using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 원하는 Enemy Shooter 오브젝트를 쏘거나 재장전
public class Enemy_Shooter : MonoBehaviour
{
    [Header("Shooter 할당")]
    public Shooter shooter; // 필요한 Shooter 불러오기
    [HideInInspector]
    public GameObject player; // 플레이어 오브젝트
    [HideInInspector]
    public Enemy_Control control; // 이 적 오브젝트의 Enemy_Control
    private float lastReloadTime; // 마지막 장전 시점

    [Header("적 발사 수치")]
    public float reloadInterval; // 재장전 시기간의 시간 간격   
    public float MaxAtkRange; // 최대 공격 사거리
    [Header("발사 확인용")]
    public bool isShoot; // 발사를 제어하는 불 변수

    // 오브젝트 활성화 시 적용
    private void OnEnable()
    {
        control = GetComponent<Enemy_Control>();
        shooter.objectRigidbody = GetComponent<Rigidbody2D>();
        // 마지막 장전 시점 초기화
        lastReloadTime = 0;       
        // 불 변수 초기화
        isShoot = false; 
        player = GameObject.Find("Player");    

        

    }

    // 현재 State에 따라 총을 발사 또는 발사하지 않음
    private void FixedUpdate()
    {
        // isShoot를 지속적으로 동기화
        isShoot = control.isShoot;

        // 최대 공격 사거리 안으로 플레이어가 들어오면
        if (control.EnemytoPlayer <= MaxAtkRange)
        {
            // 발사가 가능할 때
            if (isShoot == true)
            {
                // 발사
                shooter.Fire();
                Debug.Log("fire");

                // 마지막 장전 시간으로 부터의 시간이 장전주기보다 길고 탄창의 탄수가 최대 탄수보다 적을 때
                if (Time.time - lastReloadTime >= reloadInterval && shooter.magAmmo != shooter.magCapacity)
                {
                    // 장전을 하는 Reload 매서드 실행
                    shooter.Reload();
                    Debug.Log("reload");
                    // 마지막 장전 시간을 현재로 설정
                    lastReloadTime = Time.time;
                }
            }
        }
    }
}