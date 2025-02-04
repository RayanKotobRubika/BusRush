using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private float _lockInputTime;
    private float _lockInputTimer;
    
    [SerializeField] private GameObject _youLose;
    [SerializeField] private GameObject _rewards;

    [SerializeField] private float _targetScale;
    [SerializeField] private float _scaleOverdrive;
    [SerializeField] private float _scaleCut;
    [SerializeField] private float _scaleDuration;
    
    private void OnEnable()
    {
        StartCoroutine(CoroutineUtils.FadeInCanvaGroup(GetComponent<CanvasGroup>(), 0.1f));
        
        InputManager.Instance.OnStartTouch += ScreenTouch;
        
        StartCoroutine(CoroutineUtils.ScaleToTarget(_youLose.transform, _targetScale, _scaleOverdrive, _scaleCut,
            _scaleDuration));
        
        StartCoroutine(CoroutineUtils.ScaleToTarget(_rewards.transform, _targetScale, _scaleOverdrive, _scaleCut,
            _scaleDuration));
        
        _lockInputTimer = _lockInputTime;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnStartTouch -= ScreenTouch;
    }

    public void ScreenTouch(Vector2 screenPos, float time)
    {
        if (_lockInputTimer > 0) return;
        
        StartCoroutine(SceneHandler.Instance.LoadLevel("MainMenuScene"));
    }

    private void Update()
    {
        _lockInputTimer -= Time.deltaTime;
    }
}
