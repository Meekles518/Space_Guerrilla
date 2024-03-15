using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Player�� ���ּ��� ���� ������ ������ ��ũ��Ʈ
public class PlayerInfo : MonoBehaviour
{
    //Shooter ��ũ��Ʈ�� ���� �ϴ� ��������, ���� �� ������ ����
    [Header("�� ���� ����, Player�� ������ �ִ� Shooter ��ũ��Ʈ�� �Ѿ�� �� ������")]
    public int bulletType1; // �߻��ϴ� �Ѿ��� Ÿ�� ��) �÷��̾� �Ѿ�, �� �Ѿ� ��
    public int magCapacity1; // źâ �뷮
    public int magAmmo1; // ���� źâ�� �����ִ� ź��
    public float recoil1; // �߻�� �ݵ�
    public float reloadTime1; // ������ �ҿ� �ð�
    public float timeBetFire1; // ����ü �߻� ����
    public int projectilesPerFire1; // �ѹ� Ŭ���� �߻��ϴ� ����ü ��
    public float timeBetProjectiles1; // �ѹ� Ŭ���� �߻�Ǵ� ����ü ���� �ð� ����
    public float reloadInterval1;


    //Shooter ��ũ��Ʈ�� ���� �ϴ� ��������, ������ ���� ������ ����
    [Header("�� ���� ����, Player�� ������ �ִ� Shooter ��ũ��Ʈ�� �Ѿ�� �� ������")]
    public int bulletType2; // �߻��ϴ� �Ѿ��� Ÿ�� ��) �÷��̾� �Ѿ�, �� �Ѿ� ��
    public int magCapacity2; // źâ �뷮
    public int magAmmo2; // ���� źâ�� �����ִ� ź��
    public float recoil2; // �߻�� �ݵ�
    public float reloadTime2; // ������ �ҿ� �ð�
    public float timeBetFire2; // ����ü �߻� ����
    public int projectilesPerFire2; // �ѹ� Ŭ���� �߻��ϴ� ����ü ��
    public float timeBetProjectiles2; // �ѹ� Ŭ���� �߻�Ǵ� ����ü ���� �ð� ����
    public float reloadInterval2;



    //ShipEntity ��ũ��Ʈ�� ���� �ϴ� ��������
    [Header("������Ʈ ����, Player�� ShipEntity ��ũ��Ʈ�� �Ѿ�� �� ������")]
    public float maxhealth; //���ּ��� ����ü��
    public float shield; // ���ּ��� ��
    public float damage; //���ּ��� ���ݷ�(����)
    public float defensestat; // ���ּ��� ��ȣ��(���ּ��� damage�� �� health+shield�� ��������)
    [Header("���� ü��")]
    public float health;
    public float rebound;
    [Header("�浹 ���� ��ġ")]
    public float collideRate; // �浹������ �����ϴ� �ֱ�


    //PlayerMovement ��ũ��Ʈ�� ���� �ϴ� ��������
    [Header("�̵�, ȸ�� �ӵ�, PlayerMovement ��ũ������ �Ѿ�� �� ������")]
    public float moveSpeed; // �̵� �ӵ�
    public float rotateSpeed; // ȸ�� �ӵ�








}
