using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//EnemyOne의 Circle을 나타내는 클래스
public class EnemyOneCircle : Enemy_Circle
{
    EnemyOneCircle()
    {
        base.enemyName = EnemyName.EnemyOne; //적의 이름을 저장
    }


    //EnemyOne의 Map 이동 로직을 구현
    public override void enemyAi()
    {
        throw new System.NotImplementedException();
    }

}
