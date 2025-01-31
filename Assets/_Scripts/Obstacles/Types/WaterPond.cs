using UnityEngine;

public class WaterPond : Obstacle
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Passenger passenger)) return;

        if (passenger.Color == Color) passenger.Agent.speed = 0.0f;
        
        Passengers.Add(passenger);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Passenger passenger)) return;

        if (passenger.Color == Color) passenger.Agent.speed = passenger.MovementSpeed;
    }

    protected override void KilledObstacle(Passenger passenger)
    {
        if (passenger.Color == Color) passenger.Agent.speed = passenger.MovementSpeed;
    }
}
