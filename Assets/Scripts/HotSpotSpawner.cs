using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSpotSpawner : MonoBehaviour
{

    // The hotspot prefab to spawn
    public GameObject hotSpotPrefab;

    //chances of spawning a hotspot
    public int hotspotSpawnChance;

    // The distance between hotspot spawns
    public float spawnDistance;

    // The next x position that a hotspot will spawn at
    float spawnNextX;

    // Start is called before the first frame update
    void Start()
    {
        spawnNextX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        int spawn = Random.Range(0, hotspotSpawnChance);
        while (spawnDistance > 0 && spawnNextX <= transform.position.x)

        {
            if (spawn > 2)
            {
                GameObject spawned = Instantiate(hotSpotPrefab);
                spawned.transform.position = new Vector3(spawnNextX, transform.position.y, transform.position.z);
                spawnNextX += spawnDistance;
            }
            else spawnNextX += spawnDistance;


        }
    }
}
