using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour
{
    public Publisher pub;
    public Subscriber sub;
    public SubscriberTwo subTwo;

    void Start()
    {
        //pub . PortalHasSpanwed is an event. This way we can add subscribers to this event.
        pub.PortalHasSpawned += sub.OnPortalHasSpawned;
        pub.PortalHasSpawned += subTwo.OnPortalHasSpawned;
        //Now we have a 'pointer' to this eventhandler.

        pub.SpawnBluePortal();
        pub.SpawnRedPortal();
    }
}
