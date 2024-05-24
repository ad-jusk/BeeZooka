using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    #region Events

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    public delegate void EscapePressed();
    public event EscapePressed OnEscapePressed;

    #endregion

    private TouchControls touchControls;
    private Camera mainCamera;

    private void Awake()
    {
        touchControls = new TouchControls();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        touchControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        touchControls.Touch.EscapeKeyPressed.performed += ctx => HandleEscapePressed(ctx);
    }

    /*
      CHANGES HERE
        USING PrimaryContact to get the start position
     */
    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnStartTouch?.Invoke(CameraUtils.ScreenToWorld(mainCamera, touchControls.Touch.PrimaryContact.ReadValue<Vector2>()), (float)ctx.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnEndTouch?.Invoke(CameraUtils.ScreenToWorld(mainCamera, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.time);
    }

    private void HandleEscapePressed(InputAction.CallbackContext ctx)
    {
        OnEscapePressed?.Invoke();
    }
}
