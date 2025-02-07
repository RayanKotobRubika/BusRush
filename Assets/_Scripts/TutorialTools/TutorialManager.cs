using System;
using UnityEditor;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    [SerializeField] private GameObject _tutorialUi;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        } 
        
        Instance = this;
    }
    
    private void Start()
    {
        if (LevelManager.Instance.Data.LevelIndex != 0)
            DestroyTutorial();
    }

    public void DestroyTutorial()
    {
        Destroy(_tutorialUi);
        Destroy(gameObject);
    }
}
