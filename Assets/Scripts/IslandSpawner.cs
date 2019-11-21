using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{
    public List<GameObject> islandPrefabs;
    public float spawnsPerMeter;
   

    float lastX = 0;
    float spawnLastX = 0;
    float move = 0;

    void Start()
    {
        lastX = transform.position.x - spawnsPerMeter;
        spawnLastX = lastX;
    }

    void Update()
    {
        move += transform.position.x - lastX;
        lastX = transform.position.x;


        while (spawnsPerMeter > 0 && move >= spawnsPerMeter)
        {
            spawnLastX += spawnsPerMeter;
            move -= spawnsPerMeter;
            GameObject spawned = Instantiate(RandomPrefab());
            spawned.transform.position = new Vector3(spawnLastX, transform.position.y, transform.position.z);
        }
    }

    GameObject RandomPrefab()
    {
        if (islandPrefabs.Count > 0)
        {
            return islandPrefabs[Random.Range(0, islandPrefabs.Count)];
        }
        return null;
    }

 
}