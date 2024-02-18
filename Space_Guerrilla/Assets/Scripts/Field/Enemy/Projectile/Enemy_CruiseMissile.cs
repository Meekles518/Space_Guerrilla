using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ������ �÷��̾��� �Ѿ��� �ൿ�� �����ϴ� ��ũ��Ʈ
public class Enemy_CruiseMissile : MonoBehaviour
{
    private Rigidbody2D rb2; // �̻����� ������ٵ�
    public Transform enemy; // �� �̻����� �߻��ϴ� ������Ʈ
    private bool dead; // �� �̻����� Ȱ��ȭ ���θ� Ȯ������ ����
    public float speed; // �̻����� �ӵ�

    private Vector3 moveDirection3; // �Ѿ��� �̵����� (Vector3)
    private Vector2 moveDirection2; // �Ѿ��� �̵����� (Vector2)
    private Vector2 bulletPosition; // ���� �Ѿ��� ��ġ
    public float lifespan;
    private float currtime;
    private Transform missileTarget;

    private Vector2 Targetposition;
    public RaycastHit2D[] Targets; // CircleCastAll�� �������� ������Ʈ��
    public LayerMask Target_layer; // �˻��� ������ ���̾�(Enemy ���̾�)
    public Transform Nearest_enemy; // �˻��� ������Ʈ�� ���� ����� Enemy ������Ʈ
    public float Scan_range;
    private Vector2 rotateDirection; // ���ּ��� ȸ���ؾ��ϴ� ����
    private float actualRotate;
    public float rotateSpeed; // ȸ�� �ӵ�

    public RaycastHit2D[] Targets1; // CircleCastAll�� �������� ������Ʈ��
    public LayerMask Target_layer1; // �˻��� ������ ���̾�(Enemy ���̾�)
    public Transform Nearest_enemy1; // �˻��� ������Ʈ�� ���� ����� Enemy ������Ʈ

    private void Awake()
    {
        // ���� ������Ʈ�� ������ٵ� ������
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        // �ӵ� ����
        //speed = 20f;
    }

    // Ǯ�Ŵ������� ��Ȱ��ȭ�� �Ѿ��� Ȱ��ȭ �ɶ� ���� �۵��� �ż���
    private void OnEnable()
    {
        // �Ѿ��� ��Ȱ��ȭ ���θ� �������� �ٲ�
        dead = false;
        Target_layer = LayerMask.GetMask("Enemy");
        // ��ó ��� Enemy ������Ʈ�� �˻�
        Targets = Physics2D.CircleCastAll(transform.position, 3, Vector2.zero, 0, Target_layer);
        // ���� ����� Enmey ������Ʈ�� �ڽ��� �߻��� ������ ����
        enemy = Nearest();
        // �Ѿ��� �̵������� �÷��̾ ���ϴ� �������� ����
        moveDirection3 = enemy.up;
        // ��꿡 �ʿ��� Vector2 ������ ���� ������ ��������
        moveDirection2 = (Vector2)moveDirection3;
        // �Ѿ��� ���� ��ġ�� ���
        bulletPosition = new Vector2(transform.position.x, transform.position.y);
        // �Ѿ��� ��� �������� �ʵ��� �ϴ� �ڷ�ƾ Disable�� ����
        currtime = 0f;
        missileTarget = null;
    }

    // �Ѿ˿� velocity�� �ο�����
    private void FixedUpdate()
    {
        // �Ѿ��� �ӵ��� ���ϴ� ������ ����
        rb2.velocity = (Vector2)transform.up.normalized * speed;

        currtime += Time.fixedDeltaTime;
        if (currtime >= lifespan)
        {
            gameObject.SetActive(false);
            dead = true;
        }

        if (missileTarget == null)
        {
            Targetposition = transform.position + transform.up.normalized;
            Find();
        }
        else
        {
            Targetposition = missileTarget.position;
            Rotate();
            if (Vector2.Distance(missileTarget.position, transform.position) >= Scan_range)
            {
                missileTarget = null;
            }

        }
    }
    private void Find()
    {
        // �˻� ���̾ Enemy�� ����
        Target_layer = LayerMask.GetMask("Player");
        // ��ó ��� Enemy ������Ʈ�� �˻�
        Targets = Physics2D.CircleCastAll(transform.position, Scan_range, Vector2.zero, 0, Target_layer);
        // ���� ����� Enmey ������Ʈ�� �ڽ��� �߻��� ������ ����
        Nearest_enemy = Nearest();
        if (Nearest_enemy != null)
        {
            missileTarget = Nearest_enemy;
        }
    }

    private Transform Nearest()
    {

        // ���� ����� Target�� ������ ����
        Transform Result = null;
        // Player�� ���� ����� Target�� �Ÿ��� ������ ����, �ʱⰪ�� ���Ƿ� �ſ� ū ���� 100���� ����
        float Difference = Scan_range * 2;

        // Targets�� ����ִ� ��� ���ҿ� foreach�� ����
        foreach (RaycastHit2D Target in Targets)
        {

            // �Ѿ��� ��ǥ��, Target�� ��ǥ�� ��������
            Vector2 My_pos = transform.position;
            Vector2 Target_pos = Target.transform.position;
            // Distance�� ���� �Ѿ˰� Target�� �Ÿ��� ��������
            float Current_difference = Vector2.Distance(My_pos, Target_pos);

            // ���� foreach���� Target���� �Ÿ���, ����Ǿ� �ִ� Distance���� ª����
            // Difference�� Result�� ���� �ʱ�ȭ
            if (Current_difference < Difference)
            {
                //�� ����� �Ÿ�, Target���� ��ü
                Difference = Current_difference;
                Result = Target.transform;
            }
        }
        // ���� ��ȯ
        return Result;
    }

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
