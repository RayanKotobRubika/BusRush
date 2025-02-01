using UnityEngine;
using UnityEngine.AI;

public class Passenger : MonoBehaviour
{
    public float MovementSpeed { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Rigidbody RB { get; private set; }
    public Collider Coll { get; private set; }
    public bool IsStopped;
    [field:SerializeField] public Transform Body { get; private set; }

    [field:SerializeField] public PassengerColor Color { get; private set; }

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        RB = GetComponent<Rigidbody>();
        Coll = GetComponent<Collider>();
        
        IsStopped = false;
        
        MovementSpeed = LevelManager.Instance.Data.PassengersMovementSpeed;

        Agent.speed = MovementSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
            Stop();
        Move();
    }

    private void Move()
    {
        Vector3 targetPos = new Vector3();
        
        targetPos = IsStopped ? transform.position : new Vector3(transform.position.x, transform.position.y, LaneManager.Instance.LanesArrivalZ.position.z);
        
        Agent.SetDestination(targetPos);
    }

    private void Stop()
    {
        Agent.SetDestination(transform.position);
    }
}
