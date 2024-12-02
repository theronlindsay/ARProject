using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class GamePlayLoop : MonoBehaviour
{
    public int level = 1;
    private int spawners = 0;
    public int kills = 0;
    public int totalKills = 0;  
    public int maxEnenmies = 10;
    public List<GameObject> spawnerList = new List<GameObject>();
    // Textmesh pro text for kill couunter
    public TextMeshProUGUI killCounter; 

    public GameObject fireButton;
    public GameObject XROrigin;
    public GameObject objectSpawner;

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
                fireButton.SetActive(true);
                //Disable AR Mesh overlay
                XROrigin.GetComponent<ARPlaneManager>().enabled = false;
                //Disable the ability to place spawnersa
                objectSpawner.GetComponent<ObjectSpawner>().enabled = false;
                objectSpawner.GetComponent<ARInteractorSpawnTrigger>().enabled = false;

                spawnPoint.GetComponent<EnemySpawner>().StartSpawning(level);
            }
        }
    }
    
}
