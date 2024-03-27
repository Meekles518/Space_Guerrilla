using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    //정보들 저장하기 위한 변수들
    public ShipEntity ship;
    public Transform tF;
    public Slider slider;
    public RectTransform rect; 

    public void FixedUpdate()
    {
        if (tF == null || ship == null || slider == null || rect == null)
        {
            ship = transform.parent.parent.parent.GetComponent<ShipEntity>();
            tF = transform.parent.parent.parent.GetComponent<Transform>();
            slider = GetComponent<Slider>();
            rect = transform.parent.GetComponent<RectTransform>();
        }

        if (ship.maxhealth > 0)
        {
            slider.value = ship.health / ship.maxhealth;
        }

        rect.position = Camera.main.WorldToScreenPoint(tF.position);
        rect.rotation = Quaternion.Euler(0, 0, 0);
    }




}
