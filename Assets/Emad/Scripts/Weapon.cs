using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    // This script defines the base class for all weapons in the game.
    [SerializeField] protected string weaponName;
    [SerializeField] protected int damage;

    public virtual void Equip()
    {
        Debug.Log($"{weaponName} equipped.");
    }

    public abstract void Attack();
}
