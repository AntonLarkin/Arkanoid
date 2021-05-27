using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpeed : BasePickUp
{
    #region Variables

    [SerializeField] private float speedModifier;

    #endregion


    #region Private methods

    protected override void ApplyEffect()
    {
        var ball = FindObjectOfType<BallBehaviour>();
        ball.ChangeSpeed(speedModifier);
    }

    #endregion
}
