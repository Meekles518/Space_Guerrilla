using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State�� ���� ���� ������(�̵�, ȸ��)�� ����ϴ� ��ũ��Ʈ
public class Enemy_Movement : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;    // �÷��̾�
    [HideInInspector]
    private Rigidbody2D enemyRigidbody; // ���� ������ٵ�
    [HideInInspector]
    public Enemy_Control control; // �� ���� ��ũ��Ʈ
    [HideInInspector]
    private ShipEntity shipEntity;


    private Vector2 Spawnposition; // Enemy ������Ʈ�� ������ġ
    private Vector2 Playerposition; // ���� �÷��̾��� ��ġ
    private Vector2 MoveTargetPosition; // �� Enemy ������Ʈ�� �̵��� ��ǥ ��ġ
    private Vector2 ShootTargetPosition; // �� Enemy ������Ʈ�� ����� ��ǥ ��ġ
    private float EnemytoPlayer; // �÷��̾�� �� �� ������ �Ÿ�

    [Header("�� �̵� ��ġ")]
    public float moveSpeed; // �̵� �ӵ�
    public float rotateSpeed; // ȸ�� �ӵ�
    public float OptimalAtkRange; // ���� ���� ��Ÿ�

    private Vector2 moveDirection; // ���ּ��� �̵��ؾ��ϴ� ����
    private Vector2 rotateDirection; // ���ּ��� ȸ���ؾ��ϴ� ����
    private float actualRotate; // ���� �������� rotateDirection�� �����ϱ� ���� ȸ���ؾ��ϴ� ����(��)
    private float x_Random; // x��ǥ ��������
    private float y_Random; // y��ǥ ��������
    private readonly float xrandomRange = 1f; // x��ǥ �������� ����
    private readonly float yrandomRange = 1f; // y��ǥ �������� ����
    private float currTime; // ����ð�

    // ������Ʈ Ȱ��ȭ �� ����
    private void OnEnable()
    {
        // �ʱⰪ�� �Ҵ�
        enemyRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed = 4f;
        rotateSpeed = 100f;
        OptimalAtkRange = 10f;
        player = GameManager.instance.player;
        Spawnposition = transform.position; // �� ������ġ
        MoveTargetPosition = Spawnposition; // Ÿ����ġ�� ��������Ʈ(������ġ)�� �ʱ�ȭ
        ShootTargetPosition = Spawnposition; // ���Ÿ���� ��������Ʈ(������ġ)�� �ʱ�ȭ
        control = GetComponent<Enemy_Control>();
        shipEntity = GetComponent<ShipEntity>();

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

        // ���¿� ���� ��ǥ ��ġ ����
        switch (control.statename)
        {
            //IDLE STATE
            case Enemy_Control.STATE.IDLE:
                MoveTargetPosition = new Vector2(transform.position.x, transform.position.y) + new Vector2(0, 1);
                ShootTargetPosition = new Vector2(transform.position.x, transform.position.y) + new Vector2(0, 1);
                break;
            //PURSUE STATE
            case Enemy_Control.STATE.PURSUE:
                MoveTargetPosition = Playerposition;
                ShootTargetPosition = Playerposition;
                break;
            //WAIT STATE
            case Enemy_Control.STATE.WAIT:
                MoveTargetPosition = new Vector2(transform.position.x, transform.position.y);
                ShootTargetPosition = Playerposition;
                break;
            //GOBACK STATE
            case Enemy_Control.STATE.GOBACK:
                MoveTargetPosition = Spawnposition;
                break;
            //RETREAT STATE
            case Enemy_Control.STATE.RETREAT:
                MoveTargetPosition = Spawnposition;
                ShootTargetPosition = Playerposition;
                break;
            //ESCAPE STATE
            case Enemy_Control.STATE.ESCAPE:
                MoveTargetPosition = control.Escapepoint;
                ShootTargetPosition = control.Escapepoint;
                break;
            default:
                break;
        }
    }


    // �� �̵��� ��Ű�� �ż���
    private void Move()
    {
        // Pursue ���¿��� ���� ������Ÿ��� �ۿ� ���� ��
        if (control.statename == Enemy_Control.STATE.PURSUE)
        {
            if (EnemytoPlayer > OptimalAtkRange)
            {
                // �̵����� = ��ǥ��ġ - ������ġ + ������ġ
                moveDirection = MoveTargetPosition - new Vector2(transform.position.x, transform.position.y) + new Vector2(x_Random, y_Random);
                // �̵��������� AddForce�� ���� (���� �ο�)
                enemyRigidbody.AddForce(moveDirection.normalized * moveSpeed);
                // ���� ���� �ӵ��� ��ǥ �ӵ��� �����ߴٸ� �ӵ��� ������Ŵ
                if (enemyRigidbody.velocity.sqrMagnitude > moveSpeed)
                {
                    enemyRigidbody.velocity = moveDirection.normalized * moveSpeed;
                }
            }
            else if (EnemytoPlayer < OptimalAtkRange)
            {
                // �̵����� = -��ǥ��ġ + ������ġ + ������ġ
                moveDirection = -MoveTargetPosition + new Vector2(transform.position.x, transform.position.y) + new Vector2(x_Random, y_Random);
                // �̵��������� AddForce�� ���� (���� �ο�)
                enemyRigidbody.AddForce(moveDirection.normalized * moveSpeed);
                // ���� ���� �ӵ��� ��ǥ �ӵ��� �����ߴٸ� �ӵ��� ������Ŵ
                if (enemyRigidbody.velocity.sqrMagnitude > moveSpeed)
                {
                    enemyRigidbody.velocity = moveDirection.normalized * moveSpeed;
                }
            }

        }
        // (GoBack �����϶�) ESCAPE STATE �� ����
        else if(control.statename == Enemy_Control.STATE.GOBACK || control.statename == Enemy_Control.STATE.ESCAPE)
        {
            // �̵����� = ��ǥ��ġ - ������ġ
            moveDirection = MoveTargetPosition - new Vector2(transform.position.x, transform.position.y);
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
        else if(control.statename == Enemy_Control.STATE.WAIT)
        {
            moveDirection = new Vector2(-1, 0);
        }
        else
        {
            // �� ������Ʈ�� �ӵ��� 0���� ����
            enemyRigidbody.velocity = new Vector2(0, 0);
        }

        shipEntity.moveDirection = moveDirection;
        
    }

    // �� ������Ʈ�� ȸ���� ��Ű�� �ż���
    private void Rotate()
    {
        
         // ȸ�� ���� = ��� ��ǥ��ġ - ������ġ
         rotateDirection = ShootTargetPosition - new Vector2(transform.position.x, transform.position.y);
         // ȸ���ؾ� �ϴ� ������ '��'�� ����
         actualRotate = Quaternion.FromToRotation((Vector2)transform.up, rotateDirection).eulerAngles.z;

         // ȸ���ؾ� �ϴ� ������ ���� ���� ȸ�� ������ ��������.
         if (actualRotate < 1 || actualRotate > 359)
         {
             // ������ �����ϱ� ���� ������ 1�� ����
             enemyRigidbody.angularVelocity = 0f;
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