using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lv1 AfterBurner ��ų�� ����ϴ� Ŭ����
public class Lv1AfterBurner : ISkillBehavior
{

    private PlayerMovement playerMovement; //�÷��̾� �̵��� ����ϴ� Ŭ����
    private Shooter[] shooters; //Shooter �迭

    public int afterBurnerCnt { get; set; } //afterBurner ��� ���� Ƚ��
    public float afterBurnerTime {get; set; } //afterBurner ���ӽð�
    public float afterBurnerSpeed { get; set; } //afterBurner �ӵ�
    private int magCapacity { get; set; } //źâ �ִ� źȯ ��

    private AegisSkillManager AegisSkillManager; //AegisSkillManager Ŭ����

    //���ӽð� �� �ӵ��� �����ڿ��� �ʱ�ȭ
    public Lv1AfterBurner(GameObject p, AegisSkillManager skillManager)
    {
        this.playerMovement = p.GetComponent<PlayerMovement>(); //PlayerMovement Ŭ������ �޾ƿ�
        this.afterBurnerCnt = 2; //��� ���� Ƚ�� 2
        this.afterBurnerTime = 2.5f;   //afterBurner ���ӽð� 2.5
        this.afterBurnerSpeed = 50f; //afterBurner �ӵ� 50 �߰�
        this.shooters = p.GetComponentsInChildren<Shooter>();
        this.magCapacity = shooters[0].magCapacity; //Shooters�� �ִ� źȯ �� �����س���
        this.AegisSkillManager = skillManager; //AegisSkillManager �޾ƿ�
    }

    public void UseSkill()
    {
        //��ų ��� Ƚ���� ���� ���� ������ ��� ����
        

        magnumSet(0);
        afterBurnerActive(); //afterBurner Ȱ��ȭ
    }

    public void CancelSkill()
    {

    }


    //���������� afterBurner�� ���(�̼� ����)�� �����ϴ� �Լ�
    private void afterBurnerActive()
    {
        
        playerMovement.moveSpeed += afterBurnerSpeed; //�̵��ӵ� ����
    }//afterBurnerActive

    //���������� afterBurner ����� ���(�̼� ����)�� �����ϴ� �Լ�
    private void afterBurnerDeactive()
    {
        playerMovement.moveSpeed -= afterBurnerSpeed; //�̵��ӵ� ����
    }//afterBurnerDeactive


    //Shooters �迭�� �߻���� �ִ�/���� źȯ�� �����ϴ� �޼���
    private void magnumSet(int targetNum)
    {
        for (int i = 0; i < shooters.Length; i++)
        {
            shooters[i].magCapacity = targetNum;
            shooters[i].magAmmo = targetNum;
        }


    }//magnumSet

     
}
