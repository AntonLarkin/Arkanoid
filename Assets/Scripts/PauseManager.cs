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
        SceneLoader.OnContinueButtonClicked += OnContinueButtonClicked_ContinueGame;
    }

    private void OnDisable()
    {
        SceneLoader.OnContinueButtonClicked -= OnContinueButtonClicked_ContinueGame;
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
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        UiManager.Instance.SetPauseViewActive(isPaused);
        isPauseViewActive = isPaused;
    }

    #endregion


    #region Event handlers

    private void OnContinueButtonClicked_ContinueGame()
    {
        Toggle();
    }

    #endregion
}