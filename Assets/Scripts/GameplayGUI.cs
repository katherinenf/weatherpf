using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayGUI : MonoBehaviour
{
    // The array of health icons to show and hide
    public GameObject[] hpIcons;

    // Text field showing the distance traveled
    public Text distanceText;

    // Text field showing the watered score
    public Text scoreText;

    // The player to poll distance from
    public Player player;

    void Update()
    {
        distanceText.text = player.distanceTraveled.ToString("N0") + "m";
    }
}
