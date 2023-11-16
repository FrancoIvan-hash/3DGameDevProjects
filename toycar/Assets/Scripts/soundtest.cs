using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundtest : MonoBehaviour
{
    // simple and cheap way to add audio to car
    AudioSource m_source;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        m_source = GetComponent<AudioSource>(); // obtain AudioSource component
    }

    // Update is called once per frame
    void Update()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>(); // get rigidbody comp of player
        m_source.volume = 0.3f * rb.velocity.magnitude; // above allows for use of velocity.magnitude
        m_source.volume += 0.2f; // adds base level of volume to hear the engine
    }
}
