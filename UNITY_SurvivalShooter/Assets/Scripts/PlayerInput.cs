using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveVerticalName = "Vertical"; // 앞뒤 움직임을 위한 입력축 이름
    public string moveHorizontalName = "Horizontal"; // 좌우 움직임을 위한 입력축 이름
    public string fireButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름

    public float moveV { get; private set; }
    public float moveH { get; private set; }
    public bool fire { get; private set; }

    private void Update()
    {
        // 게임중에 입력
        if(!GameManager.instance.isPaused && !GameManager.instance.isGameover)
        {
            moveV = Input.GetAxis(moveVerticalName);
            moveH = Input.GetAxis(moveHorizontalName);
            fire = Input.GetButton(fireButtonName);
        }
    }
}
