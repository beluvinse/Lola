using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CommonZombie : Enemy
{
    public Transform player;
    private Animator _myAnim;

    [Header("Values")]
    [SerializeField] private float _chaseRadius;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackDelay;
    [SerializeField] private bool _isAttacking;

    private float attackCounter;

    private void Awake()
    {
        _myAnim = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _myAnim.SetBool("moving", false);
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
            Attack();
        }

    }

    public override void Attack()
    {
        if (_isAttacking)
        {
            attackCounter -= Time.fixedDeltaTime;
            if(attackCounter <= 0)
            {
                attackCounter = _attackDelay;
                player.GetComponent<HealthManager>().TakeDamage(_damage);
                Debug.Log("te ataco");
            }
        }
        else
        {
            attackCounter -= Time.fixedDeltaTime;
        }
    }

    private void FollowPlayer()
    {
        _myAnim.SetBool("moving", true);
        if (_isAttacking) _navMeshAgent.isStopped = true;
        else _navMeshAgent.isStopped = false;
    }

}