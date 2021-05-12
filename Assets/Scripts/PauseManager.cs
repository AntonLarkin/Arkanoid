using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : SingletonMonoBehaviour<PauseManager>
{
    #region Variables

    private bool isPaused;
    private bool isPauseViewActive;

    #endregion


    #region Properties

    public bool IsPaused => isPaused;
    public bool IsPauseViewActive => isPauseViewActive;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        UiManager.OnExitButtonClicked += EndGame;
        UiManager.OnContinueButtonClicked += ContinueGame;
        GameOverSequence.OnReloadShowScore += ToggleFreezeScreen;
    }

    private void OnDisable()
    {
        UiManager.OnExitButtonClicked -= EndGame;
        UiManager.OnContinueButtonClicked -= ContinueGame;
        GameOverSequence.OnReloadShowScore -= ToggleFreezeScreen;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPauseViewActive)
            {
                Toggle();
            }
        }
    }

    #endregion


    #region Public methods

    public void Toggle()
    {
        ToggleFreezeScreen();

        UiManager.Instance.SetPauseViewActive(isPaused);
        isPauseViewActive = isPaused;
    }

    private void ContinueGame()
    {
        Toggle();
    }

    private void EndGame()
    {
        Toggle();
    }

    public void ToggleFreezeScreen()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    #endregion
}