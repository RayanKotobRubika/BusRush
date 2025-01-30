using System;
using UnityEditor;
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
    
    public PassengerColor CurrentColor { get; private set; }

    private void Start()
    {
        DisableAllPanels();
        SwitchColor((int)CurrentColor);
    }

    public void SwitchColor(int colorIndex)
    {
        PassengerColor color = (PassengerColor)colorIndex;
        
        DisableAllPanels();
        
        switch (color)
        {
            case PassengerColor.Red:
                CurrentColor = PassengerColor.Red;
                _redPanel.gameObject.SetActive(true);
                break;
            case PassengerColor.Blue:
                CurrentColor = PassengerColor.Blue;
                _bluePanel.gameObject.SetActive(true);
                break;
            case PassengerColor.Green:
                CurrentColor = PassengerColor.Green;
                _greenPanel.gameObject.SetActive(true);
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
}
