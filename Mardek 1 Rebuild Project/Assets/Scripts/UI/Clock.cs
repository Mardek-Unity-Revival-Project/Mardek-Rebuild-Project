using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    private float playedTime;
    public TextMeshProUGUI displayText;
    private int seconds;
    private int minutes;
    private int hours;
    private string stringSeconds;
    

    // Start is called before the first frame update
    void Start()
    {
        playedTime = 0;
        seconds = 0;
        minutes = 0;
        hours = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playedTime += Time.deltaTime;
        seconds = Mathf.RoundToInt(playedTime);

        if (playedTime > 60)
        {
            playedTime = 0;
            minutes += 1;
        }
        if (minutes > 60)
        {
            minutes = 0;
            hours += 1;
        }

        if (seconds < 10)
        {
            stringSeconds = "0" + seconds.ToString();
        }
        else
        {
            stringSeconds = seconds.ToString();
        }

        displayText.text = string.Format("{0}:{1}:{2}", hours.ToString(), minutes.ToString("X2"), stringSeconds); 
    }
}
