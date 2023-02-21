using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPuddle : MonoBehaviour
{
    [SerializeField] private float _damage = 0.05f;
    [SerializeField] private float _time = 10f;

    private float contador;

    private HealthManager _player;
    [SerializeField] private bool _isOnPuddle = false;
    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        Acid();
        if(contador >= _time)
        {
            _isOnPuddle = false;
            Destroy(this.gameObject);
        }

    }



    void Acid()
    {
        if(_isOnPuddle)
            _player.TakeDamage(_damage);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<HealthManager>())
        {
            _player = other.GetComponent<HealthManager>();
            _isOnPuddle = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<HealthManager>())
        {
            _player = null;
            _isOnPuddle = false;
        }
    }

}
