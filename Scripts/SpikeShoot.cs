using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeShoot : MonoBehaviour
{
    Vector2 startPosition;

    private void Start()
    {
        startPosition = gameObject.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            gameObject.transform.position = startPosition;
        
    }
}
