using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{


    [SerializeField] private AudioClip coinSound;

    protected virtual void Collect()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.CoinCollected();

        if (coinSound != null)
            AudioSource.PlayClipAtPoint(coinSound, transform.position);

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(); // Call virtual method
        }
    }

}
