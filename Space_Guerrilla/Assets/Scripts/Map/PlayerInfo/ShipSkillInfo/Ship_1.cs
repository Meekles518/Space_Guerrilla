using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    [CreateAssetMenu]
    public class Ship_1 : ScriptableObject
    {
       



        //���� Skill���� �̸��� ��Ÿ���� List�ȿ� �����ؾ� ��

        //skill�� �̸����� ���� ����Ʈ
        public List<string> skillNames = new List<string>() {"Skill1", "Skill2", "Skill3"};

        //skill�� �ִ� ��Ÿ���� ������ ����Ʈ
        public List<float> skillMaxCooltime = new List<float>() {3.0f, 5.0f, 6.0f };


        //�� Skill���� �̸��� �Լ������� ����� �����ؾ� ��.
        //��� Skill���� ��� �� MapManager�� ������ ���� ��Ÿ���� ���¸� Ȯ���ؾ� �Ѵ�.
        //�� ��ų�� �ǹ��ϴ� �Լ� ���� �� �� ��ų�� Ư¡��� ��Ÿ�� �������� ���� ����.

        //Skill1 �Լ�
        public void Skill1()
        {


        }//Skill1


        //SKill2 �Լ�
        public void Skill2()
        {


        }//Skill2

        //Skill3 �Լ�
        public void Skill3()
        {


        }//Skill3



    }



}
 
