using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private bool started;
    public GameObject player;
    public GameObject EnemyHolder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //Remove started if you want to change when the enemy starts moving
        started = true;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        if(started)
        {
            EnemyHolder.transform.LookAt(player.transform);
            EnemyHolder.transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }

    // start the enemy movement
    public void StartEnemy()
    {
        started = true;
    }
}
