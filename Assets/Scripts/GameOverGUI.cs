using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverGUI : MonoBehaviour
{
    // Text field showing the distance traveled
    public Text distanceText;

    // Text field showing the watered score
    public Text scoreText;

    public void Show(float distance, int score)
    {
        gameObject.SetActive(true);
        distanceText.text = distance.ToString("N0") + "m";
        scoreText.text = score.ToString();
    }
}
