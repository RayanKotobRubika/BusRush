using System;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _activatedMusicButton;
    [SerializeField] private GameObject _deactivatedMusicButton;
    
    [SerializeField] private GameObject _activatedSfxButton;
    [SerializeField] private GameObject _deactivatedSfxButton;

    [SerializeField] private float _bouncyScale;
    [SerializeField] private float _bounceDuration;
    
    [SerializeField] private GameObject _creditsPanel;
    
    public bool IsSettingsActive { get; private set; }

    private void OnEnable()
    {
        bool activeMusic = PlayerPrefs.GetInt("ActivateMusic") == 1;
        bool activeSfx = PlayerPrefs.GetInt("ActivateSfx") == 1;
        
        _activatedMusicButton.SetActive(activeMusic);
        _deactivatedMusicButton.SetActive(!activeMusic);
        
        _activatedSfxButton.SetActive(activeSfx);
        _deactivatedSfxButton.SetActive(!activeSfx);

        IsSettingsActive = true;
    }

    public void ToggleMusic(bool setActive)
    {
        _activatedMusicButton.SetActive(setActive);
        _deactivatedMusicButton.SetActive(!setActive);

        StartCoroutine(setActive
            ? CoroutineUtils.BouncyScale(_activatedMusicButton.transform, _bouncyScale, _bounceDuration, true)
            : CoroutineUtils.BouncyScale(_deactivatedMusicButton.transform, _bouncyScale, _bounceDuration, true));

        PlayerPrefs.SetInt("ActivateMusic", setActive ? 1 : 0);
        PlayerPrefs.Save();
    }
    
    public void ToggleSfx(bool setActive)
    {
        _activatedSfxButton.SetActive(setActive);
        _deactivatedSfxButton.SetActive(!setActive);

        StartCoroutine(setActive
            ? CoroutineUtils.BouncyScale(_activatedSfxButton.transform, _bouncyScale, _bounceDuration, true)
            : CoroutineUtils.BouncyScale(_deactivatedSfxButton.transform, _bouncyScale, _bounceDuration, true));
        
        PlayerPrefs.SetInt("ActivateSfx", setActive ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void OpenCredits()
    {
        _creditsPanel.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void CloseSettings()
    {
        gameObject.SetActive(false);

        IsSettingsActive = false;
    }
}
