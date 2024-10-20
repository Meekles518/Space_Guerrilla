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

    public bool IsActive { get; private set; } //��ų Ȱ��ȭ ����

    public int skillCnt { get;  set; } //��ų ��� ���� Ƚ��

    public Image skillImg { get;  set; } // ��ų�� image

    protected ISkillBehavior skillBehavior; //��ų �ൿ �������̽�


    //AegisSkillBase ������
    public AegisSkillBase(int level)
    {
        this.Level = level; //��ų�� ���� ����
    }//AegisSkillBase ������ ��


    protected abstract void SetBehavior(int level); //������ ���� ��ų �ൿ�� �����ϴ� �޼���

    //ISkillBehavior �������̽��� ��ӹ��� ��ų �ൿ Ŭ������ �޾ƿ��� �޼���
    public void UseSkill()
    {
        IsActive = true; //��ų ��� �� ǥ��
        skillBehavior.UseSkill(); //��ų ��� �޼��� ȣ��
    }

    //ISkillBehavior �������̽��� ��ӹ��� ��ų �ൿ Ŭ������ �޾ƿ��� �޼���
    public void CancelSkill()
    {
        IsActive = false;   //��ų �̻�� �� ǥ��
        skillBehavior.CancelSkill(); //��ų ��� �޼��� ȣ��
    }




}
