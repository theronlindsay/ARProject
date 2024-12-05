using System.Collections.Generic;
using UnityEngine;

public class LevelReplay : MonoBehaviour
{
    public int currentLevel;
    public List<GameObject> spawnerList = new List<GameObject>();

    public GameObject gamePlayLoop; 

    private void Start()
    {
        currentLevel = GetComponent<GamePlayLoop>().level;
    }

    public void ReplayLevel()
    {
        gamePlayLoop.GetComponent<GamePlayLoop>().ResetLevel();
        foreach (GameObject spawner in spawnerList)
        {
            spawner.GetComponent<EnemySpawner>().StartSpawning(currentLevel);
        }
    }
}
