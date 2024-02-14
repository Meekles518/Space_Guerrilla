using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오브젝트의 지속적인 생성, 파괴에 의한 메모리 손실을 없애기 위해 제작
// 모든 프리팹들을 리스트로 저장해 각 리스트에서 필요한 오브젝트를 꺼내서 사용
// 추후에 각 유형의 적들을 넣어서 전대매니저 스크립트로 원하는 위치에 전대를 생성할 수 있게 할 예정
public class PoolManager : MonoBehaviour
{

    //Prefab을 보관할  변수
    public GameObject[] Prefabs;

    //Pool 을 담을 List변수
    public List<GameObject>[] Pools;

    //Pools list 초기화
    private void Awake()
    {
        // Pools에 담을 프리팹의 개수를 배열길이로 설정
        Pools = new List<GameObject>[Prefabs.Length];

        // 각 프리팹의 종류를 List<GameObject>로 불러옴
        for (int i = 0; i < Pools.Length; i++)
        {

            Pools[i] = new List<GameObject>();

        }
    }

    //GameObject를 return해주는 함수 
    public GameObject Get(int idx, Transform fireTransform)
    {
        // Select 선언, 초기화
        GameObject Select = null;

        //Pools[idx]에 대해서 E로 접근
        foreach (GameObject E in Pools[idx])
        {

            //E가 비활설화 되어있는 경우
            if (!E.activeSelf)
            {

                // Select에 E를 할당
                Select = E;
                // 위치를 발사기의 위치로 변경
                // 이렇게 안하면 마지막으로 비활성화된 자리에서 날아감
                Select.transform.position = fireTransform.position;
                // 활성화
                Select.SetActive(true);
                break;
            }
        }


        //Select의 값이 없을 경우
        if (!Select)
        {

            //Instantiate로 Prefabs[idx]에 있는 원소를 발사기의 위치에 생성하고,
            Select = Instantiate(Prefabs[idx], fireTransform.position, fireTransform.rotation);

            //Pools에 Add해주기
            Pools[idx].Add(Select);

        }

        // 할당된 게임 오브젝트를 리턴
        return Select;

    }




}