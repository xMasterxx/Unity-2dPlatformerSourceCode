using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeStarter : MonoBehaviour
{
    GameManager gameManagerScript;
    public float time;
    public bool shake;
    
    void Awake()
    {
        gameManagerScript = GameObject.FindWithTag("MainCamera").GetComponent<GameManager>();
    }
    void Update()
    {
        if (shake)
        {
            gameManagerScript.cameraShake = shake; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shake = true;
        }
    }
}
