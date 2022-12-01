using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] private int _enemysToKillInWave;
    [SerializeField] private int _waves;
    AdditiveSceneLoader loader;

    public Transform door;

    private int _currentWave = 1;
    [SerializeField] private int currentEnemies;

    HealthManager player;
    private float _health;

    private void Start()
    {
        currentEnemies = _enemysToKillInWave;
        loader = FindObjectOfType<AdditiveSceneLoader>();
        player = FindObjectOfType<HealthManager>();
    }

    private void Update()
    {
        if (currentEnemies == 0)
        {
            if(_currentWave == _waves)
            {
                OpenDoor();
                loader.AllEnemiesKilled();
            }
            else
            {
                _currentWave++;
                currentEnemies = _enemysToKillInWave;
            }
            
        }


        _health = player.getHealth();
        
        if(_health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("nueva zona");
            GetComponent<BoxCollider>().enabled = false;
            currentEnemies = _enemysToKillInWave;
            _currentWave = 1;
        }
    }

    public void EnemyDown()
    {
        currentEnemies--;
    }


    public int getCurrentWave()
    {
        return _currentWave;
    }

    public int getWaves()
    {
        return _waves;
    }

    public void OpenDoor()
    {
        if (door.transform.position.x < 8f)
            door.transform.position += Vector3.right * 5f * Time.deltaTime;
    }

}
