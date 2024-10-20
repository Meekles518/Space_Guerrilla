using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;

namespace Aegis
{
    //Aegis�� ����� ��ų���� enum���� ����
    public enum AegisSkillName
    {
        AfterBurner,
        StreamLiner,
        LaunchCruiseMissile,
    }
}


//Aegis ��ų ����/���/��ȣ�ۿ� ���� �����ϴ� Ŭ����
//�� Aegis Prefab�� �߰��Ǿ�� �� ��ũ��Ʈ(����)
public class AegisSkillManager : MonoBehaviour
{
    //Aegis�� ��� ������ ��ų���� ������ �ִ� Dict
    public Dictionary<AegisSkillName, AegisSkillBase> activeSkill = new Dictionary<AegisSkillName, AegisSkillBase>();
    public AegisSkillFactory skillFactory;

    //Player ���ּ��� ���õ� ������Ʈ ���� ����
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private Shooter[] shooters; 


    private void OnEnable()
    {
        skillFactory = new AegisSkillFactory(); //��ų ���丮 �ν��Ͻ� ����

        //���ּ����� �ʿ��� ������Ʈ�� ��������
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        shooters = GetComponentsInChildren<Shooter>(); 
    }

    //��ų�� ��밡�� Dict�� �߰�/��ü �ϴ� �޼���
    public void AddSkill(AegisSkillName skillName, int level)
    {
        //��ų ���丮���� Ư�� ��ų�� Ư�� ���� �ν��Ͻ� �����ؼ� ��������
        AegisSkillBase skillBase = skillFactory.createSkill(skillName, playerMovement, shooters, level);
        activeSkill.Add(skillName, skillBase); //Dict�� �߰�


    }//AddSkill

    //��ų�� Dict���� �����ϴ� �޼���
    public void RemoveSkill(AegisSkillName skillName)
    {
        if (activeSkill.ContainsKey(skillName))
        {
            activeSkill.Remove(skillName);
        }
    }//RemoveSkill

    //��ų ��� ��ư�� ������ ���� ����(����/��� ��)�� �����Ű�� �޼���
    public void Skill(AegisSkillName skillName)
    {
        switch (skillName)
        {
            case AegisSkillName.AfterBurner:

                //StreamLiner�� �۵� ���� �ƴ϶��
                if (!checkActive(AegisSkillName.StreamLiner))
                {
                    //��ų ���
                }

                //StreamLiner �۵� ���� ��
                else
                {
                    //StreamLiner ĵ�� �� ��ų ��� ����

                }

                break;

            case AegisSkillName.StreamLiner:

                //AfterBurner�� �۵� ���� �ƴ϶��
                if (!checkActive(AegisSkillName.AfterBurner))
                {
                    //��ų ���

                }

                break;

            case AegisSkillName.LaunchCruiseMissile:

                //AfterBurner, StreamLiner�� �۵����� �ƴ϶��
                if (!checkActive(AegisSkillName.AfterBurner) && !checkActive(AegisSkillName.StreamLiner))
                {
                    //��ų ���

                }


                break;
        }



    }//Skill
    
    //�ٸ� ��ų�� ��������� Ȯ���ϴ� �޼���
    private bool checkActive(AegisSkillName skillName)
    {
        if (!activeSkill.ContainsKey(skillName) || !activeSkill[skillName].IsActive)
        {
            return true;
        }
        return false;
    }//checkActive

    private void FixedUpdate()
    {
        if (playerInput.skillQ)
        {
            Skill(AegisSkillName.AfterBurner);
        }

        if (playerInput.skillZ)
        {
            Skill(AegisSkillName.StreamLiner);
        }

        if (playerInput.skillX)
        {
            Skill(AegisSkillName.LaunchCruiseMissile);
        }



    }



}
