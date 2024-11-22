using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody2D paddleRigidBody;
    private Vector2 direction;
    public float speed = 30f;
    public float maxX;
    private Vector2 startPosition;

    public Transform leftlaser;
    public Transform rightlaser;
    public GameObject laser;
    public float laserDuration;
    private bool canShootLaser;
    private float laserTimer;
    public float laserCooldown;
    public float laserCoolDownTimer;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(0, -8.79f);

        paddleRigidBody = GetComponent<Rigidbody2D>();
        paddleRigidBody.isKinematic = true;

        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        paddleMovement();
        paddleBoundaries();
        laserCountDown();

        if (canShootLaser && laserCoolDownTimer <= 0)
        {
            shootLaser();
            laserCoolDownTimer = laserCooldown;
        }

        if (laserCoolDownTimer > 0)
        {
            laserCoolDownTimer -= Time.deltaTime;   
        }
    }
    private void paddleBoundaries()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -maxX, maxX);
        transform.position = position;
    }

    private void paddleMovement()
    {
        //get user input
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }

        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.zero;
        }
        //move paddle according to input
        transform.Translate(direction * speed * Time.deltaTime);

    }

    public void resetPaddle()
    {
        paddleRigidBody.velocity = Vector2.zero;
        transform.position = startPosition;
    }


    //LASER
    public void activateLaserPowerUp()
    {
        if(!canShootLaser)
        {
            canShootLaser = true;
            laserTimer = laserDuration;
        }
    }

    private void laserCountDown()
    {
        if (canShootLaser)
        {
            laserTimer -= Time.deltaTime;

            if(laserTimer <= 0)
            {
                deactivatePowerUps();
            }
        }
    }

    public void deactivatePowerUps()
    {
        canShootLaser = false;
    }

    private void shootLaser()
    {
        Instantiate(laser, leftlaser.position, Quaternion.identity);
        Instantiate(laser, rightlaser.position, Quaternion.identity);
    }
}
