using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour
{
    private Rigidbody rb;
    AudioSource scrape;
    private float vol;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scrape = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        vol = rb.velocity.x * rb.velocity.x +
              rb.velocity.y * rb.velocity.y +
              rb.velocity.z * rb.velocity.z;

        if (vol > 0.01f) vol = 1.0f; else vol = 0.0f;
        scrape.volume = vol;
    }
}
