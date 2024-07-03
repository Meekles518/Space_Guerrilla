using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public class AegisSkill : FieldSkill
    {

        private PlayerInput playerInput; // PlayerInput�� �ҷ���
        private PlayerMovement playerMovement;
        private Shooter[] shooters; //PlayerShooter���� ������ �迭


        //skill�� �̸����� ���� ����Ʈ
        public List<string> skillNames = new List<string>() { 
            "Aegis_fieldRepair", 
            "Aegis_dimensionJump", 
            "Aegis_ward",
            "Aegis_remoteRepair", 
            "Aegis_missile", 
            "Aegis_afterBurner", 
            "Aegis_streamLiner" };
        //

        //skill�� �ִ� ��Ÿ���� ������ ����Ʈ
        //-1�� �нú�, 0�� ����Ʈ ��ų, �� �ܴ̿� ��� ��ų �ǹ�
        public List<float> skillMaxCooltime = new List<float>() { 
            -1f,
            3f, 
            1f, 
            4f, 
            4f, 
            0f, 
            0f };

        //���� ���� ȸ����
        public float remoteRepairVal = 120;

        //���� ��ų ���� Ƚ��, ��ų ����� Ƚ��, ��ų ���� �ð�, �̵��ӵ� ���� ��ġ
        public int afterBurnerCnt = 2;
        public int afterBurnerUse;
        public float afterBurnerTime = 2.5f;
        public float afterBurnerSpeed = 50f;
        public bool afterBurnerUsing = false;
        public int magCapacity;

        //���� ��ų ���� Ƚ��, ��ų ����� Ƚ��, ��ų ���� �ð�, �� ���� ���� �ð� ��ġ
        public float streamLinerCnt = 1;
        public int streamLinerUse;
        public float streamLinerTime = 3f;
        public float streamLinerCool = 2f;

        public override void SetSkillBtn()
        {
            
        }


        private void OnEnable()
        {
            //��� Ƚ�� �ʱ�ȭ
            afterBurnerUse = 0;
            streamLinerUse = 0;

            //player�� component�� ��������
            playerInput = GetComponent<PlayerInput>();
            playerMovement = GetComponent<PlayerMovement>();
            shooters = GetComponentsInChildren<Shooter>();
            magCapacity = shooters[0].magCapacity;
        }


        // Update is called once per frame
        private void FixedUpdate()
        {

        }

        //�̾Ʒ��� ��ų �Լ��� ���� ����


        IEnumerator Aegis_afterBurner(float time)
        {
            //��� Ƚ���� �����ִٸ�
            if (afterBurnerCnt > afterBurnerUse)
            {
                
                //�켱 ��ų ����������� üũ
                if (!afterBurnerUsing)
                {
                    
                    afterBurnerUsing = true; //����� ǥ��
                    playerMovement.moveSpeed += afterBurnerSpeed; //�̵��ӵ� ����

                    //��� shooter�� �ִ� źȯ�� ���� źȯ ���� 0���� �����
                    for (int i = 0; i < shooters.Length(); i++)
                    {
                        shooters[i].magCapacity = 0;
                        shooters[i].magAmmo = 0;
                    }

                    yield return new WaitForSeconds(time);

                    //��ų�� ȿ���� ���ӵǰ� ���� ��쿡�� �Ʒ� �ڵ� ����
                    if (afterBurnerUsing)
                    {
                        playerMovement.moveSpeed -= afterBurnerSpeed;

                        //��� shooter�� �ִ� źȯ�� ���� źȯ ���� �ִ�� �����
                        for (int i = 0; i < shooters.Length(); i++)
                        {
                            shooters[i].magCapacity = magCapacity;
                            shooters[i].magAmmo = magCapacity;
                        }

                        afterBurnerUse = false; //��� ���� ǥ��
                        afterBurnerUse++; //����� Ƚ�� ����
                    }

                     

                }

                //��ų ȿ�� ���� �� �ٽ� ��ų ��ư�� ���� �ߵ��� ��� �� ��쿡
                else
                {
                    //��ų�� ȿ���� ���ӵǰ� ���� ��쿡�� �Ʒ� �ڵ� ����
                    if (afterBurnerUsing)
                    {
                        playerMovement.moveSpeed -= afterBurnerSpeed;

                        //��� shooter�� �ִ� źȯ�� ���� źȯ ���� �ִ�� �����
                        for (int i = 0; i < shooters.Length(); i++)
                        {
                            shooters[i].magCapacity = magCapacity;
                            shooters[i].magAmmo = magCapacity;
                        }

                        afterBurnerUse = false; //��� ���� ǥ��
                        afterBurnerUse++; //����� Ƚ�� ����
                    }
                }

            }


        }

         
        public void Aegis_streamLiner()
        {

        }

    }
}
 
