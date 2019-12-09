using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subscriber : MonoBehaviour
{
    public void OnPortalHasSpawned(object course,EventArgs e)
    {
        Debug.Log("BluePortalSpawned");
    }
}
