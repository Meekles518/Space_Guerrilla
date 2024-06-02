using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    //Player ȭ�鿡 ��� UI�� �����ϴ� ��ũ��Ʈ(HPbar, �̴ϸ� ����)

    //�Ʒ��� 2�� ������ Inspector â���� ��� �����س��ƾ� ��
    public GameObject BulletCnt; //Canvas�� Bullet cnt ������Ʈ�� ������ ����
    public GameObject ReloadCircle; //Canvas�� Reload Circle ������Ʈ�� ������ ����


    public TextMeshProUGUI txt; //Bullet Cnt�� TextMeshPro�� ������ ����
    public Image circle; // Reload Circle�� Image�� ������ ���� 

    public int magCpapcity; //��ü źâ ũ�� ������ ����
    public int magAmmo; //���� ���� źȯ ũ�� ������ ����
    public int activeShooter; //���� Ȱ��ȭ�� Shooter�� ��

    public PlayerShooter[] shooters; //PlayerShooter���� ������ �迭
    


    public float lastreloadtime; //������ ���� ����
    public float reloadtime; //�������� �ʿ��� �ð��� ����


    public void Awake()
    {
        //UI ������ �ʿ��� Component ��������
        circle = ReloadCircle.GetComponent<Image>();
        txt = BulletCnt.GetComponent<TextMeshProUGUI>();
    }



    public void FixedUpdate()
    {

        shooters = GameManager.instance.player.GetComponents<PlayerShooter>();
        magUpdate();
        reloadCircleUpadte();

    }

    //���� źȯ ���� ǥ���ϴ� UI�� �����ϴ� �Լ�
    public void magUpdate()
    {
        //�� �ֱ⸶�� ��ü źâ ũ��� ���� ���� źȯ ũ�⸦ �ʱ�ȭ
        magCpapcity = 0;
        magAmmo = 0;
        activeShooter = 0;


        foreach (var pShooter in shooters)
        {
            //Shooter�� magCapacity, �ִ� �Ѿ��� 0 ���϶�� �˻��� �ʿ䰡 �����Ƿ� �ǳʶٱ�.
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
            //txt ���� ���ֱ�
            txt.text = $"{magAmmo / activeShooter}/{magCpapcity / activeShooter}";
        }

        else
        {
            txt.text = "0/0";
        }
         

    }

    //�������� �ʿ��� �ð��� ������ ǥ���ϴ� UI�� �����ϴ� �Լ�
    public void reloadCircleUpadte()
    {
        foreach (var pShooter in shooters)
        {
            //pShooter�� �ִ� źâ�� 0�̻��̰�, PlayerShooter���� �������� �����Ѵٸ�
            if (pShooter.shooter.magCapacity > 0 && pShooter.reloadCheck())
            {

                lastreloadtime = Time.time; //���� �ð� ����


                break;
            }
        }


        circle.fillAmount = 0;

    }



}
