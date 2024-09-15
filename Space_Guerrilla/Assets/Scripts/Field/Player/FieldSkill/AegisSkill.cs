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
        public int magCapacity;

        //���� ��ų ���� Ƚ��, ��ų ����� Ƚ��, ��ų ���� �ð�, �� ���� ���� �ð� ��ġ
        public float streamLinerCnt = 1;
        public int streamLinerUse = 0;
        public float streamLinerTime = 3f;
        public float streamLinerCool = 2f;
        public bool streamLinerUsing = false; // ���� źȯ �۵��� ����
        public int streamLinerMax = 999;
        public float streamLinerRotateTime = 1.5f;


        //���� ��ų ���� Ƚ��, ��ų�� ����� Ƚ��
        public float cruiseMissileCnt = 1;
        public int cruiseMissileUse = 0;

        public bool isSkillActive = false; //���� �������� ��ų�� �ִ��� ����


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
            //�ӽ÷� Q, Z, X Ű�� ���� �����͹���, ��Ʈ�����̳�, �̻��� ��ų ����

            if (playerInput.skillQ)
            {
                StartCoroutine("Aegis_afterBurner");
            }

            if (playerInput.skillZ)
            {
                StartCoroutine("Aegis_streamLiner");
            }

            if (playerInput.skillX)
            {
                launchCruiseMissile();
            }

        }

        //�̾Ʒ��� ��ų �Լ��� ���� ����
        //1. �����͹��� ����/��� ����
        //2. ��Ʈ�����̳� ����/��� ����
        //3. �̻��� ���� ����
        //4. �� ��ų���� ���� ���� ����(�����͹��� ���� �ٸ� ��ų ����ĵ��, ��� �Ұ�)

        public IEnumerator Aegis_afterBurner()
        {
            //��� Ƚ���� �����ִٸ�
            if (afterBurnerCnt > afterBurnerUse && !isSkillActive)
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
                Debug.Log("afterBurnerActive");

                isSkillActive = true; //��ų ����� ǥ��
                afterBurnerUsing = true; //����� ǥ��
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

                Debug.Log("afterBurnerCancel");

                afterBurnerUsing = false; //��� ���� ǥ��
                playerMovement.moveSpeed -= afterBurnerSpeed;

                //��� shooter�� �ִ� źȯ�� ���� źȯ ���� �ִ�� �����
                magnumSet();

                
                afterBurnerUse++; //����� Ƚ�� ����

                isSkillActive = false; //��ų ������� �ƴ� ǥ��

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

            Debug.Log("magnumZero");
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

            Debug.Log("magnumSet");
        }

        
        public IEnumerator Aegis_streamLiner()
        {
            //��ų ��� Ƚ���� �����ְ�, ��ų ������� �ƴϸ� ��� �����ϴٸ�
            if (streamLinerCnt > streamLinerUse && !streamLinerUsing && !isSkillActive)
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
            if (!streamLinerUsing && !isSkillActive)
            {
                Debug.Log("streamLinerActive");

                streamLinerUsing = true; //��ų ����� ǥ��
                isSkillActive = true; //��ų ����� ǥ��

                magnumZero(); // ȸ�� �ϴ� ���� �߻� ���ϰ� źȯ ����

                //shooter ȸ����Ű��
                StartCoroutine(rotateShooter(true));
 

            }
        }

        //shooter ȸ�� �� �Ѿ� ���� �����ϴ� Coroutine
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

                //�����͹��� ��� ���� �ƴ϶��, źȯ ���� �������� �ϱ�
                magnumSet();
                
            }

        }

        private void streamLinerCancel()
        {
            //��ų ������̶��
            if (streamLinerUsing)
            {
                Debug.Log("streamLinerCancel");

                isSkillActive = false; // ��ų ��� ���� ���·� ǥ�� 
                streamLinerUse++; // ����� Ƚ�� �߰�
                streamLinerUsing = false; //��� �� �ƴ� ǥ��
                magnumZero(); // źȯ 0���� �����
                StartCoroutine(rotateShooter(false)); // shooter ���� �������� ȸ��
            }
        }

        private void launchCruiseMissile()
        {
            //�ٸ� ��ų�� �������̶�� �̻��� �߻� �Ұ�
            if (isSkillActive)
            {
                return;
            }

            if (cruiseMissileCnt <= cruiseMissileUse)
            {
                return;    
            }

            Debug.Log("launchCruiseMissile");

            const int poolManagerPrefabIdx = 2; // Player_CruiseMissile�� ����� idx, ���� Pool Manager�� Prefabs �迭 ���� �� ���� �ʿ�

            for (int i = 0; i < shooters.Length; i++)
            {
                // �� shooter�� transform�� ����
                Transform shooterTransform = shooters[i].transform;

                // PoolManager�� Get �޼��带 ȣ���Ͽ� �̻��� ����
                GameManager.instance.poolManager.Get(poolManagerPrefabIdx, shooterTransform);

                // �̻��Ͽ� �߰����� ������ �ʿ��ϴٸ� ���⼭ ����
                
            }

            cruiseMissileUse++;
        }


    }
}
 
