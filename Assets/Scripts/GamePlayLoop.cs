using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class GamePlayLoop : MonoBehaviour
{
    private bool isGameRunning = false;
    public int level = 0;
    private int spawners = 0;
    public int kills = 0;
    public int totalKills = 0;  
    public int maxEnemies = 10;
    public List<GameObject> spawnerList = new List<GameObject>();
    // Textmesh pro text for kill couunter
    public TextMeshProUGUI killCounter; 

    public GameObject fireButton;
    public GameObject XROrigin;
    public GameObject objectSpawner;
    
    private WinLose winLose;
    public TMPro.TextMeshProUGUI levelText;

    public void Start(){
        fireButton.SetActive(false);
        winLose = GetComponent<WinLose>();
    }

    public void EnemyKilled(){
        kills++; // this gets reset when level is incremented
        totalKills++; //this doesnt get reset
        killCounter.text = kills + "/" + level * maxEnemies;
        winLose.EnemyDestroyed(); // Increment the count of destroyed enemies

        if(kills >= level * maxEnemies){
            level++;
            levelText.text = level.ToString();
            kills = 0;
            killCounter.text = kills + "/" + level * maxEnemies;
            foreach (GameObject spawner in spawnerList)
            {
                spawner.GetComponent<EnemySpawner>().StartSpawning(level);
            }
        }
    }

    public void AddSpawner(GameObject spawner){
        Debug.Log("Spawner added");
        spawnerList.Add(spawner);
        spawners++;

        if(spawners == 3){
            level++;
            levelText.text = level.ToString();
            killCounter.text = kills + "/" + level * maxEnemies;
            Debug.Log("Got all spawners");
            fireButton.SetActive(true);
            foreach (GameObject spawnPoint in spawnerList)
            {
                //Disable AR Mesh overlay
                XROrigin.GetComponent<ARPlaneManager>().enabled = false;
                //Disable the ability to place spawners
                objectSpawner.GetComponent<ObjectSpawner>().enabled = false;
                objectSpawner.GetComponent<ARInteractorSpawnTrigger>().enabled = false;

                spawnPoint.GetComponent<EnemySpawner>().StartSpawning(level);

                isGameRunning = true;
            }
        }
    }

    public void ResetLevel(){
        kills = 0;
        totalKills = level * maxEnemies;
        killCounter.text = kills + "/" + level * maxEnemies;
        levelText.text = level.ToString();
        winLose.ResetUI();
        //Find all Enemy and Bullet objects and destroy them
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            if(bullet != null){
                Destroy(bullet);
            }
        }
    }
    
}
