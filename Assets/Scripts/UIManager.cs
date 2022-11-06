using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Image myBar;
    public GameObject player;
    public Manager manager;
    public Text wavesInfo;

    private float _health;
    private float _maxHealth;

    private int _currentWave;
    private int _waves;

    private void Start()
    {
        wavesInfo = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        _health = player.GetComponent<HealthManager>().getHealth();
        _maxHealth = player.GetComponent<HealthManager>().getMaxHealth();
        myBar.fillAmount = _health / _maxHealth;

        _currentWave = manager.GetComponent<Manager>().getCurrentWave();
        _waves = manager.GetComponent<Manager>().getWaves();

        wavesInfo.text = "Ola: " + _currentWave + "/" + _waves;
    }
}
