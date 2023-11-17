using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottimaScript : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 2.0f;
        Vector3 moveInput = new Vector3(
            Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        rb.velocity = moveInput.normalized * speed;
    }
}
