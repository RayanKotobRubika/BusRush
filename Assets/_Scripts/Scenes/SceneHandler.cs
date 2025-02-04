using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }
    
    [SerializeField] private Animator _animator;
    [SerializeField] private float _transitionTime = 1f;
    
    [SerializeField] private Slider _progressBar;
    
    private void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        } 
        
        Destroy(gameObject);
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        _progressBar.gameObject.SetActive(true);

        do
        {
            _progressBar.value = scene.progress;
        } while (scene.progress < 0.9f);
        
        _progressBar.gameObject.SetActive(false);
        
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
}
