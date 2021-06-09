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

    [Range(-5, 0)]
    [SerializeField] private float minValueX;

    [Range(0, 5)]
    [SerializeField] private float maxValueX;

    [Header("Speed")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;

    private Transform padTransform;
    private bool isLaunched;

    private Vector2 padOffset;

    [Header("Audio")]
    [SerializeField] private AudioClip collisionAudioClip;

    [Header("Explosion")]
    [SerializeField] private Sprite explosiveBall;
    [SerializeField] private Gradient explosiveTrail;
    private bool isExplosive;
    private float explosionRadius;
    private GameObject explosionFX;
    private Blocks hitedBlock;

    private Gradient baseTrail;
    private Sprite baseSprite;

    #endregion


    #region Properties

    public bool IsLaunched { get; private set; }

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        padTransform = FindObjectOfType<PadBehaviour>().transform;

        CalculatePadOffset();
    }

    private void Start()
    {
        baseTrail = gameObject.GetComponent<TrailRenderer>().colorGradient;
        baseSprite = gameObject.GetComponent<SpriteRenderer>().sprite;

        if (NeedLaunchBall())
        {
            LaunchBall();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Block))
        {
            SFxAudioSource.Instance.PlaySfx(collisionAudioClip);

            if (isExplosive)
            {
                ExplodeBlocks();
                Instantiate(explosionFX, transform.position, Quaternion.identity);
            }
        }
    }

    private void Update()
    {
        if (!IsLaunched)
        {
            UpdateBallPosition();

            if (NeedLaunchBall())
            {
                LaunchBall();
            }
        }
    }

    #endregion


    #region Public methods

    public void UpdateBallPosition()
    {
        Vector2 padPosition = padTransform.position;
        padPosition -= padOffset;
        transform.position = padPosition;
    }

    public void RestartBall()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = baseSprite;
        gameObject.GetComponent<TrailRenderer>().colorGradient = baseTrail;
        ReloadBallSize();
        IsLaunched = false;
        isExplosive = false;
        UpdateBallPosition();
    }

    public void ReloadBallSize()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void ChangeSpeed(float speedFactor)
    {
        var newVelocityLength = Mathf.Clamp(rb.velocity.magnitude * speedFactor, minSpeed, maxSpeed);
        rb.velocity = rb.velocity.normalized * newVelocityLength;
    }

    public void ChangeSize(float sizeModifier)
    {
        transform.localScale = new Vector3(sizeModifier, sizeModifier, sizeModifier);
    }

    public void MagnitBall()
    {
        IsLaunched = false;

        CalculatePadOffset();
    }

    public void MakeBallExplosive(GameObject explosionFX)
    {
        isExplosive = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = explosiveBall;
        gameObject.GetComponent<TrailRenderer>().colorGradient = explosiveTrail;
        this.explosionFX = explosionFX;
    }

    public void SetExplosionRadius(float explosionRadius)
    {
        this.explosionRadius = explosionRadius;
    }

    #endregion


    #region Private methods

    private bool NeedLaunchBall()
    {
        return Input.GetMouseButtonDown(0) || GameManager.Instance.IsAutoPlay;
    }

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

    private void ExplodeBlocks()
    {
        var blocksInRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D blockInRadius in blocksInRadius)
        {
            if (blockInRadius.CompareTag(Tags.Block))
            {
                hitedBlock = blockInRadius.GetComponent<Blocks>();
                hitedBlock.IsHit = true;
            }
        }
    }

    private void CalculatePadOffset()
    {
        padOffset = padTransform.position - transform.position;
    }

    #endregion
}
