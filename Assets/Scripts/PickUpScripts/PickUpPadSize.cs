using UnityEngine;

public class PickUpPadSize : BasePickUp
{
    #region Variables

    [SerializeField] private float scalePositionX;
    [SerializeField] private float bordersDifference;

    #endregion


    #region Private methods

    protected override void ApplyEffect()
    {
        PadBehaviour pad = FindObjectOfType<PadBehaviour>();

        if (pad.transform.localScale.x < 1 && scalePositionX > 1)
        {
            pad.ReloadPad();
        }
        else if (pad.transform.localScale.x > 1 && scalePositionX < 1)
        {
            pad.ReloadPad();
        }
        else
        {
            pad.ScalePadWidth(scalePositionX, bordersDifference);
        }
    }

    #endregion
}
