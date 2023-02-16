using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject typeOfBullet;
    [SerializeField] public float shotDelay;
    
    private Vector3 shootingRay;

    public PlayerMovement playerMov;

    public float shotCounter;

    public Transform pointToShoot;
    private bool isFiring;

    LineRenderer laserLine;

    public bool getIsFiring()
    {
        return isFiring;
    }

    public void setIsFiring(bool val)
    {
        isFiring = val;
    }

    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //Debug.DrawLine(this.transform.position, playerMov.getLookAt(), Color.green);
        shootingRay = new Vector3(playerMov.getLookAt().x, this.transform.position.y, playerMov.getLookAt().z);
        Debug.DrawLine(this.transform.position, shootingRay, Color.red);
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                laserLine.SetPosition(0, pointToShoot.position);
                shotCounter = shotDelay;
                //Physics.Raycast(this.transform.position, playerMov.getLookAt(), 25f);
                //Instantiate(typeOfBullet, pointToShoot.position, pointToShoot.rotation);
            }
        }
        else
        {
            shotCounter -= Time.deltaTime;

        }
    }



    /*
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireTimer > fireRate)
        {
            fireTimer = 0;
            
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (playerCamera.transform.forward * gunRange));
            }
            StartCoroutine(ShootLaser());
        }
    }
    
    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
    */
}

