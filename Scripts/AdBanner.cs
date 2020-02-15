using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdBanner : MonoBehaviour
{
    AdsManager adsManagerScript;

    void Start()
    {
        adsManagerScript = GameObject.Find("AdsManager").GetComponent<AdsManager>();
    }

    public void HideBanner()
    {
        adsManagerScript.DisableBanner();
    }

    public void ShowBanner()
    {
        adsManagerScript.ShowBanner();
    }
}

