using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    //��ų�� ����ϴ� �ൿ�� �����ϴ� �������̽�
    //��� ��ų���� �� �������̽��� ��ӹ޾� ����
    public interface ISkillBehavior
    {


        //���� ��ų�� ����ϴ� �޼���
        public abstract void UseSkill();

        //������� ��ų�� ����ϴ� �޼���
        public abstract void CancelSkill();

        //�̿ܿ��� �ʿ��Ѱ� �ִٸ�..


    }


