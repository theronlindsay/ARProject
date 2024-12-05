using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public GameObject enemyPrefab;  // The enemy prefab to spawn
    [SerializeField] public GameObject spawnPoint; // The spawnerPrefab that defines the spawn location
    public float minSpawnInterval = 1f;  // Minimum spawn interval (in seconds)
    public float maxSpawnInterval = 5f;  // Maximum spawn interval (in seconds)
    public float spawnHeight = 1f;  // Height at which to spawn the enemies
    public int numEnemies = 0; // Determines the current amount of enemeies that have been spawned
    public int maxEnemies = 10;
    public int eliminations;
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);  // Spawn area size (X, Z)

    public GamePlayLoop gamePlayLoop;

    private bool isSpawning = false;  // Whether the spawning is active

    // Start is called before the first frame update
    void Start()
    {
        gamePlayLoop = GameObject.Find("EventSystem").GetComponent<GamePlayLoop>();
        gamePlayLoop.AddSpawner(gameObject);
    }

    public void StartSpawning(int level)
    {
        if (!isSpawning)
        {
            numEnemies = 0;
            eliminations = 0;
            isSpawning = true;
            StartCoroutine(SpawnEnemies(level * maxEnemies));
        }
    }

    private IEnumerator SpawnEnemies(int totalEnemies)
    {
        // while we are still spawning enemies, and we have not reached the total number of enemies to spawn, or havent eliminated all enemies, spawn a new enemy
        while (isSpawning && (eliminations < totalEnemies || numEnemies < totalEnemies)){}
        {
            // Wait for a random interval before spawning the next enemy
            float spawnDelay = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnDelay);

            Vector3 spawnPosition = spawnPoint.transform.position;

            // Spawn the enemy at the calculated position
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.GetComponent<EnemyAI>().SetSpawner(gameObject);
            numEnemies++;
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void EliminateEnemy()
    {
        numEnemies--;
    }
}