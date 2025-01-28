using System;
using UnityEngine;
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

    public void AddPassenger()
    {
        _currentPassengers++;
        _slider.value = (float)_currentPassengers / _capacity;
    }
}
