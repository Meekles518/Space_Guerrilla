using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State�� ���� ���� ������(�̵�, ȸ��)�� ����ϴ� ��ũ��Ʈ
public class Drone_Movement : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;    // �÷��̾�
    private Rigidbody2D droneRigidbody; // ����� ������ٵ�
    [HideInInspector]
    public Drone_Control control; // �� ���� ��ũ��Ʈ

    private Vector2 Playerposition; // ���� �÷��̾��� ��ġ
    [HideInInspector]
    public Vector2 Targetposition; // ����� �̵�, �ٶ� ��ǥ ��ġ

    [Header("��� ��ġ ����")]
    public float moveSpeed; // �̵� �ӵ�
    public float rotateSpeed; // ȸ�� �ӵ�

    [HideInInspector]
    public Vector2 moveDirection; // ���ּ��� �̵��ؾ��ϴ� ����
    private Vector2 rotateDirection; // ���ּ��� ȸ���ؾ��ϴ� ����
    private float actualRotate; // ���� �������� rotateDirection�� �����ϱ� ���� ȸ���ؾ��ϴ� ����(��)
    public float x_Random;
    private float y_Random;
    public float x_Range;
    public float y_Range;
    private float currTime; // ����ð�
    

    // ������Ʈ Ȱ��ȭ �� ����
    private void OnEnable()
    {
        // �ʱⰪ�� �Ҵ�
        droneRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        control = GetComponent<Drone_Control>();
        Targetposition = transform.position;
    }


    private void FixedUpdate()
    {
        // �÷��̾��� ��ġ�� ���������� ����
        Playerposition = player.transform.position;
        
        // �̵��� ������ ��ġ�� ������ �����ϱ� ���� �÷��̾� ������ ������ �������� �̵�
        // �ڷ�ƾ�� ������� �ʴ� ������ Random.Range�� �ڷ�ƾ���� ���������� ���ŵ��� ����, x_Random�� y_Random�� �ϳ��� ������ ���� (�ڷ�ƾ���� �ϴ� ����� ������ ��������)
        // ���� �ð��� ��� ����ȭ
        currTime += Time.deltaTime;
        // �ð��� ������ �ʱ�ȭ�κ��� 3�� �����ٸ�
        if (currTime > 2)
        {
            x_Random = Random.Range(-x_Range, x_Range);
            y_Random = Random.Range(-y_Range, y_Range);
            currTime = 0;
        }

        // Idle State�� �� ��ǥ��ġ�� ������ġ(�ڽ��� ������ġ) + y��������� ����
        if (control.statename == Drone_Control.STATE.IDLE && control.isSpread == false && control.isAuto == false)
        {
            //���콺 Ŀ���� ��ġ�� ���ϰ� ��
            Targetposition = control.mousePosition;
        }

        // AUTO State�� �� ��ǥ��ġ�� 
        else if (control.statename == Drone_Control.STATE.IDLE && control.isSpread == false && control.isAuto == true)
        {
            //Player�� ��ġ���� ���� ���� ���� ������ ��ġ�� ���ϰ� ��
            Targetposition = Playerposition + new Vector2(x_Random, y_Random);
        }

        // SPREAD State�� �� ��ǥ��ġ�� 
        else if (control.statename == Drone_Control.STATE.SPREAD)
        {
            //���콺 Ŀ���� �ݴ� ��ġ�� ���� ��
            //Targetposition = new Vector2(control.selfposition.position.x, control.selfposition.position.y) + new Vector2(control.selfposition.position.x - Input.mousePosition.x, control.selfposition.position.y - Input.mousePosition.y);
            Targetposition = ((Vector2)transform.position - control.mousePosition) +(Vector2)transform.position;
        }

        // Engage State�� �� ��ǥ��ġ�� 
        else if (control.statename == Drone_Control.STATE.ENGAGE)
        {
            // Targetposition = ����ġ, ��ĳ�� �Ἥ ���� �Ÿ� ���� �� �� ���� ����� �� ����
            //Drone_Control���� ã�� Target�� ���� �̵�
            Targetposition = control.TargetPosition;
           
        }

        // Follow State�� �� ��ǥ ��ġ��
        else if (control.statename == Drone_Control.STATE.FOLLOW)
        {
            //Drone_Control�� ����Ǿ� �ִ� FollowPosition���� �̵�
            Targetposition = control.FollowPosition;
        }

        // ������ ����
        Move();
        // ȸ�� ����
        Rotate();

    }//FIxedUpdate


    // ����� �̵��� ��Ű�� �ż���
    //�� State�� ���� �̵��� if������ �����ؾ� ��
    private void Move()
    {
            // �̵����� = ��ǥ��ġ - ������ġ + ������ġ
            moveDirection = Targetposition - (Vector2)transform.position;
            // �̵��������� AddForce�� ���� (���� �ο�)
            droneRigidbody.AddForce(moveDirection.normalized * moveSpeed);
            // ���� ����� �ӵ��� ��ǥ �ӵ��� �����ߴٸ� �ӵ��� ������Ŵ
            if (droneRigidbody.velocity.sqrMagnitude > moveSpeed)
            {
                droneRigidbody.velocity = moveDirection.normalized * moveSpeed;
            }

         

    }//Move

    // ��� ������Ʈ�� ȸ���� ��Ű�� �ż���
    private void Rotate()
    {

        // ȸ�� ���� = ��ǥ��ġ - ������ġ
        rotateDirection = Targetposition - (Vector2)transform.position;
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


    }//Rotate
}