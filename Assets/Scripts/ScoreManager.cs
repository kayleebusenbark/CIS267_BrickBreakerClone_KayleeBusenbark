using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    private int score;  
    // Start is called before the first frame update
    void Start()
    {
        resetScore();
        updateScoreText();
        
    }

    public void addScore(int points)
    {
        score += points;
        updateScoreText();
    }

    public void resetScore()
    {
        score = 0;
        updateScoreText();
    }

    private void updateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public void displayFinalScore()
    {
        finalScoreText.text = "Final Score: " + score;
    }

}
