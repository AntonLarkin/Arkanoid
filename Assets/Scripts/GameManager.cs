using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Variables

    [Header("AutoPlay")]
    [SerializeField] private bool isAutoPlay;

    private int score;

    #endregion


    #region Propertiess

    public bool IsAutoPlay => isAutoPlay;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        GameOverSequence.OnReload += ReloadScore;
        GameOverSequence.OnReloadShowScore += ShowFinalScore;
        Blocks.OnDestroyed += Blocks_OnDestroyed;
    }

    private void OnDisable()
    {
        GameOverSequence.OnReload -= ReloadScore;
        GameOverSequence.OnReloadShowScore -= ShowFinalScore;
        Blocks.OnDestroyed -= Blocks_OnDestroyed;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        UiManager.Instance.SetGameOverViewActive(false);
    //     }
    //}

    #endregion


    #region Private methods

    private void AddScore(int score)
    {
        this.score += score;
        UiManager.Instance.UpdateScoreLabel(this.score);
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
        UiManager.Instance.UpdateScoreLabel(score);
    }

    private void ShowFinalScore()
    {
        UiManager.Instance.SetGameOverViewActive(true);
        UiManager.Instance.UpdateFinalScoreLabel(score);
    }

    #endregion
}
