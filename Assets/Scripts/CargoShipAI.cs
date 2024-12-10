using System.Collections;
using UnityEngine;

public class CargoShipAI : MonoBehaviour
{
    [SerializeField] GameObject healthPrefab;
    [SerializeField] GameObject timePrefab;

    public Transform target; // object to orbit around
    public float orbitSpeed = 10f;
    public float orbitDistance = 10f;

    public Vector3 orbitPosition = Vector3.up; // Axis to orbit around

    private Renderer objectRenderer; // sets Visibility for Cargo ship

    // booleans for the health and time slow powerups
    private bool healthPickup = true;
    private bool timePickup = true;

    public void Start()
    {
        target = GameObject.Find("Player").transform;
        StartCoroutine(DropItems());
    }
    private IEnumerator DropItems()
    {
        yield return new WaitForSeconds(5);
        DropHealth();
        yield return new WaitForSeconds(5);
        DropSloth();
        yield return new WaitForSeconds(5);
        Despawn();
    }

    public void Orbit(){
        transform.RotateAround(target.position, orbitPosition, orbitSpeed * Time.deltaTime);
        Vector3 desiredPosition = (transform.position - target.position).normalized * orbitDistance + target.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime);
    }

    public void Update()
    {
        Orbit();
    }

    public void DropHealth()
    {
        if (healthPrefab != null)
        {
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
            healthPickup = false;
        }
        else
        {
            Debug.LogWarning("HealthPrefab is not assigned!");
        }
    }

    public void DropSloth()
    {
        if (timePrefab != null)
        {
            Instantiate(timePrefab, transform.position, Quaternion.identity);
            timePickup = false;
        }
        else
        {
            Debug.LogWarning("TimePrefab is not assigned!");
        }
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}
