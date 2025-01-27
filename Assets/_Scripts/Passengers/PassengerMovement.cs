using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PassengerMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        
    }
}
