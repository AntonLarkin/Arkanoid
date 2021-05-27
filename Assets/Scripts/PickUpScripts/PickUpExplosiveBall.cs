using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpExplosiveBall : BasePickUp
{
    #region Variables

    [SerializeField] private float explosionRadius;
    [SerializeField] private GameObject explosionFX;
 
    #endregion


    #region Private methods

    protected override void ApplyEffect()
    {
        BallBehaviour ball = FindObjectOfType<BallBehaviour>();
        ball.MakeBallExplosive(explosionFX);
        ball.SetExplosionRadius(explosionRadius);
    }

    #endregion

}
