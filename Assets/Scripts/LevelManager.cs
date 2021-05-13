using System.Collections.Generic;
using UnityEngine;

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
        UiManager.OnExitButtonClicked += ReloadBlockCount;
        GameOverSequence.OnReload += ReloadCountOfBlocks;
        Blocks.OnDestroyed += Blocks_OnDestroyed;
        Blocks.OnCreated += Blocks_OnCreated;
    }

    private void OnDisable()
    {
        UiManager.OnExitButtonClicked -= ReloadBlockCount;
        GameOverSequence.OnReload -= ReloadCountOfBlocks;
        Blocks.OnDestroyed -= Blocks_OnDestroyed;
        Blocks.OnCreated -= Blocks_OnCreated;
    }

    #endregion


    #region Private methods

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

    private void ReloadBlockCount()
    {
        blockCount = 0;
        blocksDestroyed = 0;
    }

    #endregion
}
