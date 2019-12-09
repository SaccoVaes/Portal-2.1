using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class LaserPointerV2 : MonoBehaviour
{
    public SteamVR_Behaviour_Pose pose;
    public SteamVR_Action_Boolean ActivateRaycast = SteamVR_Input.GetBooleanAction("GrabGrip");
    public SteamVR_Action_Boolean FireRaycast = SteamVR_Input.GetBooleanAction("Teleport");

    public PortalManager pm;

    public bool active = true;
    public Color color;
    public float thickness = 0.002f;
    public Color clickColor = Color.green;
    public GameObject holder;
    public GameObject pointer;
    bool isActive = false;
    public bool addRigidBody = false;
    public Transform reference;
    public event PointerEventHandler PointerIn;
    public event PointerEventHandler PointerOut;
    public event PointerEventHandler PointerClick;

    Transform previousContact = null;

    //Field for checking if sidegrib is pressed.
    public bool isCasting;


    private void Start()
    {
        PointerClick += pm.PortalFired;

        if (pose == null)
            pose = this.GetComponent<SteamVR_Behaviour_Pose>();
        if (pose == null)
            Debug.LogError("No SteamVR_Behaviour_Pose component found on this object");

        if (ActivateRaycast == null)
            Debug.LogError("No ui interaction action has been set on this component.");

        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;
        holder.transform.localRotation = Quaternion.identity;

        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = holder.transform;
        pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
        pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
        pointer.transform.localRotation = Quaternion.identity;
        BoxCollider collider = pointer.GetComponent<BoxCollider>();
        if (addRigidBody)
        {
            if (collider)
            {
                collider.isTrigger = true;
            }
            Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
            rigidBody.isKinematic = true;
        }
        else
        {
            if (collider)
            {
                Destroy(collider);
            }
        }
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        pointer.GetComponent<MeshRenderer>().material = newMaterial;
    }

    ////When the player releases, check if the portal can be spawned.
    //private void PortalFired(object sender, PointerEventArgs e)
    //{
    //    //Debug.Log(sender);
    //    if (pm.CanSpawnPortal(e))
    //    {
    //        pm.SpawnPortal(sender, e);
    //    }
    //}

    public virtual void OnPointerIn(PointerEventArgs e)
    {
        if (PointerIn != null)
            PointerIn(this, e);
    }

    public virtual void OnPointerClick(PointerEventArgs e)
    {
        if (PointerClick != null)
            PointerClick(this, e);
    }

    public virtual void OnPointerOut(PointerEventArgs e)
    {
        if (PointerOut != null)
            PointerOut(this, e);
    }


    private void Update()
    {
        if (!isActive)
        {
            isActive = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (ActivateRaycast != null && ActivateRaycast.GetState(pose.inputSource))
            isCasting = true;
        else isCasting = false;

        if (isCasting)
        {
            float dist = 100f;
            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit);

            if (previousContact && previousContact != hit.transform)
            {
                PointerEventArgs args = new PointerEventArgs();
                args.fromInputSource = pose.inputSource;
                args.distance = 0f;
                args.flags = 0;
                args.target = previousContact;
                OnPointerOut(args);
                previousContact = null;
            }
            if (bHit && previousContact != hit.transform)
            {
                PointerEventArgs argsIn = new PointerEventArgs();
                argsIn.fromInputSource = pose.inputSource;
                argsIn.distance = hit.distance;
                argsIn.flags = 0;
                argsIn.target = hit.transform;
                OnPointerIn(argsIn);
                previousContact = hit.transform;
            }
            if (!bHit)
            {
                previousContact = null;
            }
            if (bHit && hit.distance < 100f)
            {
                dist = hit.distance;
            }

            //if (bHit && ActivateRaycast.GetStateUp(pose.inputSource))
            //{
            //    PointerEventArgs argsClick = new PointerEventArgs();
            //    argsClick.fromInputSource = pose.inputSource;
            //    argsClick.distance = hit.distance;
            //    argsClick.flags = 0;
            //    argsClick.target = hit.transform;
            //    OnPointerClick(argsClick);
            //}

            if (bHit && ActivateRaycast.GetState(pose.inputSource) && FireRaycast.GetStateDown(pose.inputSource))
            {
                PointerEventArgs argsClick = new PointerEventArgs();
                argsClick.fromInputSource = pose.inputSource;
                argsClick.distance = hit.distance;
                argsClick.flags = 0;
                argsClick.target = hit.transform;
                OnPointerClick(argsClick);
            }

            if (ActivateRaycast != null && ActivateRaycast.GetState(pose.inputSource))
            {
                pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
                pointer.GetComponent<MeshRenderer>().material.color = clickColor;
            }
            else
            {
                pointer.transform.localScale = new Vector3(thickness, thickness, dist);
                pointer.GetComponent<MeshRenderer>().material.color = color;
            }
            pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);

        }
        else
        {
            pointer.transform.localScale = new Vector3(0, 0, 0);

        }
    }
    
    /*
    public struct PointerEventArgs
    {
        public SteamVR_Input_Sources fromInputSource;
        public uint flags;
        public float distance;
        public Transform target;
    }
    */

    public delegate void PointerEventHandler(object sender, PointerEventArgs e);
}

