using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextStopTimeUI : MonoBehaviour
{
    TextMeshProUGUI nextArriveTimeText;
    private void Awake()
    {
        nextArriveTimeText= GetComponent<TextMeshProUGUI>();
        
    }
    void Start()
    {
        SetArriveTimeText();
        StationManager.Instance.OnBusArrived += Instance_OnBusArrived;
    }

    private void Instance_OnBusArrived()
    {
        SetArriveTimeText();
    }

    private void SetArriveTimeText()
    {
        nextArriveTimeText.text = "Next Stop Arrive Time : " + StationManager.Instance.GetNextStation().GetArriveTime().GetClock().ToString();
    }
}
