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

    public bool IsPaused => isPaused;
    public bool IsPauseViewActive => isPauseViewActive;

    #region Unity lifecycle

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPauseViewActive)
            {
                Toggle();
                pauseView.SetActive(true);
                isPauseViewActive = true;
            }
        }
    }

    #endregion


    #region Public methods

    public void Toggle()
    {

        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }
    public void ContinueGame()
    {
        Toggle();
        pauseView.SetActive(false);
        isPauseViewActive = false;
    }
    public void EndGame()
    {
        Toggle();
        pauseView.SetActive(false);
        isPauseViewActive = false;
    }

    #endregion


}