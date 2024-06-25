using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� ĳ���͸� �����ϱ� ���� ����� �Է��� ����
// ������ �Է°��� �ٸ� ������Ʈ���� ����� �� �ֵ��� ����
// �Ʒ��� ������ ���� ��Ĵ�� ���ϴ� �Էµ��� ��� �߰� ����
public class PlayerInput : MonoBehaviour
{
    // Input ���ÿ� �ִ� �����ϴ� string�� �°� Name������ �Ҵ�
    private string moveVerticalName = "Vertical"; // ���Ʒ� �������� ���� �Է��� �̸�
    private string moveHorizontalName = "Horizontal"; // �¿� �������� ���� �Է��� �̸�
    private string fireButtonName = "Fire1"; // �߻縦 ���� �Է� ��ư �̸�
    private string specialButtonName = "Fire2"; // Ư�� ������ ���� �Է� ��ư �̸�
    private string reloadButtonName = "Reload"; // �������� ���� �Է� ��ư �̸�
    private string missileButtonName = "Missile";
    private string autoButtonName = "Auto";
    private string skillQName = "SkillQ";
    private string skillZName = "SkillZ";
    private string skillXName = "SkillX";
    private string skillCName = "SkillC";
    private string skill1Name = "Skill1";
    private string skill2Name = "Skill2";
    private string skill3Name = "Skill3";
    private string skill4Name = "Skill4";


    // �� �Ҵ��� ���ο����� ����
    public float moveVertical { get; private set; } // ������ ������ �Է°�
    public float moveHorizontal { get; private set; } // ������ ȸ�� �Է°�
    public bool fire { get; private set; } // ������ �߻� �Է°�
    public bool special { get; private set; } // ������ Ư�� ���� �Է°�
    public bool reload { get; private set; } // ������ ������ �Է°�
    public bool missile { get; private set; }
    public bool auto { get; private set; }  //������ Auto��� ��ȯ
    public bool isAuto = false; //���� Auto���� �ƴ����� ������ bool ����

    public bool skillQ { get; private set; }
    public bool skillZ { get; private set; }
    public bool skillX { get; private set; }
    public bool skillC { get; private set; }
    public bool skill1 { get; private set; }
    public bool skill2 { get; private set; }
    public bool skill3 { get; private set; }
    public bool skill4 { get; private set; }





    // �������� ����� �Է��� ����
    private void Update()
    {
        // ���ӿ��� ���¿����� ����� �Է��� �������� �ʴ´�
        /*if (GameManager.instance != null
            && GameManager.instance.isGameover)
        {
            moveVertical = 0;
            moveHorizontal = 0;  
            fire = false;
            special = false;
            reload = false;
            return;
        }*/

        // moveVertical�� ���� �Է� ����
        moveVertical = Input.GetAxis(moveVerticalName);
        // moveHorizontal�� ���� �Է� ����
        moveHorizontal = Input.GetAxis(moveHorizontalName);
        // fire�� ���� �Է� ����
        fire = Input.GetButton(fireButtonName);
        //special�� ���� �Է� ����
        special = Input.GetButton(specialButtonName);
        // reload�� ���� �Է� ����
        reload = Input.GetButtonDown(reloadButtonName);
        missile = Input.GetButtonDown(missileButtonName);
        auto = Input.GetButtonDown(autoButtonName);

        skillQ = Input.GetButton(skillQName);
        skillZ = Input.GetButton(skillZName);
        skillX = Input.GetButton(skillXName);
        skillC = Input.GetButton(skillCName);
        skill1 = Input.GetButton(skill1Name);
        skill2 = Input.GetButton(skill2Name);
        skill3 = Input.GetButton(skill3Name);
        skill4 = Input.GetButton(skill4Name);


        if (auto && isAuto == false)
        {
            isAuto = true;
        }

        else if (auto && isAuto == true)
        {
            isAuto = false;
        }

    }
}
