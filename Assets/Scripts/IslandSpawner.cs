using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{ 
    public float spawnsPerMeter;
    public GameObject islandPrefab;
    public GameObject tree;
    public GameObject flower;

    public List<Sprite> dryIslandParts;



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
        GameObject frontVis = Instantiate(islandPrefab);
        frontVis.transform.position = new Vector3(position, transform.position.y, transform.position.z);
        frontVis.transform.GetComponent<SpriteRenderer>().sprite = dryIslandParts[0];
        //makeTwo("front", position);
        position = position + 1;
        int floraType = (Random.Range(0, 2));
        while (length > 0)
        {
            length = length - 1;
            GameObject midVis = Instantiate(islandPrefab);
            midVis.transform.position = new Vector3(position, transform.position.y, transform.position.z);
            midVis.transform.GetComponent<SpriteRenderer>().sprite = (dryIslandParts[Random.Range(1,dryIslandParts.Count - 2)]);
            //makeTwo("mid", position);
            AddFlora(position, floraType);
            position = position + 1;
        }
        GameObject endVis = Instantiate(islandPrefab);
        endVis.transform.position = new Vector3(position, transform.position.y, transform.position.z);
        endVis.transform.GetComponent<SpriteRenderer>().sprite = dryIslandParts[dryIslandParts.Count - 1];
        //makeTwo("back", position);

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

}