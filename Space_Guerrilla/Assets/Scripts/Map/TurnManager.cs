using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TurnManger의 기능
// 1. 현재 플레이어 턴인지 적 턴인지를 구별 -> bool isTurn을 설정
// isTurn true -> 턴종버튼 클릭 -> isTurn false
// isTurn false -> 적들의 행동을 다 끝냈다는 신호 무언가(모든적을 list로 받아 적컨트롤 내의 행동종료를 for문으로 전부 확인) -> isTurn true
// 2. 적 행동종료시 전투 발생시 전투 scene으로 전환 -> 바로 위줄처럼 list로 받은 적컨드롤 내의 전투여부를 확인하면 될듯


public class TurnManager : MonoBehaviour
{
    public bool isTurn;

    // Start is called before the first frame update
    void Start()
    {
        isTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
