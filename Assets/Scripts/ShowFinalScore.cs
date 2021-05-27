using UnityEngine;
using UnityEngine.UI;

public class ShowFinalScore : MonoBehaviour
{

    #region Variables

    [SerializeField] private Text finalScore;
    private GameManager gameManager;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        finalScore.text = gameManager.Score.ToString();
    }

    #endregion

}
