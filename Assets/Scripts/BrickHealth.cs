using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickHealth : MonoBehaviour
{
    private int health;
    private SpriteRenderer spriteRenderer;
    public Sprite[] healthStates;

    public GameObject powerUpPrefab;
    private GameManager gameManager;
    private ScoreManager scoreManager;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        scoreManager = FindObjectOfType<ScoreManager>();


        health = healthStates.Length;
        spriteRenderer.sprite = healthStates[health - 1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();

            if (ball != null && !ball.isNoBreakActive())
            {
                Damage();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Laser"))
        {
            Damage();
        }
    }

    private void Damage()
    {
        health--;

        if(health <= 0)
        {
            releasePowerUp();
            Destroy(gameObject);
            gameManager.subtractBrickFromCount();
            scoreManager.addScore(100);

        }
        else
        {
            scoreManager.addScore(10);
            spriteRenderer.sprite = healthStates[health - 1];
        }
    }

    private void releasePowerUp()
    {
        if(powerUpPrefab != null)
        {
            GameObject powerUp = Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            powerUp.transform.SetParent(null);
        }
    }
}

//in the if of damage
//            //gameManager.subtractBrickFromCount();
