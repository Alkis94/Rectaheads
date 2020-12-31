using System;
using UnityEngine;
using System.Collections;
using TMPro;

public class TimeCountDown : MonoBehaviour
{
    [SerializeField]
    private int TimeLimit = 300;
    private TextMeshProUGUI timeText;

    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        float timeLeft = TimeLimit;
        string minutes;
        string seconds = "0";

        while (true)
        {
            timeLeft -= Time.deltaTime;
            minutes = Mathf.Floor(timeLeft / 60).ToString("00");
            seconds = Mathf.Floor(timeLeft % 60).ToString("00");
            timeText.text = minutes + ":" + seconds;

            if (timeLeft <= 0)
            {
                LevelEndManager.Instance.LevelFinished();
                timeText.text = "00:00";
                break;
            }

            yield return null;
        }
    }
}