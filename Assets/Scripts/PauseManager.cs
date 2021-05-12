using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : SingletonMonoBehaviour<PauseManager>
{
    #region Variables

    [SerializeField] private GameObject pauseView;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;

    private bool isPaused;
    private bool isPauseViewActive;

    #endregion

    #region Properties

    public bool IsPaused => isPaused;
    public bool IsPauseViewActive => isPauseViewActive;

    #endregion


    #region Unity lifecycle

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

        pauseView.SetActive(isPaused);
        isPauseViewActive = isPaused;
    }

    public void ContinueGame()
    {
        Toggle();
    }

    public void EndGame()
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