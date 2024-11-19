using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartHealth : MonoBehaviour
{
    public Image[] hearts; // Array to hold heart images
    private int maxHealth = 3;
    private int currentHealth;
    public GameObject gameOverScreen; // Reference to the Game Over screen  

    public GameObject redFlashPanel; // Reference to the red flash panel
    public float flashDuration = 0.2f; // Duration of the flash
    private Image panelImage;
    

    //public Image[] cracks;
    //public float crackDuration = 0.5f;

    void Start()
    {
        panelImage = redFlashPanel.GetComponent<Image>();
        currentHealth = maxHealth; // Initialize health
        UpdateHearts();

        //foreach (var crack in cracks)
        //{
        //    crack.gameObject.SetActive(false);
        //}
    }

    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHearts();
            FlashRedScreen();
        }

        //if (currentHealth <= cracks.Length)
        //{
        //    StartCoroutine(ShowCracks(3 - currentHealth)); // Show crack based on damage taken
        //}


        // Check if health is below zero and handle player death if necessary
        if (currentHealth <= 0)
        {
            Debug.Log("Player has died!");
            //Show the Game Over screen
            gameOverScreen.SetActive(true);
            // Pause Game
            Time.timeScale = 0;

        }
    }

    //private IEnumerator ShowCracks(int crackIndex)
    //{
    //    // Ensure the crack image is visible
    //    cracks[crackIndex].gameObject.SetActive(true);

    //    // Wait for the specified duration
    //    yield return new WaitForSeconds(crackDuration);

    //    // Hide the crack image after the duration
    //    //cracks[crackIndex].gameObject.SetActive(false);
    //}

    //not used yet
    //public void Heal()
    //{
    //    if (currentHealth < maxHealth)
    //    {
    //        currentHealth++;
    //        UpdateHearts();
    //    }
    //}

    public void FlashRedScreen()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        // Enable the panel (make the red flash visible)
        redFlashPanel.SetActive(true);

        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Disable the panel (hide the red flash)
        redFlashPanel.SetActive(false);
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHealth; // Enable or disable heart images
        }
    }
}
