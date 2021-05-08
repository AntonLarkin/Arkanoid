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

    #endregion

    #region Unity lifecycle
    private void Start()
    {
        ballTransform = FindObjectOfType<BallBehaviour>().transform;
    }
    private void Update()
    {
        Vector3 padPosition = Vector3.zero;

        Vector3 positionInPixels = Input.mousePosition;
        Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(positionInPixels);

        padPosition = positionInWorld;
        padPosition.y = transform.position.y;
        padPosition.z = transform.position.z;

        padPosition.x = Mathf.Clamp(padPosition.x, minX, maxX);
        transform.position = padPosition;
    }

    #endregion

}
