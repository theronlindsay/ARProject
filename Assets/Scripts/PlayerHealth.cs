using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void TakeDamage(int damage)
    {
        Debug.Log("Player took " + damage + " damage");
    }
}
