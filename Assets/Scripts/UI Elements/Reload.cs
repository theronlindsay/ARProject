using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    public int maxAmmo = 10;           // Maximum ammo capacity
    public int currentAmmo;            // Current ammo
    public Text ammoDisplay;           // UI Text to display the ammo count
    public float reloadTime = 2f;      // Time it takes to reload

    private bool isreloading = false;  // Flag to check if reloading is in progress
    
    void Start()
    {
        // Initialize current ammo with max ammo at the start
        currentAmmo = maxAmmo;
        UpdateAmmoDisplay();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isreloading)
        {
            reload();
        }
    }

    public void reload()
    {
        if (currentAmmo < maxAmmo)
        {
            // Start reloading
            isreloading = true;

            // Start reloading animation or delay
            StartCoroutine(reloadCoroutine());
        }
        else
        {
            Debug.Log("Ammo already full.");
        }
    }

    IEnumerator reloadCoroutine()
    {
        // Wait for the reload time (simulate reload animation/delay)
        yield return new WaitForSeconds(reloadTime);

        // After reloading, set current ammo to maxAmmo
        currentAmmo = maxAmmo;

        // Update the UI
        UpdateAmmoDisplay();

        // Set reloading flag to false
        isreloading = false;
    }

    void UpdateAmmoDisplay()
    {
        // Update the ammo display text (you can modify this to use other UI components)
        if (ammoDisplay != null)
        {
            ammoDisplay.text = currentAmmo + " / " + maxAmmo;
        }
    }
}
