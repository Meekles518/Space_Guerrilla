using System;
using System.Collections;
using UnityEngine;

//�÷��̾�, �� ���ּ�, ��е� ���ֿ��� �̵��ϰ� ü���ִ� ��� ������Ʈ���� ������ ������ ��������� ���
public class ShipEntity : MonoBehaviour
{
    public bool dead { get; protected set; } //���ּ��� ��� ���θ� �� �� �ִ� ����
    private Collider2D collideEnemy; // �浹�� ����� �ݶ��̴�
    private string objectTag; // ������ �±�
    private Rigidbody2D rb2;
    private bool GD;

    [Header("������Ʈ ����")]
    public float maxhealth; //���ּ��� �ִ�ü��
    public float shield; // ���ּ��� ��
    public float damage; //���ּ��� ���ݷ�(����)
    public float defensestat; // ���ּ��� ��ȣ��(���ּ��� damage�� �� health+shield�� ��������)
    [Header("�浹 ���� ��ġ")]
    public float collideRate; // �浹������ �����ϴ� �ֱ�
    private bool inCollision; // ���� �浹 ���θ� �Ǵ��ϴ� �� ����
    [Header("���� ü��")]
    public float health;

    public Vector2 moveDirection;
    public Vector2 faceDirection;
    public float rebound;
    public float Degree;


    //onEnable�� �ʱ� �� ����
    protected virtual void OnEnable()
    {
        dead = false; // ��������� ���� �Ҵ�
        inCollision = false; // �浹������ ���� �Ҵ�
        objectTag = gameObject.tag; // �ڽ��� �±׸� �±� ������ �Ҵ�
        maxhealth = maxhealth * (1 + defensestat * 0.75f / 100);
        health = maxhealth;
        rb2= GetComponent<Rigidbody2D>();
        GD = false;
        Degree = 180;
    }

    //ShipEntity ��ũ��Ʈ�� ���� ������ �ܺο��� �ҷ����� �Լ�. ��� �󵵰� ���� ������ ����ǰ�,
    //��κ��� ������Ʈ���� ShipEntity ��ũ��Ʈ�� ������ ���� ���̱⿡, ���⿡ �Լ� ����

    public void getShipEntity(PlayerInfo playerInfo)
    {
        this.maxhealth = playerInfo.maxhealth;
        this.shield = playerInfo.shield;
        this.damage = playerInfo.damage;
        this.defensestat = playerInfo.defensestat;
        this.health = playerInfo.health;
        this.rebound = playerInfo.rebound;
        this.collideRate = playerInfo.collideRate;
    }

    public void getShipEntity(PlayerBulletInfo playerBulletInfo)
    {
        this.maxhealth = playerBulletInfo.maxhealth;
        this.shield = playerBulletInfo.shield;
        this.damage = playerBulletInfo.damage;
        this.defensestat = playerBulletInfo.defensestat;
        this.health = playerBulletInfo.health;
        this.rebound = playerBulletInfo.rebound;
        this.collideRate = playerBulletInfo.collideRate;
    }

    public void getShipEntity(ShipEntity shipEntity)
    {
        this.maxhealth = shipEntity.maxhealth;
        this.shield = shipEntity.shield;
        this.damage = shipEntity.damage;
        this.defensestat = shipEntity.defensestat;
        this.health = shipEntity.health;
        this.rebound = shipEntity.rebound;
        this.collideRate = shipEntity.collideRate;
    }



    // �浹���϶� ����
    private void OnTriggerStay2D(Collider2D other)
    {
        inCollision = true; // �浹���� ������ ����
        collideEnemy = other; // �浹����� �ݶ��̴� ������
        if (GD == false)
        {
            StartCoroutine("GiveDamage"); // ���� �ڷ�ƾ ����
        }
        
    }

    // �浹�� ������ ������ ����
    private void OnTriggerExit2D(Collider2D other)
    {
        inCollision = false; // �浹���� �������� ����
        collideEnemy = null; // �浹��� �������� �ʱ�ȭ
        StopCoroutine("GiveDamage"); //���� �ڷ�ƾ ����
        GD = false;
    }

    // �浹�� ��� ������Ʈ�� �������� �ִ� �ڷ�ƾ
    private IEnumerator GiveDamage()
    {
        GD = true;
        // �浹���̰� �浹�� ����� �±װ� ���� �ٸ� ��
        if (inCollision == true && collideEnemy.tag != objectTag)
        {          
                // ���κ��� ShipEntity �������� �õ�    
                ShipEntity shipEntity = collideEnemy.GetComponent<ShipEntity>();
                // ����� ShipEntity�� ���������� ���������� ��
                if (shipEntity != null)
                {
                if (collideEnemy.tag == "Player" || collideEnemy.tag == "Enemy")
                {
                    ShipCollide(shipEntity.moveDirection, shipEntity.rebound);
                }
                else
                {
                    // ����� �ǰ� �ż��带 ����
                    shipEntity.TakeDamage(damage + (defensestat / 6));
                }
                }           
        }
        // �ǰ��ֱ⸶�� �ݺ�
        yield return new WaitForSeconds(collideRate);
    }

    // �浹�� �������� �޴� �ż���
    public virtual void TakeDamage(float otherDamage)
    {
        // ���尡 ���� �ִٸ� �������� ����� ��
        if (shield > 0)
        {
            shield -= otherDamage * 1000 / (100 + damage) / defensestat;
        }
        // ���尡 ���ٸ� �������� ü������ ��
        else
        {
            //���Ƿ� ���� �����غ�, �޴� �������� ��� �������� �����, �� ������ ���� �� ����
            health -= otherDamage / (damage + (defensestat / 6)) * 100;
        }
        // ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual void ShipCollide(Vector2 moveDirection, float rebound)
    {
        faceDirection = new Vector2(this.transform.position.x - collideEnemy.transform.position.x, this.transform.position.y - collideEnemy.transform.position.y).normalized;
        Degree = Quaternion.FromToRotation((Vector2)faceDirection, (Vector2)moveDirection).eulerAngles.z;
        if (Degree < 10 || Degree > 350)
        {
            rb2.AddForce(moveDirection.normalized * rebound);
            health = health - 10;
        }
        if (health <= 0 && !dead)
        {
            Die();
        }

    }

    // ��� ó��
    public virtual void Die()
    {
        // ��� ���¸� ������ ����
        dead = true;
        // ���� ������Ʈ�� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }






}
