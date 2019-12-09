using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{
    // The hotspot prefab to spawn
    public GameObject hotSpotPrefab;

    //chances of spawning a hotspot
    public float hotspotSpawnChance;

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
            float spawn = Random.Range(0f, 1f);
            if (spawn > hotspotSpawnChance)
            {
                Instantiate(islandPrefab, new Vector3(spawnNextX, transform.position.y, transform.position.z), transform.rotation);
                spawnNextX += spawnDistance;
            }
            else
            {
                GameObject spawned = Instantiate(hotSpotPrefab);
                spawned.transform.position = new Vector3(spawnNextX, transform.position.y, transform.position.z);
                spawnNextX += spawnDistance;
            }
        }
    }
}