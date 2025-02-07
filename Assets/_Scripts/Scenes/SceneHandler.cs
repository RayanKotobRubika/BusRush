using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(-3)]
public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }
    
    [SerializeField] private Animator _animator;
    [SerializeField] private float _transitionTime = 1f;
    
    [SerializeField] private Slider _progressBar;
    //private CatLoadingAnimator _loadingScreenPassengers;

    [SerializeField] private Image _panel;

    [SerializeField] private List<Material> _loadingScreenMaterials;

    [field:SerializeField] public List<LevelData> Levels { get; private set; }
    private int _currentLevel;
    
    private void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        }
        else
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);
            PlayerPrefs.Save();
            _currentLevel = 0;
        }
        
        // _loadingScreenPassengers = FindFirstObjectByType<CatLoadingAnimator>();
        // _loadingScreenPassengers.ScaleDown();
    }

    private void Start()
    {
        _panel.material = _loadingScreenMaterials[Random.Range(0, 3)];
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _progressBar.value = 0;
        do
        {
            _progressBar.value = scene.progress * 0.01f;
        } while (scene.progress < 0.9f);
        
        _progressBar.value = 1;
        
        scene.allowSceneActivation = true;

        StartCoroutine(FadeOut());
    }

    public IEnumerator LoadLevel(string sceneName)
    {
        _panel.material = _loadingScreenMaterials[Random.Range(0, 3)];
        
        _animator.SetTrigger("TrStart");
        
        //_loadingScreenPassengers.ScaleUp();

        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        
        yield return new WaitForSeconds(_transitionTime);
        
        LoadScene(sceneName);
    }

    public IEnumerator FadeOut()
    {
        _animator.SetTrigger("TrEnd");

        yield return new WaitForSeconds(0.1f);
        
        // _loadingScreenPassengers = FindFirstObjectByType<CatLoadingAnimator>();
        //
        // _loadingScreenPassengers.ScaleDown();
        
        yield return new WaitForSeconds(_transitionTime);

        LevelManager.Instance.ReadyToPlay = true;
    }

    public LevelData GetCurrentLevelData()
    {
        return Levels[PlayerPrefs.GetInt("CurrentLevel", 0)];
    }
}
