using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveVerticalName = "Vertical"; // �յ� �������� ���� �Է��� �̸�
    public string moveHorizontalName = "Horizontal"; // �¿� �������� ���� �Է��� �̸�
    public string fireButtonName = "Fire1"; // �߻縦 ���� �Է� ��ư �̸�

    public float moveV { get; private set; }
    public float moveH { get; private set; }
    public bool fire { get; private set; }

    private void Update()
    {
        // �����߿� �Է�
        if(!GameManager.instance.isPaused && !GameManager.instance.isGameover)
        {
            moveV = Input.GetAxis(moveVerticalName);
            moveH = Input.GetAxis(moveHorizontalName);
            fire = Input.GetButton(fireButtonName);
        }
    }
}
