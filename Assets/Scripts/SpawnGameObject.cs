using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObject : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private int maxCount = 50000;

    private float lastSpawnTime = 0;
    private int currentSpawned = 0;
    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastSpawnTime + spawnDelay && currentSpawned < maxCount)
        {
            Instantiate(prefabToSpawn);
            prefabToSpawn.transform.position = this.transform.position;
            currentSpawned++;
            lastSpawnTime = Time.time;
        }
    }
}
