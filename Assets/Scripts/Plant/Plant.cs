using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private float minimumSwipeDistance = 1f;
    [SerializeField]
    private float maximumSwipeTime = 1f;
    [SerializeField]
    private float swipeStrength = 270f;

    private Rigidbody2D rigidBody;
    private PlantSwipeHandler swipeHandler;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        swipeHandler = PlantSwipeHandler.Instance;
    }

    private void OnEnable() {
        swipeHandler.Initialize(rigidBody, minimumSwipeDistance, maximumSwipeTime, swipeStrength);
    }

    private void OnDisable() {
        swipeHandler.Dispose();
    }
}
