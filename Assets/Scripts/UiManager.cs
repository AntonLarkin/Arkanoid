using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButtonInGame;
    [SerializeField] private Button exitButtonMainMenu;
    [SerializeField] private Button startGameButton;

    [SerializeField] private Text scoreLabel;
    [SerializeField] private Text finalScoreLabel;

    #endregion


    #region Events

    public static event Action OnContinueButtonClicked;
    public static event Action OnExitButtonClicked;

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

    public void UpdateScoreLabel(int score)
    {
        scoreLabel.text = score.ToString();
    }

    public void UpdateFinalScoreLabel(int score)
    {
        finalScoreLabel.text = ($"Your final score is : {score.ToString()}");
    }

    public void StartGame()
    {
        SceneTransitions.GoToFirstScene();
        startView.SetActive(false);
    }

    public void ContinueGame()
    {
        OnContinueButtonClicked?.Invoke();
    }

    public void ExitGame()
    {
        SceneTransitions.GoToStartScene();
        startView.SetActive(true);

        gameOverView.SetActive(false);
        pauseView.SetActive(false);
        OnExitButtonClicked?.Invoke();
    }

    public void ReloadGame()
    {
        SceneTransitions.GoToFirstScene();

        gameOverView.SetActive(false);
        OnExitButtonClicked?.Invoke();
    }

    #endregion
}
