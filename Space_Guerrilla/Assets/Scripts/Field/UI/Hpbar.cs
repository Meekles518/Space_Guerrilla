using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    //정보들 저장하기 위한 변수들
    public ShipEntity ship; //우주선의 ShipEntity를 저장
    public Transform tF; //우주선의 Transform 저장

    public GameObject cvs; //Hpbar를 가지고 있을 Canvas를 저장할 변수. Canvas 없이 Ui를 Instantiate 시 화면에 나타나지 않음

    //단순 구현을 위해 Hpbar를 HpbarBg > Hpbar > Hp의 구조로 만들었음
    public Image img; //HpbarBg Prefab을 저장할 변수.
    private Image imageInstance; //생성한 HpbarBg를 저장할 변수 
    private Image hpFillAmount; //Hp의 Fill Amount 를 관리하는 Image를 저장할 변수

    public RectTransform rect; //UI의 rect 좌표 저장 변수

    public float height = 3.0f; //Hpbar와 우주선의 높이 간격 차이를 저장할 변수


    public void OnEnable()
    {
        //각 Component 가져오기
        ship = GetComponent<ShipEntity>();
        tF = GetComponent<Transform>();
        cvs = GameObject.Find("Canvas");

        //이전에 Hpbar 생성된 적이 없다면
        if (imageInstance == null)
        {
            //Instantiate로 맨 화면에 생성
            imageInstance = Instantiate(img, cvs.transform);

            //필요한 component들 가져오기
            hpFillAmount = imageInstance.rectTransform.GetChild(0).GetComponentInChildren<Image>();
            rect = imageInstance.GetComponent<RectTransform>();
        }

        //이전에 생성된 적이 있던 개체라면
        else
        {
            //비활성화된 Hpbar를 활성화.
            imageInstance.gameObject.SetActive(true); 
        }

    }


    public void FixedUpdate()
    {

        rect.position = Camera.main.WorldToScreenPoint(new Vector3(tF.position.x, tF.position.y + height, -11f));

        //우주선의 체력이 양수라면
        if (ship.health > 0)
        {
            //매 주기마다 hp의 fillAmount 값 조정
            hpFillAmount.fillAmount = ship.health / ship.maxhealth;
        }

        //우주선의 체력이 0 이하가 된다면
        else if (ship.health <= 0)
        {
            //Hpbar Ui를 비활성화
            //imageInstance.gameObject.SetActive(false);
        }
        

    }




}
