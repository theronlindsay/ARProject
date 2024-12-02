using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    public HeartHealth damage;
    public Text GameOver;
    public Text Victory;
    // Static variable to store the count of destroyed enemies
    public static int destroyedEnemiesCount = 0;

    // If you want to display the count on the UI
    public Text enemiesDestroyedText;

    private void Start()
    {
        GameOver.gameObject.SetActive(false);
        Victory.gameObject.SetActive(false);
    }
    void Update()
    {
        // Update the UI text with the current count
        if (enemiesDestroyedText != null)
        {
            enemiesDestroyedText.text = "Enemies killed: " + destroyedEnemiesCount + " / " + 40;
        }

        if(destroyedEnemiesCount >= 40)
        {
            Victory.gameObject.SetActive(true);
        }

        if (damage.IsDead())
        {
            GameOver.gameObject.SetActive(true);
        }
    }

    // Method to be called when an enemy is destroyed
    public static void EnemyDestroyed()
    {
        destroyedEnemiesCount++; // Increment the count of destroyed enemies
    }
}
