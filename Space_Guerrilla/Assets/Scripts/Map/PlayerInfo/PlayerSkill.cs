using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Skill
{

    public class PlayerSkill : MonoBehaviour
    {

        //��򰡿��� Skill�� �̸��� ��Ÿ�� �������� �����س���, MapManager���� �Ǵ��� ���ּ��� ������ ����
        //�� ���ּ��� ��ų ������ ���⿡ �����ϰ�, ��ư ���� �� �� ��ų���� ����� ��ư�� ���� �� ��ġ ����



        //skill�� �̸����� ���� ����Ʈ
        public List<string> skillNames = new List<string>();

        //skill�� �ִ� ��Ÿ���� ������ ����Ʈ
        public List<float> skillMaxCooltime = new List<float>();

        //skill�� ���� ��Ÿ���� ������ ����Ʈ
        public List<float> skillCurrentColltime = new List<float>();


        public float skillVerticalDistance; //Skill UI���� ���� �Ÿ� 

        public float skillHorizontalDinstance; //Skill UI���� ���� �Ÿ�


        public void SetSkillBtn()
        {
            //Skill�� ������ Ȧ����
            if (skillNames.Count % 2 == 1)
            {
                //Ȧ�� ���� ��ų ��ư�� ���������� ���� �� OnClick �Լ��� ��ų ��� �߰�.



            }

            //Skill�� ������ ¦����
            else if (skillNames.Count % 2 == 0)
            {


            }



        }//SetSkillBtn

    }

}
 
 
