using UnityEngine;

public class PickUpMagnet : BasePickUp
{
    #region Private methods

    protected override void ApplyEffect()
    {
        PadBehaviour pad = FindObjectOfType<PadBehaviour>();

        if (pad != null)
        {
            pad.MakeMagnetPad(duration);
        }
    }

    #endregion

}
