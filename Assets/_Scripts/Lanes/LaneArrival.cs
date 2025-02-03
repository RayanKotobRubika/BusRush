using UnityEngine;

public class LaneArrival : MonoBehaviour
{
    [SerializeField] private WaitingQueues[] _sliders;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Passenger passenger)) return;
    
        foreach (WaitingQueues slider in _sliders)
        {
            if (passenger.Color != slider.Color) continue;
            
            slider.AddPassenger();
            Destroy(passenger.gameObject);
            return;
        }
    }
}
