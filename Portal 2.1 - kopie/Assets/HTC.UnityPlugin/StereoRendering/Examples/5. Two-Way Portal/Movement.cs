using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform eye;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            this.transform.position += eye.forward * speed * Time.deltaTime;
        }
    }
}
