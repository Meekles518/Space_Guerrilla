using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//EnemyOne�� �⺻ ������ ���� ��ũ��Ʈ
public class EnemyOneInfo : ShipInfo
{
    //�����ڿ� EnemyOne�� �⺻ ���� �ο�
    //���� ���Ƿ� ����

    //Enemy Control ��ũ��Ʈ�� ���� �ϴ� ��������
    public float MaxAtkRange { get; set; } // �ִ� ���� ��Ÿ�

    public float smallAgrro { get; set; } // ���� ��׷� ����
    public float largeAgrro { get; set; } // ū ��׷� ����
    public float TimeTillAtk { get; set; } // �������� ���� Ÿ�̸�

    //Enemy Movement ��ũ��Ʈ�� ���� �ϴ� ��������
    public float OptimalAtkRange { get; set; } // ���� ���� ��Ÿ�


    public EnemyOneInfo()
    {
        //Shooter ��ũ��Ʈ�� ���� �ϴ� ��������
        bulletType1 = 1; // �߻��ϴ� �Ѿ��� Ÿ�� ��) �÷��̾� �Ѿ�, �� �Ѿ� ��
        magCapacity1 = 10; // źâ �뷮
        magAmmo1 = 10; // ���� źâ�� �����ִ� ź��
        recoil1 = 0.1f; // �߻�� �ݵ�
        reloadTime1 = 3f; // ������ �ҿ� �ð�
        timeBetFire1 = 1f; // ����ü �߻� ����
        projectilesPerFire1 = 1; // �ѹ� Ŭ���� �߻��ϴ� ����ü ��
        timeBetProjectiles1 = 0.2f; // �ѹ� Ŭ���� �߻�Ǵ� ����ü ���� �ð� ����

        //Shooter �� Enemy Shooter ��ũ��Ʈ�� ���� �ϴ� ��������
        reloadInterval1 = 0.1f;

        //Enemy Movement ��ũ��Ʈ�� ���� �ϴ� ��������
        moveSpeed = 10f; // �̵� �ӵ�
        rotateSpeed = 10f; // ȸ�� �ӵ�
        OptimalAtkRange = 15f; // ���� ���� ��Ÿ�

        //Enemy Control ��ũ��Ʈ�� ���� �ϴ� ��������
        MaxAtkRange = 20f; // �ִ� ���� ��Ÿ�
        smallAgrro = 25f; // ���� ��׷� ����
        largeAgrro = 50f; // ū ��׷� ����
        TimeTillAtk = 3f; // �������� ���� Ÿ�̸�

        //ShipEntity ��ũ��Ʈ�� ���� �ϴ� ��������
        maxhealth = 30f; //���ּ��� ����ü��
        shield = 0f; // ���ּ��� ��
        damage = 10f; //���ּ��� ���ݷ�(����)
        defensestat = 0f; // ���ּ��� ��ȣ��(���ּ��� damage�� �� health+shield�� ��������)
        health = 30f;
        rebound = 0f;
        collideRate = 0.1f;



    }
}
