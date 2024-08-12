using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText; // Change to TextMeshProUGUI
    private int score = 0;
    private float lastKillTime;
    private int comboMultiplier = 1;
    private int chainCount = 0;
    private string lastEnemyType;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount, string enemyType)
    {
        if (Time.time - lastKillTime < 1.0f) // 1 second for combo
        {
            comboMultiplier++;
        }
        else
        {
            comboMultiplier = 1;
        }

        if (enemyType == lastEnemyType)
        {
            chainCount++;
        }
        else
        {
            chainCount = 1;
        }

        score += amount * comboMultiplier * chainCount;
        UpdateScoreText();
        lastKillTime = Time.time;
        lastEnemyType = enemyType;
    }

    public void ResetChain()
    {
        comboMultiplier = 1;
        chainCount = 1;
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
