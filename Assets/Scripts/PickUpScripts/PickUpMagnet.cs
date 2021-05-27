using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMagnet : BasePickUp
{
    #region Variables


    #endregion


    #region Private methods


    protected override void ApplyEffect()
    {
        BallBehaviour ball = FindObjectOfType<BallBehaviour>();
        ball.MakeBallMagnetic();
    }

    #endregion

}
