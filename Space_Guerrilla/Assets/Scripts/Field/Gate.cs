using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    TextMeshPro gateTxt;
    public GameObject player;
    public float distToPlayer;
    public float escapeRange = 10;


    // Start is called before the first frame update
    public void Awake()
    {
        gateTxt = GetComponent<TextMeshPro>();
        gateTxt.text = null;
        player = GameManager.instance.player;

    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        distToPlayer = Vector2.Distance((Vector2)transform.position, player.transform.position);
        if (distToPlayer < escapeRange)
        {
            gateTxt.text = $"Escape?";
        }
        else if(distToPlayer > escapeRange)
        {
            gateTxt.text = null;
        }
    }
}
