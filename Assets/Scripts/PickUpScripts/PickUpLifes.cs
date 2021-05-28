using UnityEngine;

public class PickUpLifes : BasePickUp
{
    #region Variables

    [SerializeField] private bool isGainingLife;

    #endregion


    #region Private methods

    protected override void ApplyEffect()
    {
        if (isGainingLife)
        {
            LivesManager.Instance.AddLife();
        }
        else
        {
            LivesManager.Instance.LoseLife();
        }
    }

    #endregion
}
