using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public GameObject enemyPrefab;  // The enemy prefab to spawn
    [SerializeField] public GameObject spawnerPrefab; // The spawnerPrefab that defines the spawn location
    public float minSpawnInterval = 1f;  // Minimum spawn interval (in seconds)
    public float maxSpawnInterval = 5f;  // Maximum spawn interval (in seconds)
    public float spawnHeight = 1f;  // Height at which to spawn the enemies
    public int numEnemies = 0; // Determines the current amount of enemeies that have been spawned
    public int maxEnemies = 10;
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);  // Spawn area size (X, Z)

    private bool isSpawning = false;  // Whether the spawning is active

    public void StartSpawning(int level)
    {
        if (!isSpawning)
        {
            numEnemies = 0;
            isSpawning = true;
            StartCoroutine(SpawnEnemies(level * maxEnemies));
        }
    }

    private IEnumerator SpawnEnemies(int totalEnemies)
    {
        while (isSpawning && numEnemies < totalEnemies)
        {
            // Wait for a random interval before spawning the next enemy
            float spawnDelay = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnDelay);

            // Use the spawnerPrefab's position to calculate the spawn position
            Vector3 spawnPosition = new Vector3(
                spawnerPrefab.transform.position.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                spawnHeight,  // Set the spawn height above the ground
                spawnerPrefab.transform.position.z + Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            // Spawn the enemy at the calculated position
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            numEnemies++;
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
