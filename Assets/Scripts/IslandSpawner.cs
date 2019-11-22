using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{
    public List<GameObject> midPrefabs;
    public float spawnsPerMeter;
    public GameObject islandFront;
    public GameObject islandBack;
   

    float lastX = 0;
    float spawnLastX = 0;
    float move = 0;
    int length = 0;

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
            BuildIsland();
        }
    }

    void BuildIsland()
    {
        float position = transform.position.x;
        length = (Random.Range(0, 4));
        GameObject front = Instantiate(islandFront);
        front.transform.position = new Vector3(position, transform.position.y, transform.position.z);
        position = position + 1;
        while (length > 0)
        {
            length = length - 1;
            GameObject mid = Instantiate(RandomMidPrefab());
            mid.transform.position = new Vector3(position, transform.position.y, transform.position.z);
            position = position + 1;
        }
            
        GameObject back = Instantiate(islandBack);
        back.transform.position = new Vector3(position, transform.position.y, transform.position.z);
        
    }

    GameObject RandomMidPrefab()
    {
        if (midPrefabs.Count > 0)
        {
            return midPrefabs[Random.Range(0, midPrefabs.Count)];
        }
        return null;
    }


}