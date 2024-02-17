using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Map
{
    //��� Node���� ������ ���� Point Class ����
    //Equals �Լ� ������� IEquatable ���
    public class Point : IEquatable<Point>
    {
        public int x; //x ��ǥ�� ������ int ����
        public int y; //y ��ǥ�� ������ int ����


        //��ǥ�� �����ϱ�
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


         

        //�� Point �� ��ǥ�� ������ Ȯ���ϴ� �Լ�. ���۾����� �ϴ� �ʿ� ���� ����?
        public bool Equals(Point other)
        {
            //this, �� Point �� Point�� other�̶� ������ ���θ� Ȯ���ϴ� �Լ�,
            //ReferenceEquals�� �� �� null�� ������ true�� ��ȯ�ϹǷ�, ù if������ �� ��츦 �ɷ���
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return x == other.x && y == other.y;
        }



    }



}
 
