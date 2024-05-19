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
    private BeeSwipeHandler swipeHandler;
    private GameManager gameEventsManager;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        swipeHandler = BeeSwipeHandler.Instance;
        gameEventsManager = GameManager.Instance;
        ResetPosition();
    }

    private void OnEnable() {
        swipeHandler.Initialize(rigidBody, minimumSwipeDistance, maximumSwipeTime, swipeStrength);
        gameEventsManager.OnBeehiveMissed += ResetPosition;
    }

    private void OnDisable() {
        swipeHandler.Dispose();
    }
    private void FixedUpdate()
    {
        // so far rotating it in fixedupdate works best. Tried rotating on collision but it didn't work well
        if (rigidBody.velocity != Vector2.zero)
            RotateBee(rigidBody.velocity);
        else
            ResetRotation();
    }

    private void ResetPosition() {
        rigidBody.velocity = Vector2.zero;
        transform.position = new(startPosition.x, startPosition.y, transform.position.z);
        ResetRotation();
    }

    private void ResetRotation()
    {
        rigidBody.rotation = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        RotateBee(Vector2.zero);
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
