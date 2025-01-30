using UnityEngine;

public class ColoredWall : Obstacle
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Passenger passenger)) return;

        if (passenger.Color != Color) Destroy(passenger.gameObject);
        
        Passengers.Add(passenger);
    }
}
