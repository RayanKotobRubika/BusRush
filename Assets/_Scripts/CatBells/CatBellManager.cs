using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatBellManager : MonoBehaviour
{
    public static CatBellManager Instance { get; private set; }
    
    [SerializeField] private float _defaultCatBellAmount;
    public float CatBellAmount { get; private set; }

    [SerializeField] private float _catBellCap;
    [SerializeField] private Slider _catBellBar;
    [SerializeField] private float _catBellRechargeRate;
    [SerializeField] private TextMeshProUGUI _debugText;
    private float _catBellRechargeTimer;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        } 
        
        Instance = this; 
        
        CatBellAmount = _defaultCatBellAmount;
        _catBellRechargeTimer = 1 / _catBellRechargeRate;
    }

    private void Update()
    {
        if (CatBellAmount < _catBellCap)
            UpdateSliderAndValue();
    }

    private void UpdateSliderAndValue()
    {
        _debugText.text = $"{CatBellAmount}";
        CatBellAmount += Time.deltaTime * _catBellRechargeRate;
        _catBellBar.value = CatBellAmount * 0.01f;
    }

    public void PlacedObstacle(float cost)
    {
        CatBellAmount -= cost;
    }
}
