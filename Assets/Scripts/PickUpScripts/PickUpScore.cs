using UnityEngine;

public class PickUpScore : BasePickUp
{
    #region Variables

    [SerializeField] private int score;

    #endregion


    #region Private methods

    protected override void ApplyEffect()
    {
        GameManager.Instance.AddScore(score);
    }

    #endregion
}
