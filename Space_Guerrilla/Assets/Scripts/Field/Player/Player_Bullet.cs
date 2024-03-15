using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ������ �÷��̾��� �Ѿ��� �ൿ�� �����ϴ� ��ũ��Ʈ
public class Player_Bullet : MonoBehaviour
{
    private Rigidbody2D rb2; // �Ѿ��� ������ٵ�
    [HideInInspector]
    public GameObject player; // �� �Ѿ��� �߻��ϴ� ������Ʈ

    private bool dead; // �� �Ѿ��� Ȱ��ȭ ���θ� Ȯ������ ����
    private Vector3 moveDirection3; // �Ѿ��� �̵����� (Vector3)
    private Vector2 moveDirection2; // �Ѿ��� �̵����� (Vector2)
    private float spread; // ���� ź����

    [Header("�Ѿ� �Ӽ�")]
    public float speed; // �Ѿ��� �ӵ�
    public float spreadRange; // ź���� ����

    private void Awake()
    {
        //Map���� ������ PlayerBulletInfo �� ���� �ο��ϱ�
        PlayerBulletInfo playerBulletInfo = MapManager.instance.playerInfo.GetComponent<PlayerBulletInfo>();

        
        this.speed = playerBulletInfo.speed; //Speed ����
        this.spreadRange = playerBulletInfo.spreadRange; //spreadRange ����

        var shipEntity = GetComponent<ShipEntity>();
        shipEntity.getShipEntity(playerBulletInfo); //ShipEntity�� �ʿ��� ���� ����


        // ���� ������Ʈ�� ������ٵ� ������
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        // ���� ź������ ź���� ���� ���̿��� �����ϰ� ����
        spread = Random.Range(-spreadRange, spreadRange);
    }

    // Ǯ�Ŵ������� ��Ȱ��ȭ�� �Ѿ��� Ȱ��ȭ �ɶ� ���� �۵��� �ż���
    private void OnEnable()
    {
        // �Ѿ��� ��Ȱ��ȭ ���θ� �������� �ٲ�
        dead = false;
        // �Ѿ��� �߻� �� ��ü�� �÷��̾ ã��
        player = GameObject.Find("Player");
        // �Ѿ��� �̵������� �÷��̾ ���ϴ� �������� ����
        moveDirection3 = Quaternion.AngleAxis(spread, new Vector3(0, 0, 1)) * player.transform.up;
        // ��꿡 �ʿ��� Vector2 ������ ���� ������ ��������
        moveDirection2 = (Vector2)moveDirection3;
        // �Ѿ��� ��� �������� �ʵ��� �ϴ� �ڷ�ƾ Disable�� ����
        StartCoroutine(Disable());
    }

    // �Ѿ˿� velocity�� �ο�����
    private void FixedUpdate()
    {
        // �Ѿ��� �ӵ��� ���ϴ� ������ ����
        rb2.velocity = moveDirection2.normalized * speed;       
    }

    // �Ѿ��� ���� ������ �����ϸ� ��Ȱ��ȭ ��Ű�� �ڷ�ƾ
    // ���Ŀ� ���� �ʿ� (���� ��� �ֺ� ���� ������Ʈ�� �˻��� ������ ��Ȱ��ȭ �ϴ� ������)
    private IEnumerator Disable()
    {
        // ���� ���°� �ƴ϶��
        while (!dead)
        {
            // �÷��̾�� �Ѿ� ������Ʈ ������ �Ÿ��� ���
            float distance = Vector2.Distance(player.transform.position, gameObject.transform.position);

            // �Ÿ��� 100f �̳���� 1���� �ڷ�ƾ �����
            if (distance <= 100f)
            {
                yield return new WaitForSeconds(1f);
            }
            // �Ÿ��� 100f �ʰ���� �Ѿ��� ��Ȱ��ȭ, ���¸� �������� �ٲ�
            else
            {
                gameObject.SetActive(false);
                dead = true;
            }          
        }
    }
}
