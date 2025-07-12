using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float baseSpeed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Rigidbody2D rb;

    [Header("Input Actions")]
    [SerializeField] private InputAction movementAction;

    [Header("Jump Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.1f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Game Canvas")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalCoinCountText;
    [SerializeField] private ScorBoard scorBoard;

    private BoxCollider2D boxCollider;
    private float movementInput;
    private bool jumpRequested;
    private float currentSpeed;

    // Public read-only properties
    public float CurrentSpeed => currentSpeed;
    public float BaseSpeed => baseSpeed;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();

        if (groundLayer == 0)
            groundLayer = LayerMask.GetMask("Ground");

        if (animator == null)
            animator = GetComponent<Animator>();

        if (gameOverPanel == null)
            Debug.LogWarning("[Awake] GameOverPanel is not assigned in the Inspector.", this);

        if (finalCoinCountText == null)
            Debug.LogWarning("[Awake] FinalCoinCountText is not assigned in the Inspector.", this);

        if (scorBoard == null)
            Debug.LogWarning("[Awake] ScorBoard is not assigned in the Inspector.", this);

        currentSpeed = baseSpeed; // Initialize speed
    }

    private void OnEnable()
    {
        movementAction.Enable();
    }

    private void OnDisable()
    {
        movementAction.Disable();
    }

    private void Update()
    {
        movementInput = movementAction.ReadValue<float>();

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        float calculatedSpeed = CalculateSpeedBasedOnTime();
        Move(calculatedSpeed);

        if (jumpRequested)
        {
            Jump();
            jumpRequested = false;
        }
    }

    public float CalculateSpeedBasedOnTime()
    {
        float maxSpeed = baseSpeed * 3.5f;
        float rampUpDuration = 90f;

        float progress = Mathf.Clamp01(Time.time / rampUpDuration);
        float targetSpeed = Mathf.Lerp(baseSpeed, maxSpeed, progress);

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 2f);

        Debug.Log($"[Smooth Boost] Time: {Time.time:F2}s | Target Speed: {targetSpeed:F2} | Current Speed: {currentSpeed:F2}");
        return currentSpeed;
    }

    private void Move(float speed)
    {
        float horizontalSpeed = movementInput * speed;
        rb.linearVelocity = new Vector2(horizontalSpeed, rb.linearVelocity.y);

        if (animator != null)
        {
            bool isMoving = Mathf.Abs(horizontalSpeed) > 0.1f;
            animator.SetBool("Move", isMoving);
        }

        Debug.Log($"[Move] Speed: {speed}, Input: {movementInput}");
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("[Jump] Jumped with force: " + jumpForce);
        }
        else
        {
            Debug.Log("[Jump] Not grounded, jump ignored.");
        }
    }

    private bool IsGrounded()
    {
        Vector2 boxCenter = (Vector2)boxCollider.bounds.center + Vector2.down * groundCheckDistance;
        Vector2 boxSize = boxCollider.bounds.size;
        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundLayer);
        return hit != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            Debug.Log("[Collision] Player hit an obstacle.");
            ShowGameOverPanel();
        }
    }

    private void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
            rb.linearVelocity = Vector2.zero;

            if (finalCoinCountText != null && scorBoard != null)
                finalCoinCountText.text = scorBoard.CoinCount.ToString();

            Debug.Log("[GameOver] Game Over Panel displayed.");
        }
        else
        {
            Debug.LogError("[GameOver] Game Over Panel is not assigned.");
        }
    }
}
