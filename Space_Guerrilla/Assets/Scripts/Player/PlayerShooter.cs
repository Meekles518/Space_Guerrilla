using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ϴ� Player ������Ʈ�� �߻縦 �����ϴ� ��ũ��Ʈ
public class PlayerShooter : MonoBehaviour
{
    public Shooter shooter; // �ʿ��� Player ������Ʈ�� Shooter (�������� �����տ��� �Ҵ��� ����)
    private PlayerInput playerInput; // PlayerInput�� �ҷ���

    public float reloadInterval; // ������ �ñⰣ�� �ð� ����
    public float lastReloadTime; // ������ ���� ����

    private void Awake()
    {
        // ����� ������Ʈ���� ��������
        playerInput = GetComponent<PlayerInput>();
        // ������ �ñⰣ�� �ð� ���� �ʱ�ȭ
        reloadInterval = 10f;
        // ������ ���� ���� �ʱ�ȭ
        lastReloadTime = 0;

        // shooter�� ���� ������ ����, �̷��� �� ������ �׽�Ʈ �� �÷��̾��� ������ ���� ��������
        // �����δ� �ʵ� ������ ���弱 �ӵ����� �ٲ� �� �־�������� ���Ŀ� ���� �ʿ�
        shooter.magCapacity = 10;
        
        shooter.objectRigidbody = GetComponent<Rigidbody2D>();
        
        shooter.recoil = 10;
        shooter.timeBetFire = 1.0f;
        shooter.timeBetProjectiles = 0.1f;
        shooter.reloadTime = 5f;


    }

    // �Է��� �����ϰ� �� �߻��ϰų� ������
    private void FixedUpdate()
    {
        // �÷��̾��� �Է��� fire�̰�, Shooter�� State�� Ready(�߻� ������ ����)���
        if (playerInput.fire && shooter.state == Shooter.State.Ready)
        {
            // �߻縦�ϴ� Fire �ż��� ����
            shooter.Fire();
           
        }

        // ������ ���� �ð����� ������ �ð��� �����ֱ⺸�� ���,  
        //źâ�� ź���� �ִ� ź������ ���� �� Ȥ�� źâ�� ����ִٸ�
        else if (Time.time - lastReloadTime >= reloadInterval && (shooter.magAmmo == 0 || shooter.magAmmo != shooter.magCapacity))
        {
                // ������ �ϴ� Reload �ż��� ����
                shooter.Reload();
                // ������ ���� �ð��� ����� ����
                lastReloadTime = Time.time;           
        }
       
    }

}