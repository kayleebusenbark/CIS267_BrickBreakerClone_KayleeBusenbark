using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIButtonHandler : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    //GAMEOVER SCREEN

    public void playGame()
    {
        //used for gameover screen and main menu screen
        gameManager.startGame = true;
        gameManager.isSandboxMode = false;
        gameManager.loadLevel();
    }

    public void playAgain()
    {
        gameManager.resetGame();
        gameManager.loadLevel();
    }

    public void exitToMainMenu()
    {
        gameManager.isSandboxMode = true;
        gameManager.startGame = false;
        gameManager.resetGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
