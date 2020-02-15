using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{

    void Update()
    {
        ScaleAnimation();
    }

    void ScaleAnimation()
    {
        float minimum = 1.0F;
        float maximum = 1.3F;
        transform.localScale = new Vector2(Mathf.PingPong(Time.time, maximum - minimum) + minimum, Mathf.PingPong(Time.time, maximum - minimum) + minimum);
    }
}
