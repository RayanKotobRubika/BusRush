using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class WaitingQueue : MonoBehaviour
{
    [field:SerializeField] public PassengerColor Color { get; private set; }
    [SerializeField] private float _vehicleFullingTime;
    [SerializeField] private TextMeshProUGUI _currentPassengersText;
    [SerializeField] private PlusText _plusText;
    private Vehicle _lastActiveVehicle;
    private float _currentCooldown;
    private float _containerTimer;
    
    private int _capacity;
    private int _currentPassengers = 0;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0;
        _capacity = Color switch
        {
            PassengerColor.Blue => LevelManager.Instance.Data.MaxBlueCapacity,
            PassengerColor.Red => LevelManager.Instance.Data.MaxRedCapacity,
            PassengerColor.Green => LevelManager.Instance.Data.MaxGreenCapacity,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        _currentPassengers = Color switch
        {
            PassengerColor.Blue => LevelManager.Instance.Data.DefaultBlueCapacity,
            PassengerColor.Red => LevelManager.Instance.Data.DefaultRedCapacity,
            PassengerColor.Green => LevelManager.Instance.Data.DefaultGreenCapacity,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void Start()
    {
        UpdateSlider();
    }

    private void Update()
    {
        if (_currentPassengers >= _capacity && VehicleManager.Instance.CurrentVehicles[0] != null &&  VehicleManager.Instance.CurrentVehicles[0].Color != Color &&
            !GameManager.Instance.IsGameOver)
            GameManager.Instance.LoseGame();
        
        if (!VehicleManager.Instance.BusIsActive) return;

        if ((Color != VehicleManager.Instance.CurrentVehicles[0].Color && !VehicleManager.Instance.CurrentVehicles[0].TakesAllColors)|| !VehicleManager.Instance.BusIsActive) return;

        if (_lastActiveVehicle != VehicleManager.Instance.CurrentVehicles[0])
        {
            _lastActiveVehicle = VehicleManager.Instance.CurrentVehicles[0];
            _currentCooldown = _vehicleFullingTime / _lastActiveVehicle.Capacity;
            _containerTimer = _currentCooldown;
        }

        if (_currentPassengers <= 0) return;

        _containerTimer -= Time.deltaTime;

        if (_containerTimer > 0) return;

        _containerTimer = _currentCooldown;
        
        VehicleManager.Instance.CurrentVehicles[0].AddPassenger();
        RemovePassenger();
    }

    public void AddPassenger()
    {
        _currentPassengers++;
        UpdateSlider();
        if (!_plusText.gameObject.activeInHierarchy)
            _plusText.gameObject.SetActive(true);
        _plusText.AddPassenger();
    }

    private void RemovePassenger()
    {
        _currentPassengers--;
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        _slider.value = (float)_currentPassengers / _capacity;
        _currentPassengersText.text = _currentPassengers.ToString();
    }
    
}
