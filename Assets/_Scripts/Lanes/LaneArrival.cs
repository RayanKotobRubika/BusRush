using UnityEngine;

public class LaneArrival : MonoBehaviour
{
    [SerializeField] private WaitingQueue[] _sliders;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Passenger passenger)) return;
    
        foreach (WaitingQueue slider in _sliders)
        {
            if (passenger.Color != slider.Color) continue;
            
            slider.AddPassenger();
            Destroy(passenger.gameObject);
            return;
        }
    }
}
