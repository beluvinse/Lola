using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    private float _health;
    public float healing;
    public GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Heal();
            Destroy(this.gameObject);
        }
    }
        void Heal()
    {
        _health = player.GetComponent<HealthManager>().getHealth();
        float health = _health + healing;
        if (health > 100)
            health = 100;
        player.GetComponent<HealthManager>().setHealth(health);
    }

}
