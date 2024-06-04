using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    //������ �����ϱ� ���� ������
    public ShipEntity ship; //���ּ��� ShipEntity�� ����
    public Transform tF; //���ּ��� Transform ����

    public GameObject cvs; //Hpbar�� ������ ���� Canvas�� ������ ����. Canvas ���� Ui�� Instantiate �� ȭ�鿡 ��Ÿ���� ����

    //�ܼ� ������ ���� Hpbar�� HpbarBg > Hpbar > Hp�� ������ �������
    public Image img; //HpbarBg Prefab�� ������ ����.
    private Image imageInstance; //������ HpbarBg�� ������ ���� 
    private Image hpFillAmount; //Hp�� Fill Amount �� �����ϴ� Image�� ������ ����

    public RectTransform rect; //UI�� rect ��ǥ ���� ����

    public float height = 3.0f; //Hpbar�� ���ּ��� ���� ���� ���̸� ������ ����


    public void OnEnable()
    {
        //�� Component ��������
        ship = GetComponent<ShipEntity>();
        tF = GetComponent<Transform>();
        cvs = GameObject.Find("Canvas");

        //������ Hpbar ������ ���� ���ٸ�
        if (imageInstance == null)
        {
            //Instantiate�� �� ȭ�鿡 ����
            imageInstance = Instantiate(img, cvs.transform);

            //�ʿ��� component�� ��������
            hpFillAmount = imageInstance.rectTransform.GetChild(0).GetComponentInChildren<Image>();
            rect = imageInstance.GetComponent<RectTransform>();
        }

        //������ ������ ���� �ִ� ��ü���
        else
        {
            //��Ȱ��ȭ�� Hpbar�� Ȱ��ȭ.
            imageInstance.gameObject.SetActive(true); 
        }

    }


    public void FixedUpdate()
    {

        rect.position = Camera.main.WorldToScreenPoint(new Vector3(tF.position.x, tF.position.y + height, -11f));

        //���ּ��� ü���� ������
        if (ship.health > 0)
        {
            //�� �ֱ⸶�� hp�� fillAmount �� ����
            hpFillAmount.fillAmount = ship.health / ship.maxhealth;
        }

        //���ּ��� ü���� 0 ���ϰ� �ȴٸ�
        else if (ship.health <= 0)
        {
            //Hpbar Ui�� ��Ȱ��ȭ
            //imageInstance.gameObject.SetActive(false);
        }
        

    }




}
