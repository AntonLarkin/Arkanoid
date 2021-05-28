using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Variables

    [Header("AutoPlay")]
    [SerializeField] private bool isAutoPlay;

    #endregion


    #region Propertiess

    public int Score { get; private set; }
    public bool IsAutoPlay => isAutoPlay;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        SceneLoader.OnExitButtonClicked += ReloadScore;
        GameOverSequence.OnReload += ReloadScore;
        Blocks.OnDestroyed += Blocks_OnDestroyed;
    }

    private void OnDisable()
    {
        SceneLoader.OnExitButtonClicked -= ReloadScore;
        GameOverSequence.OnReload -= ReloadScore;
        Blocks.OnDestroyed -= Blocks_OnDestroyed;
    }

    #endregion


    #region Public methods

    public void AddScore(int score)
    {
        if (score < 0 && -score > Score)
        {
            ReloadScore();
        }
        else
        {
            this.Score += score;
            UiManager.Instance.UpdateScoreLabel(Score);
        }
    }

    #endregion


    #region Event handler

    private void Blocks_OnDestroyed(int score)
    {
        AddScore(score);
    }

    private void ReloadScore()
    {
        Score = 0;
        UiManager.Instance.UpdateScoreLabel(Score);
    }

    #endregion
}
