using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Blocks : MonoBehaviour
{

    #region Variables

    [SerializeField] private GameObject[] blocksByStages;

    [SerializeField] private int score;



    #endregion


    #region Properties

    public int Stage { get; set; }

    #endregion

    #region Events 

    public static event Action OnCreated;
    public static event Action <int> OnDestroyed;

    #endregion

    #region Unity lifecycle

    private void OnEnable()
    {
        GameOverSequence.OnReload += Reload;
    }
 
    private void Start()
    {
        OnCreated?.Invoke();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Stage == blocksByStages.Length - 1)
        {
            OnDestroyed?.Invoke(score);
            gameObject.SetActive(false);
            return;
        }
        blocksByStages[Stage].SetActive(false);
        Stage++;
        blocksByStages[Stage].SetActive(true);

    }

    #endregion

    #region Event handler

    private void Reload()
    {
        gameObject.SetActive(true);
        Stage = 0;

        for (int i = 0; i < blocksByStages.Length; i++)
        {
            blocksByStages[i].SetActive(true);
        }
    }

    #endregion

}
