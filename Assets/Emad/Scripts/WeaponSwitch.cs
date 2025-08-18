using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
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
            return;

        if (pistol != null) pistol.SetActive(false);
        if (sword != null) sword.SetActive(false);

        if (weaponToEquip != null)
        {
            weaponToEquip.SetActive(true);
            currentWeapon = weaponToEquip;

            // If the weapon implements IWeapon, we can call Equip()
            IWeapon weaponScript = weaponToEquip.GetComponent<IWeapon>();
            weaponScript?.Equip();
        }
    }
}