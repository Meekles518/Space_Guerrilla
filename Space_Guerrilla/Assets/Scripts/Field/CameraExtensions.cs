using Cinemachine;
using Map;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraExtensionss : MonoBehaviour
{
  
    
    public float mapX;
    public float mapY;
    public GameObject player;
    public GameObject followcamera;
    public CinemachineVirtualCamera view;
    public float moveSpeed;

    public void Start()
    {
        followcamera = GameObject.Find("Follow Camera");
        player = GameObject.Find("Player");
        view = followcamera.GetComponent<CinemachineVirtualCamera>();

    }

    void Update()
    {
        if(Mathf.Abs(followcamera.transform.position.x) > mapX || Mathf.Abs(followcamera.transform.position.y) > mapY)
        {
            view.Follow = null;

        }
        else
        {
            view.Follow = player.transform;

        }

    }
}
