using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ϴ� Player ������Ʈ�� �߻縦 �����ϴ� ��ũ��Ʈ
public class PlayerShooter : MonoBehaviour
{
    private PlayerInput playerInput; // PlayerInput�� �ҷ���

    [Header("Shooter �����Ҵ� �ʿ�")]
    public Shooter shooter; // �ʿ��� Player ������Ʈ�� Shooter (�������� �����տ��� �Ҵ��� ����)
    private float reloadInterval; // ������ �ñⰣ�� �ð� ����
    public float lastReloadTime; // ������ ���� ����
    public float reloadTime; // �������� �ʿ��� �ҿ� �ð�

    private void Awake()
    {
        // ����� ������Ʈ���� ��������
        playerInput = GetComponent<PlayerInput>();
        // ������ �ñⰣ�� �ð� ���� �ʱ�ȭ
        reloadInterval = shooter.reloadInterval;
        // �������� �ʿ��� �ð� ��������
        reloadTime = shooter.reloadTime;
        // ������ ���� ���� �ʱ�ȭ
        lastReloadTime = 0;
        shooter.objectRigidbody = GetComponent<Rigidbody2D>();
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
        else if (reloadCheck())
        {
            // ������ ���� �ð��� ����� ���� **�� �κ��� ������ ����Ų��!
            lastReloadTime = shooter.lastReloadTime;

            // ������ �ϴ� Reload �ż��� ����
            shooter.Reload();
                          
        }
       
    }

    public bool reloadCheck()
    {
        return Time.time - lastReloadTime >= reloadInterval && !(shooter.magAmmo == shooter.magCapacity);
    }



}