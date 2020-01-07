using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsDetection : MonoBehaviour
{
    public Transform RespawnPoint;
    private GameObject Player;
    //private bool IsRunning;

    public void Start()
    {
        Player = GameObject.Find("Player");
    }
    public void OntriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.transform.position = RespawnPoint.position;
        }
       
    }
}
