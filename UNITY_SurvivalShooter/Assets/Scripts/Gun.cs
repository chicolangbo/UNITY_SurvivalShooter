using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform fireTransform;
    public ParticleSystem muzzleEffect;
    private LineRenderer bulletLineRenderer;

    private AudioSource gunShotAudioSource;
    public AudioClip gunShot;

    public float damage = 25;

    private float fireDistance = 50f; // �����Ÿ�
    public float timeBetFire = 0.12f; // �Ѿ� �߻� ����
    private float lastFireTime; // ���� ���������� �߻��� ����

    private void Awake()
    {
        bulletLineRenderer = GetComponent<LineRenderer>();
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
        gunShotAudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        lastFireTime = 0f;
    }

    public void Fire()
    {
        if(Time.time > lastFireTime + timeBetFire)
        {
            lastFireTime = Time.time;
            Shot();
            gunShotAudioSource.PlayOneShot(gunShot);
        }
    }

    private void Shot()
    {
        var hitPosition = fireTransform.position + fireTransform.forward * fireDistance;

        var ray = new Ray(fireTransform.position, fireTransform.forward);
        
        if(Physics.Raycast(ray, out RaycastHit hit, fireDistance))
        {
            var target = hit.collider.GetComponent<IDamageable>();
            if(target != null)
            {
                target.OnDamage(damage, hit.point, hit.normal);
            }
            hitPosition = hit.point;
        }

        StartCoroutine(ShotEffect(hitPosition));
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        muzzleEffect.Play();

        // ������ ���۰� ��
        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLineRenderer.enabled = false;
    }
}
