using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    private readonly float speed = 15;
    [SerializeField] private float leftBound = -40;

    void Update()
    {


        transform.Translate(Vector2.left * Time.deltaTime * speed);


        if (transform.position.x < leftBound && gameObject.CompareTag("Cloud"))
        {
            Destroy(gameObject);
        }

    }
}
