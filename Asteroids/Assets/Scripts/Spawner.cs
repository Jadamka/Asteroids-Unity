using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject asteroid;

    float x;
    float y;

    public int spawnAmount = 4;

    private GameObject[] spawners = new GameObject[7];

    void Start()
    {
        spawners = GameObject.FindGameObjectsWithTag("Spawner");

        Spawn();
    }

    void Spawn()
    {
        GameObject currentSpawner = spawners[Random.Range(0,8)];

        for(int i = 0; i < 4; i++)
        {
            Instantiate(asteroid, currentSpawner.transform.position, Quaternion.identity);
            currentSpawner = spawners[Random.Range(0, 8)];
        }
    }
}
