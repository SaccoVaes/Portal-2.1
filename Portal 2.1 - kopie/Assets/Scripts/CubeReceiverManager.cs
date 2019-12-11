using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///    This class manages all the actions of the cube receiver.
/// </summary>
public class CubeReceiverManager : MonoBehaviour
{
    //Reference to animation components.
    public Animation CubePressed;
    public MeshRenderer Meshrenderer;
    public Material RedReceiverMat;
    public Material GreenReceiverMat;

    public UnityEvent<GameObject> OnButtonPressed;
    public UnityEvent OnButtonStay;
    public UnityEvent OnButtonReleased;

    //Reference to the door components.
    public door door;

    public void Start()
    {
        Meshrenderer = this.GetComponent<MeshRenderer>();
    }


    public void OnCollisionEnter(Collision collision)
    {
        Meshrenderer.material = GreenReceiverMat;
    }

    public void OnCollisionStay(Collision collision)
    {
        
    }

    public void OnCollisionExit(Collision collision)
    {
        Meshrenderer.material = RedReceiverMat;
    }
}
