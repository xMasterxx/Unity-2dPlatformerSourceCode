using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public bool horz = true;
    public GameObject moveFirstPoint;
    public GameObject moveSecondPoint;
    Vector2 firstPoint;
    Vector2 secondPoint;
    [SerializeField] float speed = 1;
    GameObject player;
    Vector3 offset;

    void Start()
    {
        player = null;
        firstPoint = moveFirstPoint.GetComponent<Transform>().position;
        secondPoint = moveSecondPoint.GetComponent<Transform>().position;
    }


    void Update()
    {
        if (horz)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x < firstPoint.x)
            {
                transform.position = new Vector2(firstPoint.x, transform.position.y);
                speed *= -1;
            }
            else if (transform.position.x > secondPoint.x)
            {
                transform.position = new Vector2(secondPoint.x, transform.position.y);
                speed *= -1;
            }
        }
        else
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            if (transform.position.y < firstPoint.y)
            {
                transform.position = new Vector2(transform.position.x, firstPoint.y);
                speed *= -1;
            }
            else if (transform.position.y > secondPoint.y)
            {
                transform.position = new Vector2(transform.position.x, secondPoint.y);
                speed *= -1;
            }
        }
        
    }

    void LateUpdate()
    {
        if (player != null)
        {
            player.transform.position = transform.position + offset;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            offset = player.transform.position - transform.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        player = null;
    }

}
