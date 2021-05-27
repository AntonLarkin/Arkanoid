using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBallSize : BasePickUp
{
    #region Variables

    [SerializeField] private float sizeModifier; 

    #endregion


    #region Private Methods

    protected override void ApplyEffect()
    {
        BallBehaviour ball = FindObjectOfType<BallBehaviour>();
        if (ball.transform.localScale.x < 1 && sizeModifier > 1)
        {
            ball.ReloadBallSize();
        }
        else if(ball.transform.localScale.x>1 && sizeModifier < 1)
        {
            ball.ReloadBallSize();
        }
        else
        {
            ball.ChangeSize(sizeModifier);
        }
    }

    #endregion
}
