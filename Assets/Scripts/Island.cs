using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public GameObject islandSegmentPrefab;
    public List<Sprite> dryIslandParts;
    public GameObject tree;
    public GameObject flower;

    void Start()
    {
        float position = transform.position.x;
        int length = (Random.Range(1, 4));
        GameObject frontVis = Instantiate(islandSegmentPrefab);
        frontVis.transform.position = new Vector3(position, transform.position.y, transform.position.z);
        frontVis.transform.GetComponent<SpriteRenderer>().sprite = dryIslandParts[0];
        position = position + 1;
        int floraType = (Random.Range(0, 2));
        while (length > 0)
        {
            length = length - 1;
            GameObject midVis = Instantiate(islandSegmentPrefab);
            midVis.transform.position = new Vector3(position, transform.position.y, transform.position.z);
            midVis.transform.GetComponent<SpriteRenderer>().sprite = (dryIslandParts[Random.Range(1, dryIslandParts.Count - 2)]);
            AddFlora(position, floraType);
            position = position + 1;
        }
        GameObject endVis = Instantiate(islandSegmentPrefab);
        endVis.transform.position = new Vector3(position, transform.position.y, transform.position.z);
        endVis.transform.GetComponent<SpriteRenderer>().sprite = dryIslandParts[dryIslandParts.Count - 1];
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
