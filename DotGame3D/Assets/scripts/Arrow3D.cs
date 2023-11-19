using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow3D : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public int direction; // four directions 0,1,2,3
                          // down, left, up, righ
    private float deathTimer;
    private int state; // 0 = alive, 1 = dying

    AudioSource whoosh;
    AudioSource bounce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        AudioSource[] audios = GetComponents<AudioSource>();
        whoosh = audios[0];
        bounce = audios[1];
        whoosh.Play();
        deathTimer = 1.0f;
        state = 0;
    }
    void checkTimer()
    {
        if (state == 1)
        {
            deathTimer -= Time.deltaTime;
        }
        if (deathTimer < 0.0f) Destroy(gameObject);
    }

    private Vector3 dirVector;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == 1) // checks if arrow is dying (collided with something)
        {
            rb.velocity = Vector3.zero; // stop movement
            checkTimer(); // destroy arrow
            return;
        }

        if (direction == 0) dirVector = new Vector3 (0.0f, 0.0f, -1.0f); // down
        if (direction == 1) dirVector = new Vector3(-1.0f, 0.0f, 0.0f); // left
        if (direction == 2) dirVector = new Vector3(0.0f, 0.0f, 1.0f); // up
        if (direction == 3) dirVector = new Vector3(1.0f, 0.0f, 0.0f); // right

        rb.velocity = dirVector.normalized * speed;

        /*if (state == 1)
        {
            deathTimer -= Time.deltaTime;
        }
        if (deathTimer < 0.0f) Destroy(gameObject);*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == 1) return;

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Arrow")
        {
            Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>());
            return;
        }

        bounce.Play();

        if (collision.gameObject.tag == "Spiker") Destroy(collision.gameObject);
        state = 1;
    }
}
