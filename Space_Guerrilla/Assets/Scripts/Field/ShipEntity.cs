using System;
using System.Collections;
using UnityEngine;

//�÷��̾�, �� ���ּ�, ��е� ���ֿ��� �̵��ϰ� ü���ִ� ��� ������Ʈ���� ������ ������ ��������� ���
public class ShipEntity : MonoBehaviour
{
    public bool dead { get; protected set; } //���ּ��� ��� ���θ� �� �� �ִ� ����
    public float startingHealth { get; protected set; } //���ּ��� �ʱ� ü��
    public float health; //���ּ��� ����ü��
    public float damage; //���ּ��� ����(������)
    public float shield; // ���ּ��� ��ȣ��(?)
    public float collideRate; // �浹������ �����ϴ� �ֱ�
    private bool inCollision; // ���� �浹 ���θ� �Ǵ��ϴ� �� ����
    private Collider2D collideEnemy; // �浹�� ����� �ݶ��̴�
    private string objectTag; // ������ �±�

    //onEnable�� �ʱ� �� ����
    protected virtual void OnEnable()
    {
        dead = false; // ��������� ���� �Ҵ�
        //health = startingHealth; //����ü�� = �ʱ�ü��
        inCollision = false; // �浹������ ���� �Ҵ�
        objectTag = gameObject.tag; // �ڽ��� �±׸� �±� ������ �Ҵ�
        collideRate = 0.5f; // �ֱ⿡ �� �Ҵ�
        shield = 0; // �ӽ÷� �ʱ�ȭ, ���߿� �����

    }

    // �浹���϶� ����
    private void OnTriggerStay2D(Collider2D other)
    {
        inCollision = true; // �浹���� ������ ����
        collideEnemy = other; // �浹����� �ݶ��̴� ������
        StartCoroutine("giveDamage"); // ���� �ڷ�ƾ ����
    }

    // �浹�� ������ ������ ����
    private void OnTriggerExit2D(Collider2D other)
    {
        inCollision = false; // �浹���� �������� ����
        collideEnemy = null; // �浹��� �������� �ʱ�ȭ
        StopCoroutine("giveDamage"); //���� �ڷ�ƾ ����
    }

    // �浹�� ��� ������Ʈ�� �������� �ִ� �ڷ�ƾ
    private IEnumerator giveDamage()
    {
        // �浹���̰� �浹�� ����� �±װ� ���� �ٸ� ��
        if (inCollision == true && collideEnemy.tag != objectTag)
        {          
                // ���κ��� ShipEntity �������� �õ�    
                ShipEntity shipEntity = collideEnemy.GetComponent<ShipEntity>();
                // ����� ShipEntity�� ���������� ���������� ��
                if (shipEntity != null)
                {
                    // ����� �ǰ� �ż��带 ����
                    shipEntity.takeDamage(damage);                   
                }           
        }
        // �ǰ��ֱ⸶�� �ݺ�
        yield return new WaitForSeconds(collideRate);
    }

    // �浹�� �������� �޴� �ż���
    public virtual void takeDamage(float otherDamage)
    {
        // ���尡 ���� �ִٸ� �������� ����� ��
        if (shield > 0)
        {
            shield -= otherDamage * 100 / (100 + damage);
        }
        // ���尡 ���ٸ� �������� ü������ ��
        else
        {
            //���Ƿ� ���� �����غ�, �޴� �������� ��� �������� �����, �� ������ ���� �� ����
            health -= otherDamage * 100 / (100 + damage);
        }
        // ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
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
