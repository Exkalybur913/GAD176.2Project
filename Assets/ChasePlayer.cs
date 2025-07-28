using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float speed;
    // Start is called before the first frame update
    public void ChaseThePlayer()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position = direction * speed * Time.deltaTime;

    }
}
