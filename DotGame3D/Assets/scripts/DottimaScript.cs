using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DottimaScript : MonoBehaviour
{
    private int dottimaState = 0; // 0 no bomb, 1 with bomb, 2 dying
    private float deathTimer = 1.0f;
    public float levelCompleteTimer = 2.0f;
    public const int lastLevel = 3;
    private Rigidbody rb;
    public GameObject shot; // arrow
    private int direction;
    private float yrot; // y rotation

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         // check to lose lives
        if (dottimaState == 2)
        {
            float shrink = 1.0f - 2.0f * Time.deltaTime;
            transform.localScale = (new Vector3(
                transform.localScale.x * shrink,
                transform.localScale.y * shrink,
                transform.localScale.z * shrink));
            deathTimer -= Time.deltaTime;
            if (deathTimer < 0.0f)
            {
                deathTimer = 1.0f;
                Scoring.lives--; // decrement lives when touch by an enemy
                dottimaState = 0;
                gameObject.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                gameObject.transform.position = new Vector3(-0.06f, 0.0f, 0.6f);
                if (Scoring.lives == 0) GameState.state = GameState.gameOver;
            }
        }
        // transitioning between levels
        if (GameState.state == GameState.levelComplete) 
        {
            rb.velocity = Vector3.zero;
            levelCompleteTimer -= Time.deltaTime;
            if (levelCompleteTimer < 0.0f)
            {
                Scoring.gamescore += 500;
                GameState.level++; // increment scene level
                SceneManager.LoadScene(GameState.level + GameState.SceneOffset); // load next scene
            }
        }

        // reached end of game
        if (GameState.state == GameState.gameOver)
        {
            rb.velocity = Vector3.zero;
            levelCompleteTimer -= Time .deltaTime;
            if (levelCompleteTimer < 0.0f)
            {
                Scoring.lives = 5;
                GameState.level = 1;
                GameState.state = GameState.gamePlay;
                SceneManager.LoadScene(GameState.MenuScene);
            }
        }

        if (GameState.state == GameState.theEnd)
        {
            rb.velocity = Vector3.zero;
            levelCompleteTimer -= Time .deltaTime;
            if (levelCompleteTimer < 0.0f)
            {
                Scoring.lives = 5;
                Scoring.gamescore += 1000;
                GameState.level = 1;
                GameState.state = GameState.gamePlay;
                SceneManager.LoadScene(GameState.MenuScene);
            }
        }

        // At the end of a level, stop updating Dottima
        if (GameState.state == GameState.theEnd) return;
        if (GameState.state == GameState.levelComplete) return;
        if (GameState.state == GameState.gameOver) return;

        float speed = 2.0f;
        Vector3 moveInput = new Vector3(
            Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        rb.velocity = moveInput.normalized * speed;

        float x, z;
        x = rb.velocity.x;
        z = rb.velocity.z;
        if (x != 0 || z != 0)
        {
            if (z < x) if (z < -x) direction = 0;
            if (z > x) if (z < -x) direction = 1;
            if (z > x) if (z > -x) direction = 2;
            if (z < x) if (z > -x) direction = 3;
        }

        if (Input.GetKeyDown("space"))
        {
            if (direction == 0) yrot = 0.0f;
            if (direction == 1) yrot = 90.0f;
            if (direction == 2) yrot = 180.0f;
            if (direction == 3) yrot = 270.0f;

            GameObject ar = Instantiate(shot, new Vector3(
                transform.position.x,
                transform.position.y + 0.2f,
                transform.position.z), 
                Quaternion.Euler(0, yrot, 0));

            ar.GetComponent<Arrow3D>().direction = direction;
            if (x != 0 || z != 0) ar.GetComponent<Arrow3D>().speed += speed;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spiker") dottimaState = 2;

        if (collision.gameObject.name == "ExitLocation")
        {
            Scoring.gamescore += (int)Scoring.levelTimer;
            Scoring.levelTimer = 100.0f;

            if (GameState.level < lastLevel) GameState.state = GameState.levelComplete;
            else GameState.state = GameState.theEnd;
        }
    }
}
