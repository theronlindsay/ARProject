using TMPro;
using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    private GamePlayLoop gamePlayLoop;
    private HeartHealth health;


    public void Start(){
        
        StartCoroutine(DestroyBullet());
        gamePlayLoop = GameObject.Find("EventSystem").GetComponent<GamePlayLoop>();
        health = GameObject.Find("HealthManager").GetComponent<HeartHealth>();
    }
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {
            gamePlayLoop.EnemyKilled();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "HealthPot")
        {
            health.Heal();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "SlothPot")
        {
            SlowAllEnemies();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }

    private void SlowAllEnemies()
    {
        // Find all objects tagged as "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            // Check if the enemy has an EnemyAI component
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.Slow(); // Call the Slow() method
            }
        }
    }

    //Destroy the bullet after 5 seconds
    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
