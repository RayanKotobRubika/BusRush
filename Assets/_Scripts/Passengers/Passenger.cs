using UnityEngine;
using UnityEngine.AI;

public class Passenger : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private NavMeshAgent _agent;

    [field:SerializeField] public PassengerColor AssignedColor { get; private set; }
    public Lane AssignedLane { get; private set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        
        _agent.speed = _movementSpeed;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, LaneManager.Instance.LanesArrivalZ.position.z);
        
        _agent.SetDestination(targetPos);
    }

    public void Initialize(Lane lane)
    {
        if (AssignedLane != null) return;
        
        AssignedLane = lane;
    }
}
