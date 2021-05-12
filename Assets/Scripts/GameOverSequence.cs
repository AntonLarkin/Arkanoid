using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameOverSequence : MonoBehaviour
{
    #region Variables

    private Blocks[] baseBlocksArr;

    private BallBehaviour ballBehaviour;
    private GameManager gameManager;

    #endregion


    #region Events

    public static event Action OnReload;
    public static event Action OnReloadShowScore;

    #endregion


    #region Unity lifecycles
    private void Start()
    {
        baseBlocksArr = FindObjectsOfType<Blocks>();
        ballBehaviour = FindObjectOfType<BallBehaviour>();
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReloadScene();
            PauseManager.Instance.ToggleFreezeScreen();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameManager.LivesCount == 0)
        {
            OnReloadShowScore?.Invoke();
            ballBehaviour.RestartBallPosition();
            PauseManager.Instance.ToggleFreezeScreen();
        }
        else
        {
            gameManager.LoseLife();
            ballBehaviour.RestartBallPosition();
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


    #region Public methods
    public void ReloadScene()
    {
        ballBehaviour.RestartBallPosition();
        ReloadBlocks();

        OnReload?.Invoke();
    }

    #endregion
}