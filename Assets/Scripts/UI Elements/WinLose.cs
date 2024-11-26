using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using NUnit.Framework;

public class WinLose : MonoBehaviour
{
    public Damage damage;
    public TextMeshProUGUI GameOver;
    public TextMeshProUGUI Victory;
    // Static variable to store the count of destroyed enemies
    public static int destroyedEnemiesCount = 0;

    // If you want to display the count on the UI
    public TextMeshProUGUI enemiesDestroyedText;

    public GamePlayLoop gamePlayLoop;

    private void Start()
    {
        GameOver.gameObject.SetActive(false);
        Victory.gameObject.SetActive(false);
        gamePlayLoop = GameObject.Find("EventSystem").GetComponent<GamePlayLoop>();
    }
    void Update()
    {
        // Update the UI text with the current count
        if (enemiesDestroyedText != null)
        {
            enemiesDestroyedText.text = "Enemies killed: " + destroyedEnemiesCount + " / " + 10 * gamePlayLoop.maxEnenmies;
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
