using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<GameObject> _spawns;

    private Manager _manager;


    private void Start()
    {
        _manager = GetComponent<Manager>();
        Spawn();
    }

    private void Spawn()
    {
        if(_manager.GetLevel() == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                Instantiate(_enemies[0], _spawns[i].transform.position, Quaternion.identity, _manager.gameObject.transform);
            }

            for (int i = 4; i < 6; i++)
            {
                Instantiate(_enemies[1], _spawns[i].transform.position, Quaternion.identity, _manager.gameObject.transform);
            }
        }
    }
}
