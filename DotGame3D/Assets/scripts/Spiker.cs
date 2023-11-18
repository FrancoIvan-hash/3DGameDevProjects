using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiker : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private int direction; // four directions 0,1,2,3
                           // down, left, up, right

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = 0;
    }

    private Vector3 dirVector;

    private void FixedUpdate()
    {
        if (direction == 0) dirVector = new Vector3(0.0f, 0.0f, -1.0f);
        if (direction == 1) dirVector = new Vector3(-1.0f, 0.0f, 0.0f);
        if (direction == 2) dirVector = new Vector3(0.0f, 0.0f, 1.0f);
        if (direction == 3) dirVector = new Vector3(1.0f, 0.0f, 0.0f);


        rb.velocity = dirVector.normalized * speed;

    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 newPosition = new Vector3(
             transform.position.x - dirVector.x * 0.7f,
             0.0f,
             transform.position.z - dirVector.z * 0.7f);

        rb.MovePosition(newPosition);
        direction = (direction + 1) % 4;
    }
}
