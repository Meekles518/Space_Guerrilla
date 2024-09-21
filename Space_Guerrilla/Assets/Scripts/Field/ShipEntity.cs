using System;
using System.Collections;
using UnityEngine;

//플레이어, 적 우주선, 드론등 우주에서 이동하고 체력있는 모든 오브젝트들의 데미지 판정과 사망판정을 담당
public class ShipEntity : MonoBehaviour
{
    public bool dead { get; protected set; } //우주선의 사망 여부를 알 수 있는 변수
    private Collider2D collideEnemy; // 충돌한 상대의 콜라이더
    private string objectTag; // 본인의 태그
    private Rigidbody2D rb2;
    private bool GD;

    [Header("오브젝트 스탯")]
    public float maxhealth; //우주선의 최대체력
    public float shield; // 우주선의 방어도
    public float damage; //우주선의 공격력(방어력)
    public float defensestat; // 우주선의 방호력(우주선의 damage와 총 health+shield에 영향을줌)
    [Header("충돌 관련 수치")]
    public float collideRate; // 충돌판정을 시행하는 주기
    private bool inCollision; // 현재 충돌 여부를 판단하는 논리 변수
    [Header("현재 체력")]
    public float health;

    public Vector2 moveDirection;
    public Vector2 faceDirection;
    public float rebound;
    public float Degree;


    //onEnable로 초기 값 설정
    protected virtual void OnEnable()
    {
        dead = false; // 사망변수에 거짓 할당
        inCollision = false; // 충돌변수에 거짓 할당
        objectTag = gameObject.tag; // 자신의 태그를 태그 변수에 할당
        maxhealth = maxhealth * (1 + defensestat * 0.75f / 100);
        health = maxhealth;
        rb2= GetComponent<Rigidbody2D>();
        GD = false;
        Degree = 180;
    }

    //ShipEntity 스크립트의 변수 값들을 외부에서 불러오는 함수. 사용 빈도가 높을 것으로 예상되고,
    //대부분의 오브젝트들인 ShipEntity 스크립트를 가지고 있을 것이기에, 여기에 함수 정의

    public void getShipEntity(PlayerInfo playerInfo)
    {
        this.maxhealth = playerInfo.maxhealth;
        this.shield = playerInfo.shield;
        this.damage = playerInfo.damage;
        this.defensestat = playerInfo.defensestat;
        this.health = playerInfo.health;
        this.rebound = playerInfo.rebound;
        this.collideRate = playerInfo.collideRate;
    }

    public void getShipEntity(PlayerBulletInfo playerBulletInfo)
    {
        this.maxhealth = playerBulletInfo.maxhealth;
        this.shield = playerBulletInfo.shield;
        this.damage = playerBulletInfo.damage;
        this.defensestat = playerBulletInfo.defensestat;
        this.health = playerBulletInfo.health;
        this.rebound = playerBulletInfo.rebound;
        this.collideRate = playerBulletInfo.collideRate;
    }

    public void getShipEntity(ShipEntity shipEntity)
    {
        this.maxhealth = shipEntity.maxhealth;
        this.shield = shipEntity.shield;
        this.damage = shipEntity.damage;
        this.defensestat = shipEntity.defensestat;
        this.health = shipEntity.health;
        this.rebound = shipEntity.rebound;
        this.collideRate = shipEntity.collideRate;
    }



    // 충돌중일때 실행
    private void OnTriggerStay2D(Collider2D other)
    {
        inCollision = true; // 충돌변수 참으로 변경
        collideEnemy = other; // 충돌상대의 콜라이더 가져옴
        if (GD == false)
        {
            StartCoroutine("GiveDamage"); // 공격 코루틴 실행
        }
        
    }

    // 충돌이 끝나는 시점에 실행
    private void OnTriggerExit2D(Collider2D other)
    {
        inCollision = false; // 충돌변수 거짓으로 변경
        collideEnemy = null; // 충돌상대 없음으로 초괴화
        StopCoroutine("GiveDamage"); //공격 코루틴 중지
        GD = false;
    }

    // 충돌시 상대 오브젝트에 데미지를 주는 코루틴
    private IEnumerator GiveDamage()
    {
        GD = true;
        // 충돌중이고 충돌한 상대의 태그가 나와 다를 때
        if (inCollision == true && collideEnemy.tag != objectTag)
        {          
                // 상대로부터 ShipEntity 가져오기 시도    
                ShipEntity shipEntity = collideEnemy.GetComponent<ShipEntity>();
                // 상대의 ShipEntity가 성공적으로 가져와졌을 때
                if (shipEntity != null)
                {
                if (collideEnemy.tag == "Player" || collideEnemy.tag == "Enemy")
                {
                    ShipCollide(shipEntity.moveDirection, shipEntity.rebound);
                }
                else
                {
                    // 상대의 피격 매서드를 실행
                    shipEntity.TakeDamage(damage + (defensestat / 6));
                }
                }           
        }
        // 피격주기마다 반복
        yield return new WaitForSeconds(collideRate);
    }

    // 충돌시 데미지를 받는 매서드
    public virtual void TakeDamage(float otherDamage)
    {
        // 쉴드가 남아 있다면 데미지는 쉴드로 들어감
        if (shield > 0)
        {
            shield -= otherDamage * 1000 / (100 + damage) / defensestat;
        }
        // 쉴드가 없다면 데미지는 체력으로 들어감
        else
        {
            //임의로 식을 설정해봄, 받는 데미지는 상대 데미지에 정비례, 내 데미지 증가 시 감소
            health -= otherDamage / (damage + (defensestat / 6)) * 100;
        }
        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual void ShipCollide(Vector2 moveDirection, float rebound)
    {
        faceDirection = new Vector2(this.transform.position.x - collideEnemy.transform.position.x, this.transform.position.y - collideEnemy.transform.position.y).normalized;
        Degree = Quaternion.FromToRotation((Vector2)faceDirection, (Vector2)moveDirection).eulerAngles.z;
        if (Degree < 10 || Degree > 350)
        {
            rb2.AddForce(moveDirection.normalized * rebound);
            health = health - 10;
        }
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
