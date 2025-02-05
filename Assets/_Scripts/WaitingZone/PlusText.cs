using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlusText : MonoBehaviour
{
    public int CurrentValue { get; private set; }

    [SerializeField] private float _activityTime;
    private float _activityTimer;
    
    private RectTransform _rectTransform;
    private TextMeshProUGUI _plusText;

    private Vector3 _initialPosition;
    [SerializeField] private float _yOffsetMovement;
    [SerializeField] private float _upMotionDuration;

    [SerializeField] private float _scaleFactor;
    [SerializeField] private float _scaleDuration;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _plusText = GetComponent<TextMeshProUGUI>();
        _initialPosition = _rectTransform.position;
    }

    private void OnDisable()
    {
        CurrentValue = 0;
        _rectTransform.position = _initialPosition;
    }

    private void Update()
    {
        _activityTimer -= Time.deltaTime;
        
        if (_activityTimer <= 0)
            gameObject.SetActive(false);
    }

    public void AddPassenger()
    {
        CurrentValue++;
        _plusText.text = "+" + CurrentValue;
        _rectTransform.position = _initialPosition;
        StartCoroutine(CoroutineUtils.EaseInOutMoveUI(_rectTransform,
            _rectTransform.anchoredPosition + Vector2.up * _yOffsetMovement, _upMotionDuration));
        StartCoroutine(CoroutineUtils.BouncyScale(_rectTransform, _scaleFactor, _scaleDuration));
        _activityTimer = _activityTime;
    }
    
}
