using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveVerticalName = "Vertical"; // �յ� �������� ���� �Է��� �̸�
    public string moveHorizontalName = "Horizontal"; // �¿� ȸ���� ���� �Է��� �̸�
    public string fireButtonName = "Fire1"; // �߻縦 ���� �Է� ��ư �̸�
    public string pauseButtonName = "Cancel";

    public float moveV { get; private set; }
    public float moveH { get; private set; }
    public bool fire { get; private set; }
    public bool pause { get; private set; }

    private void Update()
    {
        // �����߿� �Է�
        moveV = Input.GetAxis(moveVerticalName);
        moveH = Input.GetAxis(moveHorizontalName);
        fire = Input.GetButton(fireButtonName);
        pause = Input.GetButtonDown(pauseButtonName);
    }
}
