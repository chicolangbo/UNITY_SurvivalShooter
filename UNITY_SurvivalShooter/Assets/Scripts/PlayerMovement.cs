using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotSpeed = 2f;
    private Vector3 direction;

    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    private Camera worldCam;
    public LayerMask layerMask;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        worldCam = Camera.main;
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Update()
    {
        direction.z = playerInput.moveV;
        direction.x = playerInput.moveH;
        direction.y = 0f;

        if(direction.magnitude > 1)
        {
            direction.Normalize();
        }

        playerAnimator.SetFloat("Move", direction.magnitude);
    }

    private void Move()
    {
        var position = playerRigidbody.position;
        position += direction * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(position);
    }

    private void Rotate()
    {
        var ray = worldCam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out var hitInfo, 100f, layerMask))
        {
            Vector3 lookPoint = hitInfo.point; // 충돌 지점
            lookPoint.y = transform.position.y; // y좌표 같게
            var look = (lookPoint - playerRigidbody.position).normalized; // 거리를 정규화
            playerRigidbody.MoveRotation(Quaternion.LookRotation(look));
        }
    }
}
