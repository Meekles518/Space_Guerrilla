using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    //Player 화면에 띄울 UI를 관리하는 스크립트(HPbar, 미니맵 제외)

    //아래의 2개 변수는 Inspector 창에서 끌어서 지정해놓아야 함
    public GameObject BulletCnt; //Canvas의 Bullet cnt 오브젝트를 저장할 변수
    public GameObject ReloadCircle; //Canvas의 Reload Circle 오브젝트를 저장할 변수


    public TextMeshProUGUI txt; //Bullet Cnt의 TextMeshPro를 저장할 변수
    public Image circle; // Reload Circle의 Image를 저장할 변수 

    public int magCpapcity; //전체 탄창 크기 저장할 변수
    public int magAmmo; //현재 장전 탄환 크기 저장할 변수
    public int activeShooter; //현재 활성화된 Shooter의 수

    public PlayerShooter[] shooters; //PlayerShooter들을 저장할 배열
    


    public float lastreloadtime; //마지막 장전 시점
    public float reloadtime; //재장전에 필요한 시간을 저장


    public void Awake()
    {
        //UI 조정에 필요한 Component 가져오기
        circle = ReloadCircle.GetComponent<Image>();
        txt = BulletCnt.GetComponent<TextMeshProUGUI>();
    }



    public void FixedUpdate()
    {

        shooters = GameManager.instance.player.GetComponents<PlayerShooter>();
        magUpdate();
        reloadCircleUpadte();

    }

    //현재 탄환 수를 표시하는 UI를 조정하는 함수
    public void magUpdate()
    {
        //매 주기마다 전체 탄창 크기와 현재 장전 탄환 크기를 초기화
        magCpapcity = 0;
        magAmmo = 0;
        activeShooter = 0;


        foreach (var pShooter in shooters)
        {
            //Shooter의 magCapacity, 최대 총알이 0 이하라면 검색할 필요가 없으므로 건너뛰기.
            if (pShooter.shooter.magCapacity <= 0)
            {
                continue;
            }

            magCpapcity += pShooter.shooter.magCapacity;
            magAmmo += pShooter.shooter.magAmmo;
            activeShooter++;

        }

        if(activeShooter > 0)
        {
            //txt 변경 해주기
            txt.text = $"{magAmmo / activeShooter}/{magCpapcity / activeShooter}";
        }

        else
        {
            txt.text = "0/0";
        }
         

    }

    //재장전에 필요한 시간을 원으로 표현하는 UI를 조정하는 함수
    public void reloadCircleUpadte()
    {
        foreach (var pShooter in shooters)
        {
            //pShooter의 최대 탄창이 0이상이고, PlayerShooter에서 재장전을 시행한다면
            if (pShooter.shooter.magCapacity > 0 && pShooter.reloadCheck())
            {

                lastreloadtime = Time.time; //현재 시간 저장


                break;
            }
        }


        circle.fillAmount = 0;

    }



}
