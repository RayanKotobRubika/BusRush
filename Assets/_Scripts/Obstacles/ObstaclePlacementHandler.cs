using System;
using UnityEngine;

public class ObstaclePlacementHandler : MonoBehaviour
{
    [field:SerializeField] public GameObject ObstaclePreview { get; private set; }

    [SerializeField] private LayerMask _gridTileLayer;

    private ObstacleUI _obstacleUI;
    
    private RectTransform _rectTransform;
    
    private GameObject _obstaclePreview;
    private Camera _mainCamera;
    
    private bool _isDragged = false;
    private bool _isSnapped = false;
    
    public static event Action<Obstacle> OnObstaclePlaced;

    private void Awake()
    {
        _obstacleUI = GetComponent<ObstacleUI>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        InputManager.Instance.OnStartHold += DragObstaclePreview;
        InputManager.Instance.OnEndHold += DropObstaclePreview;

        StartCoroutine(CoroutineUtils.BouncyScale(transform, 1.15f, 0.15f, true));
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
        if (!_isDragged || _obstaclePreview == null) return;
        
        SnapPreviewOnGrid();

        _obstaclePreview.SetActive(_isSnapped);
    }
    
    public void DragObstaclePreview(Vector2 screenPos, float time)
    {
        if (CatBellManager.Instance.CatBellAmount < _obstacleUI.RelatedObstacle.CatBellCost) return;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, screenPos, _mainCamera, out Vector2 localPoint);

        if (!_rectTransform.rect.Contains(localPoint)) return;
        
        _isDragged = true;
        
        _obstacleUI.ObstacleImage.enabled = false;
        
        _obstaclePreview = Instantiate(ObstaclePreview, GetPreviewPosition(InputManager.Instance.CurrentFingerPosition), Quaternion.identity);
    }

    public void DropObstaclePreview(Vector2 screenPos, float time)
    {
        if (!_isDragged) return;
        
        _isDragged = false;
        
        _obstacleUI.ObstacleImage.enabled = true;
        
        TryPlacingObstacle();
    }
    
    private Vector3 GetPreviewPosition(Vector2 screenPosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(screenPosition);
        
        return Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity) ? hit.point : new Vector3(10000,0,0);
    }
    
    private void SnapPreviewOnGrid()
    {
        _obstaclePreview.transform.position = GetPreviewPosition(InputManager.Instance.CurrentFingerPosition);
        _obstaclePreview.transform.position += new Vector3(0, -_obstaclePreview.transform.position.y + ObstacleManager.Instance.ObstacleHeight, 0);

        Vector3 pos1 = new Vector3(
            _obstaclePreview.transform.position.x + (-_obstacleUI.RelatedObstacle.Dimensions.x + 1) / 2 * ObstacleManager.Instance.GridCellSize, 
            ObstacleManager.Instance.ObstacleHeight, 
            _obstaclePreview.transform.position.z + (-_obstacleUI.RelatedObstacle.Dimensions.y + 1) / 2 * ObstacleManager.Instance.GridCellSize
        );
        
        Vector3 pos2 = new Vector3(
            _obstaclePreview.transform.position.x + (_obstacleUI.RelatedObstacle.Dimensions.x - 1) / 2 * ObstacleManager.Instance.GridCellSize, 
            ObstacleManager.Instance.ObstacleHeight, 
            _obstaclePreview.transform.position.z + (_obstacleUI.RelatedObstacle.Dimensions.y - 1) / 2 * ObstacleManager.Instance.GridCellSize
        );

        _isSnapped = false;

        RaycastHit[] hits = new RaycastHit[1];

        if (Physics.RaycastNonAlloc(pos1 + Vector3.up, Vector3.down, hits, 10f, _gridTileLayer) == 0) return;
        if (Physics.RaycastNonAlloc(pos2 + Vector3.up, Vector3.down, hits, 10f, _gridTileLayer) == 0) return;
        
        _obstaclePreview.transform.position = new Vector3(
            hits[0].transform.position.x + (_obstacleUI.RelatedObstacle.Dimensions.x - 1) / 2 * ObstacleManager.Instance.GridCellSize,
            ObstacleManager.Instance.ObstacleHeight, 
            hits[0].transform.position.z + (_obstacleUI.RelatedObstacle.Dimensions.y - 1) / 2 * ObstacleManager.Instance.GridCellSize
        );
        
        _isSnapped = true;
    }
    
    private void TryPlacingObstacle()
    {
        if (_isSnapped)
        {
            Instantiate(_obstacleUI.RelatedObstacle, _obstaclePreview.transform.position, Quaternion.identity, ObstacleManager.Instance.ObstaclesParent);
            CatBellManager.Instance.PlacedObstacle(_obstacleUI.RelatedObstacle.CatBellCost);
            OnObstaclePlaced?.Invoke(_obstacleUI.RelatedObstacle);
            if (TutorialManager.Instance != null) TutorialManager.Instance.DestroyTutorial();
        }
        Destroy(_obstaclePreview);
        
    }
}
