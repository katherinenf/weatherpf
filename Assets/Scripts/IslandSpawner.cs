using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{
    public List<GameObject> dryMidPrefabs;
    public List<GameObject> wetMidPrefabs;
    public float spawnsPerMeter;
    public GameObject dryIslandFront;
    public GameObject dryIslandBack;
    public GameObject wetIslandFront;
    public GameObject wetIslandBack;
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
        makeTwo("front", position);
        position = position + 1;
        int floraType = (Random.Range(0, 2));
        while (length > 0)
        {
            length = length - 1;
            makeTwo("mid", position);
            AddFlora(position, floraType);
            position = position + 1;
        }
        makeTwo("back", position);

    }

    GameObject RandomMidPrefab(List<GameObject> prefabType)
    {
        if (prefabType.Count > 0)
        {
            return prefabType[Random.Range(0, dryMidPrefabs.Count)];
        }
        return null;
    }

    //adds trees or flowers based on type given
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

    //inserts both a dry and wet island prefab at the same location with SpriteRenderer disabled on wet
    void makeTwo(string type, float position)
    {
        if (type == "front")
        {
            GameObject frontVis = Instantiate(dryIslandFront);
            frontVis.transform.position = new Vector3(position, transform.position.y, transform.position.z);
            GameObject frontInvis = Instantiate(wetIslandFront);
            frontInvis.transform.position = new Vector3(position, transform.position.y, transform.position.z);
            frontInvis.GetComponent<SpriteRenderer>().enabled = false;

        }
        if (type == "mid")
        {
            GameObject midVis = Instantiate(RandomMidPrefab(dryMidPrefabs));
            midVis.transform.position = new Vector3(position, transform.position.y, transform.position.z);
            GameObject midInvis = Instantiate(RandomMidPrefab(wetMidPrefabs));
            midInvis.transform.position = new Vector3(position, transform.position.y, transform.position.z);
            midInvis.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (type == "back")
        {
            GameObject backVis = Instantiate(dryIslandBack);
            backVis.transform.position = new Vector3(position, transform.position.y, transform.position.z);
            GameObject backInvis = Instantiate(wetIslandBack);
            backInvis.transform.position = new Vector3(position, transform.position.y, transform.position.z);
            backInvis.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}