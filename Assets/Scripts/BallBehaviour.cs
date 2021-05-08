using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{

    #region Variables

    [Header("Base settings")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Random direction")]
    [SerializeField] private float speed;
    [SerializeField] private float startPositionY;
    [SerializeField] private float startDirectionY;

    [Range(-5,0)]
    [SerializeField] private float minValueX;

    [Range(0, 5)]
    [SerializeField] private float maxValueX;

    private Transform padTransform;
    private bool isLaunched;

    #endregion


    #region Properties

    public bool IsLaunched { get; set; }

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        padTransform = FindObjectOfType<PadBehaviour>().transform;
    }
    private void Update()
    {
        if (!IsLaunched)
        {
            UpdateBallPosition();

            if (Input.GetMouseButtonDown(0))
            {
                LaunchBall();
            }
        }
    }

    #endregion


    #region Private methods

    private void LaunchBall()
    {
        rb.velocity = GetRandomDirection();
        IsLaunched = true;
    }

    private Vector2 GetRandomDirection()
    {
        float xPosition = Random.Range(minValueX, maxValueX);
        Vector2 direction = new Vector2(xPosition, startDirectionY).normalized;
        Vector2 velocity = direction * speed;
        return velocity;
    }

    #endregion


    #region Public methods

    public void UpdateBallPosition()
    {
        Vector3 padPosition = padTransform.position;
        //padPosition.y = transform.position.y;
        padPosition.y = startPositionY;
        transform.position = padPosition;
    }

    #endregion

}
