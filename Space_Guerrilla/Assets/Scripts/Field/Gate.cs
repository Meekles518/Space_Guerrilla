using Map;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    TextMeshPro gateTxt; // 게이트의 텍스트메쉬프로 컴포넌트
    public GameObject player; // 플레이어 오브젝트
    //public int gateNum; // 게이트를 구별하기 위한 번호
    public float distToPlayer; // 게이트와 플레이어 사이의 거리
    public float escapeRange = 10f; // 탈출이 가능하기 위한 게이트와 플레이어 사이의 거리
    public float escapeTime = 30f; // 전투가 시작하고 탈출이 활성화 되기 위한 최소한의 시간

    public Node gateNode; //게이트를 구별하기 위해 번호 대신 Node 사용

    public void OnEnable()
    {
        gateTxt = GetComponent<TextMeshPro>(); // 텍스트메쉬프로 컴포넌트를 가져옴
        gateTxt.text = null; // 텍스트를 초기화
        player = GameManager.instance.player; // 게임매니저에서 플레이어 오브젝트를 찾음
    }

    public void FixedUpdate()
    {
        distToPlayer = Vector2.Distance((Vector2)transform.position, player.transform.position); // 플레이어와 게이트 사이의 거리를 지속적으로 계산
        if (distToPlayer < escapeRange) // 탈출 가능거리안에 플레이어가 있다면
        {
            if(GameManager.instance.gameTime > escapeTime) // 게임시간이 탈출버튼이 활성화 되는 시간을 지났다면
            {
                gateTxt.text = $"Escape Active"; // 탈출 활성화 텍스트 나옴
                GameManager.instance.canEscape = true; // 게임매니저의 탈출가능 bool변수를 참으로 설정
                GameManager.instance.gateNode = gateNode;
            }
            else if(GameManager.instance.gameTime < escapeTime) // 게임시간이 탈출버튼이 활성화 되는 시간을 지나지 않았다면
            {
                gateTxt.text = $"Escape Inactive"; // 탈출 비활성화 텍스트 나옴
            }                 
        }
        else if(distToPlayer > escapeRange) // 탈출 가능거리안에 플레이어가 없다면
        {
            gateTxt.text = null; // 텍스트 초기화
        }
    }
}
