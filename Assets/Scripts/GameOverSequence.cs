using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameOverSequence : MonoBehaviour
{
    #region Variables

    private Blocks[] baseBlocksArr;
    public BallBehaviour ballBehaviour;
    private bool isReadyToReload;

    #endregion


    #region Properties

    public bool ReadyToReload { get; set; }

    #endregion


    #region Events

    public static event Action OnReload;

    #endregion

    #region Unity lifecycles
    private void Start()
    {
        baseBlocksArr = FindObjectsOfType<Blocks>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        OnReload?.Invoke();
        ReadyToReload = true;
        ballBehaviour.IsLaunched = false;
        ballBehaviour.UpdateBallPosition();
    }

    #endregion


}
