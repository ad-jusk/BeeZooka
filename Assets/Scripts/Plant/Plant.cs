using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private float minimumSwipeDistance = 1f;
    [SerializeField]
    private float maximumSwipeTime = 1f;
    [SerializeField]
    private float swipeStrength = 270f;
    [SerializeField]
    private Vector2 startPosition = new(0, -3);

    private Rigidbody2D rigidBody;
    private PlantSwipeHandler swipeHandler;
    private GameEventsManager gameEventsManager;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        swipeHandler = PlantSwipeHandler.Instance;
        gameEventsManager = GameEventsManager.Instance;
    }

    private void OnEnable() {
        swipeHandler.Initialize(rigidBody, minimumSwipeDistance, maximumSwipeTime, swipeStrength);
        gameEventsManager.OnNextLevel += ResetPosition;
    }

    private void OnDisable() {
        swipeHandler.Dispose();
    }

    private void ResetPosition() {
        rigidBody.velocity = Vector2.zero;
        transform.position = new(startPosition.x, startPosition.y, transform.position.z);
    }
}
