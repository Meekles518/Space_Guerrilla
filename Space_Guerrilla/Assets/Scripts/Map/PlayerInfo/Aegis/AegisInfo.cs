using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AegisInfo : ShipInfo
{
    //�������� AegisInfo�� �⺻ �����͸� ����
    //��� ������ ���Ƿ� �� ������.
    public AegisInfo()
    {
        //Shooter ��ũ��Ʈ�� ���� �ϴ� ��������, �� ������ ����
        bulletType1 = 0; // �߻��ϴ� �Ѿ��� Ÿ�� ��) �÷��̾� �Ѿ�, �� �Ѿ� ��
        magCapacity1 = 10; // źâ �뷮
        magAmmo1 = 10; // ���� źâ�� �����ִ� ź��
        recoil1 = 0.1f; // �߻�� �ݵ�
        reloadTime1 = 3f; // ������ �ҿ� �ð�
        timeBetFire1 = 1f; // ����ü �߻� ����
        projectilesPerFire1 = 1; // �ѹ� Ŭ���� �߻��ϴ� ����ü ��
        timeBetProjectiles1 = 0.2f; // �ѹ� Ŭ���� �߻�Ǵ� ����ü ���� �ð� ����
        reloadInterval1 = 0.1f;


        //ShipEntity ��ũ��Ʈ�� ���� �ϴ� ��������
        maxhealth = 100f; //���ּ��� �ִ�ü��
        shield = 10f; // ���ּ��� ��
        damage = 10f; //���ּ��� ���ݷ�(����)
        defensestat = 3f; // ���ּ��� ��ȣ��(���ּ��� damage�� �� health+shield�� ��������)
        health = 100f; // ���ּ��� ����ü��
        rebound = 0.1f; // ���ּ��� �ݵ�
        collideRate = 0.1f; // �浹������ �����ϴ� �ֱ�

        //PlayerMovement ��ũ��Ʈ�� ���� �ϴ� ��������
        moveSpeed = 10f; // �̵� �ӵ�
        rotateSpeed = 10f; // ȸ�� �ӵ�



    }

}
