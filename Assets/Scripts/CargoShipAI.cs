using System.Collections.Generic;
using UnityEngine;

public class CargoShipAI : MonoBehaviour
{
    [SerializeField] GameObject healthPrefab;
    [SerializeField] GameObject timePrefab;

    public Transform target; // object to orbit around
    public float orbitSpeed = 10f;
    public float orbitDistance = 10f;

    public Vector3 orbitPosition = Vector3.up; // Axis to orbit around

    public int maxEnemies;
    public int kills;
    public int level;

    private Renderer objectRenderer; // sets Visibility for Cargo ship

    // booleans for the health and time slow powerups
    private bool healthPickup = true;
    private bool timePickup = true;

    public void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        kills = GetComponent<GamePlayLoop>().totalKills;
        level = GetComponent<GamePlayLoop>().level;
        maxEnemies = GetComponent<GamePlayLoop>().maxEnemies;

        if (objectRenderer != null)
        {
            objectRenderer.enabled = false; // sets visibility to False by default
        }
    }

    public void Update()
    {
        // when half of the enemies in any given level are killed, the Cargo Ship will spawn
        if (kills >= (level * maxEnemies) / 2)
        {
            if (objectRenderer != null && !objectRenderer.enabled)
            {
                objectRenderer.enabled = true;
            }

            if (target != null)
            {
                transform.RotateAround(target.position, orbitPosition, orbitSpeed * Time.deltaTime);
            }
        }

        // if both prefabs are gone, then the Cargo Ship despawns
        if (!healthPickup && !timePickup)
        {
            Despawn();
        }
    }

    public void DropHealth()
    {
        if (healthPrefab != null)
        {
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
            healthPickup = false;
        }
        else
        {
            Debug.LogWarning("HealthPrefab is not assigned!");
        }
    }

    public void DropSloth()
    {
        if (timePrefab != null)
        {
            Instantiate(timePrefab, transform.position, Quaternion.identity);
            timePickup = false;
        }
        else
        {
            Debug.LogWarning("TimePrefab is not assigned!");
        }
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}
