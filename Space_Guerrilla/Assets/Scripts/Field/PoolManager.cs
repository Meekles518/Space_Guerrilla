using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������Ʈ�� �������� ����, �ı��� ���� �޸� �ս��� ���ֱ� ���� ����
// ��� �����յ��� ����Ʈ�� ������ �� ����Ʈ���� �ʿ��� ������Ʈ�� ������ ���
// ���Ŀ� �� ������ ������ �־ ����Ŵ��� ��ũ��Ʈ�� ���ϴ� ��ġ�� ���븦 ������ �� �ְ� �� ����
public class PoolManager : MonoBehaviour
{

    //Prefab�� ������  ����
    public GameObject[] Prefabs;

    //Pool �� ���� List����
    public List<GameObject>[] Pools;

    //Pools list �ʱ�ȭ
    private void Awake()
    {
        // Pools�� ���� �������� ������ �迭���̷� ����
        Pools = new List<GameObject>[Prefabs.Length];

        // �� �������� ������ List<GameObject>�� �ҷ���
        for (int i = 0; i < Pools.Length; i++)
        {

            Pools[i] = new List<GameObject>();

        }
    }

    //GameObject�� return���ִ� �Լ� 
    public GameObject Get(int idx, Transform fireTransform)
    {
        // Select ����, �ʱ�ȭ
        GameObject Select = null;

        //Pools[idx]�� ���ؼ� E�� ����
        foreach (GameObject E in Pools[idx])
        {

            //E�� ��Ȱ��ȭ �Ǿ��ִ� ���
            if (!E.activeSelf)
            {

                // Select�� E�� �Ҵ�
                Select = E;
                // ��ġ�� �߻���� ��ġ�� ����
                // �̷��� ���ϸ� ���������� ��Ȱ��ȭ�� �ڸ����� ���ư�
                Select.transform.position = fireTransform.position;
                // Ȱ��ȭ
                Select.SetActive(true);
                break;
            }
        }


        //Select�� ���� ���� ���
        if (!Select)
        {

            //Instantiate�� Prefabs[idx]�� �ִ� ���Ҹ� �߻���� ��ġ�� �����ϰ�,
            Select = Instantiate(Prefabs[idx], fireTransform.position, fireTransform.rotation);

            //Pools�� Add���ֱ�
            Pools[idx].Add(Select);

        }

        // �Ҵ�� ���� ������Ʈ�� ����
        return Select;

    }




}