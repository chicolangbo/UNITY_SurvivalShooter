using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : LivingEntity
{
    public Slider hpSlider;
    private Animator playerAnimator;
    private PlayerMovement playerMovement;
    private PlayerShooter playerShooter;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
    }

    private void Update()
    {
        
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        hpSlider.minValue = 0;
        hpSlider.maxValue = startHp;
        hpSlider.value = hp;

        playerMovement.enabled = true;
        playerShooter.enabled = true;
    }

    public override void HpUp(float hpPoint)
    {
        base.HpUp(hpPoint);

        hpSlider.value = hp;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(!death)
        {
            // ¿Àµð¿À Àç»ý
        }
        base.OnDamage(damage, default(Vector3), default(Vector3));
        hpSlider.value = hp;
    }

    public override void Die()
    {
        base.Die();
        hpSlider.value = hp;

        playerAnimator.SetTrigger("Die");
        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(death)
        {
            return;
        }

        // ¾ÆÀÌÅÛ È¹µæ
    }
}
