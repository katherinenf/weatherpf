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
        int length = (Random.Range(1, 4));
        GameObject frontVis = Instantiate(islandSegmentPrefab, transform);
        frontVis.transform.GetComponent<SpriteRenderer>().sprite = dryIslandParts[0];
        int floraType = (Random.Range(0, 2));
        for (int i = 0; i < length; i++)
        {
            GameObject midVis = Instantiate(islandSegmentPrefab, transform);
            midVis.transform.Translate(i + 1, 0, 0);
            midVis.transform.GetComponent<SpriteRenderer>().sprite = (dryIslandParts[Random.Range(1, dryIslandParts.Count - 2)]);
            AddFlora(midVis, floraType);
        }
        GameObject endVis = Instantiate(islandSegmentPrefab, transform);
        endVis.transform.Translate(length + 1, 0, 0);
        endVis.transform.GetComponent<SpriteRenderer>().sprite = dryIslandParts[dryIslandParts.Count - 1];
    }

    //adds trees or flowers based on type given
    void AddFlora(GameObject segment, int type)
    {
        if (type > 0)
        {
            GameObject flora = Instantiate(tree, segment.transform);
        }
        else
        {
            GameObject flora = Instantiate(flower, segment.transform);
        }
    }
}
