using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneBulletInfo : BulletInfo
{


    //�����ڿ� EnemyOne�� �⺻ �����͸� ����
    //��� ������ ���� �ο�
    public EnemyOneBulletInfo()
    {
        //Enemy Bullet ��ũ��Ʈ�� �־�� �� ��������
        speed = 10f; // �Ѿ��� �ӵ�
        spreadRange = 0.3f; // ź���� ����

        //Enemy Bullet�� ShipEntity ��ũ��Ʈ�� �־�� �� ��������
        maxhealth = 30f; //���ּ��� ����ü��
        shield = 0f; // ���ּ��� ��
        damage = 10f; //���ּ��� ���ݷ�(����)
        defensestat = 0f; // ���ּ��� ��ȣ��(���ּ��� damage�� �� health+shield�� ��������)
        health = 30f;
        rebound = 0f;
        collideRate = 0.1f;

    }//������ ��

}
