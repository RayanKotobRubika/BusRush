using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    
    public void CloseCredits()
    {
        _settingsPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
