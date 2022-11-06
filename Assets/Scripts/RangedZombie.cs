using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RangedZombie : Enemy
{
    public Transform player;
    private Animator _myAnim;

    [Header("Values")]
    [SerializeField] private float _chaseRadius;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackDelay;
    [SerializeField] private bool _isAttacking;

    public BulletController bullet;
    public Transform pointToShoot;

    private float attackCounter;

    private void Awake()
    {
        _myAnim = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        FollowPlayer();
        if (distance <= _chaseRadius && distance > _attackRadius)
        {
            _navMeshAgent.SetDestination(player.position);
            transform.LookAt(player);
            _isAttacking = false;
        }
        else if (distance <= _attackRadius)
        {
            _isAttacking = true;
            transform.LookAt(player);
            Attack();
        }

    }

    public override void Attack()
    {
        if (_isAttacking)
        {
            attackCounter -= Time.fixedDeltaTime;
            if (attackCounter <= 0)
            {
                attackCounter = _attackDelay;
                Instantiate(bullet, pointToShoot.position, pointToShoot.rotation);
                Debug.Log("te disparo");
            }
        }
        else
        {
            attackCounter -= Time.fixedDeltaTime;
        }
    }

    

    private void FollowPlayer()
    {
        if (_isAttacking) _navMeshAgent.isStopped = true;
        else _navMeshAgent.isStopped = false;
    }

}
