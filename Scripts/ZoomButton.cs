
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ZoomButton : MonoBehaviour
{
    Button zoomButton;
    GameManager gameManagerScript;
    bool zoomed;
    void Start()
    {
        gameManagerScript = GameObject.FindWithTag("MainCamera").GetComponent<GameManager>();
        zoomButton = GetComponent<Button>();
        zoomButton.onClick.AddListener(Zoom);
    }

    void Zoom()
    {
        if (zoomed)
        {
            gameManagerScript.isZoomed = false;
            zoomed = false;
        }
        else
        {
            gameManagerScript.isZoomed = true;
            zoomed = true;
        }
    }


}
