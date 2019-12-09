using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

/// <summary>
/// I made this class static because there should only be 1 portalmanager active in the scene.
/// </summary>
public class PortalManager : MonoBehaviour
{
    //Prefabs to spawn
    public GameObject prefabBluePortal;
    public GameObject prefabRedPortal;
    //References to the current Portal objects
    private GameObject BluePortal;
    private GameObject RedPortal;
    //References to the walls/floors where the portals are located.
    private GameObject ParentBluePortal;
    private GameObject ParentRedPortal;
    public GameObject RightHand;
    public GameObject LeftHand;

    public void Start()
    {
        RightHand = GameObject.FindGameObjectWithTag("RightHand");
        LeftHand = GameObject.FindGameObjectWithTag("LeftHand");
    }

    //When the player releases, check if the portal can be spawned.
    public void PortalFired(GameObject sender, PointerEventArgs e)
    {
        if (CanSpawnPortal(e))
        {
            SpawnPortal(sender, e);
        }
    }

    //Method for checking if both portals are active. If only one, or none are active, the portals should not be activated.
    public bool ShouldInitialisePortal()
    {
        //check if both portals exist in the world.
        return (BluePortal != null && RedPortal != null) ? true : false;
    }

    //Initialise components 
    public void InitialisePortals()
    {

    }

    //Method for checking if the portal spawn requirements are met.
    public bool CanSpawnPortal(PointerEventArgs e)
    {
        Debug.Log(e.target.gameObject);
        //Check if tag is portalable
        if (e.target.gameObject.CompareTag("Portalable") == false)
        {
            return false;
        }
        //Check if the target gameobject has a portal active already
        if (GameObject.ReferenceEquals(e.target.gameObject, ParentBluePortal) || GameObject.ReferenceEquals(e.target.gameObject, ParentRedPortal))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //Method to spawn the correct portal as a child of the wall.
    public void SpawnPortal(GameObject sender, PointerEventArgs e)
    {
        //If the right hand shot...
        if (GameObject.ReferenceEquals(sender, RightHand)){
            ParentRedPortal = e.target.gameObject;
            //Destroy current redportal
            Destroy(RedPortal);
            //Instantiate portal as a child of the parent wall;
            RedPortal = Instantiate(prefabRedPortal,ParentRedPortal.transform);
        } else if(GameObject.ReferenceEquals(sender, LeftHand))
        {
            ParentBluePortal = e.target.gameObject;
            //Destroy current blueportal
            Destroy(BluePortal);
            //Instantiate portal as a child of the parent wall;
            BluePortal = Instantiate(prefabBluePortal, ParentBluePortal.transform);
        }
    }
}


