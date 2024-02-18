using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ī�޶� �� ������ �������� �ϴ� ��ũ��Ʈ 

public class CameraExtensions : MonoBehaviour
{
    [HideInInspector]
    public Transform playerTransform;
   [HideInInspector]
    public float cameraMoveSpeed;
    [HideInInspector]
    public float height;
    [HideInInspector]
    public float width;
    [HideInInspector]
    public Vector3 cameraPosition;
    [HideInInspector]
    public Vector2 center;

    [Header("��ũ�� �Է�")]
    public Vector2 mapSize;

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
