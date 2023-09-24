using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;
    public Transform gunPivot;

    private PlayerInput playerInput;
    private Animator playerAnimator;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        gun.gameObject.SetActive(true); // æÎ ø÷ enable¿Ã æ∆¥“±Ó
    }

    private void Update()
    {
        if(!GameManager.instance.isPaused && playerInput.fire)
        {
            gun.Fire();
        }
    }
}
