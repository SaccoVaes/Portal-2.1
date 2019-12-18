using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///    This class manages all the actions of the cube receiver.
/// </summary>
public class CubeReceiverManager : MonoBehaviour
{
    //References to components to change the cube receiver.
    public Animation AnimationCubeReceiver;
    public MeshRenderer Meshrenderer;
    public Material RedReceiverMat;
    public Material GreenReceiverMat;

    //Unity Events, so i can specify the action in the inspector.
    public UnityEvent OnButtonPressed;
    public UnityEvent OnButtonStay;
    public UnityEvent OnButtonReleased;

    private int index;

    public void Start()
    {
        Meshrenderer = this.GetComponent<MeshRenderer>();
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Interactable") || collision.collider.CompareTag("Player"))
        {
            index++;
            Meshrenderer.material = GreenReceiverMat;
            AnimationCubeReceiver.Play("ButtonPressed");
            OnButtonPressed.Invoke();
        }
        
    }

    public void OnCollisionStay(Collision collision)
    {
        
    }

    //When the cube or player ends the collision...
    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Interactable") || collision.collider.CompareTag("Player"))
        {
            if(index > 1)
            {
                index--;
            }
            else
            {
                index--;
                //Changes to cube receiver itself.
                Meshrenderer.material = RedReceiverMat;
                AnimationCubeReceiver.Play("ButtonLifted");

                //Invoke the UnityEvent.
                OnButtonReleased.Invoke();
            }
         
        }
    }
}
