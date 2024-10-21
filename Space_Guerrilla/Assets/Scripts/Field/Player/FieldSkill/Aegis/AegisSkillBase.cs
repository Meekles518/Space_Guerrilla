using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;
using UnityEngine.UI;
//


public abstract class AegisSkillBase
{
    
    public AegisSkillName SkillName { get; set; } //��ų �̸�

    public int Level { get; private set; } //��ų ����

    public int skillCnt { get;  set; } //��ų ��� ���� Ƚ��

    public Image skillImg { get;  set; } // ��ų�� image

    protected ISkillBehavior skillBehavior; //��ų �ൿ �������̽�

    public GameObject player { get; set; } //�÷��̾� ������Ʈ

    //AegisSkillBase ������
    public AegisSkillBase(int level)
    {
        this.Level = level; //��ų�� ���� ����
    }//AegisSkillBase ������ ��


    protected abstract void SetBehavior(int level); //������ ���� ��ų �ൿ�� �����ϴ� �޼���

    //ISkillBehavior �������̽��� ��ӹ��� ��ų �ൿ Ŭ������ �޾ƿ��� �޼���
    public void UseSkill()
    {
        skillBehavior.UseSkill(); //��ų ��� �޼��� ȣ��
    }





}
