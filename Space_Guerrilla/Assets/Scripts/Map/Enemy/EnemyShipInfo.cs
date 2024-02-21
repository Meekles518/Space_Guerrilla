using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    //�������� ���� ���� Field�� ��ũ��Ʈ���� ����ϴ� ��������� �״�� ä��.


    //Shooter ��ũ��Ʈ�� ���� �ϴ� ��������
    //reloadInterval ������ Enemy Shooter���� �����ؾ� ��.
    [Header("�� ���� ����")]
    public int bulletType; // �߻��ϴ� �Ѿ��� Ÿ�� ��) �÷��̾� �Ѿ�, �� �Ѿ� ��
    public int magCapacity; // źâ �뷮
    public int magAmmo; // ���� źâ�� �����ִ� ź��
    public float recoil; // �߻�� �ݵ�
    public float reloadTime; // ������ �ҿ� �ð�
    public float timeBetFire; // ����ü �߻� ����
    public int projectilesPerFire; // �ѹ� Ŭ���� �߻��ϴ� ����ü ��
    public float timeBetProjectiles; // �ѹ� Ŭ���� �߻�Ǵ� ����ü ���� �ð� ����
    public float reloadInterval; 


    //Enemy Shooter ��ũ��Ʈ�� ���� �ϴ� ������,
    //MaxAtkRange ������ Enemy Control ��ũ��Ʈ���� �����ؾ� ��.
    [Header("�� �߻� ��ġ")]     
    public float MaxAtkRange; // �ִ� ���� ��Ÿ�
    //reloadInterval ���� ���� Enemy Shooter ��ũ��Ʈ�� �־�� ��


    //Enemy Movement ��ũ��Ʈ�� ���� �ϴ� ������
    [Header("�� �̵� ��ġ")]
    public float moveSpeed; // �̵� �ӵ�
    public float rotateSpeed; // ȸ�� �ӵ�
    public float OptimalAtkRange; // ���� ���� ��Ÿ�


    //Enemy Control ��ũ��Ʈ�� ���� �ϴ� ������
    [Header("�� ���� ��ġ ����")]
    public float smallAgrro; // ���� ��׷� ����
    public float largeAgrro; // ū ��׷� ����
    public float TimeTillAtk; // �������� ���� Ÿ�̸�
    //MaxAtkRange ���� ���� Enemy Control ��ũ��Ʈ�� �־�� ��

    //ShipEntity ��ũ��Ʈ�� ���� �ϴ� ��������
    [Header("������Ʈ ����, Player�� ShipEntity ��ũ��Ʈ�� �Ѿ�� �� ������")]
    public float maxhealth; //���ּ��� ����ü��
    public float shield; // ���ּ��� ��
    public float damage; //���ּ��� ���ݷ�(����)
    public float defensestat; // ���ּ��� ��ȣ��(���ּ��� damage�� �� health+shield�� ��������)
    [Header("���� ü��")]
    public float health;



}
