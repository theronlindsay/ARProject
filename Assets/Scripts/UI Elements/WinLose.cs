using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WinLose : MonoBehaviour
{
    public HeartHealth damage;
    public int totalEnemies = 60;
    public TMPro.TextMeshProUGUI gameOver;
    public TMPro.TextMeshProUGUI Victory;
    // Static variable to store the count of destroyed enemies
    public static int destroyedEnemiesCount = 0;

    // If you want to display the count on the UI
    public TMPro.TextMeshProUGUI enemiesDestroyedText;

    public GameObject replayLevel;
    public GameObject bigRestartButton;
    public GameObject smallRestartButton;

    private void Start()
    {
        gameOver.gameObject.SetActive(false);
        Victory.gameObject.SetActive(false);
        replayLevel.SetActive(false);
        bigRestartButton.SetActive(false);
        smallRestartButton.SetActive(true);
    }
    void Update()
    {
        // Update the UI text with the current count
        if (enemiesDestroyedText != null)
        {
            enemiesDestroyedText.text = "Total Eliminations: " + destroyedEnemiesCount + " / " + totalEnemies;
        }

        if(destroyedEnemiesCount >= totalEnemies)
        {
            Victory.gameObject.SetActive(true);
            //pause the game
            Time.timeScale = 0;
            bigRestartButton.SetActive(true);
            smallRestartButton.SetActive(false);
        }
    }

    // Method to be called when an enemy is destroyed
    public void EnemyDestroyed()
    {
        destroyedEnemiesCount++; // Increment the count of destroyed enemies
    }

    public void GameOver(){
        Time.timeScale = 0;
        gameOver.gameObject.SetActive(true);
        replayLevel.SetActive(true);
        bigRestartButton.SetActive(true);
        smallRestartButton.SetActive(false);
    }

    public void ResetUI(){
        gameOver.gameObject.SetActive(false);
        Victory.gameObject.SetActive(false);
        replayLevel.SetActive(false);
        bigRestartButton.SetActive(false);
        smallRestartButton.SetActive(true);
        damage.Heal(4);
        Time.timeScale = 1;

    }
}
