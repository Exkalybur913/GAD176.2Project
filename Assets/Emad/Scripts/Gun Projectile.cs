
using UnityEngine;
using TMPro;


public class GunP : MonoBehaviour
{
    //bullet 
    public GameObject bullet;

    //bullet speed
    public float shootForce, upwardForce;

    //Gun stats for different guns
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    //Recoil
    public Rigidbody playerRb;
    public float recoilForce;

    //bools
    bool shooting;
    bool readyToShoot; 
    bool reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;

    // UI and effects
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    //Debug
    public bool allowInvoke = true;

    private void Awake()
    {
        //Magzine full after reload
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        //Set ammo display
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
    }
    private void MyInput()
    {
        //Chek button hold
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
           
            bulletsShot = 0;

            Shoot();
        }
    }

    public void Shoot()
    {
        readyToShoot = false;

        //Find hit position with raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
 
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); 

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Bullet spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); 

        //Instantiate bullets
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); 
        currentBullet.transform.forward = directionWithSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        //Effects
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        //RestShot to allow different times in between shots
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;

            //Add Recoil 
            playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
        }

        //Repeoat shoot for shotguns
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {

        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
