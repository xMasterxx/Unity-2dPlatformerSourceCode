using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InvokeAnimation : MonoBehaviour
{
    
    private void OnEnable()
    {
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        gameObject.transform.DOScale(1f, 0.6f);
    }
}
