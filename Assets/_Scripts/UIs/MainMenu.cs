using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _creditsPanel;

    [SerializeField] private GameObject _activatedMusicButton;
    [SerializeField] private GameObject _deactivatedMusicButton;
    
    [SerializeField] private GameObject _activatedSfxButton;
    [SerializeField] private GameObject _deactivatedSfxButton;

    [SerializeField] private float _bouncyScale;
    [SerializeField] private float _bounceDuration;

    [SerializeField] private TextMeshProUGUI _levelText;

    private int _currentLevel;

    public void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            _currentLevel = PlayerPrefs.GetInt("Level", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.Save();
        }

        _levelText.text = _currentLevel.ToString();
    }

    public void OpenSettings()
    {
        _settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        _settingsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        _settingsPanel.SetActive(false);
        _creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        _creditsPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }

    public void BounceButton(Transform buttonTransform)
    {
        StopAllCoroutines();
        StartCoroutine(CoroutineUtils.BouncyScale(buttonTransform, _bouncyScale, _bounceDuration));
    }

    public void ToggleMusic(bool setActive)
    {
        _activatedMusicButton.SetActive(setActive);
        _deactivatedMusicButton.SetActive(!setActive);

        StartCoroutine(setActive
            ? CoroutineUtils.BouncyScale(_activatedMusicButton.transform, _bouncyScale, _bounceDuration)
            : CoroutineUtils.BouncyScale(_deactivatedMusicButton.transform, _bouncyScale, _bounceDuration));

        PlayerPrefs.SetInt("ActivateMusic", setActive ? 1 : 0);
        PlayerPrefs.Save();
    }
    
    public void ToggleSfx(bool setActive)
    {
        _activatedSfxButton.SetActive(setActive);
        _deactivatedSfxButton.SetActive(!setActive);

        StartCoroutine(setActive
            ? CoroutineUtils.BouncyScale(_activatedSfxButton.transform, _bouncyScale, _bounceDuration)
            : CoroutineUtils.BouncyScale(_deactivatedSfxButton.transform, _bouncyScale, _bounceDuration));
        
        PlayerPrefs.SetInt("ActivateSfx", setActive ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void Play()
    {
        //SceneManager.LoadScene()
    }
}
