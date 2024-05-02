using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private PlayerInput playerInput;

    private InputAction touchPositionAction;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        touchPositionAction = playerInput.actions.FindAction("TouchPosition");
    }

    private void OnEnable() {
        touchPositionAction.performed += TouchPressed;
    }

    private void OnDisable() {
        touchPositionAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context) {
        Vector3 position = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        position.z = player.transform.position.z;
        player.transform.position = position;
    }
}