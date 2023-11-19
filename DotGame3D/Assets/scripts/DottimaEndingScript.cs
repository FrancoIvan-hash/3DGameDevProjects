using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DottimaEndingScript : MonoBehaviour
{
    private float TotalTime;
    // Start is called before the first frame update
    void Start()
    {
        TotalTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        TotalTime += Time.deltaTime;
        if (TotalTime > 12.6f) SceneManager.LoadScene(GameState.MenuScene);
    }
}
