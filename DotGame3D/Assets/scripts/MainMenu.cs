using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true; // make mouse cursor visible 
    }
    public void PlayGame()
    {
        Scoring.gamescore = 0;
        Scoring.levelTimer = 100.0f;
        SceneManager.LoadScene(GameState.FirstLevelScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
