using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Variables
    [Header("Game UI")]
    [SerializeField] private Text scoreLabel;
    [SerializeField] private GameObject gameOverView;
    [SerializeField] private Text finalScoreLabel;
    [SerializeField] private GameObject[] livesKeeper;

    [Header("AutoPlay")]
    [SerializeField] private bool isAutoPlay;

    private int score;
    private int livesCount;

    #endregion


    #region Propertiess
    public bool IsAutoPlay => isAutoPlay;
    public int LivesCount => livesCount;

    #endregion


    #region Unity lifecycle
    private void OnEnable()
    {
        GameOverSequence.OnReloadShowScore += ShowFinalScore;
        GameOverSequence.OnReload += ReloadLives;
        GameOverSequence.OnReload += ReloadScore;
        Blocks.OnDestroyed += Blocks_OnDestroyed;
    }
    private void OnDisable()
    {
        GameOverSequence.OnReloadShowScore -= ShowFinalScore;
        GameOverSequence.OnReload -= ReloadLives;
        Blocks.OnDestroyed -= Blocks_OnDestroyed;
        GameOverSequence.OnReload -= ReloadScore;
    }
    private void Start()
    {
        ReloadLives();
        ReloadScore();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameOverView.SetActive(false);
        }
    }

    #endregion


    #region Public methods
    public void LoseLife()
    {
        livesKeeper[livesCount].SetActive(false);
        livesCount--;
    }

    #endregion


    #region Private methods

    private void AddScore(int score)
    {
        this.score += score;
        UpdateScoreLabel();
    }
    private void UpdateScoreLabel()
    {
        scoreLabel.text = score.ToString();
    }

    #endregion


    #region Event handler
    private void Blocks_OnDestroyed(int score)
    {
        AddScore(score);
    }
    private void ReloadScore()
    {
        score = 0;
        UpdateScoreLabel();
    }
    private void ReloadLives()
    {
        livesCount = 2;
        for (int i = 0; i < livesKeeper.Length; i++)
        {
            livesKeeper[i].SetActive(true);
        }
    }
    private void ShowFinalScore()
    {
        gameOverView.SetActive(true);
        Time.timeScale = 0;
        finalScoreLabel.text = ($"Your final score is : {score.ToString()}\n PRESS <SPACE> TO RESTART");
    }

    #endregion
}
