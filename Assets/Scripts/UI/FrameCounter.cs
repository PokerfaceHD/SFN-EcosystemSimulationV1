using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameCounter : MonoBehaviour
{
    public Text text;
    float timeOfNextFrameCountRefresh;
    void Update()
    {
        if (timeOfNextFrameCountRefresh < Time.time)
        {
            timeOfNextFrameCountRefresh++;
            text.text = (Mathf.RoundToInt(1 / Time.deltaTime)).ToString();
        }
    }
}
