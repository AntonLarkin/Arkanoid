using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Blocks : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject[] blocksByStages;
    [SerializeField] private int score;

    [SerializeField] private GameObject destroyParticlePrefab;

    [Header("Pick Up")]
    [SerializeField] private GameObject[] pickUpPrefabs;
    private GameObject pickUpPrefab;

    [Range(1, 100)]
    [SerializeField] private int pickUpCreationRate;

    #endregion


    #region Events 

    public static event Action OnCreated;
    public static event Action<int> OnDestroyed;

    #endregion


    #region Properties

    public int Stage { get; set; }
    public bool IsHit { get; set; }

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        GameOverSequence.OnReload += ReloadStages;
    }

    private void OnDisable()
    {
        GameOverSequence.OnReload -= ReloadStages;
    }

    private void Start()
    {
        OnCreated?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Stage == blocksByStages.Length - 1)
        {
            DestroyBlock();
            return;
        }

        blocksByStages[Stage].SetActive(false);
        Stage++;
        blocksByStages[Stage].SetActive(true);
    }

    private void Update()
    {
        if (IsHit)
        {
            DestroyBlock();
        }
    }

    #endregion


    #region Public methods

    public void ResetBlocks()
    {
        gameObject.SetActive(true);
    }

    #endregion


    #region Private methods

    private void DestroyBlock()
    {
        ReloadStages();
        gameObject.SetActive(false);

        OnDestroyed?.Invoke(score);

        Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
        if (NeedCreatePickUp())
        {
            pickUpPrefab = pickUpPrefabs[Random.Range(0, pickUpPrefabs.Length)];
            Instantiate(pickUpPrefab, transform.position, Quaternion.identity);
        }
    }

    private bool NeedCreatePickUp()
    {
        var randomNumber = Random.Range(1, 101);
        return pickUpCreationRate >= randomNumber;
    }

    #endregion


    #region Event handler

    private void ReloadStages()
    {
        Stage = 0;

        for (int i = 0; i < blocksByStages.Length; i++)
        {
            blocksByStages[i].SetActive(i == 0);
        }
    }

    #endregion
}
