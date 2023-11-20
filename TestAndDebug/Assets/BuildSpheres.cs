using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpheres : MonoBehaviour
{
    public GameObject block;
    public int width = 10;
    public int height = 4;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting");
        Debug.LogWarning("This is a warning");
        Debug.LogError("Error");

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Instantiate(block, new Vector3(x, y + 0.5f, 0), Quaternion.identity); // obj will be in its nat rot
                // debugging
                //Debug.Log("Y value: " + (y + 0.5f));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
