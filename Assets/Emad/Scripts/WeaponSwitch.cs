using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    /// This script allows the player to switch between different weapons (pistol and sword) using number keys.
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject sword;
    [SerializeField] private TextMeshProUGUI equippedWeapon;

    private GameObject currentWeapon;

    private void Start()
    {
        EquipWeapon(pistol); // Equip pistol by default
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(pistol);
            if (equippedWeapon != null)
            {
                equippedWeapon.text = "Equipped: Pistol";
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(sword);
            if (equippedWeapon != null)
            {
                equippedWeapon.text = "Equipped: Sword";
            }

        }
    }

    private void EquipWeapon(GameObject weaponToEquip)
    {
        if (currentWeapon == weaponToEquip)
            return;// If the weapon is already equipped, do nothing

        if (pistol != null) pistol.SetActive(false);
        if (sword != null) sword.SetActive(false);

        if (weaponToEquip != null)
        {
            weaponToEquip.SetActive(true);
            currentWeapon = weaponToEquip;

            IWeapon weaponScript = weaponToEquip.GetComponent<IWeapon>();
            weaponScript?.Equip();//
        }
    }
}