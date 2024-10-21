using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;

namespace Aegis
{
    //Aegis�� ����� ��ų���� enum���� ����
    public enum AegisSkillName
    {
        None,
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
    public SkillFactory<AegisSkillBase, AegisSkillName, AegisSkillManager> skillFactory;

    //Player ���ּ��� ���õ� ������Ʈ ���� ����
    private GameObject player;
    private PlayerInput playerInput;

    public AegisSkillName curStatus; //���� ��� ���� ��ų�� ���� ǥ��


    public void OnEnable()
    {
        skillFactory = new AegisSkillFactory(); //��ų ���丮 �ν��Ͻ� ����
        player = this.gameObject; //�� ������Ʈ�� ������ �ִ� GameObject(���ּ� ��ü)
        playerInput = player.GetComponent<PlayerInput>(); //PlayerInput ������Ʈ ��������
        curStatus = AegisSkillName.None; //���� ��� ���� ��ų �ʱ�ȭ
    }

    //��ų�� ��밡�� Dict�� �߰�/��ü �ϴ� �޼���
    public void AddSkill(AegisSkillName skillName, int level)
    {
        //��ų ���丮���� Ư�� ��ų�� Ư�� ���� �ν��Ͻ� �����ؼ� ��������
        AegisSkillBase skillBase = skillFactory.createSkill(skillName, player, level, this);
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
        if (activeSkill.ContainsKey(skillName))
        {
            activeSkill[skillName].UseSkill();
        }

    }//Skill
    

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
