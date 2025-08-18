using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    /// This interface defines the basic functionality for objects that can take damage in the game.
    void TakeDamage(int amount);
}