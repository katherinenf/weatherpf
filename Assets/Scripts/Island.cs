using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public GameObject[] islandSegmentPrefabs;
    public GameObject tree;
    public GameObject flower;

    GameObject frontPrefab { get => islandSegmentPrefabs[0]; }
    GameObject backPrefab { get => islandSegmentPrefabs[islandSegmentPrefabs.Length - 1]; }

    void Start()
    {
        int length = Random.Range(1, 4);
        GameObject frontVis = Instantiate(frontPrefab, transform);
        int floraType = (Random.Range(0, 2));
        for (int i = 0; i < length; i++)
        {
            GameObject midVis = Instantiate(PickRandomMiddleSegment(), transform);
            midVis.transform.Translate(i + 1, 0, 0);
            AddFlora(midVis, floraType);
        }
        GameObject endVis = Instantiate(backPrefab, transform);
        endVis.transform.Translate(length + 1, 0, 0);
    }

    GameObject PickRandomMiddleSegment()
    {
        return islandSegmentPrefabs[Random.Range(1, islandSegmentPrefabs.Length - 2)];
    }

    //adds trees or flowers based on type given
    void AddFlora(GameObject segment, int type)
    {
        if (type > 0)
        {
            Instantiate(tree, segment.transform);
        }
        else
        {
            Instantiate(flower, segment.transform);
        }
    }
}
