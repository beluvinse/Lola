using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class Enemy : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] protected float _life;
    [SerializeField] protected float _maxLife;
    [SerializeField] protected float _damage;
    protected NavMeshAgent _navMeshAgent;

    private Manager _manager;

    private void Start()
    {
        _manager = GetComponentInParent<Manager>();
    }

    public abstract void Attack();

    public void TakeDamage(float val)
    {
        _life -= val;
        if (_life <= 0) DestroyObject();
    }


    private void DestroyObject()
    {
        _manager.EnemyDown();
        Destroy(this.gameObject);
    }
}
