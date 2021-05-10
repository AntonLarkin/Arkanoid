using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    #region Variables

    private List<GameObject> baseBlocks;

    private int blockCount;
    private int blocksDestroyed;

    #endregion


    #region Unity lifecycle
    private void OnEnable()
    {
        GameOverSequence.OnReload += ReloadCountOfBlocks;
        Blocks.OnDestroyed += Blocks_OnDestroyed;
        Blocks.OnCreated += Blocks_OnCreated;
    }
    private void OnDisable()
    {
        GameOverSequence.OnReload -= ReloadCountOfBlocks;
        Blocks.OnDestroyed -= Blocks_OnDestroyed;
        Blocks.OnCreated -= Blocks_OnCreated;
    }

    private void Update()
    {

    }

    #endregion


    #region Public methods

    private void BlockDestroyed()
    {
        blockCount--;
        blocksDestroyed++;
        print(blockCount);
        if (blockCount <= 0)
        {
            SceneTransitions.GoToNextScene();
            blocksDestroyed = 0;
        }
    }

    private void BlockCreated()
    {
        blockCount++;
        Debug.Log(blockCount);
    }

    #endregion

    #region Event handlers

    private void Blocks_OnDestroyed(int score)
    {
        BlockDestroyed();
    }

    private void Blocks_OnCreated()
    {

        BlockCreated();
    }

    private void ReloadCountOfBlocks()
    {
        blockCount += blocksDestroyed;
        blocksDestroyed = 0;
    }



    #endregion
}
