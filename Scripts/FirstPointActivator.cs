using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPointActivator : MonoBehaviour
{
   public bool toSecond;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            toSecond = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            toSecond = false;
        }
    }
}
