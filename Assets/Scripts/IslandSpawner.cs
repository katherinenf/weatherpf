using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{
    public List<GameObject> midPrefabs;
    public float spawnsPerMeter;
    public GameObject islandFront;
    public GameObject islandBack;
    public GameObject tree;
    public GameObject flower;
   

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
        length = (Random.Range(1, 4));
        GameObject front = Instantiate(islandFront);
        front.transform.position = new Vector3(position, transform.position.y, transform.position.z);
        position = position + 1;
        int floraType = (Random.Range(0, 2));
        while (length > 0)
        {
            length = length - 1;
            GameObject mid = Instantiate(RandomMidPrefab());
            mid.transform.position = new Vector3(position, transform.position.y, transform.position.z);
            AddFlora(position, floraType);
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

    void AddFlora(float pos, int type)
    {
        float floraHeight = transform.position.y + 1;
        if (type > 0)
        {
            GameObject flora = Instantiate(tree);
            flora.transform.position = new Vector3(pos, floraHeight, transform.position.z);
        }
        if (type == 0)
        {
            GameObject flora = Instantiate(flower);
            flora.transform.position = new Vector3(pos, floraHeight, transform.position.z);
        }

    }

}