using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player) player = GameObject.Find("Dottima");
        if (player)
        {
            transform.position = new Vector3 (player.transform.position.x,
                2.0f, player.transform.position.z - 1.6f);
            // y comp assumes y position of player is 0
        }
    }
}
