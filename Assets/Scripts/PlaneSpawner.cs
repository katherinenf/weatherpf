using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    // The plane prefab to spawn
    public GameObject planePrefab;

    // Distance between plane spawns
    public float spawnDistance;

    // Vertical extents that planes will be spawned
    public float verticalExtent;

    // The next x position that a plane will spawn at
    float spawnNextX;

    void Start()
    {
        spawnNextX = transform.position.x;
    }

    void Update()
    {
        while (spawnDistance > 0 && spawnNextX <= transform.position.x)
        {
            GameObject spawned = Instantiate(planePrefab);
            spawned.transform.position = new Vector3(spawnNextX, GetRandomSpawnY(), transform.position.z);
            spawnNextX += spawnDistance;
        }
    }

    float GetRandomSpawnY()
    {
        return transform.position.y + Random.Range(-verticalExtent, verticalExtent);
    }

}


