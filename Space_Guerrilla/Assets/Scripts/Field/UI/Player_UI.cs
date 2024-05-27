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


    



    public void FixedUpdate()
    {

    }








}
