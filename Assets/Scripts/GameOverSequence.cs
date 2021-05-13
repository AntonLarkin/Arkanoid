using UnityEngine;
using System;

public class GameOverSequence : MonoBehaviour
{
    #region Variables

    private Blocks[] baseBlocksArr;
    private BallBehaviour ballBehaviour;
    private GameManager gameManager;

    private bool isReadyToReload;

    #endregion


    #region Events

    public static event Action OnReload;
    public static event Action OnReloadShowScore;

    #endregion


    #region Unity lifecycles

    private void OnEnable()
    {
        UiManager.OnExitButtonClicked += ReloadScene;
    }

    private void OnDisable()
    {
        UiManager.OnExitButtonClicked -= ReloadScene;
    }

    private void Start()
    {
        baseBlocksArr = FindObjectsOfType<Blocks>();
        ballBehaviour = FindObjectOfType<BallBehaviour>();
        gameManager = GameManager.Instance;
    }

    // private void Update()                                        
    // {
    //     if (Input.GetKeyDown(KeyCode.Space)&&isReadyToReload)
    //     {
    //         ReloadScene();
    //        PauseManager.Instance.ToggleFreezeScreen();
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LivesManager.Instance.LivesCount < 0)
        {
            ballBehaviour.RestartBallPosition();
            isReadyToReload = true;

            OnReloadShowScore?.Invoke();
        }
        else
        {
            LivesManager.Instance.LoseLife();
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

    private void ReloadScene()
    {
        ballBehaviour.RestartBallPosition();
        ReloadBlocks();
        isReadyToReload = false;

        OnReload?.Invoke();
    }

    #endregion

}