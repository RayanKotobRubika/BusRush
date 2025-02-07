using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private float _touchOrDragThreshold = 0.3f;
    private float _toucheOrDragTimer;
    
    public Vector2 CurrentFingerPosition { get; private set; }
    
    public bool TouchOngoing { get; private set; }
    public bool IsHolding { get; private set; }
    
    private Vector2 _touchStartPosition;
    private float _touchStartTime;
    
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    
    public delegate void StartHoldEvent(Vector2 position, float time);
    public event StartHoldEvent OnStartHold;
    
    public delegate void EndHoldEvent(Vector2 position, float time);
    public event EndHoldEvent OnEndHold;
    
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;
    
    private TouchControls _touchControls;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        } 
        
        Instance = this;
        
        _touchControls = new TouchControls();
        _toucheOrDragTimer = _touchOrDragThreshold;
        
        TouchOngoing = false;
        IsHolding = false;

        _touchStartPosition = new Vector2();
    }

    private void OnEnable()
    {
        _touchControls.Enable();
    }

    private void OnDisable()
    {
        _touchControls.Disable();
    }

    private void Start()
    {
        _touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        _touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void Update()
    {
        CurrentFingerPosition = _touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        
        if (TouchOngoing)
        {
            _toucheOrDragTimer -= Time.deltaTime;
            if (_toucheOrDragTimer <= 0 && !IsHolding)
            {
                IsHolding = true;
                if (OnStartHold != null) OnStartHold(_touchStartPosition, _touchStartTime + _touchOrDragThreshold);
            }
        }
        else
        {
            _toucheOrDragTimer = _touchOrDragThreshold;
        }
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        _touchStartPosition = _touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        _touchStartTime = (float)context.startTime;
        
        TouchOngoing = true;
        _toucheOrDragTimer = _touchOrDragThreshold;
        
        if (OnStartTouch != null) OnStartTouch(_touchStartPosition, _touchStartTime);
    }
    
    private void EndTouch(InputAction.CallbackContext context)
    {
        TouchOngoing = false;
        if (OnEndTouch != null) OnEndTouch(_touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
        
        if (IsHolding)
        {
            IsHolding = false;
            if (OnEndHold != null) OnEndHold(_touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
        }
        
        _toucheOrDragTimer = _touchOrDragThreshold;
    }
}
