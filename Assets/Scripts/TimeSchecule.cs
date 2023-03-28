using UnityEngine;
using TMPro;
using System;

public class TimeSchecule : MonoBehaviour
{
    public static TimeSchecule Instance { get; private set; }
    public event Action OnClockUpdated;
    private float timer;
    private static Clock m_Clock;
    private void Awake()
    {
        Instance = this;
        m_Clock = new Clock(0,0);
    }
    private void Update()
    {
        timer += Time.deltaTime *10;
        if (timer >= 1f)
        {
            m_Clock.minutes++;
            timer= 0f;
            OnClockUpdated?.Invoke();
        }
    }
    public static Clock GetClock()
    {
        return m_Clock.GetClock();
    }
    
}
