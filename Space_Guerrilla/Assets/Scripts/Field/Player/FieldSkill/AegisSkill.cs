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
        public int afterBurnerUse = 0;
        public float afterBurnerTime = 2.5f;
        public float afterBurnerSpeed = 50f;
        public bool afterBurnerUsing = false; // ��ų ȿ��(�ӵ� ����) �۵��� ����
        public bool afterBurnerAvailable = true; // ��ų ��� ���� ����
        public int magCapacity;

        //���� ��ų ���� Ƚ��, ��ų ����� Ƚ��, ��ų ���� �ð�, �� ���� ���� �ð� ��ġ
        public float streamLinerCnt = 1;
        public int streamLinerUse = 0;
        public float streamLinerTime = 3f;
        public float streamLinerCool = 2f;
        public bool streamLinerUsing = false; // ���� źȯ �۵��� ����
        public bool streamLinerAvailable = false; // ��ų ��� ���� ����
        public int streamLinerMax = 999;
        public float streamLinerRotateTime = 1.5f;


        //�̻��� �߻� ��ų�� ���� ������
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
        //1. �����͹��� ����/��� ����
        //2. ��Ʈ�����̳� ����/��� ����
        //3. �̻��� ���� ����
        //4. �� ��ų���� ���� ���� ����(�����͹��� ���� �ٸ� ��ų ����ĵ��, ��� �Ұ�)

        public IEnumerator Aegis_afterBurner()
        {
            //��� Ƚ���� �����ִٸ�
            if (afterBurnerCnt > afterBurnerUse)
            {
                
                //�켱 ��ų ����������� üũ
                if (!afterBurnerUsing)
                {

                    afterBurnerActive();

                    yield return new WaitForSeconds(afterBurnerTime);

                    afterBurnerCancel();

                }

                //��ų ȿ�� ���� �� �ٽ� ��ų ��ư�� ���� �ߵ��� ��� �� ��쿡
                else
                {
                    afterBurnerCancel();
                }

            }


        }

        private void afterBurnerActive()
        {
            if (!afterBurnerUsing)
            {
                afterBurnerUsing = true; //����� ǥ��
                streamLinerAvailable = false; // ��Ʈ�����̳� ��� �Ұ� ǥ��
                streamLinerCancel(); // ��Ʈ�����̳� ��ų ��� �ߴ�(��� �۵����� ������ ����Ǵ� �Լ�)
                playerMovement.moveSpeed += afterBurnerSpeed; //�̵��ӵ� ����

                //��� shooter�� �ִ� źȯ�� ���� źȯ ���� 0���� �����
                magnumZero();

                //���Ŀ� ��ų �������� ĵ�� �̹����� �����ϴ� �ڵ� �ʿ�

            }
        }

        private void afterBurnerCancel()
        {
            //��ų�� ȿ���� ���ӵǰ� ���� ��쿡�� �Ʒ� �ڵ� ����
            if (afterBurnerUsing)
            {
                afterBurnerUsing = false; //��� ���� ǥ��
                playerMovement.moveSpeed -= afterBurnerSpeed;

                //��� shooter�� �ִ� źȯ�� ���� źȯ ���� �ִ�� �����
                magnumSet();

                
                afterBurnerUse++; //����� Ƚ�� ����

                //���Ŀ� ��ų �������� ������ �̹����� �����ϴ� �ڵ� �ʿ�
            }
        }

        private void magnumZero()
        {
            //��� shooter�� �ִ� źȯ�� ���� źȯ ���� 0���� �����
            for (int i = 0; i < shooters.Length; i++)
            {
                shooters[i].magCapacity = 0;
                shooters[i].magAmmo = 0;
            }
        }
        private void magnumSet()
        {
            //�����͹��ʳ� ��Ʈ�����̳� ��ų�� ���� �۵����̶�� return�� ���� źȯ �������� ��ŵ
            if (afterBurnerUsing || streamLinerUsing)
            {
                return;
            }

            //��� shooter�� ���� źȯ�� �ִ� źȯ ���� �ִ�� �����ϱ�
            for (int i = 0; i < shooters.Length; i++)
            {
                shooters[i].magCapacity = magCapacity;
                shooters[i].magAmmo = magCapacity;
            }
        }

        
        public IEnumerator Aegis_streamLiner()
        {
            //��ų ��� Ƚ���� �����ְ�, ��ų ������� �ƴϸ� ��� �����ϴٸ�
            if (streamLinerCnt > streamLinerUse && !streamLinerUsing && streamLinerAvailable)
            {

                streamLinerActive(); // ��ų ����

                // ȸ�� �ð� + ��� ���� �ð��� ���� �Ŀ�,
                yield return new WaitForSeconds(streamLinerRotateTime + streamLinerTime);

                streamLinerCancel(); // ��ų ��� ���, ���� ���·� ���� 

            }
        }
        
        private void streamLinerActive()
        {
            //��ų ������� �ƴϰ�, ��� �����ϴٸ�
            if (!streamLinerUsing && streamLinerAvailable)
            {
                streamLinerUsing = true; //��ų ����� ǥ��

                magnumZero(); // ȸ�� �ϴ� ���� �߻� ���ϰ� źȯ ����

                //shooter ȸ����Ű��
                StartCoroutine(rotateShooter(true));
 

            }
        }

        //shooter ȸ�� �� �Ѿ� ���� �����ϴ� Coroutine
        private IEnumerator rotateShooter(bool active)
        {

            Quaternion rightShooter = shooters[0].transform.rotation;
            Quaternion leftShooter = shooters[1].transform.rotation;
            float elapsedTime = 0f;

            //active�� true���, ������ ���� �������� ȸ��
            if (active)
            {
                //streamLinerRotateTime ���� shooter �� ȸ����Ű��
                while (elapsedTime < streamLinerRotateTime)
                {
                    shooters[0].transform.rotation = Quaternion.Lerp(rightShooter, Quaternion.Euler(0, 0, 0), elapsedTime / streamLinerRotateTime);
                    shooters[1].transform.rotation = Quaternion.Lerp(leftShooter, Quaternion.Euler(0, 0, 0), elapsedTime / streamLinerRotateTime);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                //ȸ�� �� ������ ���� ���� ��ǥ ������ �� ����
                shooters[0].transform.rotation = Quaternion.Euler(0, 0, 0);
                shooters[1].transform.rotation = Quaternion.Euler(0, 0, 0);

                //��� shooter�� ����źȯ ǥ��
                for (int i = 0; i < shooters.Length; i++)
                {
                    shooters[i].magCapacity = streamLinerMax;
                    shooters[i].magAmmo = streamLinerMax;

                }
            }

            //active�� false���, ������ ������ �������� �ǵ����� ȸ��
            else
            {
                //streamLinerRotateTime ���� shooter �� ȸ����Ű��
                while (elapsedTime < streamLinerRotateTime)
                {
                    shooters[0].transform.rotation = Quaternion.Lerp(rightShooter, Quaternion.Euler(0, 0, -30f), elapsedTime / streamLinerRotateTime);
                    shooters[1].transform.rotation = Quaternion.Lerp(leftShooter, Quaternion.Euler(0, 0, 30f), elapsedTime / streamLinerRotateTime);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                //ȸ�� �� ������ ���� ���� ��ǥ ������ �� ����
                shooters[0].transform.rotation = Quaternion.Euler(0, 0, -30f);
                shooters[1].transform.rotation = Quaternion.Euler(0, 0, 30f);

                // ������ ���� �ð� - ȸ�� �ҿ� �ð�, �� ���� ������ �ð����� ���
                yield return new WaitForSeconds(streamLinerCool - streamLinerRotateTime);

                //�����͹��� ��� ���� �ƴ϶��, źȯ ���� �������� �ϱ�
                magnumSet();
                streamLinerAvailable = true; // ��ų ��� ���� ���·� ǥ�� 
            }

        }

        private void streamLinerCancel()
        {
            //��ų ������̶��
            if (streamLinerUsing)
            {
                streamLinerUse++; // ����� Ƚ�� �߰�
                streamLinerUsing = false; //��� �� �ƴ� ǥ��
                streamLinerAvailable = false; // �����Ϸ� ��� �Ұ����� ǥ��
                magnumZero(); // źȯ 0���� �����
                StartCoroutine(rotateShooter(false)); // shooter ���� �������� ȸ��
            }
        }

    }
}
 
