using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toycarscript : MonoBehaviour
{
    [SerializeField] private float rotateSpace = 1.6f;
    // using FixedUpdate instead of Update
    // -- want calculations to happen at fixed intervals rather than every frame
    // -- this will hopefully work on different systems regardless of framerates
    private void FixedUpdate()
    {
        Rigidbody rigidb = GetComponent<Rigidbody>();

        rigidb.freezeRotation = true; // keeps car from trumbling control
        if (Input.GetKey("right")) 
        {
            transform.Rotate(Vector3.up, rotateSpace);
        }
        if (Input.GetKey("left"))
        {
            transform.Rotate(Vector3.up, -rotateSpace);
        }
        if (Input.GetKey("space"))
        {
            if (rigidb)
            {
                // 10.0f = experimental power factor related to car's engine
                rigidb.AddForce(10.0f * (transform.rotation * Vector3.forward));
            }
        }
    }

}
