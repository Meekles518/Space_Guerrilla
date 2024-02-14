using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// �÷��̾� ���ּ��� �̵� �� ȸ�� �� �������� ����
public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput; // PlayerInput�� ������
    private Rigidbody2D playerRigidbody; // �÷��̾��� ������ٵ�

    public float moveSpeed; // �̵� �ӵ�
    public float rotateSpeed; // ȸ�� �ӵ�

    private Vector2 moveDirection; // ���ּ��� �̵��ؾ��ϴ� ����
    private Vector2 rotateDirection; // ���ּ��� ȸ���ؾ��ϴ� ����
    private Vector2 mousePosition; // ����� �󿡼��� ���� ���콺 ��ġ
    private float actualRotate; // ���� �������� rotateDirection�� �����ϱ� ���� ȸ���ؾ��ϴ� ����(��)

    private void Awake()
    {
        // ����� ������Ʈ���� ��������
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        //playerAnimator = GetComponent<Animator>();

        // �̵��ӵ� ����
        moveSpeed = 5f;
        // ȸ���ӵ� ����
        rotateSpeed = 100f;
    }

    // Ư�� �����ֱ⿡ ���� Rotate�� Move ����
    private void FixedUpdate()
    {
        // ������ ����
        Move();
        // ȸ�� ����
        Rotate();
    }

    // �Է°��� ���� ���ּ��� ������
    private void Move()
    {
        // ������, ������ �Է°��� ���� moveDirection ����
        moveDirection = new Vector2(playerInput.moveHorizontal, playerInput.moveVertical);
        // �� ������ �������� * �̵��ӵ���ŭ�� addForce�� ���� (���� �ǵ�)
        playerRigidbody.AddForce(moveDirection.normalized * moveSpeed);
        // ���� �÷��̾��� �ӵ��� ��ǥ �ӵ��� �����ߴٸ� �ӵ��� ������Ŵ
        if(playerRigidbody.velocity.sqrMagnitude > moveSpeed) 
        {
            playerRigidbody.velocity = moveDirection.normalized * moveSpeed;
        }
    }

    // ���콺 �������� ���ּ��� ȸ��
    private void Rotate()
    {
        // mousePosition�� ���� �Է� ����
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ���콺 ��ǥ���� �ڽ��� ��ǥ ���� ���Ͱ� ���ϱ�
        rotateDirection = mousePosition - (Vector2) transform.position;

        // ȸ���ؾ� �ϴ� ������ ���� '��'�� ����
        actualRotate = Quaternion.FromToRotation((Vector2) transform.up, rotateDirection).eulerAngles.z;


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
