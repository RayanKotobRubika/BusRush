using UnityEngine;
using UnityEngine.AI;

public class Passenger : MonoBehaviour
{
    public float MovementSpeed { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    [field:SerializeField] public PassengerColor Color { get; private set; }

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        
        Agent.speed = LevelManager.Instance.Data.PassengersMovementSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
            Stop();
        Move();
    }

    private void Move()
    {
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, LaneManager.Instance.LanesArrivalZ.position.z);
        
        Agent.SetDestination(targetPos);
    }

    private void Stop()
    {
        Agent.SetDestination(transform.position);
    }
}
