using HTC.UnityPlugin.StereoRendering;
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

    //References to materials to set the wall behind the portals.
    private MeshRenderer rendererParentBlue;
    private MeshRenderer rendererParentRed;
    public Material MatBlue;
    public Material MatRed;
    public Material MatWall;

    private PortalDoorCollideDetection PortalDetectionPortalBlue;
    private PortalDoorCollideDetection PortalDetectionPortalRed;
    public PortalSet Portalset;
    public CapsuleCollider BodyCollider;

    public SteamVR_LaserPointer laserPointer;
    public void Start()
    {
        //laserPointer.PointerOut += SpawnPortal;
    }

    //When the player releases, check if the portal can be spawned.
    public void PortalFired(object sender, PointerEventArgs e)
    {
        if (CanSpawnPortal(e))
        {
            SpawnPortal(sender, e);
        }
        if (ShouldInitialisePortal())
        {
            InitialisePortals();
        }
    }

    //Method for checking if both portals are active. If only one, or none are active, the portals should not be activated.
    public bool ShouldInitialisePortal()
    {
        //check if both portals exist in the world.
        return (BluePortal != null && RedPortal != null) ? true : false;
    }

    //Initialise components of the portal to link them together.
    public void InitialisePortals()
    {
        //Get the stereorenderer components
        StereoRenderer StereoRendererPortalBlue = BluePortal.GetComponentInChildren<StereoRenderer>();
        StereoRenderer StereoRendererPortalRed = RedPortal.GetComponentInChildren<StereoRenderer>();

        //Set the destinations.
        StereoRendererPortalBlue.anchorTransform = RedPortal.transform.GetChild(1);
        StereoRendererPortalRed.anchorTransform = BluePortal.transform.GetChild(1);

        //Set the should render boolean to true;
        StereoRendererPortalBlue.shouldRender = true;
        StereoRendererPortalRed.shouldRender = true;

        //Set the color of the parent wall to x material;
        rendererParentBlue.material = MatBlue;
        rendererParentRed.material = MatRed;

        //Set the portal Detection variables, so the player can teleport to its destination on contact.
        PortalDetectionPortalBlue = BluePortal.GetComponentInChildren<PortalDoorCollideDetection>();
        PortalDetectionPortalRed = RedPortal.GetComponentInChildren<PortalDoorCollideDetection>();

        PortalDetectionPortalBlue.playerCollider = BodyCollider;
        PortalDetectionPortalRed.playerCollider = BodyCollider;
        PortalDetectionPortalBlue.portalManger = Portalset;
        PortalDetectionPortalRed.portalManger = Portalset;


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
    public void SpawnPortal(object sender, PointerEventArgs e)
    {
        //If the right hand shot...
        if (e.fromInputSource == Valve.VR.SteamVR_Input_Sources.RightHand) {
            //Set color back to gray
            if(ParentRedPortal != null)
            {
                rendererParentRed.material = MatWall;
            }
            ParentRedPortal = e.target.gameObject;
            rendererParentRed = ParentRedPortal.GetComponentInChildren<MeshRenderer>();
            rendererParentRed.material = MatRed;

            //Destroy current redportal
            Destroy(RedPortal);
            //Instantiate portal as a child of the parent wall;
            RedPortal = Instantiate(prefabRedPortal,ParentRedPortal.transform);
        } else if(e.fromInputSource == Valve.VR.SteamVR_Input_Sources.LeftHand)
        {
            //Set color back to gray
            if (ParentBluePortal != null)
            {
                rendererParentBlue.material = MatWall;
            }
            ParentBluePortal = e.target.gameObject;
            rendererParentBlue = ParentBluePortal.GetComponentInChildren<MeshRenderer>();
            rendererParentBlue.material = MatBlue;
            //Destroy current blueportal
            Destroy(BluePortal);
            //Instantiate portal as a child of the parent wall;
            BluePortal = Instantiate(prefabBluePortal, ParentBluePortal.transform);
        }
    }
}


