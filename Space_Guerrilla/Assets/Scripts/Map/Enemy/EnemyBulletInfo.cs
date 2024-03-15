using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Enemy�� �Ѿ� ������ ���� ��ũ��Ʈ
public class EnemyBulletInfo : MonoBehaviour
{

    //Enemy Bullet ��ũ��Ʈ�� �־�� �� ��������
    [Header("�Ѿ� �Ӽ�")]
    public float speed; // �Ѿ��� �ӵ�
    public float spreadRange; // ź���� ����


    //Enemy Bullet�� ShipEntity ��ũ��Ʈ�� �־�� �� ��������
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




}
