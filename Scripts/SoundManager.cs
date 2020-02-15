using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public GameObject turnSoundOnButton;
    public GameObject turnSoundOffButton;

    void Start()
    {
        if (PlayerPrefs.GetString("Sound") == "Off")
        {
            SoundOff();
        }
        else
        {
            SoundOn();
        }
    }

    public void SoundOn()
    {
        AudioListener.pause = false;
        PlayerPrefs.SetString("Sound", "On");
        turnSoundOffButton.GetComponent<Button>().interactable = true;
        turnSoundOnButton.GetComponent<Button>().interactable = false;
    }

    public void SoundOff()
    {
        AudioListener.pause = true;
        PlayerPrefs.SetString("Sound", "Off");
        turnSoundOnButton.GetComponent<Button>().interactable = true;
        turnSoundOffButton.GetComponent<Button>().interactable = false;
    }
}
