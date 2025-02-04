using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-3)]
public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }
    
    [SerializeField] private Animator _animator;
    [SerializeField] private float _transitionTime = 1f;
    
    [SerializeField] private Slider _progressBar;

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
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        }
        else
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
            PlayerPrefs.Save();
            _currentLevel = 1;
        }
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        //_progressBar.gameObject.SetActive(true);

        do
        {
            _progressBar.value = scene.progress;
        } while (scene.progress < 0.9f);
        
        //_progressBar.gameObject.SetActive(false);
        
        scene.allowSceneActivation = true;

        StartCoroutine(FadeOut());
    }

    public IEnumerator LoadLevel(string sceneName)
    {
        _animator.SetTrigger("TrStart");
        
        yield return new WaitForSeconds(_transitionTime);
        
        LoadScene(sceneName);
    }

    public IEnumerator FadeOut()
    {
        _animator.SetTrigger("TrEnd");
        
        yield return new WaitForSeconds(_transitionTime);

        LevelManager.Instance.ReadyToPlay = true;
    }

    public LevelData GetCurrentLevelData()
    {
        return Levels[PlayerPrefs.GetInt("CurrentLevel", 1) - 1];
    }
}
