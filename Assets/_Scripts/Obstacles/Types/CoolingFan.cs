using System;
using UnityEngine;

public class CoolingFan : Obstacle
{
    [SerializeField] private float _pushStrength;
    [SerializeField] private ParticleSystem _windEfect;
    [SerializeField] private ParticleSystem _buildEffect;

    private void Start()
    {
        _windEfect.Play();
        _buildEffect.Play();
    }

    protected override void EnterObstacle(Passenger passenger)
    {
        base.EnterObstacle(passenger);
        
        passenger.Agent.enabled = false;
        passenger.RB.isKinematic = false;
        passenger.RB.AddForce(_pushStrength * Vector3.back, ForceMode.VelocityChange);
        
        Passengers.Add(passenger);
    }
    
    protected override void ExitObstacle(Passenger passenger)
    {
        base.ExitObstacle(passenger);
        
        passenger.Agent.enabled = true;
        passenger.RB.isKinematic = true;
        
        Passengers.Remove(passenger);
    }

    protected override void KilledObstacle(Passenger passenger)
    {
        base.KilledObstacle(passenger);
        
        passenger.Agent.enabled = true;
        passenger.RB.isKinematic = true;
    }
}
