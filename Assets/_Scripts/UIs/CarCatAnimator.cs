using System;
using UnityEngine;

public class CarCatAnimator : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    
    private void OnEnable()
    {
        InputManager.Instance.OnStartTouch += TryAnimatingCar;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnStartTouch -= TryAnimatingCar;
    }

    private void Start()
    {
        StartCoroutine(CoroutineUtils.FloatObject(transform, 1f, 2f, 0.2f));
    }

    public void TryAnimatingCar(Vector2 screenPos, float time)
    {
        Debug.Log(screenPos);
        Ray ray = _mainCamera.ScreenPointToRay(screenPos);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 10f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                _animator.SetTrigger("TrCar");
            }
        }
    }
}
