using UnityEngine;
using UnityEngine.UI;
using System;

public class UiManager : SingletonMonoBehaviour<UiManager>
{
    #region Variables

    [Header("Game UI")]
    [SerializeField] private GameObject pauseView;
    [SerializeField] private GameObject gameOverView;
    [SerializeField] private GameObject startView;

    [SerializeField] private Text scoreLabel;

    #endregion


    #region Public methods

    public void SetPauseViewActive(bool isPaused)
    {
        pauseView.SetActive(isPaused);
    }

    public void SetGameOverViewActive(bool isActive)
    {
        gameOverView.SetActive(isActive);
    }

    public void SetStartViewActive()
    {
        startView.SetActive(true);
        pauseView.SetActive(false);
    }

    public void UpdateScoreLabel(int score)
    {
        scoreLabel.text = score.ToString();
    }

    #endregion
}
