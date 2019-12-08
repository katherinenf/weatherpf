using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    // The array of health icons to show and hide
    public GameObject[] hpIcons;

    // Text field showing the distance traveled
    public Text distanceText;

    // Text field showing the watered score
    public Text scoreText;

    // The player to poll distance from
    public Player player;

    // The gameover screen to enable
    public GameOverGUI gameOver;

    // The distance the player has traveled this game session
    public float distanceTraveled;

    // The current health of the player
    public float currentHealth;

    // Set when the game ends
    public bool gameIsOver;

    // The number of plants watered this game session
    public int score;

    // The timeline for animating hurricanes
    public PlayableDirector hurricaneDirector;

    // Public singleton accessor
    public static GameManager Instance { get => _instance; }

    // Private singleton instance
    private static GameManager _instance;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        currentHealth = hpIcons.Length;
        UpdateHPIcons();
    }

    void Update()
    {
        distanceText.text = distanceTraveled.ToString("N0") + "m";
        scoreText.text = score.ToString();
    }

    void UpdateHPIcons()
    {
        for (int i = 0; i < hpIcons.Length; i++)
        {
            hpIcons[i].SetActive(currentHealth > i);
        }
    }

    void GameOver()
    {
        gameIsOver = true;
        gameOver.Show(distanceTraveled, score);
    }

    public void LoseHealth()
    {
        if (!gameIsOver)
        {
            currentHealth--;
            UpdateHPIcons();

            if (currentHealth <= 0)
            {
                GameOver();
            }
        }
    }

    public void AddScore(int amount)
    {
        if (!gameIsOver)
        {
            score += amount;
        }
    }

    // Starts the hurricane animation or does nothing if it's already going
    public void StartHurricane()
    {
        // Play only if not already playing
        if (hurricaneDirector.state != PlayState.Playing)
        {
            hurricaneDirector.Play();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}
