using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionScript : MonoBehaviour
{

    public float disableTime;
    public float enableTime;
    public GameObject nextGameObject = null;
    private void OnEnable()
    {
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        gameObject.transform.DOScale(1f, 3f);
    }
    void Start()
    {
        StartCoroutine(DisableTimer(disableTime));
        StartCoroutine(NextGameobjectEnable(enableTime));
       

    }
    IEnumerator DisableTimer(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    IEnumerator NextGameobjectEnable(float time)
    {
        yield return new WaitForSeconds(time);
        nextGameObject.SetActive(true);
    }
}
