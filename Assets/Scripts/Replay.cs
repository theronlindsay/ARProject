using UnityEngine;

public class Replay : MonoBehaviour
{
    public int currentLevel;
    public EnemySpawner spawner;

    private void Start()
    {
        currentLevel = GetComponent<GamePlayLoop>().level;
        spawner = GetComponent<EnemySpawner>();
    }

    public void replayLevel()
    {
        spawner.StartSpawning(currentLevel);
    }
}
