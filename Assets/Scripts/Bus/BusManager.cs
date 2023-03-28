using System;
using System.Collections.Generic;
using UnityEngine;

public class BusManager : MonoBehaviour
{
    public event Action OnPassengerAdded;
    public event Action<BusStation> OnDoorOpened;
    public int Money { get; private set; }

    private BusStation activeBusStation;
    private bool isDoorOpen;
    
    public static BusManager Instance { get; private set; }
    private void Awake()
    {
        Instance= this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OpenDoor();
    }
    private void OpenDoor()
    {
        if(TryOpenDoor())
        {
            isDoorOpen= true;
            OnDoorOpened?.Invoke(activeBusStation);
        }
    }
    bool TryOpenDoor()
    {
        if (!isDoorOpen && activeBusStation != null)
            return true;
        else
            return false;
    }
    public void AddPassenger()
    {
        OnPassengerAdded?.Invoke();
    }
    public void AddMoney(int amount)
    {
        Money += amount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out BusStation busStation))
        {
            activeBusStation = busStation;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out BusStation busStation))
        {
            activeBusStation = null;
            isDoorOpen= false; 
        }
    }
   
}
