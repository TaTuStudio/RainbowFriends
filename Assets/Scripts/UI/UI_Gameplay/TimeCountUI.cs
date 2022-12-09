using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCountUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    public void _SetTime(float seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);

        DateTime dateTime = DateTime.Today.Add(time);

        timeText.text = dateTime.ToString("mm:ss");
    }
}
