using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Skill
{
    [CreateAssetMenu]
    public class Aegis : ScriptableObject
    {

        //���� Skill���� �̸��� ��Ÿ���� List�ȿ� �����ؾ� ��

        //skill�� �̸����� ���� ����Ʈ
        public List<string> skillNames = new List<string>() { "Aegis_fieldRepair", "Aegis_dimensionJump", "Aegis_ward", "Aegis_remoteRepair", "Aegis_missile", "Aegis_afterBurner", "Aegis_streamLiner"};

        //skill�� �ִ� ��Ÿ���� ������ ����Ʈ
        //-1�� �нú�, 0�� ����Ʈ ��ų, �� �ܴ̿� ��� ��ų �ǹ�
        public List<float> skillMaxCooltime = new List<float>() { -1f, 3f, 1f, 4f, 4f, 0f, 0f};



    }

}
 
