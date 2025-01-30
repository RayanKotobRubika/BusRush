using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleUI : MonoBehaviour
{
    //[field:SerializeField] public Obstacle RelatedObstacle { get; private set; }
    [field:SerializeField] public GameObject ObstaclePreview { get; private set; }
    
    private GameObject _obstaclePreview;
    private Camera _mainCamera;
    
    private RectTransform _rectTransform;
    private Image _image;
    private bool _isDragged = false;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        InputManager.Instance.OnStartHold += DragObstaclePreview;
        InputManager.Instance.OnEndHold += DropObstaclePreview;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnStartHold -= DragObstaclePreview;
        InputManager.Instance.OnEndHold -= DropObstaclePreview;
    }

    private void Start()
    {
        _mainCamera = GameManager.Instance.MainCamera;
    }

    private void Update()
    {
        if (_isDragged && _obstaclePreview != null)
        {
            _obstaclePreview.transform.position = GetPreviewPosition(InputManager.Instance.CurrentFingerPosition);
        }
    }

    public void DragObstaclePreview(Vector2 screenPos, float time)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, screenPos, _mainCamera, out Vector2 localPoint);

        if (!_rectTransform.rect.Contains(localPoint)) return;
        Debug.Log("Collided !");
        _isDragged = true;
        
        _image.enabled = false;
        
        _obstaclePreview = Instantiate(ObstaclePreview, GetPreviewPosition(InputManager.Instance.CurrentFingerPosition), Quaternion.identity);
    }

    public void DropObstaclePreview(Vector2 screenPos, float time)
    {
        if (!_isDragged) return;
        
        _isDragged = false;
        
        _image.enabled = true;
    }
    
    public Vector3 GetPreviewPosition(Vector2 screenPosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(screenPosition);
        return Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity) ? hit.point : new Vector3();
    }
}
