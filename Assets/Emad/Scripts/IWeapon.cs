using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    /// This interface defines the basic functionality for a weapon in the game.
    void Attack();
    void Equip();
}