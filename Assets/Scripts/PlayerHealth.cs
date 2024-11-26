using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Damage damageManager;
    public bool tookDamage = false; 


    public void Start()
    {
        damageManager = GameObject.Find("HealthManager").GetComponent<Damage>();
    }
    public void TakeDamage()
    {
        damageManager.TakeDamage(1);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy" && tookDamage == false)
        {
            tookDamage = true;
            TakeDamage();
            Destroy(collision.gameObject);
            tookDamage = false;
        }

    }

    private void Update(){
        //Set the player's position to the camera's position
        transform.position = Camera.main.transform.position;
    }
}
