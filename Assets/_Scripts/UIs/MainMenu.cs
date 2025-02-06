using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;

    [SerializeField] private float _bouncyScale;
    [SerializeField] private float _bounceDuration;

    [SerializeField] private TextMeshProUGUI _levelText;

    [SerializeField] private bool _allowLevelChoice;
    [SerializeField] private TextMeshProUGUI _levelChoiceText;
    [SerializeField] private GameObject _levelSelectionInterface;

    public void Start()
    {
        AudioManager.Instance.PlayMusic("Menu");
        if (!_allowLevelChoice)
            _levelSelectionInterface.SetActive(false);

        UpdateText(SceneHandler.Instance.GetCurrentLevelData().LevelIndex);
    }

    private void Update()
    {
        if (!AudioManager.Instance.MusicSource.isPlaying)
            AudioManager.Instance.PlayMusic("Menu");
    }

    public void OpenSettings()
    {
        _settingsPanel.SetActive(true);
    }

    public void BounceButton(Transform buttonTransform)
    {
        StopAllCoroutines();
        StartCoroutine(CoroutineUtils.BouncyScale(buttonTransform, _bouncyScale, _bounceDuration, true));
    }

    public void IncreaseLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        
        if (currentLevel >= SceneHandler.Instance.Levels.Count) return;
        
        PlayerPrefs.SetInt("CurrentLevel", currentLevel + 1);
        
        UpdateText(currentLevel + 1);
    }

    public void DecreaseLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        
        if (currentLevel <= 0) return;
        
        PlayerPrefs.SetInt("CurrentLevel", currentLevel - 1);

        UpdateText(currentLevel - 1);
    }

    private void UpdateText(int currentLevel)
    {
        if (_allowLevelChoice)
            _levelChoiceText.text = currentLevel.ToString();
        
        if (currentLevel != 0)
            _levelText.text = "LEVEL " + currentLevel;
        else
            _levelText.text = "TUTORIAL";
    }
}
