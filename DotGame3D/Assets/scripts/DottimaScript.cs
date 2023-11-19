using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DottimaScript : MonoBehaviour
{
    private int dottimaState = 0; // 0 no bomb, 1 with bomb, 2 dying
    private float deathTimer = 1.0f;

    public float levelCompleteTimer = 2.0f;
    public const int lastLevel = 6;

    private Rigidbody rb;
    public GameObject shot; // arrow
    private int direction;
    private float yrot; // y rotation

    private GameObject bomb;

    public AudioClip yikes;
    public AudioClip gameOver;
    public AudioClip levelComplete;
    //public AudioClip findExit;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; // make mouse cursor invisible at the start of game
        rb = GetComponent<Rigidbody>();
        //AudioSource.PlayClipAtPoint(findExit, Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
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
                SceneManager.LoadScene(GameState.EndScene);
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

        if (dottimaState == 1)
        {
            if (Input.GetKeyDown("space"))
            {
                bomb.GetComponent<Bomb3D>().bombState = 1;
                bomb.transform.SetParent(null);
                dottimaState = 0;
            }
        }

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
                if (Scoring.lives == 0)
                {
                    GameState.state = GameState.gameOver;
                    AudioSource.PlayClipAtPoint(gameOver, Camera.main.transform.position);
                }
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (dottimaState == 2) return;

        if (dottimaState == 0)
        {
            if (collision.gameObject.tag == "Bomb")
            {
                bomb = collision.gameObject;
                bomb.transform.SetParent(gameObject.transform);

                bomb.transform.localPosition = new Vector3(0.0f, 5.0f, 0.0f);

                Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>());
                dottimaState = 1;
            }
        }

        if (collision.gameObject.tag == "Spiker" || collision.gameObject.tag == "Robot")
        {
            // drop the bomb first
            if (dottimaState == 1)
            {
                bomb.GetComponent<Bomb3D>().bombState = 1;
                bomb.transform.SetParent(null);
            }
            dottimaState = 2;
            AudioSource.PlayClipAtPoint(yikes, Camera.main.transform.position);
        }

        if (collision.gameObject.name == "ExitLocation")
        {
            AudioSource.PlayClipAtPoint(levelComplete, Camera.main.transform.position);
            Scoring.gamescore += (int)Scoring.levelTimer;
            Scoring.levelTimer = 100.0f;

            if (GameState.level < lastLevel) GameState.state = GameState.levelComplete;
            else GameState.state = GameState.theEnd;
        }

    }
}
