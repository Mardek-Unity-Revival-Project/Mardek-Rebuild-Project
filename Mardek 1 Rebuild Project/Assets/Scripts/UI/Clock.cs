using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    public static float playedTime { get; private set; }

    public TextMeshProUGUI displayText;
    private int seconds;
    private int minutes;
    private int hours;
    

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

        hours = seconds / 3600;
        minutes = (seconds % 3600) / 60;
        seconds = (seconds % 3600) % 60;

        displayText.text = string.Format("{0}:{1}:{2}", hours.ToString(), minutes.ToString("D2"), seconds.ToString("D2")); 
    }
}
