using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnInfo : MonoBehaviour
{
    public int EnemyTypes;
    public Transform EnemySpawn;

    public EnemySpawnInfo(int enemyTypes, Transform enemySpawn)
    {
        EnemyTypes = enemyTypes;
        EnemySpawn = enemySpawn;
    }

   
}
