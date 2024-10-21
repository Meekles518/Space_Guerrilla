using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv1StreamLiner : MonoBehaviour, ISkillBehavior
{
    private Shooter[] shooters; //Shooter �迭

    public int streamLinerCnt { get; set; } //��� ���� Ƚ��
    public float streamLinerTime { get; set; } //���� �ð�
    public float streamLinerCool {  get; set; } //���� �ð�\
    public int streamLinerMax {  get; set; } //�ִ� źâ �� ����
    public float streamLinerRotateTime { get; set; } //���� ȸ���� �ʿ��� �ð�
    private int magCapacity { get; set; } //�ִ� źâ ��

    AegisSkillManager AegisSkillManager; //

    public Lv1StreamLiner(GameObject p, AegisSkillManager skillManager)
    {
        this.shooters = p.GetComponentsInChildren<Shooter>();
        streamLinerCnt = 1;
        streamLinerTime = 5.0f;
        streamLinerCool = 3.0f;
        streamLinerMax = 999;
        streamLinerRotateTime = 1.5f;
        magCapacity = shooters[0].magCapacity;
        this.AegisSkillManager = skillManager;
       
    }

    public void UseSkill()
    {


    }//UseSkill


    public void CancelSkill()
    {

    }


    //shooter ȸ���� �����ϴ� �ڷ�ƾ
    private IEnumerator rotateShooter(bool active)
    {
        //0���� ����, 1���� ����, 2���� �߾� ����
        //�ӽ÷� �ϵ� �ڵ����� ���� ������ -30, ������ 30 �� z�� rotation ������ ������.
        Quaternion rightShooter = Quaternion.Euler(0, 0, -30f);
        Quaternion leftShooter = Quaternion.Euler(0, 0, 30f);
        Quaternion midShooter = Quaternion.Euler(0, 0, 0);
        float elapsedTime = 0f;

        //active�� true���, ������ ���� �������� ȸ��
        if (active)
        {
            //streamLinerRotateTime ���� shooter �� ȸ����Ű��
            while (elapsedTime < streamLinerRotateTime)
            {
                shooters[0].transform.localRotation = Quaternion.Lerp(rightShooter, midShooter, elapsedTime / streamLinerRotateTime);
                shooters[1].transform.localRotation = Quaternion.Lerp(leftShooter, midShooter, elapsedTime / streamLinerRotateTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            //ȸ�� �� ������ ���� ���� ��ǥ ������ �� ����
            shooters[0].transform.localRotation = midShooter;
            shooters[1].transform.localRotation = midShooter;

  
        }

        //active�� false���, ������ ������ �������� �ǵ����� ȸ��
        else
        {
            //streamLinerRotateTime ���� shooter �� ȸ����Ű��
            while (elapsedTime < streamLinerRotateTime)
            {
                shooters[0].transform.localRotation = Quaternion.Lerp(midShooter, rightShooter, elapsedTime / streamLinerRotateTime);
                shooters[1].transform.localRotation = Quaternion.Lerp(midShooter, leftShooter, elapsedTime / streamLinerRotateTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            //ȸ�� �� ������ ���� ���� ��ǥ ������ �� ����
            shooters[0].transform.localRotation = Quaternion.Euler(0, 0, -30f);
            shooters[1].transform.localRotation = Quaternion.Euler(0, 0, 30f);

            // ������ ���� �ð� - ȸ�� �ҿ� �ð�, �� ���� ������ �ð����� ���
            yield return new WaitForSeconds(streamLinerCool - streamLinerRotateTime);


        }

    }


    //Shooters �迭�� �߻���� �ִ�/���� źȯ�� �����ϴ� �޼���
    private void magnumSet(int targetNum)
    {
        for (int i = 0; i < shooters.Length; i++)
        {
            shooters[i].magCapacity = targetNum;
            shooters[i].magAmmo = targetNum;
        }


    }//magnumSet

     
}
