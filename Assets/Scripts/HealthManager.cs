using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthManager : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;

    [Header("Audio")]
    [SerializeField] private AudioClip _takeDamageSFX;
    private AudioSource _myAudioSource;

    private void Start()
    {
        _myAudioSource = FindObjectOfType<AudioSource>();
    }

    public float getHealth()
    {
        return _health;
    }

    public void setHealth(float health)
    {
        _health = health;
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
        _myAudioSource.clip = _takeDamageSFX;
        if (!_myAudioSource.isPlaying)
        {
            _myAudioSource.Play();
        }
    }
}
