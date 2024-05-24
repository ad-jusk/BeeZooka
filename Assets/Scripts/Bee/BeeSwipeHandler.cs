using UnityEngine;

public class BeeSwipeHandler : Singleton<BeeSwipeHandler>
{
    public bool IsSwipeable = false;

    private float minimumSwipeDistance,
        maximumSwipeTime,
        swipeStrength;

    private Vector2 startPosition,
        endPosition;
    private float startTime,
        endTime;

    private InputManager inputManager;
    private Rigidbody2D rigidBody;

    public void Initialize(Rigidbody2D rigidBody, float minimumSwipeDistance, float maximumSwipeTime, float swipeStrength)
    {
        this.minimumSwipeDistance = minimumSwipeDistance;
        this.maximumSwipeTime = maximumSwipeTime;
        this.swipeStrength = swipeStrength;

        this.rigidBody = rigidBody;

        inputManager = InputManager.Instance;

        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        Debug.Log("startPosition: " + startPosition);
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        Debug.Log("endPosition: " + endPosition);
        endTime = time;
        Swipe();
    }

    private void Swipe()
    {
        float swipeDistance = Vector3.Distance(startPosition, endPosition);
        bool distanceOk = swipeDistance >= minimumSwipeDistance;
        bool timeOk = (endTime - startTime) <= maximumSwipeTime;
        bool beeNotInMotion = rigidBody.velocity.Equals(Vector2.zero);

        if (IsSwipeable && distanceOk && timeOk && beeNotInMotion)
        {
            Vector3 direction3D = endPosition - startPosition;
            Vector2 direction2D = (Vector2)direction3D.normalized;

            MovePlant(direction2D, swipeDistance);
        }
    }

    /*
        MOVES BEE IN GIVEN DIRECTION. SWIPE DISTANCE CAN BE USED TO ADJUST SWIPE FORCE.
        FOR NOW THE FORCE IS THE SAME REGARDLESS OF SWIPE DISTANCE
    */
    private void MovePlant(Vector2 direction, float swipeDistance)
    {
        rigidBody.AddForce(swipeStrength * direction);
    }

    public void Dispose()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }
}
