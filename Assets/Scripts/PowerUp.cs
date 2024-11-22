using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float fallSpeed;

    // Start is called before the first frame update
    void Start()
    {    
    }

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Paddle"))
        {
            Paddle paddle = collision.GetComponent<Paddle>();
            if(paddle != null)
            {
                applyPowerUp(paddle);

            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("OB"))
        {
            Destroy(gameObject);
        }
    }

    private void applyPowerUp(Paddle paddle)
    {
        if(gameObject.tag == "ExtraLife")
        {
            FindObjectOfType<GameManager>().addLife();
        }
        else if(gameObject.tag == "LaserPowerUp")
        {
            paddle.activateLaserPowerUp();
        }
        else if(gameObject.tag == "NoBreak")
        {
            Ball ball = FindObjectOfType<Ball>();
            ball.activateNoBreak();
        }
    }
}
