using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private PlayerInput playerInput;

    private InputAction touchPositionAction;
    private InputAction escapeKeyPressedAction; // ALSO WORKS FOR ANDROID

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        touchPositionAction = playerInput.actions.FindAction("TouchPosition");
        escapeKeyPressedAction = playerInput.actions.FindAction("EscapeKey");
    }

    private void OnEnable() {
        touchPositionAction.performed += TouchPressed;
        escapeKeyPressedAction.performed += EscapeKeyPressed;
    }

    private void OnDisable() {
        touchPositionAction.performed -= TouchPressed;
        escapeKeyPressedAction.performed -= EscapeKeyPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context) {
        Vector3 position = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        position.z = player.transform.position.z;
        player.transform.position = position;
    }

    private void EscapeKeyPressed(InputAction.CallbackContext context) {
        
        const string sceneName = "StartScene";

        if(!SceneManagerScript.GetCurrentSceneName().Equals(sceneName)) {
            SceneManagerScript.LoadScene(sceneName);
        }
    }
}