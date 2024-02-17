using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Map
{
    //모든 Node들이 가지고 있을 Point Class 정의
    //Equals 함수 사용위해 IEquatable 사용
    public class Point : IEquatable<Point>
    {
        public int x; //x 좌표를 저장할 int 변수
        public int y; //y 좌표를 저장할 int 변수


        //좌표값 저장하기
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


         

        //두 Point 의 좌표가 같은지 확인하는 함수. 수작업으로 하니 필요 없을 수도?
        public bool Equals(Point other)
        {
            //this, 이 Point 가 Point형 other이랑 같은지 여부를 확인하는 함수,
            //ReferenceEquals는 둘 다 null일 때에도 true를 반환하므로, 첫 if문으로 그 경우를 걸러냄
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return x == other.x && y == other.y;
        }



    }



}
 
