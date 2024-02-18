using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ϴ� Enemy Shooter ������Ʈ�� ��ų� ������
public class Enemy_Shooter : MonoBehaviour
{
    [Header("Shooter �Ҵ�")]
    public Shooter shooter; // �ʿ��� Shooter �ҷ�����
    [HideInInspector]
    public GameObject player; // �÷��̾� ������Ʈ
    [HideInInspector]
    public Enemy_Control control; // �� �� ������Ʈ�� Enemy_Control
    private float lastReloadTime; // ������ ���� ����

    [Header("�� �߻� ��ġ")]
    public float reloadInterval; // ������ �ñⰣ�� �ð� ����   
    public float MaxAtkRange; // �ִ� ���� ��Ÿ�
    [Header("�߻� Ȯ�ο�")]
    public bool isShoot; // �߻縦 �����ϴ� �� ����

    // ������Ʈ Ȱ��ȭ �� ����
    private void OnEnable()
    {
        control = GetComponent<Enemy_Control>();
        shooter.objectRigidbody = GetComponent<Rigidbody2D>();
        // ������ ���� ���� �ʱ�ȭ
        lastReloadTime = 0;       
        // �� ���� �ʱ�ȭ
        isShoot = false; 
        player = GameObject.Find("Player");    

        

    }

    // ���� State�� ���� ���� �߻� �Ǵ� �߻����� ����
    private void FixedUpdate()
    {
        // isShoot�� ���������� ����ȭ
        isShoot = control.isShoot;

        // �ִ� ���� ��Ÿ� ������ �÷��̾ ������
        if (control.EnemytoPlayer <= MaxAtkRange)
        {
            // �߻簡 ������ ��
            if (isShoot == true)
            {
                // �߻�
                shooter.Fire();
                Debug.Log("fire");

                // ������ ���� �ð����� ������ �ð��� �����ֱ⺸�� ��� źâ�� ź���� �ִ� ź������ ���� ��
                if (Time.time - lastReloadTime >= reloadInterval && shooter.magAmmo != shooter.magCapacity)
                {
                    // ������ �ϴ� Reload �ż��� ����
                    shooter.Reload();
                    Debug.Log("reload");
                    // ������ ���� �ð��� ����� ����
                    lastReloadTime = Time.time;
                }
            }
        }
    }
}