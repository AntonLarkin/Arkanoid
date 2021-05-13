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
        GameOverSequence.OnReload += ReloadLives;
    }

    private void OnDisable()
    {
        GameOverSequence.OnReload -= ReloadLives;
    }

    private void Start()
    {
        ReloadLives();
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
        livesKeeper[livesCount].SetActive(false);
        livesCount--;
    }

    #endregion


    #region Event handler

    private void ReloadLives()
    {
        livesCount = 2;

        for (int i = 0; i < livesKeeper.Length; i++)
        {
            livesKeeper[i].SetActive(true);
        }
    }

    private void AddLife()
    {
        if (livesCount == 1)
        {
            ReloadLives();

        }
        else if (livesCount == 0)
        {
            livesCount = 1;

            for (int i = 0; i <= livesCount; i++)
            {
                livesKeeper[i].SetActive(true);
            }
        }
        else if (livesCount == -1)
        {
            livesCount = 0;
            livesKeeper[0].SetActive(true);
        }
    }

    #endregion
}

