using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    public LayerMask whatIsTarget;

    private LivingEntity targetEntity;
    private NavMeshAgent pathFinder;

    public ParticleSystem hitEffect;

    private Animator enemyAnimator;
    
    public float damage;
    public float timeBetAttack = 0.5f;
    private float lastAttackTime;

    private bool hasTarget
    {
        get
        {
            return targetEntity != null && !targetEntity.death;
        }
    }

    private void Awake()
    {
        pathFinder = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        onDeath += () =>
        {
            Destroy(GetComponent<Collider>());
            Destroy(GetComponent<SphereCollider>());
        };
    }

    private void Start()
    {
        StartCoroutine(UpdatePath());
    }

    private void Update()
    {
        enemyAnimator.SetBool("HasTarget", hasTarget);

        if(death)
        {
            transform.position += new Vector3 ( 0, -0.01f, 0f );
        }
    }

    public void Setup(float newHealth, float newDamage, float newSpeed)
    {
        startHp = newHealth;
        hp = newHealth;
        damage = newDamage;
        pathFinder.speed = newSpeed;
    }

    private IEnumerator UpdatePath()
    {
        while(!death)
        {
            if(hasTarget)
            {
                pathFinder.isStopped = false;
                pathFinder.SetDestination(targetEntity.transform.position);
            }
            else
            {
                pathFinder.isStopped = true;
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, whatIsTarget);

                for (int i = 0; i < colliders.Length; i++)
                {
                    var livingEntity = colliders[i].GetComponent<LivingEntity>();
                    if (livingEntity != null && !livingEntity.death)
                    {
                        targetEntity = livingEntity;
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        if(!death)
        {
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();
        }
    }

    public override void Die()
    {
        base.Die();
        var colls = GetComponents<Collider>();
        foreach(var coll in colls)
        {
            coll.enabled = false;
        }

        pathFinder.isStopped = true;
        pathFinder.enabled = false;

        enemyAnimator.SetTrigger("Die");
    }

    private void OnTriggerStay(Collider other)
    {
        if (targetEntity == null)
            return;

        if((Time.time > lastAttackTime + timeBetAttack) && (other.gameObject == targetEntity.gameObject))
        {
            lastAttackTime = Time.time;
            var target = other.GetComponent<PlayerHp>();
            target.OnDamage(damage, default(Vector3), default(Vector3));
        }
    }
}
