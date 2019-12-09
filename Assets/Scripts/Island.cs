using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    // Island segment prefabs to spawn. The first and last index must be the start and end segment.
    public GameObject[] islandSegmentPrefabs;

    // Flora types
    public GameObject tree;
    public GameObject flower;

    GameObject frontPrefab { get => islandSegmentPrefabs[0]; }
    GameObject backPrefab { get => islandSegmentPrefabs[islandSegmentPrefabs.Length - 1]; }

    void Start()
    {
        // Select what type of storm waters this island
        StormType stormType = PickStormType();

        // Select the length of this island's middle
        int length = Random.Range(1, 4);

        // Spawn the front segment
        GameObject frontVis = Instantiate(frontPrefab, transform);
        frontVis.GetComponent<Waterable>().IslandInit(stormType);

        // Spawn the middle segments
        for (int i = 0; i < length; i++)
        {
            GameObject midVis = Instantiate(PickRandomMiddleSegment(), transform);
            midVis.GetComponent<Waterable>().IslandInit(stormType);
            midVis.transform.Translate(i + 1, 0, 0);
            AddFlora(midVis, stormType);
        }

        // Spawn the end segment
        GameObject endVis = Instantiate(backPrefab, transform);
        endVis.GetComponent<Waterable>().IslandInit(stormType);
        endVis.transform.Translate(length + 1, 0, 0);
    }

    GameObject PickRandomMiddleSegment()
    {
        return islandSegmentPrefabs[Random.Range(1, islandSegmentPrefabs.Length - 2)];
    }

    //adds trees or flowers based on type given
    void AddFlora(GameObject segment, StormType stormType)
    {
        GameObject flora; 
        switch (stormType)
        {
            case StormType.Light: flora = Instantiate(flower, segment.transform); break;
            case StormType.Heavy: flora = Instantiate(tree, segment.transform); break;
            default: return;
        }

        flora.GetComponent<Waterable>().IslandInit(stormType);
    }

    StormType PickStormType()
    {
        int max = System.Enum.GetValues(typeof(StormType)).Length - 1;
        return (StormType)Random.Range(0, max);
    }
}
