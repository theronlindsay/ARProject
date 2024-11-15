using Unity.Tutorials.Core.Editor;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private bool started;
    public GameObject player;
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
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }

    //When the enemy touches the player, the player looses health
    public void OnTriggerEnter(Collider collision)  
    {
        Debug.Log("Collision detected");
        if (collision.gameObject == player)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }

    // start the enemy movement
    public void StartEnemy()
    {
        started = true;
    }
}
