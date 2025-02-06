using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class ObstacleUI : MonoBehaviour
{
    [field:SerializeField] public Obstacle RelatedObstacle { get; private set; }
    [field:SerializeField] public GameObject ObstaclePreview { get; private set; }
    [field:SerializeField] public Sprite ObstacleSprite { get; private set; }
    [field:SerializeField] public TextMeshProUGUI CostText { get; private set; }
    
    private GameObject _obstaclePreview;
    private Camera _mainCamera;
    
    private RectTransform _rectTransform;
    private Image _image;
    private bool _isDragged = false;
    private bool _isSnapped = false;

    [SerializeField] private LayerMask _gridTileLayer;

    [SerializeField] private GameObject _lockGroup;
    private bool _isLocked;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _image.sprite = ObstacleSprite;
    }

    private void OnEnable()
    {
        InputManager.Instance.OnStartHold += DragObstaclePreview;
        InputManager.Instance.OnEndHold += DropObstaclePreview;

        StartCoroutine(CoroutineUtils.BouncyScale(transform, 1.15f, 0.15f));
    }

    private void OnDisable()
    {
        InputManager.Instance.OnStartHold -= DragObstaclePreview;
        InputManager.Instance.OnEndHold -= DropObstaclePreview;
    }

    private void Start()
    {
        _mainCamera = GameManager.Instance.MainCamera;
        CostText.text = $"{RelatedObstacle.CatBellCost}";

        _isLocked = ObstacleManager.Instance.ObstacleUnlockLevels
            .Any(o => o.Obstacle == RelatedObstacle && o.Level > SceneHandler.Instance.GetCurrentLevelData().LevelIndex);
        
        gameObject.SetActive(!_isLocked);
        _lockGroup.SetActive(_isLocked);
    }

    private void Update()
    {
        if (!_isDragged || _obstaclePreview == null) return;
        
        SnapPreviewOnGrid();

        _obstaclePreview.SetActive(_isSnapped);
    }

    public void DragObstaclePreview(Vector2 screenPos, float time)
    {
        if (CatBellManager.Instance.CatBellAmount < RelatedObstacle.CatBellCost) return;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, screenPos, _mainCamera, out Vector2 localPoint);

        if (!_rectTransform.rect.Contains(localPoint)) return;
        
        _isDragged = true;
        
        _image.enabled = false;
        
        _obstaclePreview = Instantiate(ObstaclePreview, GetPreviewPosition(InputManager.Instance.CurrentFingerPosition), Quaternion.identity);
    }

    public void DropObstaclePreview(Vector2 screenPos, float time)
    {
        if (!_isDragged) return;
        
        _isDragged = false;
        
        _image.enabled = true;
        
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
            _obstaclePreview.transform.position.x + (-RelatedObstacle.Dimensions.x + 1) / 2 * ObstacleManager.Instance.GridCellSize, 
            ObstacleManager.Instance.ObstacleHeight, 
            _obstaclePreview.transform.position.z + (-RelatedObstacle.Dimensions.y + 1) / 2 * ObstacleManager.Instance.GridCellSize
            );
        
        Vector3 pos2 = new Vector3(
            _obstaclePreview.transform.position.x + (RelatedObstacle.Dimensions.x - 1) / 2 * ObstacleManager.Instance.GridCellSize, 
            ObstacleManager.Instance.ObstacleHeight, 
            _obstaclePreview.transform.position.z + (RelatedObstacle.Dimensions.y - 1) / 2 * ObstacleManager.Instance.GridCellSize
            );

        _isSnapped = false;

        Debug.DrawRay(pos1+Vector3.up, Vector3.down, Color.red, 10f);
        if (!Physics.Raycast(pos1 + Vector3.up, Vector3.down, out RaycastHit hit1, 10f, _gridTileLayer)) return;
        
        
        Debug.DrawRay(pos2+Vector3.up, Vector3.down, Color.green, 10f);
        if (!Physics.Raycast(pos2 + Vector3.up, Vector3.down, out RaycastHit hit2, 10f, _gridTileLayer)) return;
        
        
        _obstaclePreview.transform.position = new Vector3(
            hit1.transform.position.x + (RelatedObstacle.Dimensions.x - 1) / 2 * ObstacleManager.Instance.GridCellSize,
            ObstacleManager.Instance.ObstacleHeight, 
            hit1.transform.position.z + (RelatedObstacle.Dimensions.y - 1) / 2 * ObstacleManager.Instance.GridCellSize
            );
        
        _isSnapped = true;
    }
    

    private void TryPlacingObstacle()
    {
        if (_isSnapped)
        {
            Instantiate(RelatedObstacle, _obstaclePreview.transform.position, Quaternion.identity, ObstacleManager.Instance.ObstaclesParent);
            CatBellManager.Instance.PlacedObstacle(RelatedObstacle.CatBellCost);
        }
        Destroy(_obstaclePreview);
        
    }
}
