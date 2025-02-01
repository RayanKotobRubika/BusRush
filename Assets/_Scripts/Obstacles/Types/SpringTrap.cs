using UnityEditor;
using UnityEngine;

public class SpringTrap : Obstacle
{
    [SerializeField] private float _bumpForce;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Passenger passenger)) return;

        if (passenger.Color == Color) BumpPassenger(passenger);
        
        Passengers.Add(passenger);
    }

    private void BumpPassenger(Passenger passenger)
    {
        passenger.Agent.enabled = false;
        passenger.RB.isKinematic = false;
        passenger.Coll.enabled = false;
        passenger.Body.transform.position += Vector3.down;
        
        Vector3 ejectionDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), 0).normalized;
        passenger.RB.AddForce(ejectionDirection * _bumpForce, ForceMode.Impulse);
        Vector3 randomTorqueDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        passenger.RB.AddTorque(randomTorqueDirection * _bumpForce, ForceMode.Impulse);
        
        Destroy(passenger.gameObject,2f);
        Destroy(passenger);
    }
}
