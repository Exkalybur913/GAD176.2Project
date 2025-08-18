using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected string weaponName;
    [SerializeField] protected int damage;

    public virtual void Equip()
    {
        Debug.Log($"{weaponName} equipped.");
    }

    public abstract void Attack();
}
