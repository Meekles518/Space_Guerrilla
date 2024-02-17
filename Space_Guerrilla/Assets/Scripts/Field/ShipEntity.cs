using System;
using System.Collections;
using UnityEngine;

//플레이어, 적 우주선, 드론등 우주에서 이동하고 체력있는 모든 오브젝트들의 데미지 판정과 사망판정을 담당
public class ShipEntity : MonoBehaviour
{
    public bool dead { get; protected set; } //우주선의 사망 여부를 알 수 있는 변수
    public float startingHealth { get; protected set; } //우주선의 초기 체력
    public float health; //우주선의 현재체력
    public float damage; //우주선의 방어력(데미지)
    public float shield; // 우주선의 방호막(?)
    public float collideRate; // 충돌판정을 시행하는 주기
    private bool inCollision; // 현재 충돌 여부를 판단하는 논리 변수
    private Collider2D collideEnemy; // 충돌한 상대의 콜라이더
    private string objectTag; // 본인의 태그

    //onEnable로 초기 값 설정
    protected virtual void OnEnable()
    {
        dead = false; // 사망변수에 거짓 할당
        //health = startingHealth; //현재체력 = 초기체력
        inCollision = false; // 충돌변수에 거짓 할당
        objectTag = gameObject.tag; // 자신의 태그를 태그 변수에 할당
        collideRate = 0.5f; // 주기에 값 할당
        shield = 0; // 임시로 초기화, 나중에 지울것

    }

    // 충돌중일때 실행
    private void OnTriggerStay2D(Collider2D other)
    {
        inCollision = true; // 충돌변수 참으로 변경
        collideEnemy = other; // 충돌상대의 콜라이더 가져옴
        StartCoroutine("giveDamage"); // 공격 코루틴 실행
    }

    // 충돌이 끝나는 시점에 실행
    private void OnTriggerExit2D(Collider2D other)
    {
        inCollision = false; // 충돌변수 거짓으로 변경
        collideEnemy = null; // 충돌상대 없음으로 초괴화
        StopCoroutine("giveDamage"); //공격 코루틴 중지
    }

    // 충돌시 상대 오브젝트에 데미지를 주는 코루틴
    private IEnumerator giveDamage()
    {
        // 충돌중이고 충돌한 상대의 태그가 나와 다를 때
        if (inCollision == true && collideEnemy.tag != objectTag)
        {          
                // 상대로부터 ShipEntity 가져오기 시도    
                ShipEntity shipEntity = collideEnemy.GetComponent<ShipEntity>();
                // 상대의 ShipEntity가 성공적으로 가져와졌을 때
                if (shipEntity != null)
                {
                    // 상대의 피격 매서드를 실행
                    shipEntity.takeDamage(damage);                   
                }           
        }
        // 피격주기마다 반복
        yield return new WaitForSeconds(collideRate);
    }

    // 충돌시 데미지를 받는 매서드
    public virtual void takeDamage(float otherDamage)
    {
        // 쉴드가 남아 있다면 데미지는 쉴드로 들어감
        if (shield > 0)
        {
            shield -= otherDamage * 100 / (100 + damage);
        }
        // 쉴드가 없다면 데미지는 체력으로 들어감
        else
        {
            //임의로 식을 설정해봄, 받는 데미지는 상대 데미지에 정비례, 내 데미지 증가 시 감소
            health -= otherDamage * 100 / (100 + damage);
        }
        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    // 사망 처리
    public virtual void Die()
    {
        // 사망 상태를 참으로 변경
        dead = true;
        // 게임 오브젝트를 비활성화
        gameObject.SetActive(false);
    }






}
