using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//���ּ��� �⺻ �����͸� ������ ��ũ��Ʈ
//�߰��� �����ؾ� �� �����Ͱ� �ִٸ�, �� Ŭ������ ��ӹ��� �Ŀ� �߰��� ������ �����ϰ�, ���� �ο��ϸ� ��
public class ShipInfo : MonoBehaviour
{
    //Shooter ��ũ��Ʈ�� ���� �ϴ� ��������, �� ������ ����
    public int bulletType1 { get; set; } // �߻��ϴ� �Ѿ��� Ÿ�� ��) �÷��̾� �Ѿ�, �� �Ѿ� ��
    public int magCapacity1 { get; set; } // źâ �뷮
    public int magAmmo1 { get; set; } // ���� źâ�� �����ִ� ź��
    public float recoil1 { get; set; } // �߻�� �ݵ�
    public float reloadTime1 { get; set; } // ������ �ҿ� �ð�
    public float timeBetFire1 { get; set; } // ����ü �߻� ����
    public int projectilesPerFire1 { get; set; } // �ѹ� Ŭ���� �߻��ϴ� ����ü ��
    public float timeBetProjectiles1 { get; set; } // �ѹ� Ŭ���� �߻�Ǵ� ����ü ���� �ð� ����
    public float reloadInterval1 { get; set; }


    //���� ����� �� ���ּ����� ���̰� ���� �� ������, �⺻ �����Ϳ��� �������� ����



    //ShipEntity ��ũ��Ʈ�� ���� �ϴ� ��������
    public float maxhealth { get; set; } //���ּ��� �ִ�ü��
    public float shield { get; set; } // ���ּ��� ��
    public float damage { get; set; } //���ּ��� ���ݷ�(����)
    public float defensestat { get; set; } // ���ּ��� ��ȣ��(���ּ��� damage�� �� health+shield�� ��������)

    public float health { get; set; } // ���ּ��� ����ü��
    public float rebound { get; set; } // ���ּ��� �ݵ�

    public float collideRate { get; set; } // �浹������ �����ϴ� �ֱ�


    //PlayerMovement ��ũ��Ʈ�� ���� �ϴ� ��������
    public float moveSpeed { get; set; } // �̵� �ӵ�
    public float rotateSpeed { get; set; } // ȸ�� �ӵ�








}
