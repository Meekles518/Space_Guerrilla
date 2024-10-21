using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv1StreamLiner : MonoBehaviour, ISkillBehavior
{
    private Shooter[] shooters; //Shooter 배열

    public int streamLinerCnt { get; set; } //사용 가능 횟수
    public float streamLinerTime { get; set; } //지속 시간
    public float streamLinerCool {  get; set; } //과열 시간\
    public int streamLinerMax {  get; set; } //최대 탄창 수 변수
    public float streamLinerRotateTime { get; set; } //포신 회전에 필요한 시간
    private int magCapacity { get; set; } //최대 탄창 수

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


    //shooter 회전을 제어하는 코루틴
    private IEnumerator rotateShooter(bool active)
    {
        //0번이 우측, 1번이 좌측, 2번이 중앙 포대
        //임시로 하드 코딩으로 각각 우측을 -30, 좌측을 30 의 z축 rotation 값으로 설정함.
        Quaternion rightShooter = Quaternion.Euler(0, 0, -30f);
        Quaternion leftShooter = Quaternion.Euler(0, 0, 30f);
        Quaternion midShooter = Quaternion.Euler(0, 0, 0);
        float elapsedTime = 0f;

        //active가 true라면, 포신을 직선 방향으로 회전
        if (active)
        {
            //streamLinerRotateTime 동안 shooter 들 회전시키기
            while (elapsedTime < streamLinerRotateTime)
            {
                shooters[0].transform.localRotation = Quaternion.Lerp(rightShooter, midShooter, elapsedTime / streamLinerRotateTime);
                shooters[1].transform.localRotation = Quaternion.Lerp(leftShooter, midShooter, elapsedTime / streamLinerRotateTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            //회전 후 오차값 방지 위해 목표 각도로 재 조정
            shooters[0].transform.localRotation = midShooter;
            shooters[1].transform.localRotation = midShooter;

  
        }

        //active가 false라면, 포신을 원래의 방향으로 되돌리는 회전
        else
        {
            //streamLinerRotateTime 동안 shooter 들 회전시키기
            while (elapsedTime < streamLinerRotateTime)
            {
                shooters[0].transform.localRotation = Quaternion.Lerp(midShooter, rightShooter, elapsedTime / streamLinerRotateTime);
                shooters[1].transform.localRotation = Quaternion.Lerp(midShooter, leftShooter, elapsedTime / streamLinerRotateTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            //회전 후 오차값 방지 위해 목표 각도로 재 조정
            shooters[0].transform.localRotation = Quaternion.Euler(0, 0, -30f);
            shooters[1].transform.localRotation = Quaternion.Euler(0, 0, 30f);

            // 과부하 설정 시간 - 회전 소요 시간, 즉 남은 과부하 시간동안 대기
            yield return new WaitForSeconds(streamLinerCool - streamLinerRotateTime);


        }

    }


    //Shooters 배열의 발사대의 최대/현재 탄환을 조절하는 메서드
    private void magnumSet(int targetNum)
    {
        for (int i = 0; i < shooters.Length; i++)
        {
            shooters[i].magCapacity = targetNum;
            shooters[i].magAmmo = targetNum;
        }


    }//magnumSet

     
}
