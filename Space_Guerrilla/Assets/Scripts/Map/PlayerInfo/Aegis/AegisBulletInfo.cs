using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AegisBulletInfo : BulletInfo
{
    //�����ڿ� AegisBulletInfo�� �⺻ �����͸� ����. 
    //��� ������ ���Ƿ� �� ������.
    public AegisBulletInfo()
    {
        //Player Bullet ��ũ��Ʈ�� �־�� �� ��������
        speed = 10f; // �Ѿ��� �ӵ�, CruiseMissile ��ũ��Ʈ�� speed ������ �ʿ���
        spreadRange = 0.3f; // ź���� ����

        //Player CruiseMissile ��ũ��Ʈ�� �־�� �� ��������
        lifespan = 10f; //�̻����� �����ֱ�
        Scan_range = 10f; //�̻����� ��ĵ ����
        rotateSpeed = 10f; // ȸ�� �ӵ�

        //Player Bullet�� ShipEntity ��ũ��Ʈ�� �־�� �� ��������
        maxhealth = 30f; //�Ѿ��� �ִ�ü��
        shield = 0f; // �Ѿ��� ��
        damage = 10f; //�Ѿ��� ���ݷ�(����)
        defensestat = 0f; // �Ѿ��� ��ȣ��(���ּ��� damage�� �� health+shield�� ��������)
        health = 30f; // �Ѿ��� ����ü��
        rebound = 0f; // �Ѿ��� �ݵ�
        collideRate = 0.1f; // �浹������ �����ϴ� �ֱ�


    }


}
