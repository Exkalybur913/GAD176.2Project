using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCoin : Coins
{
    [SerializeField] private AudioClip specialSound;

    protected override void Collect()
    {
        base.Collect(); 
                
        Debug.Log("Special coin collected!");

        if (specialSound != null)
            AudioSource.PlayClipAtPoint(specialSound, transform.position);

    }

}
