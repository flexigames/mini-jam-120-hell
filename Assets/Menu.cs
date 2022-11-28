using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Start()
    {
        Sounds.Play("Monster_Scream");
        var time = Timer.time;
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;

        timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Main");
            Timer.time = 0;
        }
    }
}
