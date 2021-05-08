using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Variables
    [SerializeField] private Text scoreLabel;

    private int score;

    #endregion

    #region Unity lifecycle
    private void OnEnable()
    {
        GameOverSequence.OnReload += ReloadScore;
        Blocks.OnDestroyed += Blocks_OnDestroyed;
    }
    private void OnDisable()
    {
        Blocks.OnDestroyed -= Blocks_OnDestroyed;
    }

    private void Start()
    {
        score = 0;
        UpdateScoreLabel();
    }

    #endregion

    #region Public methods

    private void AddScore(int score)
    {
        this.score += score;
        UpdateScoreLabel();
    }

    #endregion

    #region Private methods

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

    #endregion
}
