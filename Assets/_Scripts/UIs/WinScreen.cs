using System;
using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private float _lockInputTime;
    private float _lockInputTimer;
    
    [SerializeField] private GameObject _youWin;
    [SerializeField] private GameObject _rewards;
    [SerializeField] private TextMeshProUGUI _goNextText;

    [SerializeField] private float _targetScale;
    [SerializeField] private float _scaleOverdrive;
    [SerializeField] private float _scaleCut;
    [SerializeField] private float _scaleDuration;
    
    private void OnEnable()
    {
        StartCoroutine(CoroutineUtils.FadeInCanvaGroup(GetComponent<CanvasGroup>(), 0.1f));
        
        InputManager.Instance.OnStartTouch += ScreenTouch;
        
        StartCoroutine(CoroutineUtils.ScaleToTarget(_youWin.transform, _targetScale, _scaleOverdrive, _scaleCut,
            _scaleDuration));
        
        StartCoroutine(CoroutineUtils.ScaleToTarget(_rewards.transform, _targetScale, _scaleOverdrive, _scaleCut,
            _scaleDuration));
        
        _lockInputTimer = _lockInputTime;
        _goNextText.gameObject.SetActive(false);
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
        
        if (_lockInputTimer < 0 && !_goNextText.gameObject.activeInHierarchy)
            _goNextText.gameObject.SetActive(true);
    }
}
