using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDamageable : MonoBehaviour
{
    public interface Damageable
    {
        void TakeDamage(int amount);
    }
}