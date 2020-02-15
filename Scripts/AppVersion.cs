using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppVersion : MonoBehaviour
{
    Text version;
    void Awake()
    {
        version = GetComponent<Text>();
    }

    void Start()
    {
        version.text = $"ver {Application.version}";
    }


}
