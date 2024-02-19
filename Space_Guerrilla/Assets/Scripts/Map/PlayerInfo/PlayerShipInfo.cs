using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Player�� ���ּ��� ���� ������ ������ ��ũ��Ʈ
public class PlayerInfo : MonoBehaviour
{
    //Shooter ��ũ��Ʈ�� ���� �ϴ� ��������
    [Header("�� ���� ����, Player�� ������ �ִ� Shooter ��ũ��Ʈ�� �Ѿ�� �� ������")]
    public int bulletType; // �߻��ϴ� �Ѿ��� Ÿ�� ��) �÷��̾� �Ѿ�, �� �Ѿ� ��
    public int magCapacity; // źâ �뷮
    public int magAmmo; // ���� źâ�� �����ִ� ź��
    public float recoil; // �߻�� �ݵ�
    public float reloadTime; // ������ �ҿ� �ð�
    public float timeBetFire; // ����ü �߻� ����
    public int projectilesPerFire; // �ѹ� Ŭ���� �߻��ϴ� ����ü ��
    public float timeBetProjectiles; // �ѹ� Ŭ���� �߻�Ǵ� ����ü ���� �ð� ����
    public float reloadInterval;


    //ShipEntity ��ũ��Ʈ�� ���� �ϴ� ��������
    [Header("������Ʈ ����, Player�� ShipEntity ��ũ��Ʈ�� �Ѿ�� �� ������")]
    public float maxhealth; //���ּ��� ����ü��
    public float shield; // ���ּ��� ��
    public float damage; //���ּ��� ���ݷ�(����)
    public float defensestat; // ���ּ��� ��ȣ��(���ּ��� damage�� �� health+shield�� ��������)
    [Header("���� ü��")]
    public float health;


    //PlayerMovement ��ũ��Ʈ�� ���� �ϴ� ��������
    [Header("�̵�, ȸ�� �ӵ�, PlayerMovement ��ũ������ �Ѿ�� �� ������")]
    public float moveSpeed; // �̵� �ӵ�
    public float rotateSpeed; // ȸ�� �ӵ�








}
