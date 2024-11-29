using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Processors;

public class HeartHealth : MonoBehaviour
{
    private int maxHealth = 3;
    private int currentHealth;
    [SerializeField] public int immuneTime = 2;
    public float crackDuration = 0.5f;
    public float flashDuration = 0.2f; // Duration of the flash

    public GameObject gameOverScreen; // Reference to the Game Over screen
    public GameObject redFlashPanel; // Reference to the red flash panel
    private Image panelImage;

    public bool immune = false;
    public bool dead = false;

    private Rigidbody2D rb;
    private Animator anim;

    public Image[] cracks;
    public Image[] hearts; // Array to hold heart images

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        panelImage = redFlashPanel.GetComponent<Image>();
        currentHealth = maxHealth; // Initialize health
        UpdateHearts();

        foreach (var crack in cracks)
        {
            crack.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && immune == false)
        {
            TakeDamage();
            if (currentHealth < 1)
            {
                Die();
            }
            StartCoroutine(Immune());
        }
    }

    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHearts();
            FlashRedScreen();
        }

        if (currentHealth <= cracks.Length)
        {
           StartCoroutine(ShowCracks(3 - currentHealth)); // Show crack based on damage taken
        }


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

    private IEnumerator ShowCracks(int crackIndex)
    {
        // Ensure the crack image is visible
        cracks[crackIndex].gameObject.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(crackDuration);

        // Hide the crack image after the duration
        //cracks[crackIndex].gameObject.SetActive(false);
    }

    public void Heal()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            UpdateHearts();
        }
    }

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

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        dead = true;
    }

    public bool IsDead()
    {
        return dead;
    }

    private IEnumerator Immune()
    {
        immune = true;
        yield return new WaitForSeconds(immuneTime);
        immune = false;
    }
}
