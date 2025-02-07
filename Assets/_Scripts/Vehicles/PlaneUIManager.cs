using System;
using UnityEngine;

public class PlaneUIManager : MonoBehaviour
{
    public static PlaneUIManager Instance;

    [SerializeField] private GameObject _planeUIPrefab;
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private Vector2 _endPos;
    [SerializeField] private float _duration;
    [SerializeField] private Transform _planeUIParent;
    private float _lifeTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;
    }

    public void CreatePlaneUI()
    {
        GameObject plane = Instantiate(_planeUIPrefab, _planeUIParent);

        RectTransform rectTransform = plane.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = _startPos;

        StartCoroutine(CoroutineUtils.MoveRectTransform(rectTransform, _startPos, _endPos, _duration));
    }
}
