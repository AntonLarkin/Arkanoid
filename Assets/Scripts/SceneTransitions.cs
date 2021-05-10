using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneTransitions
{

    #region Public methods

    public static void GoToNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        if (nextSceneIndex < sceneCount)
        {
            SceneManager.LoadScene(nextSceneIndex);

            return;
        }
    }

    public static void GoToStartScene()
    {
        SceneManager.LoadScene(0);
    }

    #endregion
}