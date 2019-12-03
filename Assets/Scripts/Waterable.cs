using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterable : MonoBehaviour
{
    // The dry visuals to hide when watered
    public GameObject[] dryVisuals;

    // The wet visuals to show when watered
    public GameObject[] wetVisuals;

    // The damages visuals to show when miswatered
    public GameObject[] damagedVisuals;

    // The point value that watering this awards
    public int pointValue;

    // If set, incorrectly watering this deals hp damage
    public bool dealsDamage;

    // Set when this has been watered
    bool isWatered;

    // The storm type required to water this
    StormType requiredStorm;

    public void IslandInit(StormType storm)
    {
        requiredStorm = storm;
    }

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

    public void OnWatered(StormType storm)
    {
        // Bail if already watered or if there is no bad effects from miswatering
        if (isWatered || (!dealsDamage && storm != requiredStorm))
            return;

        isWatered = true;

        // Hide the dry stuff
        foreach (GameObject go in dryVisuals)
        {
            go.SetActive(false);
        }

        if (storm != requiredStorm)
        {
            // Show the wet stuff
            foreach (GameObject go in damagedVisuals)
            {
                go.SetActive(true);
            }

            // Deal damage
            GameManager.Instance.LoseHealth();
        }
        else
        {
            // Show the wet stuff
            foreach (GameObject go in wetVisuals)
            {
                go.SetActive(true);
            }

            // Award score
            GameManager.Instance.AddScore(pointValue);
        }
    }
}
