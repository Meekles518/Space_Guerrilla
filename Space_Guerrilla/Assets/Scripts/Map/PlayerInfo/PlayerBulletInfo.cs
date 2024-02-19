using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Player�� �Ѿ˿� ���� ������ ������ ��ũ��Ʈ
public class PlayerBulletInfo : MonoBehaviour
{

    //Player Bullet ��ũ��Ʈ�� �־�� �� ��������
    [Header("�Ѿ� �Ӽ�, Player Bullet ��ũ��Ʈ�� �Ѿ�� �� ������")]
    public float speed; // �Ѿ��� �ӵ�, CruiseMissile ��ũ��Ʈ�� speed ������ �ʿ���
    public float spreadRange; // ź���� ����


    //Player CruiseMissile ��ũ��Ʈ�� �־�� �� ��������

    [Header("�̻��� �Ӽ�, Player CruiseMissile ��ũ��Ʈ�� �Ѿ�� �� ������")]
    
    public float lifespan;
    public float Scan_range;
    public float rotateSpeed; // ȸ�� �ӵ�





    //Player Bullet�� ShipEntity ��ũ��Ʈ�� �־�� �� ��������
    [Header("Player Bullet�� ShipEntity ��ũ��Ʈ�� �Ѿ�� �� ������")]
    [Header("������Ʈ ����")]
    public float maxhealth; //���ּ��� ����ü��
    public float shield; // ���ּ��� ��
    public float damage; //���ּ��� ���ݷ�(����)
    public float defensestat; // ���ּ��� ��ȣ��(���ּ��� damage�� �� health+shield�� ��������)
    [Header("�浹 ���� ��ġ")]
    public float collideRate; // �浹������ �����ϴ� �ֱ�
    private bool inCollision; // ���� �浹 ���θ� �Ǵ��ϴ� �� ����
    [Header("���� ü��")]
    public float health;


    


}
