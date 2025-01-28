using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SliderWaitingQueues : MonoBehaviour
{
    [field:SerializeField] public PassengerColor Color { get; private set; }
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

        if (_currentPassengers <= 0) return;
        
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
