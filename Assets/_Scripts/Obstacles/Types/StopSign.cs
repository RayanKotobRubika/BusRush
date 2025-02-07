using UnityEngine;

public class StopSign : Obstacle
{
    [SerializeField] private ParticleSystem _buildEffect;

    private void Start()
    {
        _buildEffect.Play();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Passenger passenger)) return;

        if (passenger.Color == Color) passenger.IsStopped = true;
        
        Passengers.Add(passenger);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Passenger passenger)) return;

        if (passenger.Color == Color) passenger.IsStopped = false;
    }

    protected override void KilledObstacle(Passenger passenger)
    {
        if (passenger.Color == Color) passenger.IsStopped = false;
    }
}
