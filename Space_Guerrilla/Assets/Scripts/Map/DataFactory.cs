using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MapManager에 저장될 우주선의 여러 데이터를 생성해내는 static 클래스
public static class DataFactory
{
    //Player의 PlayerInfo를 만들어내는 메서드
    public static ShipInfo CreatePlayerInfo(ShipName shipName)
    {
        switch (shipName)
        {
            //Aegis일 때에
            case ShipName.Aegis:
                return new AegisInfo();



            default:
                return null;
        }
    }

    //Player의 PlayerBulletInfo를 만들어내는 메서드
    public static BulletInfo CreatePlayerBulletInfo(ShipName shipName)
    {
        switch (shipName)
        {
            //Aegis일 때에
            case ShipName.Aegis:
                return new AegisBulletInfo();

            default:
                return null;
        }



    }//CreatePlayerBulletInfo

    //Enemy의 EnemyInfo를 만들어내는 메서드
    public static ShipInfo CreateEnemyInfo(EnemyName enemyName)
    {
        switch(enemyName)
        {
            //EnemyOne일 때에
            case EnemyName.EnemyOne:
                return new EnemyOneInfo();

            default:
                return null;

        }//switch
         

    }//CreateEnemyInfo

    //Enemy의 EnemyBulletInfo를 만들어내는 메서드
    public static BulletInfo CreateEnemyBulletInfo(EnemyName enemyName)
    {
        switch (enemyName)
        {
            //EnemyOne일 때에
            case EnemyName.EnemyOne:
                return new EnemyOneBulletInfo();

            default:
                return null;

        }//switch

    }//CreateEnemyBulletInfo



}//DataFactor