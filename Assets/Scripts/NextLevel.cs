using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private int currentLevel;
    public EnemySpawner spawner;
    public List<GameObject> spawnerList = new List<GameObject>();

    private void Start()
    {
        currentLevel = GetComponent<GamePlayLoop>().level;
        spawner = GetComponent<EnemySpawner>();
    }

    public void PlayNextLevel()
    {
        currentLevel += 1;

        foreach(GameObject spawner in spawnerList)
        {
            spawner.GetComponent<EnemySpawner>().StartSpawning(currentLevel);
        }
    }
}
