using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottimaScript : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject shot; // arrow
    private int direction;
    private float yrot; // y rotation

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

        float x, z;
        x = rb.velocity.x;
        z = rb.velocity.z;
        if (x != 0 || z != 0)
        {
            if (z < x) if (z < -x) direction = 0;
            if (z > x) if (z < -x) direction = 1;
            if (z > x) if (z > -x) direction = 2;
            if (z < x) if (z > -x) direction = 3;
        }

        if (Input.GetKeyDown("space"))
        {
            if (direction == 0) yrot = 0.0f;
            if (direction == 1) yrot = 90.0f;
            if (direction == 2) yrot = 180.0f;
            if (direction == 3) yrot = 270.0f;

            GameObject ar = Instantiate(shot, new Vector3(
                transform.position.x,
                transform.position.y + 0.2f,
                transform.position.z), 
                Quaternion.Euler(0, yrot, 0));

            ar.GetComponent<Arrow3D>().direction = direction;
            if (x != 0 || z != 0) ar.GetComponent<Arrow3D>().speed += speed;
        }

    }
}
