using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private float minimumSwipeDistance = 1f;
    [SerializeField]
    private float maximumSwipeTime = 1f;
    [SerializeField]
    private float swipeStrength = 270f;

    private InputManager inputManager;

    private Vector2 startPosition, endPosition;
    private float startTime, endTime;

    private Rigidbody2D rigidBody;

    private void Awake() {
        inputManager = InputManager.Instance;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable() {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time) {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time) {
        endPosition = position;
        endTime = time;
        Swipe();
    }

    private void Swipe() {

        float swipeDistance = Vector3.Distance(startPosition, endPosition);
        bool distanceOk = swipeDistance >= minimumSwipeDistance;
        bool timeOk = (endTime - startTime) <= maximumSwipeTime;

        if(distanceOk && timeOk) {
            Vector3 direction3D = endPosition - startPosition;
            Vector2 direction2D = (Vector2)direction3D.normalized;
            MovePlant(direction2D, swipeDistance);
        }
    }

    /*
        MOVES PLANT IN GIVEN DIRECTION. SWIPE DISTANCE CAN BE USED TO ADJUST SWIPE FORCE.
        FOR NOW THE FORCE IS THE SAME REGARDLESS OF SWIPE DISTANCE
    */
    private void MovePlant(Vector2 direction, float swipeDistance) {
        rigidBody.AddForce(swipeStrength * direction);
    }
}
