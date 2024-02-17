using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지
// 감지된 입력값을 다른 컴포넌트들이 사용할 수 있도록 제공
// 아래에 구현해 놓은 방식대로 원하는 입력들을 계속 추가 가능
public class PlayerInput : MonoBehaviour
{
    // Input 세팅에 있는 상응하는 string에 맞게 Name변수를 할당
    private string moveVerticalName = "Vertical"; // 위아래 움직임을 위한 입력축 이름
    private string moveHorizontalName = "Horizontal"; // 좌우 움직임을 위한 입력축 이름
    private string fireButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름
    private string specialButtonName = "Fire2"; // 특수 공격을 위한 입력 버튼 이름
    private string reloadButtonName = "Reload"; // 재장전을 위한 입력 버튼 이름
    private string missileButtonName = "Missile";
    private string autoButtonName = "Auto";

    

    // 값 할당은 내부에서만 가능
    public float moveVertical { get; private set; } // 감지된 움직임 입력값
    public float moveHorizontal { get; private set; } // 감지된 회전 입력값
    public bool fire { get; private set; } // 감지된 발사 입력값
    public bool special { get; private set; } // 감지된 특수 공격 입력값
    public bool reload { get; private set; } // 감지된 재장전 입력값
    public bool missile { get; private set; }
    public bool auto { get; private set; }  //감지된 Auto모드 변환
    public bool isAuto = false; //현재 Auto인지 아닌지를 저장할 bool 변수


    // 매프레임 사용자 입력을 감지
    private void Update()
    {
        // 게임오버 상태에서는 사용자 입력을 감지하지 않는다
        /*if (GameManager.instance != null
            && GameManager.instance.isGameover)
        {
            moveVertical = 0;
            moveHorizontal = 0;  
            fire = false;
            special = false;
            reload = false;
            return;
        }*/

        // moveVertical에 관한 입력 감지
        moveVertical = Input.GetAxis(moveVerticalName);
        // moveHorizontal에 관한 입력 감지
        moveHorizontal = Input.GetAxis(moveHorizontalName);
        // fire에 관한 입력 감지
        fire = Input.GetButton(fireButtonName);
        //special에 관한 입력 감지
        special = Input.GetButton(specialButtonName);
        // reload에 관한 입력 감지
        reload = Input.GetButtonDown(reloadButtonName);
        missile = Input.GetButtonDown(missileButtonName);
        auto = Input.GetButtonDown(autoButtonName);

        if (auto && isAuto == false)
        {
            isAuto = true;
            Debug.Log("isAuto T");
        }

        else if (auto && isAuto == true)
        {
            isAuto = false;
            Debug.Log("isAuto F");
        }

    }
}
