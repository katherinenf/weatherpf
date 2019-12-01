using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flora : MonoBehaviour
{
    // The dry visuals to hide when watered
    public GameObject[] dryVisuals;

    // The wet visuals to show when watered
    public GameObject[] wetVisuals;

    void Start()
    {
        // Start by showing the dry stuff
        foreach (GameObject go in dryVisuals)
        {
            go.SetActive(true);
        }

        // Hide the wet stuff
        foreach (GameObject go in wetVisuals)
        {
            go.SetActive(false);
        }
    }

    public void OnWatered()
    {
        // Start by showing the dry stuff
        foreach (GameObject go in dryVisuals)
        {
            go.SetActive(false);
        }

        // Hide the wet stuff
        foreach (GameObject go in wetVisuals)
        {
            go.SetActive(true);
        }

        // Play watered emitter here!
    }
}
