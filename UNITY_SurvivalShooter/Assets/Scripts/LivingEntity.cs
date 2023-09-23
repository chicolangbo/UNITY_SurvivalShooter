using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startHp = 100f;
    public float hp { get; protected set; }
    public bool death = false;
    public event Action onDeath; // 매개변수 없는 메소드 이벤트

    // 생명체가 활성화될 때 상태를 리셋해주는 함수
    protected virtual void OnEnable()
    {
        death = false;
        hp = startHp;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        hp -= damage;

        if(hp <= 0 && !death)
        {
            Die();
        }
    }

    public virtual void HpUp(float hpPoint)
    {
        if(death || hp >= startHp)
        {
            return;
        }

        hp += hpPoint;
        if(hp > startHp)
        {
            hp = startHp;
        }
    }

    public virtual void Die()
    {
        if(onDeath != null)
        {
            onDeath(); // die시 이벤트 발동
        }

        death = true;
    }
}
