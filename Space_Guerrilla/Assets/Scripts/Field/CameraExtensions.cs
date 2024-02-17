using Cinemachine;
using Map;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 카메라가 맵 밖으로 못나가게 하는 스크립트 

public class CameraExtensions : MonoBehaviour
{

    public Transform playerTransform;
    public Vector3 cameraPosition;
    public Vector2 center;
    public Vector2 mapSize;

    public float cameraMoveSpeed;
    public float height;
    public float width;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
        cameraMoveSpeed = GameObject.Find("Player").GetComponent<PlayerMovement>().moveSpeed;
    }

    void FixedUpdate()
    {
        LimitCameraArea();
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
