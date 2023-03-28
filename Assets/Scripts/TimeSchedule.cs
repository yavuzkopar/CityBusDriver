using UnityEngine;
using TMPro;
using System;

public class TimeSchedule : MonoBehaviour
{
    public static TimeSchedule Instance { get; private set; }
    public event Action OnClockUpdated;
    private float timer;
    [SerializeField] float timeSpeed = 10;
    private static Clock m_Clock;

    private void Awake()
    {
        Instance = this;
        m_Clock = new Clock(0,0);
    }
    private void Update()
    {
        timer += Time.deltaTime *timeSpeed;
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
