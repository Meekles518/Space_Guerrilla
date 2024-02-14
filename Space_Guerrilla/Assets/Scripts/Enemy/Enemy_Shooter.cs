using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ϴ� Enemy Shooter ������Ʈ�� ��ų� ������
public class Enemy_Shooter : MonoBehaviour
{
    public Shooter shooter; // �ʿ��� Shooter �ҷ�����
    public GameObject player; // �÷��̾� ������Ʈ
    public Enemy_Control control; // �� �� ������Ʈ�� Enemy_Control

    public float reloadInterval; // ������ �ñⰣ�� �ð� ����
    public float lastReloadTime; // ������ ���� ����
    public float MaxAtkRange; // �ִ� ���� ��Ÿ�
    public bool isShoot; // �߻縦 �����ϴ� �� ����

    // ������Ʈ Ȱ��ȭ �� ����
    private void OnEnable()
    {
        control = GetComponent<Enemy_Control>();
        shooter.objectRigidbody = GetComponent<Rigidbody2D>();
        // ������ �ñⰣ�� �ð� ���� �ʱ�ȭ
        reloadInterval = 10f;
        // ������ ���� ���� �ʱ�ȭ
        lastReloadTime = 0;       
        // �� ���� �ʱ�ȭ
        isShoot = false; 
        // shooter�� ���� ������ ����
        shooter.magCapacity = 10;
        shooter.projectilesPerFire = control.projectilesPerFire;
        //shooter.bulletType = 1;
        shooter.recoil = 10;
        shooter.timeBetFire = 1.0f;
        shooter.timeBetProjectiles = 0.1f;
        shooter.reloadTime = 1f;
        player = GameObject.Find("Player");    
        // �ִ� ���� ��Ÿ��� Enemy_Control���� �޾ƿ�
        MaxAtkRange = control.MaxAtkRange;

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

                // ������ ���� �ð����� ������ �ð��� �����ֱ⺸�� ��� źâ�� ź���� �ִ� ź������ ���� ��
                if (Time.time - lastReloadTime >= reloadInterval && shooter.magAmmo != shooter.magCapacity)
                {
                    // ������ �ϴ� Reload �ż��� ����
                    shooter.Reload();
                    // ������ ���� �ð��� ����� ����
                    lastReloadTime = Time.time;
                }
            }
        }
    }
}