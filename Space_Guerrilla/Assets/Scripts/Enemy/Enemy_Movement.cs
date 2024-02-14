using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State�� ���� ���� ������(�̵�, ȸ��)�� ����ϴ� ��ũ��Ʈ
public class Enemy_Movement : MonoBehaviour
{
    public GameObject player;    // �÷��̾�
    private Rigidbody2D enemyRigidbody; // ���� ������ٵ�
    public Enemy_Control control; // �� ���� ��ũ��Ʈ

    public Vector2 Spawnposition; // Enemy ������Ʈ�� ������ġ
    private Vector2 Playerposition; // ���� �÷��̾��� ��ġ
    public Vector2 Targetposition; // �� Enemy ������Ʈ�� �̵�, �ٶ� ��ǥ ��ġ
    public float EnemytoPlayer; // �÷��̾�� �� �� ������ �Ÿ�

    public float moveSpeed; // �̵� �ӵ�
    public float rotateSpeed; // ȸ�� �ӵ�
    public float OptimalAtkRange; // ���� ���� ��Ÿ�

    public Vector2 moveDirection; // ���ּ��� �̵��ؾ��ϴ� ����
    private Vector2 rotateDirection; // ���ּ��� ȸ���ؾ��ϴ� ����
    private float actualRotate; // ���� �������� rotateDirection�� �����ϱ� ���� ȸ���ؾ��ϴ� ����(��)
    private float x_Random; // x��ǥ ��������
    private float y_Random; // y��ǥ ��������
    private float xrandomRange = 1f; // x��ǥ �������� ����
    private float yrandomRange = 1f; // y��ǥ �������� ����
    private float currTime; // ����ð�

    // ������Ʈ Ȱ��ȭ �� ����
    private void OnEnable()
    {
        // �ʱⰪ�� �Ҵ�
        enemyRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed = 4f;
        rotateSpeed = 100f;
        OptimalAtkRange = 5f;
        player = GameObject.Find("Player");
        Spawnposition = transform.position; // �� ������ġ
        Targetposition = Spawnposition; // Ÿ����ġ�� ��������Ʈ(������ġ)�� �ʱ�ȭ
        control = GetComponent<Enemy_Control>();

    }


    private void FixedUpdate()
    {
        // �÷��̾��� ��ġ�� ���������� ����
        Playerposition = player.transform.position;
        // �÷��̾�� �� ������ �Ÿ��� ���������� ����
        EnemytoPlayer = control.EnemytoPlayer;

        // ������ ����
        Move();
        // ȸ�� ����
        Rotate();

        // �̵��� ������ ��ġ�� ������ �����ϱ� ���� �÷��̾� ������ ������ �������� �̵�
        // �ڷ�ƾ�� ������� �ʴ� ������ Random.Range�� �ڷ�ƾ���� ���������� ���ŵ��� ����, x_Random�� y_Random�� �ϳ��� ������ ���� (�ڷ�ƾ���� �ϴ� ����� ������ ��������)
        // ���� �ð��� ��� ����ȭ
        currTime += Time.deltaTime;
        // �ð��� ������ �ʱ�ȭ�κ��� 3�� �����ٸ�
        if (currTime > 3)
        {
            // x_Random�� y_Random�� �������� ����
            x_Random = Random.Range(-xrandomRange, xrandomRange);
            y_Random = Random.Range(-yrandomRange, yrandomRange);
            // ���� �ð� �ʱ�ȭ
            currTime = 0;
        }
        // Idle State�� �� ��ǥ��ġ�� ������ġ(�ڽ��� ������ġ) + y��������� ����
        if (control.statename == Enemy_Control.STATE.IDLE)
        {
            Targetposition = new Vector2(transform.position.x, transform.position.y) + new Vector2(0, 1);
        }
        // Pursue State�� �� ��ǥ��ġ�� �÷��̾� ��ġ�� ����
        else if (control.statename == Enemy_Control.STATE.PURSUE)
        {
            Targetposition = Playerposition;
        }
        // Wait State�� �� ��ǥ��ġ�� �÷��̾� ��ġ�� ����
        else if (control.statename == Enemy_Control.STATE.WAIT)
        {
            Targetposition = Playerposition;
        }
        // GoBack State�� �� ��ǥ��ġ�� ������ġ�� ����
        else if (control.statename == Enemy_Control.STATE.GOBACK)
        {
            Targetposition = Spawnposition;
        }
        // Retreat State�� �� ��ǥ��ġ�� �÷��̾� ��ġ�� ����
        else if (control.statename == Enemy_Control.STATE.RETREAT)
        {
            Targetposition = Playerposition;
        }
    }


