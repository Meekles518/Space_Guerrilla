using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MapManager�� ����� ���ּ��� ���� �����͸� �����س��� static Ŭ����
public static class DataFactory
{
    //Player�� PlayerInfo�� ������ �޼���
    public static ShipInfo CreatePlayerInfo(ShipName shipName)
    {
        switch (shipName)
        {
            //Aegis�� ����
            case ShipName.Aegis:
                return new AegisInfo();



            default:
                return null;
        }
    }

    //Player�� PlayerBulletInfo�� ������ �޼���
    public static BulletInfo CreatePlayerBulletInfo(ShipName shipName)
    {
        switch (shipName)
        {
            //Aegis�� ����
            case ShipName.Aegis:
                return new AegisBulletInfo();

            default:
                return null;
        }



    }//CreatePlayerBulletInfo

    //Enemy�� EnemyInfo�� ������ �޼���
    public static ShipInfo CreateEnemyInfo(EnemyName enemyName)
    {
        switch(enemyName)
        {
            //EnemyOne�� ����
            case EnemyName.EnemyOne:
                return new EnemyOneInfo();

            default:
                return null;

        }//switch
         

    }//CreateEnemyInfo

    //Enemy�� EnemyBulletInfo�� ������ �޼���
    public static BulletInfo CreateEnemyBulletInfo(EnemyName enemyName)
    {
        switch (enemyName)
        {
            //EnemyOne�� ����
            case EnemyName.EnemyOne:
                return new EnemyOneBulletInfo();

            default:
                return null;

        }//switch

    }//CreateEnemyBulletInfo



}//DataFactor