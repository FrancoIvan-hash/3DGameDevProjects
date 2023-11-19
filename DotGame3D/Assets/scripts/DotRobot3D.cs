using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotRobot3D : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public int direction; // four directions 0,1,2,3
                          // down, left, up, right
    public Vector3 dirVector;

    AudioSource thud;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thud = GetComponent<AudioSource>();
        direction = 0; // down direction
        dirVector = new Vector3(0.0f, 0.0f, -1.0f); // start moving down 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (direction == 0) dirVector = new Vector3(0.0f, 0.0f, -1.0f);
        if (direction == 1) dirVector = new Vector3(-1.0f, 0.0f, 0.0f);
        if (direction == 2) dirVector = new Vector3(0.0f, 0.0f, 1.0f);
        if (direction == 3) dirVector = new Vector3(1.0f, 0.0f, 0.0f);

        if (direction == 0) rb.MoveRotation(Quaternion.Euler(0.0f, 180.0f, 0.0f)); // down dir, move opp
        if (direction == 1) rb.MoveRotation(Quaternion.Euler(0.0f, 270.0f, 0.0f)); // left dir, move left
        if (direction == 2) rb.MoveRotation(Quaternion.Euler(0.0f, 0.0f, 0.0f)); // up dir, down rot
        if (direction == 3) rb.MoveRotation(Quaternion.Euler(0.0f, 90.0f, 0.0f)); // right dir, right rot

        rb.velocity = dirVector.normalized * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        thud.Play();
        Vector3 newPosition = new Vector3(
            transform.position.x - dirVector.x * 0.02f,
            transform.position.y,
            transform.position.z - dirVector.z * 0.02f);

        rb.MovePosition(newPosition);
        direction = (direction + 1) % 4; // keep direction between 0 and 3
    }
}
