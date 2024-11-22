using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D ballRigidBody;
    public float force;
    private bool launched = false;
    private Transform paddle;
    private Paddle paddleMovement;
    private GameManager gameManager;


    private bool noBreak;
    public float noBreakDuration;
    private float noBreakTimer;
    private Color originalColor;
    //HEX COLOR 2A6FD9
    public Color noBreakColor = new Color(0.16f, 0.43f, 0.85f); 

    // Start is called before the first frame update
    void Start()
    {
        ballRigidBody = GetComponent<Rigidbody2D>();
        paddle = transform.parent;
        paddleMovement = paddle.GetComponent<Paddle>();
        gameManager = FindObjectOfType<GameManager>();
        originalColor = GetComponent<SpriteRenderer>().color;    
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !launched)
        {
            launch();
        }

        handleNoBreakTimer();
        
    }
    public void launch()
    {
        transform.SetParent(null);
        ballRigidBody.isKinematic = false;

        ballRigidBody.AddForce(new Vector2(force, force));
        launched = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OB"))
        {
            if(gameManager.isSandboxMode == true)
            {
                resetBall();
                paddleMovement.resetPaddle();
            }
            else
            {
                resetBall();
                paddleMovement.resetPaddle();
                gameManager.lostLife();
            }
        }
    }

    public void resetBall()
    {
        ballRigidBody.velocity = Vector2.zero;
        ballRigidBody.isKinematic = true;
        launched = false;

        transform.position = paddle.position + new Vector3(0,0.75f,0);
        transform.SetParent(paddle);
    }

    //no break functionality for ball
    public void activateNoBreak()
    {
        if(!noBreak)
        {
            noBreak = true;
            noBreakTimer = noBreakDuration;
            GetComponent<SpriteRenderer>().color = noBreakColor;
        }
    }
    public bool isNoBreakActive()
    {
        return noBreak;
    }

    public void deactivatePowerUps()
    {
        noBreak = false;
        GetComponent<SpriteRenderer>().color = originalColor;


    }

    private void handleNoBreakTimer()
    {
        if (noBreak)
        {
            noBreakTimer -= Time.deltaTime;

            if (noBreakTimer <= 0)
            {
                deactivatePowerUps();
            }
        }
    }
}
