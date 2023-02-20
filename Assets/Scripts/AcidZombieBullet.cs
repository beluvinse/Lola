using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidZombieBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    public float GetSpeed()
    {
        return _speed;
    }
}
