using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void OpenSettings()
    {
        _settingsPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
    }
}
