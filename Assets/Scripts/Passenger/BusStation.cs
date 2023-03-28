
using System.Collections.Generic;
using UnityEngine;

public class BusStation : MonoBehaviour
{
    [SerializeField]
    private Transform passenger;
    [SerializeField]
    private Clock arriveTime;

    private float timer;
    private int passengerCount;
    private int maxPassenger = 3;
    bool isStationActive = true;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            if(passengerCount < maxPassenger && isStationActive)
                GeneratePasengers();
            timer = 0f;
        }
    }

    private void GeneratePasengers()
    {
        float radius = 1f;
        Vector3 spawnPoint = GetRandomPointInsideCircleXZ(radius);
        spawnPoint += transform.position;
        Transform passengerTransform = Instantiate(passenger, spawnPoint, Quaternion.identity);
        Passenger passegerInstance = passengerTransform.GetComponent<Passenger>();
        passegerInstance.Setup(this);
        passengerCount++;

        passegerInstance.OnGetToBus += () => passengerCount--;
    }

    private Vector3 GetRandomPointInsideCircleXZ(float radius)
    {
        Vector3 spawnPoint = Random.insideUnitCircle * radius;
        spawnPoint.z = spawnPoint.y;
        spawnPoint.y = 0;
        return spawnPoint;
    }
    public void SetStationActive(bool isStationActive)
    {
        this.isStationActive = isStationActive;
    }
    public Clock GetArriveTime()
    {
        return arriveTime;
    }
    public void AddArriveTime(Clock clock)
    {
        arriveTime += clock;
    }
}
