using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

//���� Map�� ��Ÿ���� ���� ����ϴ� Circle�� ��� ������ ���� Ŭ����,
//��� Ai ��ũ��Ʈ�� Enemy_Circle Ŭ������ ��ӹ޴´�??
public class Enemy_Circle : MonoBehaviour
{

    //���� �ൿ���� enum
    public enum Behavior
    {
        Defensive,
        Offensive,
        Opportunistic,
        Stationary,

    }


    public Behavior behavior; //���� �ൿ������ ������ ����


    //virtual, ���� �Լ��� ���� Ai ������ ����. �� Class�� ��ӹ޴� ���� �ٸ� ������ ����
    //�Լ��� ���� �ϼ���Ű��.
    public virtual void movement()
    {

    }





}

