﻿using UnityEngine;

public abstract class BasePickUp : MonoBehaviour
{
    [SerializeField] private int scoreBonus;

    #region Unity lifecycle
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.Pad))
        {
            ApplyEffect();
            AddScoreBonus();
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag(Tags.BottomWall))
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Private methods

    protected abstract void ApplyEffect();

    private void AddScoreBonus()
    {
        GameManager.Instance.AddScore(scoreBonus);
    }

    #endregion
}
