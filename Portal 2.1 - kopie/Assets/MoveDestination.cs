using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDestination : MonoBehaviour
{
    public Transform PortalCaster;
    public Transform PortalReceiver;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = PortalReceiver.position - new Vector3(0, 0, 0.4f);
        //Get the rotation from the caster.
        Quaternion receiverrotation = PortalCaster.rotation;
        Vector3 rotationVector = new Vector3(receiverrotation.x, (360.0f - receiverrotation.y),receiverrotation.z);

        transform.rotation = Quaternion.Euler(rotationVector);
    }

}
