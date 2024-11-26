using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GamePlayLoop gamePlayLoop;


    public void Start(){
        gamePlayLoop = GameObject.Find("EventSystem").GetComponent<GamePlayLoop>();
    }
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

            gamePlayLoop.EnemyKilled();
        }
    }
}
