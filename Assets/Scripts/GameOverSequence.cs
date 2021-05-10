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
    }
    private void Update()
    {
        gameManager = FindObjectOfType<GameManager>();      //некрасивый костыль, но работает..

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            ReloadScene();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (gameManager.LivesCount == 0)
        {
            OnReloadShowScore?.Invoke();
        }
        else
        {
            gameManager.LoseLife();
            ballBehaviour.IsLaunched = false;
            ballBehaviour.UpdateBallPosition();
        }
    }

    #endregion


    #region Private methods
    private void ReloadBlocks()
    {
        for (int i = 0; i < baseBlocksArr.Length; i++)
        {
            baseBlocksArr[i].gameObject.SetActive(true);
        }
    }

    #endregion


    #region Public methods
    public void ReloadScene()
    {
        OnReload?.Invoke();
        ReloadBlocks();
        ballBehaviour.IsLaunched = false;
        ballBehaviour.UpdateBallPosition();
    }

    #endregion
}