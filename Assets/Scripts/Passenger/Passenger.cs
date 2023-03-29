using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    public event Action OnGetToBus;
    public static event Action<int> OnMoneyUpdated;

    private BusStation myBusStation;
    private BusStation targetBusstation;
    private float moneyAmount = 10;
  
    private Animator animator;
    enum PassengerState
    {
        Idle,
        GoingToBus,
        InBus,
        GoingToStation
    }
    private PassengerState state = PassengerState.Idle;
    void Start()
    {
        BusManager.Instance.OnDoorOpened += BusManager_OnDoorOpened;
        animator = GetComponentInChildren<Animator>();
    }

    private void BusManager_OnDoorOpened(BusStation busStation)
    {
        if(busStation == myBusStation)
            BoardToTheBus();
        
        if (busStation == targetBusstation && state == PassengerState.InBus)
            DisEmbark();
    }

    private void BoardToTheBus()
    {
        if (state == PassengerState.InBus) return;
        state = PassengerState.GoingToBus;
        transform.parent = BusManager.Instance.transform;
    }

    private void DisEmbark()
    {
        transform.parent = null;
        state = PassengerState.GoingToStation;
    }

    void Update()
    {
        float moveSpeed = 3f;
        if (state == PassengerState.GoingToBus)
            GoToBus(moveSpeed);

        if (state == PassengerState.GoingToStation)
            GoToTargetStation(moveSpeed);

        bool isBusLate = TimeSchedule.GetClock() > myBusStation.GetArriveTime();

        if (isBusLate)
           if(moneyAmount > -10)
                moneyAmount -= Time.deltaTime;
    }

    private void GoToTargetStation(float moveSpeed)
    {
        bool isOnWay = Vector3.Distance(transform.position, targetBusstation.transform.position) > 0.1f;
        if (isOnWay)
            MoveToDestination(moveSpeed, targetBusstation.transform.position);
        else
            Destroy(gameObject);
    }

    private void GoToBus(float moveSpeed)
    {
        bool isOnWay = Vector3.Distance(transform.position, BusManager.Instance.transform.position) > 0.1f;
        if (isOnWay)
            MoveToDestination(moveSpeed, BusManager.Instance.transform.position);
        else
            ReachToTheBus();
    }

    private void ReachToTheBus()
    {
        state = PassengerState.InBus;
        OnGetToBus?.Invoke();
        OnMoneyUpdated((int)moneyAmount);
    }

    private void MoveToDestination(float moveSpeed,Vector3 destination)
    {
        Vector3 directionToDestination = (destination - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, directionToDestination, Time.deltaTime * 5f);
        transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * moveSpeed);
        animator.SetBool("isWalking", true);
    }

    private void OnDisable()
    {
        BusManager.Instance.OnDoorOpened -= BusManager_OnDoorOpened;
    }
    public void Setup(BusStation busStation)
    {
        myBusStation = busStation;
        var allStationsExceptMine = FindObjectsOfType<BusStation>().ToList();
        allStationsExceptMine.Remove(busStation);
        int randomIndex = UnityEngine.Random.Range(0,allStationsExceptMine.Count);
        targetBusstation = allStationsExceptMine[randomIndex];
    }
}
