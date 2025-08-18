using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pistol : Weapon
{
    [Header("Shooting")]
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask targetMask;

    [Header("Ammo")]
    [SerializeField] private int magazineSize = 10;
    [SerializeField] private float reloadTime = 1.5f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI ammoText;

    private int currentAmmo;
    private bool isReloading = false;
    private float nextFireTime;

    private void Start()
    {
        currentAmmo = magazineSize;
        UpdateAmmoUI();
    }

    private void Update()
    {
        if (isReloading) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                nextFireTime = Time.time + fireRate;
                Attack();
            }
            else
            {
                Debug.Log("Out of ammo! Press R to reload.");
            }
        }
    }

    public override void Attack()
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("Player camera not assigned.");
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        // Optional: Draw the ray in Scene view
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, range, targetMask))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Debug.Log($"{weaponName} hit {hit.collider.name} for {damage} damage.");
            }
        }

        currentAmmo--;
        UpdateAmmoUI();
        Debug.Log($"{weaponName} fired. Ammo: {currentAmmo}/{magazineSize}");
    }

    private System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;

        UpdateAmmoUI();
        Debug.Log("Reloaded.");
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"Ammo: {currentAmmo}/{magazineSize}";
        }
    }

    public override void Equip()
    {
        base.Equip();
        isReloading = false;
        UpdateAmmoUI();
    }
}