using System;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    [SerializeField]
    private List<BusStation> busStations= new List<BusStation>();
    private int stationIndex;
    [SerializeField] private Clock loopTime; 
    public event Action OnBusArrived;

    public static StationManager Instance { get; private set; }
    [SerializeField] GameObject goodFeedBack, BadFeedBack;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        BusManager.Instance.OnDoorOpened += BusManager_OnDoorOpened;
    }

    private void BusManager_OnDoorOpened(BusStation activeStation)
    {
        activeStation.SetStationActive(false);
        bool isArrivedOnTime = activeStation.GetArriveTime() > TimeSchecule.GetClock();
        if (isArrivedOnTime)
            goodFeedBack.SetActive(true);
        else
            BadFeedBack.SetActive(true);

        CheckLastStation(activeStation);
        for (int i = 0; i < busStations.Count; i++)
        {
            if (busStations[stationIndex] == activeStation) // control of bus in right station
            {
                SetNextStationIndex();
                OnBusArrived?.Invoke();
            }
        }
    }

    private void CheckLastStation(BusStation station)
    {
        BusStation lastStation = busStations[busStations.Count - 1];
        if (station == lastStation)
        {
            ApplyRouteTimeToAllStations();
        }
    }

    private void ApplyRouteTimeToAllStations()
    {
        foreach (BusStation station in busStations)
        {
            station.AddArriveTime(loopTime);
            station.SetStationActive(true);
        }
    }

    void SetNextStationIndex()
    {
        stationIndex++;
        if (stationIndex>= busStations.Count)
            stationIndex= 0;
    }
    public BusStation GetNextStation()
    {
        return busStations[stationIndex];
    }
}
