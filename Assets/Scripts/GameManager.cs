using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public int lifeCount = 3;

    public bool gameOver;
    public bool startGame;

    public GameObject gameOverScreen;
    private ScoreManager scoreManager;

    public GameObject noDestroy;

    private Ball ball;
    private Paddle paddle;

    private List<string> availableLevels;

    private int brickCount;

    public bool isSandboxMode;

    // Start is called before the first frame update
    void Start()
    {
        isSandboxMode = true;
        startGame = false;
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        scoreManager = FindObjectOfType<ScoreManager>();

        resetGame();
    }

    // Update is called once per frame
    void Update()
    {
        updateLifeText();

        if (startGame == true)
        {
            loadLevel();
        }
    }

    //Brick tracker
    public void addBrickToCount(int count)
    {
        brickCount += count;
        Debug.Log("BrickCount: " + brickCount);

    }

    public void subtractBrickFromCount()
    {
        brickCount--;
        nextlevel();

        Debug.Log("BrickCount: " + brickCount);

    }
    private void nextlevel()
    {
        if (brickCount <= 0)
        {
            disableActivePowerUps();
            loadLevel();
        }
    }

    //LIFE
    public void lostLife()
    {
        lifeCount--;
        updateLifeText();
        disableActivePowerUps();

        if (lifeCount <= 0)
        {
            loadGameOverScreen();
        }

    }
    private void updateLifeText()
    {
        livesText.text = "Lives: " + lifeCount;
    }

    //Next Level or display GameIver

    public void loadLevel()
    {
        startGame = false;
        int randomLevel = Random.Range(0, availableLevels.Count);
        string nextLevel = availableLevels[randomLevel];

        ball.resetBall();

        DontDestroyOnLoad(noDestroy);

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(nextLevel);
    }


    //this helps give the scence enough time to locate the brick spawner locations and spawn in the bricks
    //before i added this the bricks were not spawning into the scenes correctly
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BrickSpawner brickSpawner = FindObjectOfType<BrickSpawner>();

        brickSpawner.spawnBrick();
        SceneManager.sceneLoaded -= OnSceneLoaded;


    }

    //GAMEOVER SCREEN
    public void loadGameOverScreen()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
        scoreManager.displayFinalScore();
        Time.timeScale = 0;
    }

    //RESET GAME
    public void resetGame()
    {
        Time.timeScale = 1;
        lifeCount = 3;
        gameOver = false;

        brickCount = 0;
        updateLifeText();
        gameOverScreen.SetActive(false);

        availableLevels = new List<string> { "Level01", "Level02", "Level03" };
        scoreManager.resetScore();
    }

    public void addLife()
    {
        lifeCount++;
        updateLifeText();
    }

    private void disableActivePowerUps()
    {
        ball.deactivatePowerUps();
        paddle.deactivatePowerUps();
        destroyFallingPowerUps();
        destroyAllLasers();
    }


    private void destroyAllLasers()
    {
        GameObject[] lasers = GameObject.FindGameObjectsWithTag("Laser");
        foreach(GameObject laser in lasers)
        {
            Destroy(laser);
        }
    }


    private void destroyFallingPowerUps()
    {
        PowerUp[] powerUps = FindObjectsOfType<PowerUp>();

        foreach (PowerUp powerUp in powerUps)
        {
            Destroy(powerUp.gameObject);
        }
    }
}