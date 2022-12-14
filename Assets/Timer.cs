using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static float time;

    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Game.isPaused)
            return;

        time += Time.deltaTime;

        int minutes = (int)time / 60;
        int seconds = (int)time % 60;

        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
