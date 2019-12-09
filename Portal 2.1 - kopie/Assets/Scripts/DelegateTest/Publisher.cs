using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Publisher : MonoBehaviour
{
    public GameObject portalBlue;
    public GameObject portalRed;

    // 1- Define a delegate
    // A delegate is an contract between a publisher (the class who raises the event) and the subscriber (the class who receives the event).
    // You declare a delegate like an object with a return type, method name and arguments. Usually you send the 
    public delegate void PortalSpawnedEventHandler(object source, EventArgs args);

    // 2- Define an event based on that delegate
    // I named it in past sense to say the portal has already spawned in this case.
    public event PortalSpawnedEventHandler PortalHasSpawned;

  
    // C# naming conventions suggest that it should be a protected virutal void method that starts with On + name of the event.
    protected virtual void OnPortalHasSpawned()
    {
        //Check if there are any subscribers to this. 
        if(PortalHasSpawned != null)
        {
            //Notify all subscribers this event has occurred.
            PortalHasSpawned(this, EventArgs.Empty);
        }
    }

    //Method to spawn blue portal.
    public void SpawnBluePortal()
    {
        Instantiate(portalBlue);

        // 3- Raise the event 
        OnPortalHasSpawned();
    }

    //Method to spawn red portal.
    public void SpawnRedPortal()
    {
        Instantiate(portalRed);
        // 3- Raise the event 
        OnPortalHasSpawned();
    }

}
