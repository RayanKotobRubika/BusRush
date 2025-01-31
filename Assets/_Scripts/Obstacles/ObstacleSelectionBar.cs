using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObstacleSelectionBar : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster _raycaster;
    [SerializeField] private EventSystem _eventSystem;
    
    [SerializeField] private Image _redPanel;
    [SerializeField] private Image _greenPanel;
    [SerializeField] private Image _bluePanel;

    [SerializeField] private GameObject _redBar;
    [SerializeField] private GameObject _greenBar;
    [SerializeField] private GameObject _blueBar;
    
    public PassengerColor CurrentColor { get; private set; }

    private void Start()
    {
        DisableAllPanels();
        DisableAllBars();
        SwitchColor((int)CurrentColor);
    }

    public void SwitchColor(int colorIndex)
    {
        PassengerColor color = (PassengerColor)colorIndex;
        
        DisableAllPanels();
        DisableAllBars();
        
        switch (color)
        {
            case PassengerColor.Red:
                CurrentColor = PassengerColor.Red;
                _redPanel.gameObject.SetActive(true);
                _redBar.SetActive(true);
                break;
            case PassengerColor.Blue:
                CurrentColor = PassengerColor.Blue;
                _bluePanel.gameObject.SetActive(true);
                _blueBar.SetActive(true);
                break;
            case PassengerColor.Green:
                CurrentColor = PassengerColor.Green;
                _greenPanel.gameObject.SetActive(true);
                _greenBar.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(color), color, null);
        }
    }

    private void DisableAllPanels()
    {
        _redPanel.gameObject.SetActive(false);
        _greenPanel.gameObject.SetActive(false);
        _bluePanel.gameObject.SetActive(false);
    }

    private void DisableAllBars()
    {
        _redBar.SetActive(false);
        _greenBar.SetActive(false);
        _blueBar.SetActive(false);
    }
}
