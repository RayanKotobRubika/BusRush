using System;
using UnityEngine;

public class LaneArrival : MonoBehaviour
{
    [SerializeField] private SliderWaitingQueues[] _sliders;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Passenger passenger)) return;

        foreach (SliderWaitingQueues slider in _sliders)
        {
            if (passenger.AssignedColor != slider.Color) continue;
            
            slider.AddPassenger();
            Destroy(passenger.gameObject);
            return;
        }
    }
}
