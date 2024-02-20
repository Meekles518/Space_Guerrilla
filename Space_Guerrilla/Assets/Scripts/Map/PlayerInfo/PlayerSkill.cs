using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Map;

namespace Skill
{

    public class PlayerSkill : MonoBehaviour
    {

        //��򰡿��� Skill�� �̸��� ��Ÿ�� �������� �����س���, MapManager���� �Ǵ��� ���ּ��� ������ ����
        //�� ���ּ��� ��ų ������ ���⿡ �����ϰ�, ��ư ���� �� �� ��ų���� ����� ��ư�� ���� �� ��ġ ����

        //2�� 20�� ����, ���ӿ�����Ʈ ������ ����, ��ư�� ����??


        //skill�� �̸����� ���� ����Ʈ
        public List<string> skillNames = new List<string>();

        //skill�� �ִ� ��Ÿ���� ������ ����Ʈ
        public List<float> skillMaxCooltime = new List<float>();

        //skill�� ���� ��Ÿ���� ������ ����Ʈ
        public List<float> skillCurrentColltime = new List<float>();


        public float skillVerticalDistance; //Skill UI���� ���� �Ÿ� 

        public float skillHorizontalDinstance; //Skill UI���� ���� �Ÿ�


        //Map ���� ��ų���� ��ư���� ȭ�鿡 ǥ���ϴ� �Լ�
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


        //�ٸ� ��ũ��Ʈ�� ��ϵǾ� �ִ� ��ų �������� ����� �������� �Լ�
        public void getSkill()
        {
            //MapManager�� shipName�� ���� cas��
            switch (MapManager.instance.shipName)
            {
                //Ship1 �̶��
                case MapManager.ShipName.Ship1:

                    //Ship_1 ������Ʈ �����ͼ�, ���� �ű��
                    Ship_1 ship_1 = GetComponent<Ship_1>();

                    skillNames = ship_1.skillNames; //��ų �̸�
                    skillMaxCooltime = ship_1.skillMaxCooltime; //��ų ��Ÿ��
                    skillCurrentColltime = ship_1.skillMaxCooltime; //��ų ���� ��Ÿ��

                    break;

                //�Ʒ��� ����ؼ� �� ���ּ� �� ��ų�� �������� �ڵ带 �ۼ��ؾ� ��.


            }



        }//getSkill 


    }

}
 
 
