using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{ 
    // The distance between island spawns
    public float spawnDistance;

    // The island prefab to spawn
    public GameObject islandPrefab;

    // The next x position that an island will spawn at
    float spawnNextX;

    void Start()
    {
        spawnNextX = transform.position.x;
    }

    void Update()
    {
        while (spawnDistance > 0 && spawnNextX <= transform.position.x)
        {
            Instantiate(islandPrefab, new Vector3(spawnNextX, transform.position.y, transform.position.z), transform.rotation);
            spawnNextX += spawnDistance;
        }
    }
}