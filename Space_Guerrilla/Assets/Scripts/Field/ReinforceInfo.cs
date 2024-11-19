using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforceInfo : MonoBehaviour
{
    public int EnemyTypes;
    public int ReinforceTime;
    public int GateNumber;

    public ReinforceInfo(int enemyTypes, int reinforceTime, int gateNumber)
    {
        EnemyTypes = enemyTypes;
        ReinforceTime = reinforceTime;
        GateNumber= gateNumber;
    }
}