    // �� �̵��� ��Ű�� �ż���
    private void Move()
    {
        // Pursue ���¿��� ���� ������Ÿ��� �ۿ� ���� ��
        if (EnemytoPlayer > OptimalAtkRange && control.statename == Enemy_Control.STATE.PURSUE)
        {                
             // �̵����� = ��ǥ��ġ - ������ġ + ������ġ
             moveDirection = Targetposition - new Vector2(transform.position.x, transform.position.y) + new Vector2(x_Random, y_Random);
             // �̵��������� AddForce�� ���� (���� �ο�)
             enemyRigidbody.AddForce(moveDirection.normalized * moveSpeed);
             // ���� ���� �ӵ��� ��ǥ �ӵ��� �����ߴٸ� �ӵ��� ������Ŵ
             if(enemyRigidbody.velocity.sqrMagnitude > moveSpeed)
             {
                enemyRigidbody.velocity = moveDirection.normalized * moveSpeed;
             }
        }
        // GoBack �����϶�
        else if(control.statename == Enemy_Control.STATE.GOBACK)
        {
            // �̵����� = ��ǥ��ġ - ������ġ
            moveDirection = Targetposition - new Vector2(transform.position.x, transform.position.y);
            // �̵��������� AddForce�� ���� (���� �ο�)
            enemyRigidbody.AddForce(moveDirection.normalized * moveSpeed);
            // ���� ���� �ӵ��� ��ǥ �ӵ��� �����ߴٸ� �ӵ��� ������Ŵ
            if (enemyRigidbody.velocity.sqrMagnitude > moveSpeed)
            {
                enemyRigidbody.velocity = moveDirection.normalized * moveSpeed;
            }
        }
        // ���� �����϶�
        // �̵������� ������ġ, ȸ�������� �÷��̾� ��ġ�̹Ƿ� ��ǥ��ġ�� ������ġ�� ��������
        else if (control.statename == Enemy_Control.STATE.RETREAT)
        {
            // �̵����� = ������ġ - ������ġ
            moveDirection = Spawnposition - new Vector2(transform.position.x, transform.position.y);
            // �̵������� normalize�ؼ� �ӵ��� ������
            // �̵��������� AddForce�� ���� (���� �ο�)
            enemyRigidbody.AddForce(moveDirection.normalized * moveSpeed);
            // ���� ���� �ӵ��� ��ǥ �ӵ��� �����ߴٸ� �ӵ��� ������Ŵ
            if (enemyRigidbody.velocity.sqrMagnitude > moveSpeed)
            {
                enemyRigidbody.velocity = moveDirection.normalized * moveSpeed;
            }
        }
        else
        {
            // �� ������Ʈ�� �ӵ��� 0���� ����
            enemyRigidbody.velocity = new Vector2(0, 0);
        }
        
    }

    // �� ������Ʈ�� ȸ���� ��Ű�� �ż���
    private void Rotate()
    {
        
         // ȸ�� ���� = ��ǥ��ġ - ������ġ
         rotateDirection = Targetposition - new Vector2(transform.position.x, transform.position.y);
         // ȸ���ؾ� �ϴ� ������ '��'�� ����
         actualRotate = Quaternion.FromToRotation((Vector2)transform.up, rotateDirection).eulerAngles.z;

         // ȸ���ؾ� �ϴ� ������ ���� ���� ȸ�� ������ ��������.
         if (actualRotate < 1 || actualRotate > 359)
         {
             // ������ �����ϱ� ���� ������ 1�� ����
             return;
         }

         // �ð�������� ȸ���� �� ����� �� �ð�������� ȸ��
         else if (actualRotate > 180)
         {
             transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed, Space.Self);
         }

         // �ݽð�������� ȸ���� �� ����� �� �ݽð�������� ȸ��
         else if (actualRotate < 180)
         {
             transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);
         }
        

    }



}