using System;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
{
    #region Events

    public static event Action OnContinueButtonClicked;
    public static event Action OnExitButtonClicked;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        GameOverSequence.OnDefeatEndGame += OnDefeatGame_EndGame;
    }

    private void OnDisable()
    {
        GameOverSequence.OnDefeatEndGame -= OnDefeatGame_EndGame;
    }

    #endregion


    #region Public methods

    public void GoToNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        if (nextSceneIndex < sceneCount)
        {
            SceneManager.LoadScene(nextSceneIndex);

            return;
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ContinueGame()
    {
        OnContinueButtonClicked?.Invoke();
    }

    public void ExitToMainMenu()
    {
        UiManager.Instance.SetStartViewActive();
        OnExitButtonClicked?.Invoke();

        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

    #endregion


    #region Event handlers

    public void OnDefeatGame_EndGame()
    {
        SceneManager.LoadScene(10);
    }

    #endregion
}
