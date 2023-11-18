using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spiker3D : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 eulervel;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        eulervel = new Vector3(0, 100.0f, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(eulervel * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
