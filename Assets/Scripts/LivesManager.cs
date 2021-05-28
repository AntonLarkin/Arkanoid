using UnityEngine;

public class LivesManager : SingletonMonoBehaviour<LivesManager>
{
    #region Variables

    [SerializeField] private GameObject[] livesKeeper;

    private int livesCount;

    #endregion


    #region Properties

    public int LivesCount => livesCount;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        SceneLoader.OnExitButtonClicked += OnExitButtonClicked_ReloadLives;
    }

    private void OnDisable()
    {
        SceneLoader.OnExitButtonClicked -= OnExitButtonClicked_ReloadLives;
    }

    private void Start()
    {
        OnExitButtonClicked_ReloadLives();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AddLife();
        }
    }

    #endregion


    #region Public methods

    public void LoseLife()
    {
        livesCount--;
        livesKeeper[livesCount].SetActive(false);
    }

    public void AddLife()
    {
        if (livesCount == livesKeeper.Length)
        {
            return;
        }

        livesKeeper[livesCount].SetActive(true);
        livesCount++;
    }

    #endregion


    #region Event handler

    private void OnExitButtonClicked_ReloadLives()
    {
        livesCount = livesKeeper.Length;

        for (int i = 0; i < livesKeeper.Length; i++)
        {
            livesKeeper[i].SetActive(true);
        }
    }

    #endregion
}

