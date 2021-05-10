using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadBehaviour : MonoBehaviour
{
    #region Variables

    [Header("Movement Limit")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    private Transform ballTransform;
    private BallBehaviour ball;

    private Vector3 padPosition = Vector3.zero;
    #endregion

    #region Unity lifecycle
    private void Start()
    {
        ballTransform = FindObjectOfType<BallBehaviour>().transform;
        ball = FindObjectOfType<BallBehaviour>();
    }
    private void Update()
    {
        if (GameManager.Instance.IsAutoPlay)
        {
            Vector3 autoPlayPadPosition = ball.transform.position;
            autoPlayPadPosition.y = transform.position.y;

            autoPlayPadPosition.x = Mathf.Clamp(autoPlayPadPosition.x, minX, maxX);
            transform.position = autoPlayPadPosition;
        }
        else
        {
            MovePad(PauseManager.Instance.IsPaused);
        }
    }

    #endregion

    private void MovePad(bool isPaused)
    {
        if (!isPaused)
        {
            Vector3 positionInPixels = Input.mousePosition;
            Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(positionInPixels);

            padPosition = positionInWorld;
        }

        padPosition.y = transform.position.y;
        padPosition.z = transform.position.z;

        padPosition.x = Mathf.Clamp(padPosition.x, minX, maxX);
        transform.position = padPosition;
    }
}
