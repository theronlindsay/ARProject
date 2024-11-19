using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Processors;

public class Damage : MonoBehaviour
{
    public HeartHealth heartHealth;
    private Rigidbody2D rb;
    private Animator anim;
    
    public int health = 3; // Initial health value
    public int damage = 1;   // Damage taken when touching a Enemy
    [SerializeField] public int immuneTime = 2;
    public bool immune = false;

    public Image[] cracks;
    public float crackDuration = 0.5f;
    public bool dead = false;
    private void Start()
    {
        heartHealth = FindFirstObjectByType<HeartHealth>(); // Finds the HeartHealth script in the scene
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        foreach (var crack in cracks)
        {
            crack.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && immune == false)
        {
            TakeDamage(damage);
            if (health < 1)
            {
                Die();
            }
            StartCoroutine(Immune());
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        heartHealth.TakeDamage();
        Debug.Log("Player Health: " + health);

        if (health <= cracks.Length)
        {
            StartCoroutine(ShowCracks(3 - health)); // Show crack based on damage taken
        }


        // Check if health is below zero and handle player death if necessary
        if (health <= 0)
        {
            Debug.Log("Player has died!");
            // You can add additional death handling logic here
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

    //private void RestartLevel()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //    Tomato.tomato = 0;
    //}

    private IEnumerator Immune()
    {
        immune = true;
        yield return new WaitForSeconds(immuneTime);
        immune = false;
    }
}