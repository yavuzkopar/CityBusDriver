using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIWorldHourText : MonoBehaviour
{
    TextMeshProUGUI worldHourText;
    void Start()
    {
        worldHourText= GetComponent<TextMeshProUGUI>();
        TimeSchecule.Instance.OnClockUpdated += Instance_OnClockUpdated;
        UpdateText();
    }

    private void Instance_OnClockUpdated()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        worldHourText.text = "Time is : " + TimeSchecule.GetClock().ToString();
    }

}
