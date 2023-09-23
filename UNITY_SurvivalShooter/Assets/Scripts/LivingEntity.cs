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
    public event Action onDeath; // �Ű����� ���� �޼ҵ� �̺�Ʈ

    // ����ü�� Ȱ��ȭ�� �� ���¸� �������ִ� �Լ�
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
            onDeath(); // die�� �̺�Ʈ �ߵ�
        }

        death = true;
    }
}
