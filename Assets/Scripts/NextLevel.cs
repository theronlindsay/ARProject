using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private int currentLevel;
    public EnemySpawner spawner;

    private void Start()
    {
        currentLevel = GetComponent<GamePlayLoop>().level;
        spawner = GetComponent<EnemySpawner>();
    }

    public void nextLevel()
    {
        currentLevel += 1;
        spawner.StartSpawning(currentLevel);
    }
}
