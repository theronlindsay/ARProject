using UnityEngine;
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

    //bug fix
    public bool allowInvoke = true;

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        //Will check if allowed to hold down button and related input
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else shooting = Input.GetKey(KeyCode.Mouse0);

        //Shooting
        if (readyToShoot && shooting && !isReloading && currentAmmo > 0)
        {
            //Sets bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
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
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
}
