using UnityEngine;

public class StopSign : Obstacle
{
    [SerializeField] private ParticleSystem _buildEffect;

    private void Start()
    {
        _buildEffect.Play();
    }
    
    protected override void EnterObstacle(Passenger passenger)
    { 
        base.EnterObstacle(passenger);
        
        passenger.IsStopped = true;
    }

    protected override void ExitObstacle(Passenger passenger)
    {
        base.ExitObstacle(passenger);
        
        passenger.IsStopped = false;
    }

    protected override void KilledObstacle(Passenger passenger)
    {
        base.KilledObstacle(passenger);
        
        passenger.IsStopped = false;
    }
}
