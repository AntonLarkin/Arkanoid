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
        SceneLoader.OnExitButtonClicked += OnExitButtonClicked_ReloadBlockCount;
        GameOverSequence.OnReload += OnReload_ReloadCountOfBlocks;
        Blocks.OnDestroyed += Blocks_OnDestroyed;
        Blocks.OnCreated += Blocks_OnCreated;
    }

    private void OnDisable()
    {
        SceneLoader.OnExitButtonClicked -= OnExitButtonClicked_ReloadBlockCount;
        GameOverSequence.OnReload -= OnReload_ReloadCountOfBlocks;
        Blocks.OnDestroyed -= Blocks_OnDestroyed;
        Blocks.OnCreated -= Blocks_OnCreated;
    }

    #endregion


    #region Private methods

    private void BlockDestroyed()
    {
        blockCount--;
        blocksDestroyed++;

        if (blockCount <= 0)
        {
            Invoke(nameof(LoadNextScene), 0.2f);
            blocksDestroyed = 0;
        }
    }

    private void LoadNextScene()
    {
        SceneLoader.Instance.GoToNextScene();
    }

    private void BlockCreated()
    {
        blockCount++;
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

    private void OnReload_ReloadCountOfBlocks()
    {
        blockCount += blocksDestroyed;
        blocksDestroyed = 0;
    }

    private void OnExitButtonClicked_ReloadBlockCount()
    {
        blockCount = 0;
        blocksDestroyed = 0;
    }

    #endregion
}
