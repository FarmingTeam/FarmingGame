using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    public TMP_Text DateText;
    public TMP_Text HourText;
    public TMP_Text MinuteText;

    public void UpdateUI(GameTime time)
    {
        DateText.text = time.date.ToString();
        HourText.text = time.hour.ToString();
        MinuteText.text = time.minute.ToString();
    }
}
