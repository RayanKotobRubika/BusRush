using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpringTrap : Obstacle
{
    [SerializeField] private float _bumpForce;
    [SerializeField] private ParticleSystem _buildEffect;
    [SerializeField] private Animator[] _animators;

    protected override void Awake()
    {
        base.Awake();

        _animators = GetComponentsInChildren<Animator>();
    }

    private void Start()
    {
        _buildEffect.Play();
    }
    
    protected override void EnterObstacle(Passenger passenger)
    {
        base.EnterObstacle(passenger);
        
        passenger.Agent.enabled = false;
        passenger.RB.isKinematic = false;
        passenger.Coll.enabled = false;
        passenger.Body.transform.position += Vector3.down;
        
        Vector3 ejectionDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), 0).normalized;
        passenger.RB.AddForce(ejectionDirection * _bumpForce, ForceMode.Impulse);
        Vector3 randomTorqueDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        passenger.RB.AddTorque(randomTorqueDirection * _bumpForce, ForceMode.Impulse);

        foreach (Animator a in _animators)
        {
            a.SetTrigger("TrBump");
        }
        
        Destroy(passenger.gameObject,2f);
        Destroy(passenger);
    }
}
