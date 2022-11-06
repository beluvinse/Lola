using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthManager : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;

    public float getHealth()
    {
        return _health;
    }

    public float getMaxHealth()
    {
        return _maxHealth;
    }

    internal void Initalize()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float val)
    {
        _health -= val;
    }
}
