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

    // The list of storms types that damage this
    public StormType[] damagingStorms;

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
        if (isWatered)
            return;

        if (storm == requiredStorm)
        {
            // Hide the dry stuff
            foreach (GameObject go in dryVisuals)
            {
                go.SetActive(false);
            }

            // Show the wet stuff
            foreach (GameObject go in wetVisuals)
            {
                go.SetActive(true);
            }

            // Award score
            GameManager.Instance.AddScore(pointValue);

            // Prevent further interaction
            isWatered = true;
        }
        else if (System.Array.IndexOf(damagingStorms, storm) != -1)
        {
            // Hide the dry stuff
            foreach (GameObject go in dryVisuals)
            {
                go.SetActive(false);
            }

            // Show the damaged stuff
            foreach (GameObject go in damagedVisuals)
            {
                go.SetActive(true);
            }

            // Deal damage
            GameManager.Instance.LoseHealth();

            // Prevent further interaction
            isWatered = true;
        }
    }
}
