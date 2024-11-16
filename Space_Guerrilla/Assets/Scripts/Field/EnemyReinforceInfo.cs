using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReinforceInfo : MonoBehaviour
{
    public int EnemyTypes;
    public int ReinforceTime;
    public float GateAngle;

    public EnemyReinforceInfo(int enemyTypes, int reinforceTime, float gateAngle)
    {
        EnemyTypes = enemyTypes;
        ReinforceTime = reinforceTime;
        GateAngle = gateAngle;
    }
}
