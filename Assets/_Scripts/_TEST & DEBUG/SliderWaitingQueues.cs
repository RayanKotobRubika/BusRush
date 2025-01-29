using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SliderWaitingQueues : MonoBehaviour
{
    [field:SerializeField] public PassengerColor Color { get; private set; }
    [SerializeField] private float _vehicleFullingTime;
    private Vehicle _lastActiveVehicle;
    private float _currentCooldown;
    private float _containerTimer;
    [SerializeField] private int _capacity;
    private int _currentPassengers = 0;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0;
    }

    private void Update()
    {
        if (!VehicleManager.Instance.BusIsActive) return;

        if (Color != VehicleManager.Instance.CurrentVehicles[0].Color || !VehicleManager.Instance.BusIsActive) return;

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
    }

    private void RemovePassenger()
    {
        _currentPassengers--;
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        _slider.value = (float)_currentPassengers / _capacity;
    }
}
