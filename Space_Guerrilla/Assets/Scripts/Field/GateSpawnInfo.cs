using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSpawnInfo : MonoBehaviour
{
    public float GateDist;
    public float GateAngle;

    public GateSpawnInfo(float gateDist, float gateAngle)
    {
        GateDist = gateDist;
        GateAngle = gateAngle;
    }
}