using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�Ѿ˿� ���� �⺻ ������ ������ ��ũ��Ʈ
//�߰��� �����ؾ� �� �����Ͱ� �ִٸ�, �� Ŭ������ ��ӹ��� �Ŀ� �߰��� ������ �����ϰ�, ���� �ο��ϸ� ��
public class BulletInfo : MonoBehaviour
{

    //Player Bullet ��ũ��Ʈ�� �־�� �� ��������
    public float speed { get; set; } // �Ѿ��� �ӵ�, CruiseMissile ��ũ��Ʈ�� speed ������ �ʿ���
    public float spreadRange { get; set; } // ź���� ����


    //Player CruiseMissile ��ũ��Ʈ�� �־�� �� ��������   
    public float lifespan { get; set; } //�̻����� �����ֱ�
    public float Scan_range { get; set; } //�̻����� ��ĵ ����
    public float rotateSpeed { get; set; } // ȸ�� �ӵ�

    //Player Bullet�� ShipEntity ��ũ��Ʈ�� �־�� �� ��������
    public float maxhealth { get; set; } //�Ѿ��� �ִ�ü��
    public float shield { get; set; } // �Ѿ��� ��
    public float damage { get; set; } //�Ѿ��� ���ݷ�(����)
    public float defensestat { get; set; } // �Ѿ��� ��ȣ��(���ּ��� damage�� �� health+shield�� ��������)

    public float health { get; set; } // �Ѿ��� ����ü��
    public float rebound { get; set; } // �Ѿ��� �ݵ�

    public float collideRate { get; set; } // �浹������ �����ϴ� �ֱ�






}
