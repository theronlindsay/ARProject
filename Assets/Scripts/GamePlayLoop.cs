using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GamePlayLoop : MonoBehaviour
{
    private int level = 1;
    private int spawners = 0;
    public int kills = 0;
    public int totalKills = 0;  
    public int maxEnenmies = 10;
    public List<GameObject> spawnerList = new List<GameObject>();
    // Textmesh pro text for kill couunter
    public TextMeshProUGUI killCounter; 

    public void Start(){
                
    }

    public void EnemyKilled(){
        kills++;
        killCounter.text = "Enemies Killed " + kills + "/" + level * maxEnenmies;
        if(kills == level * maxEnenmies){
            level++;
            totalKills += kills;
            kills = 0;
            foreach (GameObject spawner in spawnerList)
            {
                spawner.GetComponent<EnemySpawner>().StartSpawning(level);
            }
        }
    }

    public void AddSpawner(GameObject spawner){
        spawnerList.Add(spawner);
        spawners++;

        if(spawners == 3){
            foreach (GameObject spawnPoint in spawnerList)
            {
                spawnPoint.GetComponent<EnemySpawner>().StartSpawning(level);
            }
        }
    }
    
}
