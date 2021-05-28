using UnityEngine;
using System;

public class GameOverSequence : MonoBehaviour
{
    #region Variables

    private Blocks[] baseBlocksArr;
    private BallBehaviour ballBehaviour;
    private PadBehaviour pad;
    private GameManager gameManager;

    private bool isReadyToReload;

    #endregion


    #region Events

    public static event Action OnReload;
    public static event Action OnDefeatEndGame;

    #endregion


    #region Unity lifecycles

    private void OnEnable()
    {
        SceneLoader.OnExitButtonClicked += OnExitButtonClicked_ReloadScene;
    }

    private void OnDisable()
    {
        SceneLoader.OnExitButtonClicked -= OnExitButtonClicked_ReloadScene;
    }

    private void Start()
    {
        baseBlocksArr = FindObjectsOfType<Blocks>();
        pad = FindObjectOfType<PadBehaviour>();
        ballBehaviour = FindObjectOfType<BallBehaviour>();
        gameManager = GameManager.Instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        LivesManager.Instance.LoseLife();

        if (LivesManager.Instance.LivesCount == 0)
        {
            ballBehaviour.RestartBall();
            ballBehaviour.ReloadBallSize();
            isReadyToReload = true;

            OnDefeatEndGame?.Invoke();
        }
        else
        {
            pad.ReloadPad();
            pad.MakePadNormal();
            ballBehaviour.RestartBall();
        }
    }

    #endregion


    #region Private methods

    private void ReloadBlocks()
    {
        for (int i = 0; i < baseBlocksArr.Length; i++)
        {
            baseBlocksArr[i].ResetBlocks();
        }
    }

    #endregion


    #region Event handlers

    private void OnExitButtonClicked_ReloadScene()
    {
        ballBehaviour.RestartBall();
        ReloadBlocks();
        isReadyToReload = false;

        OnReload?.Invoke();
    }

    #endregion
}