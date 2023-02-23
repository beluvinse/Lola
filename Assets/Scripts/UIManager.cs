using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Image myBar;
    public Image myBar2;
    public GameObject player;
    public Manager manager;
    public GunController gun;
    public Text wavesInfo;
    public Text bullets;

    private float _health;
    private float _maxHealth;
    
    private float _roll;
    private float _maxRoll;

    private int _currentWave;
    private int _waves;
    
    private int _currentBullets;
    private int _maxBullets;

    private void Start()
    {
        wavesInfo = GetComponentInChildren<Text>();
        _maxHealth = player.GetComponent<HealthManager>().getMaxHealth();
        _maxRoll = player.GetComponent<PlayerMovement>().getRollDelay();
        _maxBullets = gun.GetComponent<GunController>().getMaxAmmo();
    }

    private void Update()
    {
        _health = player.GetComponent<HealthManager>().getHealth();
        myBar.fillAmount = _health / _maxHealth;
        
        _roll = player.GetComponent<PlayerMovement>().getRollCounter();
        //myBar2.fillAmount = _maxRoll - ( _maxRoll +_roll );
        myBar2.fillAmount = _roll / _maxRoll;

        _currentWave = manager.GetComponent<Manager>().getCurrentWave();
        _waves = manager.GetComponent<Manager>().getWaves();
        wavesInfo.text = "Ola: " + _currentWave + "/" + _waves;

        _currentBullets = gun.GetComponent<GunController>().getAmmo();
        bullets.text = "Balas: " + _currentBullets + "/" + _maxBullets;
    }
}
