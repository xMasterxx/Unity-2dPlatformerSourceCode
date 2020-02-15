using System.Collections;
using UnityEngine;
using DG.Tweening;


public class FailMenuScript : MonoBehaviour
{

    public GameObject halo;
    public GameObject haloTwo;
    public GameObject stageStatus;
    [SerializeField] float time = 3.7f;
    [SerializeField] bool invoke;
    [SerializeField] float minimum = 0.6F;
    [SerializeField] float maximum = 1.0F;

    void Start()
    {
        invoke = gameObject.activeSelf;
    }

    void Update()
    {
        if (invoke)
        {
            ScaleAnimation();
        }
        StartCoroutine(ScaleAnimationTimer(time));
    }
    void ScaleAnimation()
    {
        
            haloTwo.transform.Rotate(Vector3.forward * 1);
            halo.transform.localScale = new Vector3(Mathf.PingPong(Time.time, maximum - minimum) + minimum, Mathf.PingPong(Time.time, maximum - minimum) + minimum);
            haloTwo.transform.localScale = new Vector3(Mathf.PingPong(Time.time, maximum - minimum) + minimum, Mathf.PingPong(Time.time, maximum - minimum) + minimum);
            stageStatus.transform.localScale = new Vector3(Mathf.PingPong(Time.time, maximum - minimum) + minimum, Mathf.PingPong(Time.time, maximum - minimum) + minimum);
           
        
    }
    IEnumerator ScaleAnimationTimer(float value)
    {
        yield return new WaitForSeconds(value);
        invoke = false;
        halo.transform.DOScale(maximum, 2f);
        haloTwo.transform.DOScale(maximum, 2f);
        stageStatus.transform.DOScale(maximum, 2f);
    }
}


