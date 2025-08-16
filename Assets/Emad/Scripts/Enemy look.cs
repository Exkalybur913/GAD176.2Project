using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrient : MonoBehaviour
{
    [SerializeField] GameObject player;
   
    void Start()
    {
        
    }

    // Making sure the enemies look at the player menacingly
    void Update()
    {
        transform.LookAt(player.transform);
    }
}
