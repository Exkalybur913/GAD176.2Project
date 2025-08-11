using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public void TakeDamage()
    {
        GameManager.Instance.EnemyKilled();
        Destroy(gameObject, 0.5f); // Destroys after half a second
        Debug.Log("Enemy Died");
    }
}
