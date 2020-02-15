using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public GameObject coin;
    public int coinsNumber;
    public float timerDisable;
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isOpening", true);
            for (int i = 0; i < coinsNumber; i++)
            {
                var position = new Vector2(Random.Range(gameObject.transform.position.x-3, gameObject.transform.position.x + 3), gameObject.transform.position.y);
                Instantiate(coin, position, Quaternion.identity);
            }
            StartCoroutine(DisableChest(timerDisable));
        }
    }

    IEnumerator DisableChest(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
