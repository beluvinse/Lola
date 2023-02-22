using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidZombieBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private GameObject _acidPuddle;
    [SerializeField] private LayerMask layer;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, Vector3.down, out hit, 0.1f, layer))
        {
            var puddle = Instantiate(_acidPuddle, hit.point, Quaternion.identity);
            puddle.transform.position.WithAxis(Axis.Y, -0.95f);
            Destroy(this.gameObject);
        }
    }

    public float GetSpeed()
    {
        return _speed;
    }

}
