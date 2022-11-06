using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject typeOfBullet;
    [SerializeField] public float shotDelay;

    public float shotCounter;

    public Transform pointToShoot;
    private bool isFiring;

    public bool getIsFiring()
    {
        return isFiring;
    }

    public void setIsFiring(bool val)
    {
        isFiring = val;
    }

    private void Update()
    {
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                shotCounter = shotDelay;
                
                Instantiate(typeOfBullet, pointToShoot.position, pointToShoot.rotation);
            }
        }
        else
        {
            shotCounter -= Time.deltaTime;

        }
    }
}
