using Cinemachine;
using Map;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 카메라가 맵 밖으로 못나가게 하는 스크립트 

public class CameraExtensions : MonoBehaviour
{
    //[HideInInspector]
    public Transform playerTransform;
    //[HideInInspector]
    public float cameraMoveSpeed;
    //[HideInInspector]
    public float height;
    //[HideInInspector]
    public float width;
    //[HideInInspector]
    public Vector3 cameraPosition;
    //[HideInInspector]
    public Vector2 center;

    [Header("맵크기 입력")]
    public Vector2 mapSize;

    //필드 진입 시 생성된 Player을 저장할 변수
    public GameObject player; 



    //카메라의 Player 추적을 GameManager의 Player 생성 후에 동작하게끔 하기 위한 변수
    private bool startCheck = false; 

    void OnEnable()
    {
        

         
    }

    void FixedUpdate()
    {
        if (!startCheck)
        {
            player = GameManager.instance.player;
            playerTransform = player.GetComponent<Transform>();
            height = Camera.main.orthographicSize;
            width = height * Screen.width / Screen.height;
            cameraMoveSpeed = player.GetComponent<PlayerMovement>().moveSpeed;
            startCheck = true;
        }

        else
        {
            cameraMoveSpeed = player.GetComponent<PlayerMovement>().moveSpeed;
            LimitCameraArea();
        }
         
    }

    void LimitCameraArea()
    {
        transform.position = Vector3.Lerp(transform.position,
                                          playerTransform.position + cameraPosition,
                                          Time.deltaTime * cameraMoveSpeed);
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
