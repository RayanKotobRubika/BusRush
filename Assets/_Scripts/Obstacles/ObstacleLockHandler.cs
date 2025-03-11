using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class ObstacleLockHandler : MonoBehaviour
{
    [SerializeField] private GameObject _lockGroup;
    [SerializeField] private TextMeshProUGUI _unlockLevelText;
    [SerializeField] private GameObject _lockPanel;
    
    private ObstacleUI _obstacleUI;
    private bool _isLocked;

    private void Awake()
    {
        _obstacleUI = GetComponent<ObstacleUI>();
    }

    private void Start()
    {
        int unlockLevel = ObstacleManager.Instance.ObstacleUnlockLevels
            .Where(o => o.Obstacle == _obstacleUI.RelatedObstacle)
            .Select(o => o.Level)
            .FirstOrDefault();
        
        _isLocked = unlockLevel > SceneHandler.Instance.GetCurrentLevelData().LevelIndex;
        
        gameObject.SetActive(!_isLocked);
        _lockGroup.SetActive(_isLocked);
        if (_isLocked) _unlockLevelText.text = "LEVEL " + unlockLevel;
    }

    private void Update()
    {
        if (CatBellManager.Instance.CatBellAmount < _obstacleUI.RelatedObstacle.CatBellCost)
            _lockPanel.SetActive(true);
        else if (CatBellManager.Instance.CatBellAmount >= _obstacleUI.RelatedObstacle.CatBellCost)
            _lockPanel.SetActive(false);
    }
}
