using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
public class ShootingUI : MonoBehaviour
{
    //bullet
    public GameObject bullet;

    //bullet force
    public float shootForce, upwardForce;

    //Gun Stuff
    public float timeBetwenShooting, spread, reloadTime, timeBetweenShots;
    public int bulletsPerTap;
    public bool allowButtonHold;

    int currentAmmo, bulletsShot;

    //bools
    bool shooting, readyToShoot, isReloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;

    public InputActionReference touchscreen;

    public int maxAmmo = 10;
    public bool isreloading = false;
    public TMPro.TextMeshProUGUI ammoDisplay;

    //bug fix
    public bool allowInvoke = true;

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        //if touchscreen is pressed, shoot
        if (touchscreen.action.triggered && readyToShoot && !isReloading && currentAmmo > 0)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Debug.Log("Shot");
        readyToShoot = false;

        //Finds the position to hit using raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); 
        RaycastHit hit;

        //checks if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75); //random point far away from player
        }

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Rotate bullet 90 degrees in x axis
        currentBullet.transform.Rotate(90, 0, 0);

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        currentAmmo--;
        bulletsShot++;

        //Invode resetShot (if not done already)
        if(allowInvoke)
        {
            Invoke("ResetShot", timeBetwenShooting);
            allowInvoke = false;
        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && currentAmmo > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

        //Update UI
        UpdateAmmoDisplay();
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }



    public void Reload()
    {
        if (currentAmmo < maxAmmo)
        {
            // Start reloading
            isreloading = true;
            Debug.Log("reloading...");

            // Start reloading animation or delay
            StartCoroutine(reloadCoroutine());
        }
        else
        {
            Debug.Log("Ammo already full.");
        }
    }

    private IEnumerator reloadCoroutine()
    {
        // Wait for the reload time (simulate reload animation/delay)
        yield return new WaitForSeconds(reloadTime);

        // After reloading, set current ammo to maxAmmo
        currentAmmo = maxAmmo;
        Debug.Log("reloaded! Ammo is full.");

        // Update the UI
        UpdateAmmoDisplay();

        // Set reloading flag to false
        isreloading = false;
    }

    public void UpdateAmmoDisplay()
    {
        // Update the ammo display text (you can modify this to use other UI components)
        if (ammoDisplay != null)
        {
            ammoDisplay.text = currentAmmo + " / " + maxAmmo;
        }
    }
}
