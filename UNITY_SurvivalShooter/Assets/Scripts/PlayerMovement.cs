using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotSpeed = 2f;
    private Vector3 direction;

    private PlayerInput playerInput; // �÷��̾� �Է��� �˷��ִ� ������Ʈ
    private Rigidbody playerRigidbody; // �÷��̾� ĳ������ ������ٵ�
    private Animator playerAnimator; // �÷��̾� ĳ������ �ִϸ�����

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
            Vector3 lookPoint = hitInfo.point; // �浹 ����
            lookPoint.y = transform.position.y; // y��ǥ ����
            var look = (lookPoint - playerRigidbody.position).normalized; // �Ÿ��� ����ȭ
            playerRigidbody.MoveRotation(Quaternion.LookRotation(look));
        }
    }
}
