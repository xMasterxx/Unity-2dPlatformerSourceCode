using UnityEngine;
using DG.Tweening;

public class ChestResetScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            gameObject.transform.DOShakePosition(2f);
        }
    }
}
