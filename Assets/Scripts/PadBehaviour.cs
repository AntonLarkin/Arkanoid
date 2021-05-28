using UnityEngine;

public class PadBehaviour : MonoBehaviour
{
    #region Variables

    [Header("Movement Limit")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    private float baseMinX;
    private float baseMaxX;

    private Transform ballTransform;

    [Header("Magnet Pad")]
    private bool isMagnet;
    [SerializeField] private GameObject magnetVFxPrefab;
    private GameObject magnetVFx;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        ballTransform = FindObjectOfType<BallBehaviour>().transform;

        baseMinX = minX;
        baseMaxX = maxX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMagnet && collision.gameObject.CompareTag(Tags.Ball))
        {
            var ball = collision.gameObject.GetComponent<BallBehaviour>();
            ball.MagnitBall();
        }
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused)
        {
            return;
        }

        if (GameManager.Instance.IsAutoPlay)
        {
            Vector3 autoPlayPadPosition = ballTransform.position;
            autoPlayPadPosition.y = transform.position.y;

            autoPlayPadPosition.x = Mathf.Clamp(autoPlayPadPosition.x, minX, maxX);
            transform.position = autoPlayPadPosition;
        }
        else
        {
            MovePad();
        }
    }

    #endregion


    #region Public methods

    public void ScalePadWidth(float widthModifier, float borderDifference)
    {
        Vector3 scaledPad = new Vector3(widthModifier, 1, 1);
        transform.localScale = scaledPad;
        minX -= borderDifference;
        maxX += borderDifference;
    }

    public void ReloadPad()
    {
        transform.localScale = new Vector3(1, 1, 1);
        minX = baseMinX;
        maxX = baseMaxX;
    }

    public void MakeMagnetPad(float duration)
    {
        isMagnet = true;

        if (magnetVFx == null)
        {
            magnetVFx = Instantiate(magnetVFxPrefab, transform);
        }

        Invoke(nameof(MakePadNormal), duration);
    }

    public void MakePadNormal()
    {
        isMagnet = false;
        if (magnetVFx != null)
        {
            Destroy(magnetVFx);
        }
    }

    #endregion


    #region Private methods

    private void MovePad()
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
