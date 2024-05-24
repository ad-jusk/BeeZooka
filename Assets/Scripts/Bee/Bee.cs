using System;
using UnityEngine;

public class Bee : MonoBehaviour
{
    [SerializeField]
    private float minimumSwipeDistance = 1f;

    [SerializeField]
    private float maximumSwipeTime = 1f;

    [SerializeField]
    private float swipeStrength = 450f;

    [SerializeField]
    private Vector2 startPosition = new(0, -3);

    private Rigidbody2D rigidBody;

    [SerializeField]
    public Vector2 velocity;
    private BeeSwipeHandler swipeHandler;
    private GameManager gameManager;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        swipeHandler = BeeSwipeHandler.Instance;
        gameManager = GameManager.Instance;
        SetSpriteSwipeable(true);
        ResetPosition();
    }

    private void OnEnable()
    {
        swipeHandler.Initialize(rigidBody, minimumSwipeDistance, maximumSwipeTime, swipeStrength);

        gameManager.OnBeehiveMissed += HandleOnBeehiveMissed;
        gameManager.OnObstacleEntered += HandleObstacleEntered;
        gameManager.OnPauseButtonClicked += HandlePauseButtonClicked;
        gameManager.OnResumeButtonClicked += HandleResumeButtonClicked;
    }

    private void SetSpriteSwipeable(bool isSwipeable)
    {
        swipeHandler.IsSwipeable = isSwipeable;
    }

    private void HandleOnBeehiveMissed()
    {
        SetSpriteSwipeable(false);
    }

    private void HandleObstacleEntered()
    {
        SetSpriteSwipeable(false);
    }

    private void HandlePauseButtonClicked()
    {
        SetSpriteSwipeable(false);
    }

    private void HandleResumeButtonClicked()
    {
        SetSpriteSwipeable(true);
    }

    private void OnDisable()
    {
        swipeHandler.Dispose();
    }

    private void FixedUpdate()
    {
        // so far rotating it in fixedupdate works best. Tried rotating on collision but it didn't work well
        velocity = rigidBody.velocity;
        if (rigidBody.velocity.magnitude > 0.1f)
        {
            RotateBee(rigidBody.velocity);
        }
        else
        {
            rigidBody.angularVelocity = 0f; // Reset angular velocity
        }
    }

    private void ResetPosition()
    {
        rigidBody.velocity = Vector2.zero;
        transform.position = new(startPosition.x, startPosition.y, transform.position.z);
        ResetRotation();
    }

    private void ResetRotation()
    {
        rigidBody.rotation = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rigidBody.angularVelocity = 0f;
    }

    private void RotateBee(Vector2 direction)
    {
        if (direction.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rigidBody.rotation = angle - 90;
        }
    }
}
