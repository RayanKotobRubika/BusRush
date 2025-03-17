using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [field:SerializeField] public PassengerColor Color { get; private set; }
    protected List<Passenger> Passengers;
    public bool IsPlaced { get; private set; }
    [field:SerializeField] public Vector2 Dimensions { get; private set; }
    [field:SerializeField] public float CatBellCost { get; private set; }
    [field:SerializeField] public float LifeTime { get; private set; }
    private float _lifeTimer;

    protected virtual void Awake()
    {
        IsPlaced = false;
        _lifeTimer = LifeTime;
        Passengers = new List<Passenger>();
    }

    private void Update()
    {
        _lifeTimer -= Time.deltaTime;

        if (!(_lifeTimer <= 0)) return;
        
        foreach (Passenger passenger in Passengers)
        {
            KilledObstacle(passenger);
        }
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Passenger passenger)) return;

        if (passenger.Color != Color) return;
        
        EnterObstacle(passenger);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Passenger passenger)) return;

        if (passenger.Color != Color) return;
        
        ExitObstacle(passenger);
    }
    
    protected virtual void EnterObstacle(Passenger passenger)
    { 
        Passengers.Add(passenger);
    }
    
    protected virtual void ExitObstacle(Passenger passenger)
    {
        Passengers.Remove(passenger);
    }

    protected virtual void KilledObstacle(Passenger passenger)
    { }
}
