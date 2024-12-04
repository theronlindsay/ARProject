using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    private bool started;
    public GameObject player;
    public GameObject EnemyHolder;
    public float speed = 0.6f;
    public float slowDuration = 5f; // How long the enemy should remain slowed
    public float slowedSpeed = 0.0f; // Reduced speed for the enemy
    private bool slowed = false;

    private GameObject spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //Remove started if you want to change when the enemy starts moving
        started = true;
        player = GameObject.Find("Player");
    }

    public void SetSpawner(GameObject spawner)
    {
        this.spawner = spawner;
    }

    // Update is called once per frame
    private void Update()
    {
        if(started)
        {
            EnemyHolder.transform.LookAt(player.transform);
            EnemyHolder.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    // start the enemy movement
    public void StartEnemy()
    {
        started = true;
    }

    public void Slow()
    {
        if (!slowed)
        {
            StartCoroutine(SlowEnemy());
        }
    }
    private IEnumerator SlowEnemy()
    {
        slowed = true;

        // Save the original speed of the enemy
        float originalSpeed = speed;

        // Set the enemy's speed to the slowed value
        speed = slowedSpeed;

        // Wait for the duration of the slow effect
        yield return new WaitForSeconds(slowDuration);

        // Restore the enemy's original speed
        speed = originalSpeed;

        slowed = false;
    }

    public void SignalDeath()
    {
        spawner.GetComponent<EnemySpawner>().EliminateEnemy();  
    }
}
