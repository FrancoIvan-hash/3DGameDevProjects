using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{
    // move the camera to a position in the back and up relative to the car
    Vector3 camoffset;
    // Start is called before the first frame update
    void Start()
    {
        camoffset = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + camoffset;
    }
}
