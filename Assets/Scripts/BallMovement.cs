using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    public Button startButton; // Assign the button from the Inspector
    public Transform initialPos;
    public Rigidbody ballRigidbody; // Assign the Rigidbody of the ball
    public float forceAmount = 500f; // Force to be applied upon collision
    public string targetTag = "ground"; // Tag of the BoxCollider
    private bool gravityEnabled = false; // To track if gravity is active

    [Header("Speed Boost Settings")]
    public string boostTag = "speedBoost";       // Tag for speed boost triggers
    public float speedBoostMultiplier = 5f;     // How much to multiply the current speed
    public float boostCooldown = 0.3f;          // Time before another boost can be applied
    public float minimumSpeed = 2f;             // Minimum speed to avoid stopping

    // Speed boost tracking
    private bool canBoost = true;
    private float boostCooldownTimer = 0f;
    private Vector3 currentMoveDirection;

    private void Start()
    {
        // Disable gravity initially
        ballRigidbody.useGravity = false;

        // Add listener to the button
        startButton.onClick.AddListener(EnableGravity);
    }

    public void ResetBallPosition()
    {
        // Reset the ball's position
        ballRigidbody.velocity = Vector3.zero; // Stop all movement
        ballRigidbody.angularVelocity = Vector3.zero; // Stop all rotation
        ballRigidbody.useGravity = false; // Disable gravity
        gravityEnabled = false; // Reset gravity state

        // Reset the position to the initial position
        ballRigidbody.transform.position = initialPos.position;
        Debug.Log("Ball position reset!");
    }

    public void EnableGravity()
    {
        // Enable gravity for the ball
        ballRigidbody.useGravity = true;
        gravityEnabled = true;
        Debug.Log("Gravity enabled!");
    }

    private void Update()
    {
        // Handle boost cooldown
        if (!canBoost)
        {
            boostCooldownTimer -= Time.deltaTime;
            if (boostCooldownTimer <= 0f)
            {
                canBoost = true;
                boostCooldownTimer = 0f;
                Debug.Log("Boost ready!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if this is a boost trigger and we can apply boost
        if (other.CompareTag(boostTag) && canBoost && gravityEnabled)
        {
            ApplySpeedBoost();
        }
    }

    private void ApplySpeedBoost()
    {
        // Ensure the ball has a minimum speed
        if (ballRigidbody.velocity.magnitude < minimumSpeed)
        {
            // Set a base velocity in the current forward direction
            currentMoveDirection = transform.forward; // Default direction if velocity is zero
            ballRigidbody.velocity = currentMoveDirection * minimumSpeed;
            Debug.Log("Boost activated from low speed!");
        }

        // Calculate the current direction from the velocity
        currentMoveDirection = ballRigidbody.velocity.normalized;

        // Calculate new velocity maintaining direction but increasing speed
        float newSpeed = ballRigidbody.velocity.magnitude * speedBoostMultiplier;

        // Apply the boosted velocity
        ballRigidbody.velocity = currentMoveDirection * newSpeed;

        // Start cooldown
        canBoost = false;
        boostCooldownTimer = boostCooldown;

        Debug.Log($"Speed Boost Applied! New Speed: {newSpeed}");
    }
}
