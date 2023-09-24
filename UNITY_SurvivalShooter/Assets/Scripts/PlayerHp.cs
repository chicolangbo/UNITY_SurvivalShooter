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
    private FadeController hitScreenEffect;

    private AudioSource playerAudioSource;
    public AudioClip playerHit;
    public AudioClip playerDeath;


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
        hitScreenEffect = UIManager.instance.hitScreen.GetComponent<FadeController>();
        playerAudioSource = GetComponent<AudioSource>();
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
            base.OnDamage(damage, default(Vector3), default(Vector3));
            hpSlider.value = hp;
            hitScreenEffect.StartFade();
            playerAudioSource.PlayOneShot(playerHit);
        }
    }

    public override void Die()
    {
        base.Die();
        hpSlider.value = hp;

        playerAnimator.SetTrigger("Die");
        playerMovement.enabled = false;
        playerShooter.enabled = false;
        playerAudioSource.PlayOneShot(playerDeath);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(death)
        {
            return;
        }
    }
}
